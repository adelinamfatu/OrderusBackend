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
        public IEnumerable<CompanyDTO> GetAllCompanies()
        {
            return companiesDisplay.GetCompanies();
        }
    }
}
