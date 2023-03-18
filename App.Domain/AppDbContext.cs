using App.Domain.Entities;
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
        public DbSet<Service> Services { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<CompanyServiceOption> CompaniesServiceOptions { get; set; }

        public DbSet<ServiceCategory> ServicesCategories { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}
