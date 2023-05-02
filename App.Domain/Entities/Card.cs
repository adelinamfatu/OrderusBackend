using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    [Table("Cards")]
    public class Card
    {
        [Key]
        public int Number { get; set; }

        public DateTime ExpirationDate { get; set; }

        [ForeignKey("Client")]
        public string ClientEmail { get; set; }

        public virtual Client Client { get; set; }
    }
}
