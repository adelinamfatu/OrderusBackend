﻿using App.API.Authentication;
using App.BusinessLogic.CompaniesLogic;
using App.DTO;
using System.Collections.Generic;
using System.Web.Http;

namespace App.API.Controllers
{
    [BasicAuthentication]
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

        [Route("services/{id}")]
        [HttpGet]
        public IEnumerable<CompanyDTO> GetCompaniesByService(int id)
        {
            return companiesDisplay.GetCompaniesByService(id);
        }

        [Route("services/details/{id}")]
        [HttpGet]
        public IEnumerable<CompanyServiceOptionDTO> GetCompanyDetails(int id)
        {
            return companiesDisplay.GetCompanyDetails(id);
        }

        [Route("comments/{id}")]
        [HttpGet]
        public IEnumerable<CommentDTO> GetCompanyComments(int id)
        {
            return companiesDisplay.GetComments(id);
        }
    }
}
