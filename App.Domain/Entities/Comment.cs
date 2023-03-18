using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    [Table("Comments")]
    public class Comment
    {
        public int ID { get; set; }

        public string Content { get; set; }

        public int Score { get; set; }

        [ForeignKey("Client")]
        public string ClientEmail { get; set; }

        public virtual Client Client { get; set; }

        [ForeignKey("Company")]
        public int CompanyID { get; set; }

        public virtual Company Company { get; set; }
    }
}
