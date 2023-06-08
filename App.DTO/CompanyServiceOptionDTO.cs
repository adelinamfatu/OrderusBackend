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

        public int NbRooms { get; set; }

        public int Surface { get; set; }

        public DateTime DateTime { get; set; }

        public int Duration { get; set; }
    }
}
