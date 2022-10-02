using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain
{
    public class AppDbContext : DbContext
    {
        public DbSet<Services> Services { get; set; }

        public DbSet<Companies> Companies { get; set; }

        public DbSet<CompaniesServiceOptions> CompaniesServiceOptions { get; set; }
    }
}
