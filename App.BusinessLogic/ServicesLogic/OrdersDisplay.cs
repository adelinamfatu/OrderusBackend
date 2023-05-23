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
            IList<CleaningServiceOrder> cleaningServiceOrders = new List<CleaningServiceOrder>();
            var orderDetails = ordersData.GetAllCleaningOrders();
            foreach(var order in orderDetails)
            {
                CleaningServiceOrder cso2 = new CleaningServiceOrder
                {
                    CompanyID = order.Employee.CompanyID,
                    EmployeeEmail = order.EmployeeEmail,
                    ClientEmail = order.ClientEmail,
                    DateTime = order.DateTime,
                    Rating = ordersData.GetOrderScore(order.ID),
                    NbRooms = int.Parse(ordersData.GetOrderExtendedProperty(order.ID, "NbRooms")),
                    Surface = int.Parse(ordersData.GetOrderExtendedProperty(order.ID, "Surface")),
                    Duration = order.Duration
                };
                cleaningServiceOrders.Add(cso2);
                Console.WriteLine(cso2.ToString());
            }

            MLContext mlContext = new MLContext(seed: 0);

            var dataView = mlContext.Data.LoadFromEnumerable(cleaningServiceOrders);
            var trainTestSplit = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);
            var trainData = trainTestSplit.TrainSet;
            var testData = trainTestSplit.TestSet;

            var employeeEmailMapping = ordersData.GetEmployeeMapping();
            var clientEmailMapping = ordersData.GetClientMapping();
            var model = Train(mlContext, trainData, employeeEmailMapping, clientEmailMapping);
            Evaluate(mlContext, model, testData);
            var duration = TestSinglePrediction(mlContext, model, po);
            return 0;
        }

        private ITransformer Train(MLContext mlContext, IDataView dataView, IDictionary<string, int> employeeEmailMapping, IDictionary<string, int> clientEmailMapping)
        {
            var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "Duration")
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "EmployeeEmailEncoded", inputColumnName: "EmployeeEmail"))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "ClientEmailEncoded", inputColumnName: "ClientEmail"))
                .Append(mlContext.Transforms.CustomMapping(new CustomDate().GetMapping(), "CustomDateMapping"))
                .Append(mlContext.Transforms.Concatenate("Features", "CompanyID", "EmployeeEmailEncoded", "OrderID", "MaterialQuantity", "ClientEmailEncoded", "CustomMappingOutput", "Rating", "NbRooms", "Surface", "Duration"))
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
                EmployeeEmail = "marianion@gmail.com",
                //OrderID = ordersData.GetLastOrderID(),
                //RequireMaterial = po.Comment != null ? true : false,
                //MaterialQuantity = po.Comment != null ? 1 : 0,
                ClientEmail = po.ClientEmail,
                DateTime = po.DateTime,
                Rating = ordersData.GetClientMeanScore(po.ClientEmail, po.CompanyID),
                NbRooms = po.NbRooms,
                Surface = po.Surface,
                Duration = 0
            };
            return predictionFunction.Predict(cleaningOrderSample).Duration;
        }

        private string AssignEmployee()
        {
            return "";
        }

        public Dictionary<string, int> GetOrderServicesCount(int companyID)
        {
            return ordersData.GetOrderServicesCount(companyID);
        }
    }
}
