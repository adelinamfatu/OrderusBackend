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
            var id = ordersData.AssignEmployee(po.DateTime, po.CompanyID);
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
                .Append(mlContext.Transforms.Concatenate("Features", "CompanyID", "EmployeeID", "ClientID", "CustomMappingOutput", "Hour", "NbRooms", "Surface", "Duration"))
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
    }
}
