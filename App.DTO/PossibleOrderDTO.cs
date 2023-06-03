using System;
using System.Collections.Generic;
using System.Text;

namespace App.DTO
{
    public class PossibleOrderDTO
    {
        public int CompanyID { get; set; }

        public int MaterialQuantity { get; set; }

        public int ServiceID { get; set; }

        public string ClientEmail { get; set; }

        public int EmployeeID { get; set; }

        public DateTime DateTime { get; set; }

        public string Comment { get; set; }

        public int Surface { get; set; }

        public int NbRooms { get; set; }
    }
}
