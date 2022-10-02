using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain
{
    [Table("CompaniesServiceOptions")]
    public class CompaniesServiceOptions
    {
        [Key, Column(Order = 0)]
        public int ServiceID { get; set; }

        public virtual Services Service { get; set; }

        [Key, Column(Order = 1)]
        public int CompanyID { get; set; }

        public virtual Companies Company { get; set; }
    }
}
