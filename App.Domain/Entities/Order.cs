using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int ID { get; set; }

        public DateTime DateTime { get; set; }

        public bool RequireMaterial { get; set; }

        public int ServiceID { get; set; }

        public virtual Service Service { get; set; }

        [ForeignKey("Client")]
        public string ClientEmail { get; set; }

        public virtual Client Client { get; set; }

        [ForeignKey("Employee")]
        public string EmployeeEmail { get; set; }

        public virtual Employee Employee { get; set; }

        public float PaymentAmount { get; set; }
    }
}
