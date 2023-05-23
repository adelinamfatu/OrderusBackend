using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.CRUD
{
    public class OrdersData
    {
        AppDbContext context;

        public OrdersData()
        {
            context = new AppDbContext();
        }

        public IEnumerable<Order> GetAllCleaningOrders()
        {
            return context.Orders.Where(o => o.ServiceID == 10);
        }

        public int GetOrderMaterialQuantity(int orderID)
        {
            return context.OrderMaterials.Where(om => om.OrderID == orderID).FirstOrDefault().Quantity;
        }

        public int GetOrderScore(int orderID)
        {
            return context.Comments.Where(c => c.OrderID == orderID).FirstOrDefault().Score;
        }

        public string GetOrderExtendedProperty(int orderID, string extendedPropertyKey)
        {
            return context.OrderExtendedProperties.Where(oep => oep.OrderID == orderID && oep.Key == extendedPropertyKey).FirstOrDefault().Value;
        }

        public int GetLastOrderID()
        {
            return context.Orders.OrderByDescending(o => o.ID).FirstOrDefault().ID + 1;
        }

        public float GetClientMeanScore(string clientEmail, int companyID)
        {
            return (float)context.Comments.Where(o => o.ClientEmail == clientEmail && o.CompanyID == companyID).Average(o => o.Score);
        }

        public IDictionary<string, int> GetEmployeeMapping()
        {
            var distinctEmployeeEmails = context.Orders.Select(o => o.EmployeeEmail).Distinct().ToList();
            return distinctEmployeeEmails.Select((value, index) => new { Value = value, Index = index }).ToDictionary(x => x.Value, x => x.Index);
        }

        public IDictionary<string, int> GetClientMapping()
        {
            var distinctClientEmails = context.Orders.Select(o => o.ClientEmail).Distinct().ToList();
            return distinctClientEmails.Select((value, index) => new { Value = value, Index = index }).ToDictionary(x => x.Value, x => x.Index);
        }

        public Dictionary<string, int> GetOrderServicesCount(int companyID)
        {
            return context.Orders.Where(o => o.Employee.CompanyID == companyID)
                                .GroupBy(o => o.Service.Name)
                                .Select(g => new
                                {
                                    ServiceName = g.Key,
                                    Count = g.Count()
                                }).ToDictionary(osc => osc.ServiceName, osc => osc.Count);
        }
    }
}
