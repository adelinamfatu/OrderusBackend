using System;
using System.Collections.Generic;
using System.Text;

namespace App.DTO
{
    public class OrderDTO
    {
        public int ID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime FinishTime { get; set; }

        public int Duration { get; set; }

        public string ServiceName { get; set; }

        public int ServiceID { get; set; }

        public float PaymentAmount { get; set; }

        public Dictionary<string, string> Details { get; set; }

        public string Comment { get; set; }

        public string ClientEmail { get; set; }

        public string EmployeeEmail { get; set; }

        public int CompanyID { get; set; }

        public bool IsFinished { get; set; }

        public string ClientNumber { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsCancelled => IsFinished && !IsConfirmed;
    }
}
