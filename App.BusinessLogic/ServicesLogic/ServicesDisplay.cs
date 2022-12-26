using App.Domain;
using App.Domain.CRUD;
using System;
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

        public IEnumerable<Service> GetServices()
        {
            return servicesData.GetAllServices();
        }

        public Service GetService(int id)
        {
            return servicesData.GetService(id);
        }

        public IEnumerable<ServiceCategory> GetCategories()
        {
            return servicesData.GetAllCategories();
        }

        public IEnumerable<Service> GetServicesByCategory(string name)
        {
            return servicesData.GetServicesByCategory(name);
        }

        public ServiceCategory GetCategory(string name)
        {
            return servicesData.GetCategory(name);
        }
    }
}
