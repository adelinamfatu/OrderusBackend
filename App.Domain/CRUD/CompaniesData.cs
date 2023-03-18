using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using App.Domain.Entities;

namespace App.Domain.CRUD
{
    public class CompaniesData
    {
        AppDbContext context; 

        public CompaniesData()
        {
            context = new AppDbContext();
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            var companies = context.Companies;
            return companies;
        }

        public IEnumerable<Company> GetCompaniesByService(int id)
        {
            return context.CompaniesServiceOptions.Where(cso => cso.ServiceID == id)
                                                    .Join(context.Companies,
                                                        cso => cso.CompanyID,
                                                        company => company.ID,
                                                        (cso, company) => company);
        }

        public CompanyServiceOption GetCompany(int serviceId, int companyId)
        {
            return context.CompaniesServiceOptions
                            .Where(cso => cso.ServiceID == serviceId)
                            .Where(cso => cso.CompanyID == companyId)
                            .Include(cso => cso.Company)
                            .Include(cso => cso.Company.Comments)
                            .FirstOrDefault();
        }

        public IEnumerable<Comment> GetComments(int id)
        {
            return context.Comments.Where(comm => comm.CompanyID == id).ToList();
        }
    }
}
