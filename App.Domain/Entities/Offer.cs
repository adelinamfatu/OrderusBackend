using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    public enum DiscountType
    {
        Value,
        Percentage
    }

    [Table("Offers")]
    public class Offer
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Client")]
        public string ClientEmail { get; set; }

        public virtual Client Client { get; set; }

        public int Discount { get; set; }

        public DiscountType Type { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
