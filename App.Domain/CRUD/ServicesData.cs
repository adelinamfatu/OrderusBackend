using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Domain.CRUD
{
    public class ServicesData
    {
        AppDbContext context;

        public ServicesData()
        {
            context = new AppDbContext();
        }

        public IEnumerable<Service> GetAllServices()
        {
            var services = context.Services;
            return services;
        }

        public Service GetService(int id)
        {
            return context.Services.FirstOrDefault(s => s.ID == id);
        }

        public IEnumerable<ServiceCategory> GetAllCategories()
        {
            return context.ServicesCategories;
        }

        public ServiceCategory GetCategory(string name)
        {
            return context.ServicesCategories.FirstOrDefault(s => s.Name == name);
        }

        public IEnumerable<Service> GetServicesByCategory(int id)
        {
            return context.Services.Where(s => s.CategoryID == id);
        }

        public IEnumerable<Service> GetServicesByCompany(int id)
        {
            return context.Services.Join(context.CompaniesServiceOptions,
                                            s => s.ID,
                                            cso => cso.ServiceID,
                                            (s, cso) => new { Company = cso, Service = s })
                                            .Where(s => s.Company.CompanyID == id)
                                            .Select(s => s.Service); 
        }
    }
}
