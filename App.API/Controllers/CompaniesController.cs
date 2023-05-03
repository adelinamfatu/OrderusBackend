using App.API.Authentication;
using App.BusinessLogic.CompaniesLogic;
using App.BusinessLogic.Helper;
using App.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace App.API.Controllers
{
    [RoutePrefix("api/companies")]
    public class CompaniesController : ApiController
    {
        CompaniesDisplay companiesDisplay = new CompaniesDisplay();

        [Route("")]
        [HttpGet]
        public IEnumerable<CompanyDTO> GetAllCompanies()
        {
            return companiesDisplay.GetCompanies();
        }

        [Route("functions")]
        [HttpGet]
        public IEnumerable<CompanyDTO> GetAllFunctions()
        {
            return companiesDisplay.GetAllFunctions();
        }

        [Route("services/{id}")]
        [HttpGet]
        [BasicAuthentication]
        public IEnumerable<CompanyDTO> GetCompaniesByService(int id)
        {
            return companiesDisplay.GetCompaniesByService(id);
        }

        [Route("services/details/{id}")]
        [HttpGet]
        [BasicAuthentication]
        public IEnumerable<CompanyServiceOptionDTO> GetCompanyDetails(int id)
        {
            return companiesDisplay.GetCompanyDetails(id);
        }

        [Route("comments/{id}")]
        [HttpGet]
        [BasicAuthentication]
        public IEnumerable<CommentDTO> GetCompanyComments(int id)
        {
            return companiesDisplay.GetComments(id);
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddCompany(CompanyDTO company)
        {
            company.RepresentativePassword = Crypto.Encrypt(company.RepresentativePassword);
            var status = companiesDisplay.AddCompany(company);
            if (status == true)
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
        public IHttpActionResult Login(CompanyDTO company)
        {
            var data = companiesDisplay.Login(company);
            if (data != "")
            {
                var password = Crypto.Decrypt(data);
                if (password == company.RepresentativePassword)
                {
                    return Ok(TokenManager.GenerateToken(company.RepresentativeEmail));
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
        public CompanyDTO GetClient()
        {
            var headers = this.Request.Headers;
            headers.TryGetValues("Authorization", out var authHeader);
            string token = authHeader.FirstOrDefault().Replace("Bearer ", "").Trim('"');
            var username = TokenManager.GetPrincipal(token).Identity.Name;
            return companiesDisplay.GetCompany(username);
        }

        [Route("photo")]
        [HttpPost]
        public async Task<IHttpActionResult> AddCompanyPhoto()
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
                    companiesDisplay.UpdateLogo(fileName);
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

        [Route("update")]
        [HttpPut]
        public IHttpActionResult UpdateCompany(CompanyDTO company)
        {
            var status = companiesDisplay.UpdateCompany(company);
            if(status == true)
            {
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }

        [Route("services/update")]
        [HttpPost]
        public IHttpActionResult UpdateCompanyServices(IEnumerable<CompanyServiceOptionDTO> services)
        {
            companiesDisplay.UpdateCompanyServices(services);
            return Ok();
        }
    }
}
