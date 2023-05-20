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
    }
}
