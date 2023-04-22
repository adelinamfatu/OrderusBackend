using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    [Table("Stocks")]
    public class Stock
    {
        [Key]
        public int ID { get; set; }

        public int CompanyID { get; set; }

        public virtual Company Company { get; set; }
    }
}
