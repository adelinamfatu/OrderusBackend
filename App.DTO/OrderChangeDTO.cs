using System;
using System.Collections.Generic;
using System.Text;

namespace App.DTO
{
    public class OrderChangeDTO
    {
        public int OrderID { get; set; }

        public string ServiceName { get; set; }

        public DateTime InitialDate { get; set; }

        public DateTime ChangedDate { get; set; }
    }
}
