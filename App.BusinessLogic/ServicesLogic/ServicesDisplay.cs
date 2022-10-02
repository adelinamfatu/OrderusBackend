using App.Domain;
using App.Domain.CRUD;
using System.Collections.Generic;

namespace App.BusinessLogic.ServicesLogic
{
    public class ServicesDisplay
    {
        ServicesData servicesData;

        public ServicesDisplay()
        {
            servicesData = new ServicesData();
        }

        public IEnumerable<Services> GetServices()
        {
            return servicesData.GetAllServices();
        }

        public Services GetService(int id)
        {
            return servicesData.GetService(id);
        }
    }
}
