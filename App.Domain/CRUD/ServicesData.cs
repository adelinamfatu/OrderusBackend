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

        public IEnumerable<Services> GetAllServices()
        {
            var services = context.Services;
            return services;
        }

        public Services GetService(int id)
        {
            return context.Services.FirstOrDefault(s => s.ID == id);
        }
    }
}
