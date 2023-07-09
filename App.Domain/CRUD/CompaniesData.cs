using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using App.Domain.Entities;
using System.Globalization;

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

        public IEnumerable<Material> GetCompanyMaterials(int id)
        {
            return context.Materials.Where(m => m.CompanyID == id);
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

        public bool AddMaterial(Material material)
        {
            var existingMaterial = context.Materials.FirstOrDefault(m => m.Name == material.Name && m.CompanyID == material.CompanyID);
            if (existingMaterial != null)
            {
                return false;
            }
            else
            {
                context.Materials.Add(material);
                context.SaveChanges();
                return true;
            }
        }

        public IEnumerable<Employee> GetEmployees(int id)
        {
            return context.Employees.Where(e => e.CompanyID == id);
        }

        public IEnumerable<Service> GetEmployeeServices(string email)
        {
            return context.EmployeeServices.Where(es => es.EmployeeEmail == email).Select(es => es.Service);
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

        public bool UpdateEmployeeDetails(Employee employee)
        {
            var existingEmployee = context.Employees.Where(e => e.Email == employee.Email).FirstOrDefault();
            existingEmployee.Phone = employee.Phone;
            context.SaveChanges();
            return true;
        }

        public void UpdateCompanyServices(IEnumerable<CompanyServiceOption> companyServiceOptions)
        {
            var companyID = companyServiceOptions.FirstOrDefault().CompanyID;

            foreach (var service in companyServiceOptions)
            {
                if (!context.CompaniesServiceOptions.Any(s => s.ServiceID == service.ServiceID && s.CompanyID == service.CompanyID))
                {
                    AddCompanyService(service);
                }
            }
            var existingServices = context.CompaniesServiceOptions.Where(cso => cso.CompanyID == companyID).ToList();
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

        public void UpdatePicture(string fileName)
        {
            var employee = context.Employees.FirstOrDefault(c => c.Email == fileName.Substring(0, fileName.Length - 4));
            employee.Picture = Resource.IISAddress + fileName;
            context.SaveChanges();
        }

        public IEnumerable<Order> GetPastOrders(string email)
        {
            return context.Orders.Where(o => o.EmployeeEmail == email && o.DateTime < DateTime.Now).OrderByDescending(o => o.DateTime);
        }

        public IEnumerable<Order> GetUnconfirmedOrders(string email)
        {
            return context.Orders.Where(o => o.EmployeeEmail == email && o.IsConfirmed == false && o.IsFinished == false);
        }

        public Dictionary<string, string> GetOrderDetails(int id)
        {
            return context.OrderExtendedProperties.Where(oep => oep.OrderID == id).ToDictionary(oep => oep.Key, oep => oep.Value);
        }

        public IEnumerable<Order> GetScheduledOrders(string email)
        {
            return context.Orders.Where(o => o.EmployeeEmail == email 
                                        && o.DateTime.Year == DateTime.Now.Year
                                        && o.DateTime.Month == DateTime.Now.Month
                                        && o.DateTime.Day >= DateTime.Now.Day
                                        && o.IsFinished == false).OrderByDescending(o => o.DateTime);
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

        public bool UpdateMaterial(Material material)
        {
            var existingMaterial = context.Materials.FirstOrDefault(m => m.Name == material.Name && m.CompanyID == material.CompanyID);
            if(existingMaterial.Name != material.Name)
            {
                existingMaterial.Name = material.Name;
            }
            if(existingMaterial.Price != material.Price)
            {
                existingMaterial.Price = material.Price;
            }
            if (existingMaterial.Quantity != material.Quantity)
            {
                existingMaterial.Quantity = material.Quantity;
            }
            context.SaveChanges();
            return true;
        }

        public bool UpdateEmployee(Employee employee)
        {
            var existingEmployee = context.Employees.FirstOrDefault(e => e.Email == employee.Email);
            existingEmployee.IsConfirmed = true;
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

        public string Login(Employee employee)
        {
            var data = context.Employees.FirstOrDefault(e => e.Email == employee.Email);
            if(data.IsConfirmed == false)
            {
                return "";
            }
            else
            {
                return data.Password;
            }
        }

        public Company GetCompany(string email)
        {
            return context.Companies.FirstOrDefault(c => c.RepresentativeEmail == email);
        }

        public void AddEmployeeService(Service service, string employeeEmail)
        {
            context.EmployeeServices.Add(new EmployeeService() { EmployeeEmail = employeeEmail, ServiceID = service.ID });
            context.SaveChanges();
        }

        public Employee GetEmployee(string email)
        {
            return context.Employees.FirstOrDefault(e => e.Email == email);
        }

        public IEnumerable<Order> GetEmployeeOrders(string email)
        {
            var currentDate = DateTime.Now;
            return context.Orders.Where(o => o.EmployeeEmail == email 
                                            && o.DateTime.Year == currentDate.Year
                                            && o.DateTime.Month == currentDate.Month);
        }

        public string GetClientPhoneNumber(int orderID)
        {
            return context.Orders.Where(o => o.ID == orderID).FirstOrDefault().Client.Phone;
        }

        public double GetCompanyScore(int id)
        {
            var commentsForCompany = context.Comments.Where(c => c.CompanyID == id);

            if (commentsForCompany.Any())
            {
                return commentsForCompany.Average(c => c.Score);
            }
            return 0;
        }

        public IEnumerable<Order> GetCancelledOrders(string email)
        {
            return context.Orders.Where(o => o.EmployeeEmail == email)
                                .Join(context.OrderExtendedProperties.Where(oep => oep.Key == Resource.OrderDateKey && oep.Value.Contains("2000")),
                                    order => order.ID,
                                    extendedProperty => extendedProperty.OrderID,
                                    (order, extendedProperty) => order)
                                .OrderByDescending(o => o.DateTime);
        }

        public Dictionary<string, float> GetCompanyEarnigs(int id)
        {
            DateTime currentDate = DateTime.Now;
            DateTime startDate = currentDate.AddMonths(-12);
            DateTime endDate = currentDate.AddMonths(-1);

            return context.Orders.Where(o => o.Employee.CompanyID == id && o.DateTime >= startDate && o.DateTime <= endDate)
                                .GroupBy(o => o.DateTime.Month)
                                .ToDictionary(
                                                group => GetLocalizedMonthName(group.Key),
                                                group => group.Sum(o => o.PaymentAmount));
        }

        private string GetLocalizedMonthName(int month)
        {
            var romanianCulture = new CultureInfo("ro-RO");
            var monthNames = romanianCulture.DateTimeFormat.MonthGenitiveNames;

            if (month >= 1 && month <= 12)
            {
                return monthNames[month - 1];
            }

            return string.Empty;
        }

        public IEnumerable<Order> GetCompanyReport(int id)
        {
            return context.Orders.Where(o => o.Employee.Company.ID == id && o.IsFinished == true);
        }
    }
}
