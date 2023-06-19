using App.Domain.CRUD;
using App.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using App.BusinessLogic.MLModels;
using App.BusinessLogic.Helper;
using Microsoft.ML.Data;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace App.BusinessLogic.ServicesLogic
{
    public class OrdersDisplay
    {
        OrdersData ordersData;

        public OrdersDisplay()
        {
            ordersData = new OrdersData();
        }

        public int GetEstimtedTime(PossibleOrderDTO po)
        {
            var id = ordersData.FindAvailableEmployee(po.DateTime, po.CompanyID, po.ServiceID);
            if (id == -1)
            {
                return -1;
            }
            po.EmployeeID = id;

            IList<CleaningServiceOrder> cleaningServiceOrders = new List<CleaningServiceOrder>();
            using (var reader = new StreamReader(FilePathResource.FileStorage + FilePathResource.ClearningServicesCsv))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                cleaningServiceOrders = csv.GetRecords<CleaningServiceOrder>().ToList();
            }

            MLContext mlContext = new MLContext(seed: 0);

            var dataView = mlContext.Data.LoadFromEnumerable(cleaningServiceOrders);
            var trainTestSplit = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);
            var trainData = trainTestSplit.TrainSet;
            var testData = trainTestSplit.TestSet;

            var model = Train(mlContext, trainData);
            Evaluate(mlContext, model, testData);
            var duration = TestSinglePrediction(mlContext, model, po);
            return (int)Math.Ceiling(duration);
        }

        private ITransformer Train(MLContext mlContext, IDataView dataView)
        {
            var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "Duration")
                .Append(mlContext.Transforms.CustomMapping(new CustomDate().GetMapping(), "CustomDateMapping"))
                .Append(mlContext.Transforms.Concatenate("Features", "CompanyID", "EmployeeID", "ClientID", "CustomMappingOutput", 
                                                            "Hour", "NbRooms", "Surface", "Duration"))
                .Append(mlContext.Regression.Trainers.FastTree());
            var model = pipeline.Fit(dataView);
            return model;
        }

        private void Evaluate(MLContext mlContext, ITransformer model, IDataView dataView)
        {
            var predictions = model.Transform(dataView);
            var metrics = mlContext.Regression.Evaluate(predictions, "Label", "Score");
        }

        private double TestSinglePrediction(MLContext mlContext, ITransformer model, PossibleOrderDTO po)
        {
            var predictionFunction = mlContext.Model.CreatePredictionEngine<CleaningServiceOrder, CleaningDurationPrediction>(model);
            var cleaningOrderSample = new CleaningServiceOrder()
            {
                CompanyID = po.CompanyID,
                EmployeeID = po.EmployeeID,
                ClientID = ordersData.GetClientID(po.ClientEmail),
                DateTime = po.DateTime,
                Hour = po.DateTime.Hour,
                NbRooms = po.NbRooms,
                Surface = po.Surface,
                Duration = 0
            };
            return predictionFunction.Predict(cleaningOrderSample).Duration;
        }
        
        public Dictionary<string, int> GetOrderServicesCount(int companyID)
        {
            return ordersData.GetOrderServicesCount(companyID);
        }

        public bool UpdateOrder(int id, List<MaterialDTO> materials)
        {
            if(materials != null)
            {
                ordersData.UpdateRequestMaterials(id);
                foreach(var material in materials)
                {
                    ordersData.AddOrderMaterial(EntityDTO.EntityToDTO(id, material));
                }
            }
            return ordersData.ConfirmOrder(id);
        }

        public IEnumerable<MaterialDTO> GetOrderMaterials(int id)
        {
            return ordersData.GetOrderMaterials(id).Select(o => EntityDTO.EntityToDTO(o));
        }

        public CompanyDTO GetCompanyInfo(int id)
        {
            return EntityDTO.EntityToDTO(ordersData.GetCompanyInfo(id));
        }

        public bool AddOrder(OrderDTO order)
        {
            var employeeEmail = ordersData.AssignEmployee(order.StartTime, order.CompanyID, order.ServiceID, order.Duration);
            if(employeeEmail == "-1")
            {
                return false;
            }
            var transformedOrder = DTOEntity.DTOtoEntity(order, employeeEmail);
            var orderID = ordersData.AddOrder(transformedOrder);
            return ordersData.AddOrderDetails(order.Details, orderID);
        }

        public bool UpdateEmployeeOrders(string email)
        {
            return ordersData.UpdateEmployeeOrders(email);
        }

        public bool UpdateOrderFinished(int id)
        {
            return ordersData.UpdateOrderFinished(id);
        }

        public bool AddOrderChangeTime(int id, TimeSpan time)
        {
            return ordersData.AddOrderChangeTime(id, time);
        }

        public IEnumerable<OrderChangeDTO> GetOrderTimeChangeRequests(string email)
        {
            var oeps = ordersData.GetOrderTimeChangeRequests(email);
            IList<OrderChangeDTO> ocd = new List<OrderChangeDTO>();
            foreach(var oep in oeps)
            {
                ocd.Add(EntityDTO.EntityToDTO(oep, ordersData.GetDate(oep.OrderID)));
            }
            return ocd;
        }

        public bool UpdateOrderTime(int id)
        {
            return ordersData.UpdateOrderTime(id);
        }

        public bool DeleteOrderChange(int id)
        {
            return ordersData.DeleteOrderChange(id);
        }

        public bool DeleteChange(int id)
        {
            return ordersData.DeleteChange(id);
        }
    }
}
