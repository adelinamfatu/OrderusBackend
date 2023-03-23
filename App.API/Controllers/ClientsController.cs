using App.API.Authentication;
using App.BusinessLogic.UsersLogic;
using App.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace App.API.Controllers
{
    [RoutePrefix("api/clients")]
    public class ClientsController : ApiController
    {
        ClientsDisplay clientsDisplay = new ClientsDisplay();

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddClient(ClientDTO client)
        {
            client.Password = Crypto.Encrypt(client.Password);
            var status = clientsDisplay.AddClient(client);
            if(status == true)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [Route("login")]
        [HttpPost]
        public string Login(ClientDTO client)
        {
            var password = Crypto.Decrypt(clientsDisplay.Login(client));
            if (password == client.Password)
            {
                return TokenManager.GenerateToken(client.Email);
            }
            else
            {
                return "Invalid user";
            }
        }
    }
}
