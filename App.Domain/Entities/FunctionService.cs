using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    [Table("FunctionServices")]
    public class FunctionService
    {
        [Key]
        public int FunctionID { get; set; }

        public virtual EmployeeFunction EmployeeFunction { get; set; }

        public int ServiceID { get; set; }

        public virtual Service Service { get; set; }
    }
}
