using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    [Table("OrderMaterials")]
    public class OrderMaterial
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Order")]
        public int OrderID { get; set; }

        public virtual Order Order { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Material")]
        public int MaterialID { get; set; }

        public virtual Material Material { get; set; }

        public int Quantity { get; set; }
    }
}
