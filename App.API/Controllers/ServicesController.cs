using App.BusinessLogic.ServicesLogic;
using App.Domain;
using System.Collections.Generic;
using System.Web.Http;

namespace App.API.Controllers
{
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

        [Route("{id:int}")]
        [HttpGet]
        public Service GetService(int id)
        {
            return servicesDisplay.GetService(id);
        }

        [Route("categories")]
        [HttpGet]
        public IEnumerable<ServiceCategory> GetCategories()
        {
            return servicesDisplay.GetCategories();
        }

        [Route("categories/{name}")]
        [HttpGet]
        public IEnumerable<Service> GetServicesByCategory(string name)
        {
            return servicesDisplay.GetServicesByCategory(name);
        }
    }
}