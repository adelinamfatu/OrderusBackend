using System;
using System.Collections.Generic;
using System.Text;

namespace App.DTO
{
    public class MaterialDTO
    { 
        public int ID { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public int Quantity { get; set; }

        public int CompanyID { get; set; }
    }
}
