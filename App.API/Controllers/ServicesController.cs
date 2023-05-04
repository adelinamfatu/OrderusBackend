using App.API.Authentication;
using App.BusinessLogic.ServicesLogic;
using App.Domain;
using App.Domain.Entities;
using App.DTO;
using System.Collections.Generic;
using System.Web.Http;

namespace App.API.Controllers
{
    [BasicAuthentication]
    [RoutePrefix("api/services")]
    public class ServicesController : ApiController
    {
        ServicesDisplay servicesDisplay = new ServicesDisplay();

        [Route("")]
        [HttpGet]
        public IEnumerable<Service> GetAllServices()
        {
            return servicesDisplay.GetServices();
        }

        [Route("{id}")]
        [HttpGet]
        public Service GetService(int id)
        {
            return servicesDisplay.GetService(id);
        }

        [Route("categories")]
        [HttpGet]
        public IEnumerable<CategoryDTO> GetCategories()
        {
            return servicesDisplay.GetCategories();
        }

        [Route("categories/{id}")]
        [HttpGet]
        public IEnumerable<ServiceDTO> GetServicesByCategory(int id)
        {
            return servicesDisplay.GetServicesByCategory(id);
        }

        [Route("companies/{id}")]
        [HttpGet]
        public IEnumerable<ServiceDTO> GetServicesByCompany(int id)
        {
            return servicesDisplay.GetServicesByCompany(id);
        }
    }
}