using App.API.Authentication;
using App.BusinessLogic.CompaniesLogic;
using App.DTO;
using System.Collections.Generic;
using System.Web.Http;

namespace App.API.Controllers
{
    [RoutePrefix("api/companies")]
    public class CompaniesController : ApiController
    {
        CompaniesDisplay companiesDisplay = new CompaniesDisplay();

        [Route("")]
        [HttpGet]
        [BasicAuthentication]
        public IEnumerable<CompanyDTO> GetAllCompanies()
        {
            return companiesDisplay.GetCompanies();
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
    }
}
