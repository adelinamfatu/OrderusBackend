using System;
using System.Collections.Generic;
using System.Text;

namespace App.DTO
{
    public class CompanyDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public string Building { get; set; }

        public string Staircase { get; set; }

        public int? ApartmentNumber { get; set; }

        public int? Floor { get; set; }

        public string Logo { get; set; }

        public string Site { get; set; }

        public string Description { get; set; }

        public string RepresentativeEmail { get; set; }

        public string RepresentativePassword { get; set; }

        public string RepresentativeName { get; set; }

        public string RepresentativeSurname { get; set; }
    }
}
