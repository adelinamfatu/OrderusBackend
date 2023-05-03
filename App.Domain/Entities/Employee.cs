using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public string Email { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        [ForeignKey("Company")]
        public int CompanyID { get; set; }

        public virtual Company Company { get; set; }

        public string Picture { get; set; }

        public bool IsConfirmed { get; set; }
    }
}
