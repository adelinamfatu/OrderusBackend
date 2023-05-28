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

        [Route("curatare")]
        [HttpGet]
        public int GetEstimtedTime(PossibleOrderDTO po)
        {
            return ordersDisplay.GetEstimtedTime(po);
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
    }
}
