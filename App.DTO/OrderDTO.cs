using System;
using System.Collections.Generic;
using System.Text;

namespace App.DTO
{
    public class OrderDTO
    {
        public DateTime DateTime { get; set; }

        public int Duration { get; set; }

        public string ServiceName { get; set; }

        public float PaymentAmount { get; set; }
    }
}
