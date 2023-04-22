using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    [Table("Offers")]
    public class Offer
    {
        [Key]
        public int ID { get; set; }

        public int ClientID { get; set; }

        public virtual Client Client { get; set; }

        public int DiscountValue { get; set; }

        public int DiscountPercentage { get; set; }
    }
}
