using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    [Table("EmployeeFunctions")]
    public class EmployeeFunction
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; }
    }
}
