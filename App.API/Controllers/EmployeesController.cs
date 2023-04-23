using App.API.Authentication;
using App.BusinessLogic.CompaniesLogic;
using App.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace App.API.Controllers
{
    [RoutePrefix("api/employess")]
    class EmployeesController : ApiController
    {
        CompaniesDisplay companiesDisplay = new CompaniesDisplay();

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddEmployee(EmployeeDTO employee)
        {
            employee.Email = Crypto.Encrypt(employee.Email);
            var status = companiesDisplay.AddEmployee(employee);
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
