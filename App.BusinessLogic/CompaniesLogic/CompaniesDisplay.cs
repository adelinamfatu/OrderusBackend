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
    }
}
