using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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

        public bool ConfirmOrder(int id)
        {
            var existingOrder = context.Orders.Where(o => o.ID == id).FirstOrDefault();
            existingOrder.IsConfirmed = true;
            context.SaveChanges();
            return true;
        }

        public void UpdateRequestMaterials(int id)
        {
            var existingOrder = context.Orders.Where(o => o.ID == id).FirstOrDefault();
            existingOrder.RequireMaterial = true;
            context.SaveChanges();
        }

        public float GetClientID(string clientEmail)
        {
            return context.Clients.Where(c => c.Email == clientEmail).FirstOrDefault().ID;
        }

        public void AddOrderMaterial(OrderMaterial material)
        {
            context.OrderMaterials.Add(material);
        }

        public int FindAvailableEmployee(DateTime startTime, int companyID, int serviceID)
        {
            var eligibleEmployees = context.Employees.Where(e => e.CompanyID == companyID && e.IsConfirmed == true)
                                                        .Join(context.EmployeeServices,
                                                                e => e.Email,
                                                                es => es.EmployeeEmail,
                                                                (e, es) => new { Employee = e, ServiceID = es.ServiceID })
                                                                    .Where(x => x.ServiceID == serviceID)
                                                                    .Select(x => x.Employee);

            var nextOrder = context.Orders.Where(o => o.Employee.Company.ID == companyID
                                            && o.DateTime.Year == startTime.Year
                                            && o.DateTime.Month == startTime.Month
                                            && o.DateTime.Day == startTime.Day
                                            && o.DateTime.Hour >= startTime.Hour)
                                            .OrderBy(o => o.DateTime)
                                            .FirstOrDefault();
            if (nextOrder == null)
            {
                return eligibleEmployees.FirstOrDefault()?.ID ?? 0;
            }

            var availableEmployees = eligibleEmployees.Where(e => !context.Orders
                                                        .Any(o => o.EmployeeEmail == e.Email && 
                                                            (o.DateTime <= startTime && DbFunctions.AddMinutes(o.DateTime, o.Duration) >= startTime)
                                                        )).ToList();

            var employeeWithLargestBreak = availableEmployees
                .OrderByDescending(e => GetBreakUntilNextOrder(e, startTime, nextOrder.DateTime))
                .FirstOrDefault();

            if (employeeWithLargestBreak != null)
            {
                return employeeWithLargestBreak.ID;
            }

            return -1;
        }

        private TimeSpan GetBreakUntilNextOrder(Employee employee, DateTime startTime, DateTime nextOrderTime)
        {
            var ordersAfterStartTime = context.Orders.Where(o => o.EmployeeEmail == employee.Email && o.DateTime > startTime)
                                                        .OrderBy(o => o.DateTime).ToList();

            if (ordersAfterStartTime.Count == 0)
            {
                return nextOrderTime - startTime;
            }

            var firstOrderAfterStartTime = ordersAfterStartTime.First();

            return firstOrderAfterStartTime.DateTime - startTime;
        }

        public string AssignEmployee(DateTime startTime, int companyID, int serviceID, int duration)
        {
            DateTime endTime = startTime.AddMinutes(duration);

            var eligibleEmployees = context.Employees.Where(e => e.CompanyID == companyID && e.IsConfirmed == true)
                                                        .Join(context.EmployeeServices,
                                                                e => e.Email,
                                                                es => es.EmployeeEmail,
                                                                (e, es) => new { Employee = e, ServiceID = es.ServiceID })
                                                                    .Where(x => x.ServiceID == serviceID)
                                                                    .Select(x => x.Employee).ToList();

            var freeEmployee = eligibleEmployees.Where(e => !context.Orders.Any(o => o.EmployeeEmail == e.Email
                                                        && o.DateTime.Year == startTime.Year
                                                        && o.DateTime.Month == startTime.Month
                                                        && o.DateTime.Day == startTime.Day)).FirstOrDefault();

            if (freeEmployee != null)
            {
                return freeEmployee.Email;
            }

            /*var availableEmployee = eligibleEmployees.Where(e =>
                                                context.Orders.Any(o => o.EmployeeEmail == e.Email
                                                                        && (o.DateTime >= startTime
                                                                            && DbFunctions.AddMinutes(o.DateTime, o.Duration) <= endTime)
                                                                        && (o.DateTime <= startTime
                                                                            && DbFunctions.AddMinutes(o.DateTime, o.Duration) >= endTime)
                                                                        && (o.DateTime <= startTime
                                                                            && DbFunctions.AddMinutes(o.DateTime, o.Duration) >= startTime)
                                                                        && (o.DateTime <= endTime
                                                                            && DbFunctions.AddMinutes(o.DateTime, o.Duration) >= endTime)
                                                                        )).FirstOrDefault();*/

            var availableEmployee = eligibleEmployees.Where(e =>
                                                !context.Orders.Any(o => o.EmployeeEmail == e.Email
                                                                    && ((o.DateTime < startTime && DbFunctions.AddMinutes(o.DateTime, o.Duration) >= startTime)
                                                                    || (o.DateTime >= startTime && o.DateTime <= endTime))
                                                )).FirstOrDefault();

            if (availableEmployee == null)
            {
                return "-1";
            }

            return availableEmployee.Email;
        }

        public IEnumerable<OrderMaterial> GetOrderMaterials(int id)
        {
            return context.OrderMaterials.Where(om => om.OrderID == id);
        }

        public Company GetCompanyInfo(int id)
        {
            return context.Orders.Where(o => o.ID == id).Select(o => o.Employee.Company).FirstOrDefault();
        }

        public int AddOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
            return context.Orders.Where(o => o.DateTime == order.DateTime && o.EmployeeEmail == order.EmployeeEmail).FirstOrDefault().ID;
        }

        public bool AddOrderDetails(Dictionary<string, string> details, int orderID)
        {
            foreach(var kvp in details)
            {
                string key = kvp.Key;
                string value = kvp.Value;
                context.OrderExtendedProperties.Add(new OrderExtendedProperties()
                {
                    Key = key,
                    Value = value,
                    OrderID = orderID
                });
            }
            context.SaveChanges();
            return true;
        }

        public bool UpdateEmployeeOrders(string email)
        {
            DateTime currentDate = DateTime.Now.Date;
            DateTime currentDateTime = DateTime.Now;
            DateTime nextDateTime = currentDateTime.AddMinutes(10);

            var ordersToUpdate = context.Orders
                .Where(o => o.EmployeeEmail == email && o.DateTime.Year == currentDate.Year &&
                                                        o.DateTime.Month == currentDate.Month &&
                                                        o.DateTime.Day == currentDate.Day).ToList();

            foreach (var order in ordersToUpdate)
            {
                var startTime = order.DateTime;
                var finishTime = order.DateTime.AddMinutes(order.Duration);
                if(startTime <= currentDateTime && currentDateTime <= finishTime)
                {
                    order.Duration += 10;
                }
                else
                {
                    order.DateTime = order.DateTime.AddMinutes(10);
                }
            }

            context.SaveChanges();
            return true;
        }

        public bool UpdateOrderFinished(int id)
        {
            context.Orders.Where(o => o.ID == id).FirstOrDefault().IsFinished = true;
            context.SaveChanges();
            return true;
        }

        public bool AddOrderChangeTime(int id, TimeSpan time)
        {
            DateTime date = DateTime.Now.Date.AddDays(1).AddHours(time.Hours).AddMinutes(time.Minutes);

            context.OrderExtendedProperties.Add(new OrderExtendedProperties()
            {
                OrderID = id,
                Key = Resource.OrderDateKey,
                Value = date.ToString()
            });
            context.SaveChanges();
            return true;
        }

        public IList<OrderExtendedProperties> GetOrderTimeChangeRequests(string email)
        {
            return context.OrderExtendedProperties.Where(o => o.Order.ClientEmail == email && o.Key == Resource.OrderDateKey).ToList();
        }

        public DateTime GetDate(int orderID)
        {
            return context.Orders.Where(o => o.ID == orderID).FirstOrDefault().DateTime;
        }

        public bool UpdateOrderTime(int id)
        {
            var order = context.OrderExtendedProperties.Where(o => o.OrderID == id && o.Key == Resource.OrderDateKey).FirstOrDefault();
            context.Orders.Where(o => o.ID == id).FirstOrDefault().DateTime = DateTime.Parse(order.Value);
            context.OrderExtendedProperties.Remove(order);
            context.SaveChanges();
            return true;
        }

        public bool DeleteOrderChange(int id)
        {
            var orderExtendedProperty = context.OrderExtendedProperties.FirstOrDefault(o => o.OrderID == id && o.Key == Resource.OrderDateKey);

            if (orderExtendedProperty != null)
            {
                context.OrderExtendedProperties.Remove(orderExtendedProperty);
                context.SaveChanges();
            }
            return true;
        }
    }
}
