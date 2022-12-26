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
            var categories = context.ServicesCategories;
            return categories;
        }

        public ServiceCategory GetCategory(string name)
        {
            return context.ServicesCategories.FirstOrDefault(s => s.Name == name);
        }
    }
}
