using App.API.Authentication;
using App.BusinessLogic.ServicesLogic;
using App.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace App.API.Controllers
{
    [BasicAuthentication]
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        OrdersDisplay ordersDisplay = new OrdersDisplay();

        [Route("cleaning")]
        [HttpPost]
        public IHttpActionResult GetEstimtedTime(PossibleOrderDTO po)
        {
            var duration = ordersDisplay.GetEstimtedTime(po);
            if(duration == -1)
            {
                return Conflict();
            }
            else
            {
                return Ok(duration);
            }
        }

        [Route("services/{id}")]
        [HttpGet]
        public Dictionary<string, int> GetOrderServicesCount(int id)
        {
            return ordersDisplay.GetOrderServicesCount(id);
        }

        [Route("update/{id}")]
        [HttpPost]
        public IHttpActionResult UpdateOrder(int id, List<MaterialDTO> materials)
        {
            var status = ordersDisplay.UpdateOrder(id, materials);
            if (status == true)
            {
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }

        [Route("materials/{id}")]
        [HttpGet]
        public IEnumerable<MaterialDTO> GetOrderMaterials(int id)
        {
            return ordersDisplay.GetOrderMaterials(id);
        }

        [Route("companies/{id}")]
        [HttpGet]
        public CompanyDTO GetCompanyInfo(int id)
        {
            return ordersDisplay.GetCompanyInfo(id);
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddOrder(OrderDTO order)
        {
            var status = ordersDisplay.AddOrder(order);
            if (status == true)
            {
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }

        [Route("delay/{email}")]
        [HttpPut]
        [BasicAuthentication]
        public IHttpActionResult UpdateEmployeeOrders(string email)
        {
            var status = ordersDisplay.UpdateEmployeeOrders(email);
            if (status == true)
            {
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }
    }
}
