using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    [Table("StockMaterials")]
    public class StockMaterial
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Stock")]
        public int StockID { get; set; }

        public virtual Stock Stock { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Material")]
        public int MaterialID { get; set; }

        public virtual Material Material { get; set; }

        public int Quantity { get; set; }
    }
}
