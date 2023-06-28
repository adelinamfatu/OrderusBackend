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
using System.Net.Mail;
using System.Net;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

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

        public bool CancelOrder(int id)
        {
            return ordersData.CancelOrder(id);
        }

        public bool UpdateOrderFinished(int id)
        {
            var status = ordersData.UpdateOrderFinished(id);
            if (status)
            {
                var order = EntityDTO.EntityToDTO(ordersData.GetOrderData(id));
                var company = EntityDTO.EntityToDTO(ordersData.GetCompanyData(id));
                var materials = ordersData.GetOrderMaterials(id).Select(o => EntityDTO.EntityToDTO(o)).ToList();
                GenerateInvoice(order, company, materials);
                SendInvoiceEmail(order.ClientEmail, order.ServiceName, order.StartTime);
            }
            return status;
        }

        private void SendInvoiceEmail(string clientEmail, string serviceName, DateTime dateTime)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(Resource.SenderEmail);
            msg.To.Add(clientEmail);
            msg.Subject = string.Format(Resource.EmailSubject, serviceName, dateTime.ToString("dd.MM.yyyy HH:mm"));
            msg.Body = Resource.EmailBody;
            msg.IsBodyHtml = true;
            Attachment invoice = new Attachment(FilePathResource.InvoicePath);
            invoice.Name = string.Format(Resource.AttachmentName, dateTime.ToString("dd/MM/yyyy_HH:mm"));
            msg.Attachments.Add(invoice);

            using (SmtpClient client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(Resource.SenderEmail, Resource.SenderPassword);
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(msg);
            }
        }

        private void GenerateInvoice(OrderDTO order, CompanyDTO company, IList<MaterialDTO> materials)
        {
            var document = new PdfDocument();

            var page = document.AddPage();

            var gfx = XGraphics.FromPdfPage(page);

            var bigFont = new XFont("Arial", 16);
            var smallFont = new XFont("Arial", 12);

            var centerX = page.Width / 2;
            var centerY = 10;

            var companyName = $"S.C. {company.Name} S.R.L.";
            var companyNameSize = gfx.MeasureString(companyName, bigFont);
            var companyNameX = centerX - (companyNameSize.Width / 2);
            var companyNameY = centerY + (companyNameSize.Height / 2);
            gfx.DrawString(companyName, bigFont, XBrushes.Black, new XPoint(companyNameX, companyNameY));

            var companyAddress = $"{company.Street} {company.StreetNumber}";
            var companyAddressSize = gfx.MeasureString(companyAddress, bigFont);
            var companyAddressX = centerX - (companyAddressSize.Width / 2);
            var companyAddressY = companyNameY + (2 * companyAddressSize.Height);
            gfx.DrawString(companyAddress, bigFont, XBrushes.Black, new XPoint(companyAddressX, companyAddressY));

            var city = "Bucuresti";
            var citySize = gfx.MeasureString(city, bigFont);
            var cityX = centerX - (citySize.Width / 2);
            var cityY = companyAddressY + (2 * citySize.Height);
            gfx.DrawString(city, bigFont, XBrushes.Black, new XPoint(cityX, cityY));

            var emptyRowHeight = 10;

            var serviceName = order.ServiceName;
            var amount = $"{order.PaymentAmount:F2}";
            var margin = 20;
            var amountSize = gfx.MeasureString(amount, smallFont);
            var amountSizeWidth = amountSize.Width;
            var serviceRowX = margin;
            var serviceRowY = cityY + (2 * amountSize.Height) + emptyRowHeight;
            var amountX = page.Width - margin - amountSizeWidth;
            gfx.DrawString(serviceName, smallFont, XBrushes.Black, new XPoint(serviceRowX, serviceRowY));
            gfx.DrawString(amount, smallFont, XBrushes.Black, new XPoint(amountX, serviceRowY));

            var lastY = serviceRowY;
            float paymentTotal = order.PaymentAmount;

            foreach (var material in materials)
            {
                paymentTotal += material.Quantity * material.Price;
                var materialQuantity = $"{material.Quantity:F2} BUC x {material.Price:F2}";
                var materialSize = gfx.MeasureString(materialQuantity, smallFont);
                var materialX = margin + 30;
                var materialY = lastY + (2 * materialSize.Height);
                gfx.DrawString(materialQuantity, smallFont, XBrushes.Black, new XPoint(materialX, materialY));
                lastY = materialY;

                var materialName = material.Name;
                var materialAmount = $"{material.Total:F2}";
                var materialNameSize = gfx.MeasureString(materialName, smallFont);
                var materialAmountSize = gfx.MeasureString(materialAmount, smallFont);
                var materialAmountSizeWidth = materialAmountSize.Width;
                var materialNameX = margin;
                var materialAmountX = page.Width - margin - materialAmountSizeWidth;
                var materialNameY = lastY + (2 * materialNameSize.Height);
                gfx.DrawString(materialName, smallFont, XBrushes.Black, new XPoint(materialNameX, materialNameY));
                gfx.DrawString(materialAmount, smallFont, XBrushes.Black, new XPoint(materialAmountX, materialNameY));
                lastY = materialNameY;
            }

            var total = "Total";
            var orderTotal = $"{paymentTotal:F2}";
            var totalSize = gfx.MeasureString(orderTotal, bigFont);
            var totalSizeWidth = totalSize.Width;
            var totalX = margin;
            var totalY = lastY + (2 * totalSize.Height);
            var orderTotalX = page.Width - margin - totalSizeWidth;
            gfx.DrawString(total, bigFont, XBrushes.Black, new XPoint(totalX, totalY));
            gfx.DrawString(orderTotal, bigFont, XBrushes.Black, new XPoint(orderTotalX, totalY));

            var paymentTVA = 0.19 * paymentTotal;
            var totalTVA = "Total TVA";
            var orderTotalTVA = $"{paymentTVA:F2}";
            var totalTVASize = gfx.MeasureString(orderTotalTVA, smallFont);
            var totalTVASizeWidth = totalTVASize.Width;
            var totalTVAX = margin;
            var totalTVAY = totalY + (2 * totalTVASize.Height);
            var orderTotalTVAX = page.Width - margin - totalTVASizeWidth;
            gfx.DrawString(totalTVA, smallFont, XBrushes.Black, new XPoint(totalTVAX, totalTVAY));
            gfx.DrawString(orderTotalTVA, smallFont, XBrushes.Black, new XPoint(orderTotalTVAX, totalTVAY));

            document.Save(FilePathResource.InvoicePath);
        }

        public void VerifyOrderCancellation()
        {
            ordersData.CancelOrders();
        }

        public void VerifyOrderChangeTimeRequest()
        {
            ordersData.CancelOrderChangeRequest();
        }
    }
}
