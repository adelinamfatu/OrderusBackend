using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    [Table("EmployeeServices")]
    public class EmployeeService
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Employee")]
        public string EmployeeEmail { get; set; }

        public virtual Employee Employee { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Service")]
        public int ServiceID { get; set; }

        public virtual Service Service { get; set; }
    }
}
