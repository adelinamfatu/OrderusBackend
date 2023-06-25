using App.BusinessLogic.Helper;
using App.Domain.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BusinessLogic.SMSLogic
{
    public class SMSDisplay
    {
        OrdersData ordersData;

        public SMSDisplay()
        {
            ordersData = new OrdersData();
        }

        public IDictionary<string, NextOrderType> RetrieveDataForSchedullingSMS()
        {
            IDictionary<string, NextOrderType> orders = new Dictionary<string, NextOrderType>();
            var nextHourOrders = ordersData.GetNextHourOrders();
            var nextDayOrders = ordersData.GetNextDayOrders();
            
            foreach(var order in nextHourOrders)
            {
                orders.Add(order, NextOrderType.NextHourOrder);
            }

            foreach (var order in nextDayOrders)
            {
                orders.Add(order, NextOrderType.NextDayOrder);
            }

            return orders;
        }
    }
}
