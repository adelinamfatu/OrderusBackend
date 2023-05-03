using App.BusinessLogic.Helper;
using App.Domain.CRUD;
using App.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BusinessLogic.CompaniesLogic
{
    public class CompaniesDisplay
    {
        CompaniesData companiesData;

        public CompaniesDisplay()
        {
            companiesData = new CompaniesData();
        }

        public IEnumerable<CompanyDTO> GetCompanies()
        {
            return companiesData.GetAllCompanies().Select(company => EntityDTO.EntityToDTO(company));
        }

        public IEnumerable<CompanyDTO> GetCompaniesByService(int id)
        {
            return companiesData.GetCompaniesByService(id).Select(company => EntityDTO.EntityToDTO(company));
        }

        public IEnumerable<CompanyDTO> GetAllFunctions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CompanyServiceOptionDTO> GetCompanyDetails(int id)
        {
            return companiesData.GetCompanyDetails(id).Select(cso => EntityDTO.EntityToDTO(cso));
        }

        public IEnumerable<CommentDTO> GetComments(int id)
        {
            return companiesData.GetComments(id).Select(comment => EntityDTO.EntityToDTO(comment));
        }

        public bool AddCompany(CompanyDTO company)
        {
            var status = companiesData.AddRepresentative(DTOEntity.DTOtoEntityRepr(company));
            if(status == false)
            {
                return status;
            }
            companiesData.AddCompany(DTOEntity.DTOtoEntity(company));
            return status;
        }

        public IEnumerable<EmployeeDTO> GetEmployees(int id)
        {
            return companiesData.GetEmployees(id).Select(employee => EntityDTO.EntityToDTO(employee));
        }

        public bool AddEmployee(EmployeeDTO employee)
        {
            return companiesData.AddEmployee(DTOEntity.DTOtoEntity(employee));
        }

        public string Login(CompanyDTO company)
        {
            return companiesData.Login(DTOEntity.DTOtoEntityRepr(company));
        }

        public string Login(EmployeeDTO employee)
        {
            return companiesData.Login(DTOEntity.DTOtoEntity(employee));
        }

        public CompanyDTO GetCompany(string username)
        {
            return EntityDTO.EntityToDTO(companiesData.GetCompany(username));
        }

        public void UpdateLogo(string fileName)
        {
            companiesData.UpdateLogo(fileName);
        }

        public bool UpdateCompany(CompanyDTO company)
        {
            return companiesData.UpdateCompany(DTOEntity.DTOtoEntity(company));
        }

        public void UpdateCompanyServices(IEnumerable<CompanyServiceOptionDTO> services)
        {
            var companyServices = services.Select(s => DTOEntity.DTOtoEntity(s));
            companiesData.UpdateCompanyServices(companyServices);
        }
    }
}
