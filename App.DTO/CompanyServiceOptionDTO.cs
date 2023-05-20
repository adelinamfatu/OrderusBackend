using System;
using System.Collections.Generic;
using System.Text;

namespace App.DTO
{
    public class CompanyServiceOptionDTO
    {
        public ServiceDTO Service { get; set; }

        public float Price { get; set; }

        public string Icon { get; set; }

        public CompanyDTO Company { get; set; }
    }
}
