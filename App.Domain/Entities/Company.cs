using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    [Table("Companies")]
    public class Company
    {
        [Key]
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
    }
}
