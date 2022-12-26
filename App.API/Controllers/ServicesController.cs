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
        public IEnumerable<Service> Get()
        {
            return servicesDisplay.GetServices();
        }

        [Route("{id:int}")]
        [HttpGet]
        public Service Get(int id)
        {
            return servicesDisplay.GetService(id);
        }

        [Route("{name:string}")]
        [HttpGet]
        public ServiceCategory Get(string name)
        {
            return servicesDisplay.GetCategory(name);
        }
    }
}