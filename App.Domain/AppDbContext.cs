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

        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeService> EmployeeServices { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Material> Materials { get; set; }

        public DbSet<OrderMaterial> OrderMaterials { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<Representative> Representatives { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<OrderExtendedProperties> OrderExtendedProperties { get; set; }
    }
}
