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
    [RoutePrefix("api/employees")]
    public class EmployeesController : ApiController
    {
        CompaniesDisplay companiesDisplay = new CompaniesDisplay();

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddEmployee(EmployeeDTO employee)
        {
            employee.Password = Crypto.Encrypt(employee.Password);
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

        [Route("login")]
        [HttpPost]
        public IHttpActionResult Login(EmployeeDTO employee)
        {
            var data = companiesDisplay.Login(employee);
            if (data != "")
            {
                var password = Crypto.Decrypt(data);
                if (password == employee.Password)
                {
                    return Ok(TokenManager.GenerateToken(employee.Email));
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
        public EmployeeDTO GetEmployee()
        {
            var headers = this.Request.Headers;
            headers.TryGetValues("Authorization", out var authHeader);
            string token = authHeader.FirstOrDefault().Replace("Bearer ", "").Trim('"');
            var username = TokenManager.GetPrincipal(token).Identity.Name;
            return companiesDisplay.GetEmployee(username);
        }

        [Route("company/{id}")]
        [HttpGet]
        [BasicAuthentication]
        public IEnumerable<EmployeeDTO> GetEmployeesByCompany(int id)
        {
            return companiesDisplay.GetEmployees(id);
        }

        [Route("update")]
        [HttpPut]
        [BasicAuthentication]
        public IHttpActionResult UpdateEmployee(EmployeeDTO employee)
        {
            var status = companiesDisplay.UpdateEmployee(employee);
            if (status == true)
            {
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }

        [Route("{email}")]
        [HttpGet]
        [BasicAuthentication]
        public EmployeeDTO GetEmployeeDetails(string email)
        {
            return companiesDisplay.GetEmployeeDetails(email);
        }
    }
}
