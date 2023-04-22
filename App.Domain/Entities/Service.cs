using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    [Table("Services")]
    public class Service
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        public virtual ServiceCategory Category { get; set; }
    }
}
