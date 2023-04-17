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
                return Conflict();
            }
        }

        [Route("login")]
        [HttpPost]
        public IHttpActionResult Login(ClientDTO client)
        {
            var data = clientsDisplay.Login(client);
            if(data != "")
            {
                var password = Crypto.Decrypt(data);
                if (password == client.Password)
                {
                    return Ok(TokenManager.GenerateToken(client.Email));
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [Route("{token}")]
        [HttpGet]
        public ClientDTO GetClient(string token)
        {
            var username = TokenManager.GetPrincipal(token).Identity.Name;
            return clientsDisplay.GetClient(username);
        }
    }
}
