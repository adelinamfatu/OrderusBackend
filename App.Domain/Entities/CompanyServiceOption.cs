using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain
{
    [Table("CompanyServiceOptions")]
    public class CompanyServiceOption
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Service")]
        public int ServiceID { get; set; }

        public virtual Service Service { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Company")]
        public int CompanyID { get; set; }

        public virtual Company Company { get; set; }
    }
}
