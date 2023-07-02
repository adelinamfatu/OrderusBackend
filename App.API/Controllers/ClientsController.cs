using App.API.Authentication;
using App.BusinessLogic.Helper;
using App.BusinessLogic.UsersLogic;
using App.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace App.API.Controllers
{
    [RoutePrefix("api/clients")]
    public class ClientsController : ApiController
    {
        ClientsDisplay clientsDisplay = new ClientsDisplay();
        HttpContext httpContext = HttpContext.Current;

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

        [Route("token")]
        [HttpGet]
        public ClientDTO GetClient()
        {
            var headers = this.Request.Headers;
            headers.TryGetValues("Authorization", out var authHeader);
            string token = authHeader.FirstOrDefault().Replace("Bearer ", "").Trim('"');
            var username = TokenManager.GetPrincipal(token).Identity.Name;
            return clientsDisplay.GetClient(username);
        }

        [Route("history/{email}")]
        [HttpGet]
        [BasicAuthentication]
        public IEnumerable<OrderDTO> GetOrders(string email)
        {
            return clientsDisplay.GetOrders(email);
        }

        [Route("photo")]
        [HttpPost]
        public async Task<IHttpActionResult> AddClientPhoto()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    return BadRequest("Unsupported media type. Only multipart/form-data is supported.");
                }

                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);
                var fileContent = provider.Contents.FirstOrDefault();

                if (fileContent != null)
                {
                    var fileName = fileContent.Headers.ContentDisposition.FileName.Trim('\"');
                    var fileStream = await fileContent.ReadAsStreamAsync();
                    ImageSaveService.SaveImageAsync(fileName, fileStream);
                    clientsDisplay.UpdatePicture(fileName);
                    return Ok();
                }
                else
                {
                    return BadRequest("No file was found in the request.");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("details/update")]
        [HttpPut]
        [BasicAuthentication]
        public IHttpActionResult UpdateClientDetails(ClientDTO client)
        {
            var status = clientsDisplay.UpdateClientDetails(client);
            if (status == true)
            {
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }

        [Route("discount/add")]
        [HttpPost]
        [BasicAuthentication]
        public IHttpActionResult AddOffer(OfferDTO offer)
        {
            var status = clientsDisplay.AddOffer(offer);
            if (status == true)
            {
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }

        [Route("discount/{email}")]
        [HttpGet]
        [BasicAuthentication]
        public IEnumerable<OfferDTO> GetOffers(string email)
        {
            return clientsDisplay.GetOffers(email);
        }

        [Route("orders/comments/{email}")]
        [HttpGet]
        [BasicAuthentication]
        public IEnumerable<OrderDTO> GetClientUncommentedOrders(string email)
        {
            return clientsDisplay.GetClientUncommentedOrders(email);
        }
    }
}
