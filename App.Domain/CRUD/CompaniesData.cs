using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Company GetCompany(int id)
        {
            return context.Companies.FirstOrDefault(c => c.ID == id);
        }
    }
}
