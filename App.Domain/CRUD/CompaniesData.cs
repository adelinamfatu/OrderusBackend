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

        public void UpdateCompanyServices(IEnumerable<CompanyServiceOption> companyServiceOptions)
        {
            foreach (var service in companyServiceOptions)
            {
                if (!context.CompaniesServiceOptions.Any(s => s.ServiceID == service.ServiceID && s.CompanyID == service.CompanyID))
                {
                    AddCompanyService(service);
                }
            }
            var existingServices = context.CompaniesServiceOptions.ToList();
            foreach (var existingService in existingServices)
            {
                if (companyServiceOptions.Any(s => s.ServiceID == existingService.ServiceID && s.CompanyID == existingService.CompanyID))
                {
                    var service = companyServiceOptions.FirstOrDefault(s => s.ServiceID == existingService.ServiceID && s.CompanyID == existingService.CompanyID);
                    if(service.Price != existingService.Price)
                    {
                        existingService.Price = service.Price;
                        context.SaveChanges();
                    }
                }
                else
                {
                    DeleteCompanyService(existingService);
                }
            }
        }

        public bool AddCompanyService(CompanyServiceOption companyServiceOption)
        {
            context.CompaniesServiceOptions.Add(companyServiceOption);
            context.SaveChanges();
            return true;
        }

        public bool DeleteCompanyService(CompanyServiceOption companyServiceOption)
        {
            context.CompaniesServiceOptions.Remove(companyServiceOption);
            context.SaveChanges();
            return true;
        }

        public bool UpdateCompanyService(CompanyServiceOption companyServiceOption)
        {
            context.CompaniesServiceOptions.Remove(companyServiceOption);
            context.SaveChanges();
            return true;
        }

        public void UpdateFunctions(List<string> functions)
        {
            foreach(var function in functions)
            {
                if(!context.EmployeeFunctions.Any(f => f.Title == function))
                {
                    AddFunction(function);
                }
            }
            var existingFunctions = context.EmployeeFunctions.ToList();
            foreach(var existingFunction in existingFunctions)
            {
                if(!functions.Contains(existingFunction.Title))
                {
                    DeleteFunction(existingFunction);
                }
            }
        }

        public bool AddFunction(string function)
        {
            context.EmployeeFunctions.Add(new EmployeeFunction() { Title = function });
            context.SaveChanges();
            return true;
        }

        public bool DeleteFunction(EmployeeFunction function)
        {
            context.EmployeeFunctions.Remove(function);
            context.SaveChanges();
            return true;
        }

        public bool UpdateCompany(Company company)
        {
            var existingCompany = context.Companies.FirstOrDefault(c => c.Name == company.Name);
            if(existingCompany.City != company.City)
            {
                existingCompany.City = company.City;
            }
            if (existingCompany.Street != company.Street)
            {
                existingCompany.Street = company.Street;
            }
            if (existingCompany.StreetNumber != company.StreetNumber)
            {
                existingCompany.StreetNumber = company.StreetNumber;
            }
            if (existingCompany.Building != company.Building)
            {
                existingCompany.Building = company.Building;
            }
            if (existingCompany.Staircase != company.Staircase)
            {
                existingCompany.Staircase = company.Staircase;
            }
            if (existingCompany.ApartmentNumber != company.ApartmentNumber)
            {
                existingCompany.ApartmentNumber = company.ApartmentNumber;
            }
            if (existingCompany.Floor != company.Floor)
            {
                existingCompany.Floor = company.Floor;
            }
            if (existingCompany.Site != company.Site)
            {
                existingCompany.Site = company.Site;
            }
            if (existingCompany.Description != company.Description)
            {
                existingCompany.Description = company.Description;
            }
            context.SaveChanges();
            return true;
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
