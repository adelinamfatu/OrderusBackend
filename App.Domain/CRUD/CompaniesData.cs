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

        public IEnumerable<CompanyServiceOption> GetCompanyDetails(int id)
        {
            return context.CompaniesServiceOptions
                            .Where(cso => cso.CompanyID == id)
                            .Include(cso => cso.Service);
        }

        public void AddCompany(Company company)
        {
            context.Companies.Add(company);
            context.SaveChanges();
        }

        public bool AddRepresentative(Representative representative)
        {
            var existingRepresentative = context.Representatives.FirstOrDefault(r => r.Email == representative.Email);
            if (existingRepresentative != null)
            {
                return false;
            }
            else
            {
                context.Representatives.Add(representative);
                context.SaveChanges();
                return true;
            }
        }

        public bool AddEmployee(Employee employee)
        {
            var existingEmployee = context.Employees.FirstOrDefault(r => r.Email == employee.Email);
            if (existingEmployee != null)
            {
                return false;
            }
            else
            {
                context.Employees.Add(employee);
                context.SaveChanges();
                return true;
            }
        }

        public void UpdateLogo(string fileName)
        {
            var company = context.Companies.FirstOrDefault(c => c.Name == fileName.Substring(0, fileName.Length - 4));
            company.Logo = Resource.IISAddress + fileName;
            context.SaveChanges();
        }

        public IEnumerable<Comment> GetComments(int id)
        {
            return context.Comments.Where(comm => comm.CompanyID == id);
        }

        public string Login(Representative representative)
        {
            return context.Representatives.FirstOrDefault(r => r.Email == representative.Email).Password;
        }

        public Company GetCompany(string email)
        {
            return context.Companies.FirstOrDefault(c => c.RepresentativeEmail == email);
        }
    }
}
