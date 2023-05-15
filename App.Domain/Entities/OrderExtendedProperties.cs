using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    [Table("OrderExtendedProperties")]
    public class OrderExtendedProperties
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Order")]
        public int OrderID { get; set; }

        public virtual Order Order { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
