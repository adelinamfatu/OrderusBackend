using App.Domain;
using App.Domain.CRUD;
using App.DTO;
using System.Linq;
using System;
using System.Collections.Generic;
using App.BusinessLogic.Helper;
using App.Domain.Entities;

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

        public IEnumerable<CategoryDTO> GetCategories()
        {
            return servicesData.GetAllCategories().Select(category => EntityDTO.EntityToDTO(category));
        }

        public IEnumerable<ServiceDTO> GetServicesByCategory(int id)
        {
            return servicesData.GetServicesByCategory(id).Select(service => EntityDTO.EntityToDTO(service));
        }

        public ServiceCategory GetCategory(string name)
        {
            return servicesData.GetCategory(name);
        }
    }
}
