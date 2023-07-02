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
            var companies = companiesData.GetCompaniesByService(id).Select(company => EntityDTO.EntityToDTO(company)).ToList();
            foreach(var company in companies)
            {
                company.Score = companiesData.GetCompanyScore(company.ID);
            }
            return companies;
        }

        public IEnumerable<MaterialDTO> GetCompanyMaterials(int id)
        {
            return companiesData.GetCompanyMaterials(id).Select(material => EntityDTO.EntityToDTO(material));
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

        public bool AddMaterial(MaterialDTO material)
        {
            return companiesData.AddMaterial(DTOEntity.DTOtoEntity(material));
        }

        public IEnumerable<EmployeeDTO> GetEmployees(int id)
        {
            var employees = companiesData.GetEmployees(id).Select(employee => EntityDTO.EntityToDTO(employee)).ToList();
            foreach(var employee in employees)
            {
                employee.Services = companiesData.GetEmployeeServices(employee.Email).Select(es => EntityDTO.EntityToDTO(es)).ToList();
            }
            return employees;
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

        public EmployeeDTO GetEmployeeDetails(string email)
        {
            var employee = EntityDTO.EntityToDTO(companiesData.GetEmployee(email));
            employee.Orders = companiesData.GetEmployeeOrders(email).Select(eo => EntityDTO.EntityToDTO(eo)).ToList();
            return employee;
        }

        public bool UpdateEmployeeDetails(EmployeeDTO employee)
        {
            return companiesData.UpdateEmployeeDetails(DTOEntity.DTOtoEntity(employee));
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

        public bool UpdateMaterial(MaterialDTO material)
        {
            return companiesData.UpdateMaterial(DTOEntity.DTOtoEntity(material));
        }

        public bool UpdateEmployee(EmployeeDTO employee)
        {
            foreach(var service in employee.Services)
            {
                companiesData.AddEmployeeService(DTOEntity.DTOtoEntity(service), employee.Email);
            }
            return companiesData.UpdateEmployee(DTOEntity.DTOtoEntity(employee));
        }

        public void UpdatePicture(string fileName)
        {
            companiesData.UpdatePicture(fileName);
        }

        public void UpdateCompanyServices(IEnumerable<CompanyServiceOptionDTO> services)
        {
            var companyServices = services.Select(s => DTOEntity.DTOtoEntity(s));
            companiesData.UpdateCompanyServices(companyServices);
        }

        public EmployeeDTO GetEmployee(string username)
        {
            return EntityDTO.EntityToDTO(companiesData.GetEmployee(username));
        }

        public IEnumerable<OrderDTO> GetScheduledOrders(string email)
        {
            return companiesData.GetScheduledOrders(email).Select(order => EntityDTO.EntityToDTO(order));
        }

        public IEnumerable<OrderDTO> GetPastOrders(string email)
        {
            return companiesData.GetPastOrders(email).Select(order => EntityDTO.EntityToDTO(order));
        }

        public IEnumerable<OrderDTO> GetUnconfirmedOrders(string email)
        {
            var orders = companiesData.GetUnconfirmedOrders(email).Select(order => EntityDTO.EntityToDTO(order)).ToList();
            foreach(var order in orders)
            {
                var phoneNumber = companiesData.GetClientPhoneNumber(order.ID);
                order.ClientNumber = phoneNumber;
            }
            
            return orders;
        }

        public Dictionary<string, string> GetOrderDetails(int id)
        {
            return companiesData.GetOrderDetails(id);
        }

        public IEnumerable<OrderDTO> GetCancelledOrders(string email)
        {
            return companiesData.GetCancelledOrders(email).Select(o => EntityDTO.EntityToDTO(o));
        }

        public Dictionary<string, float> GetCompanyEarnigs(int id)
        {
            return companiesData.GetCompanyEarnigs(id);
        }

        public IEnumerable<OrderDTO> GetCompanyReport(int id)
        {
            return companiesData.GetCompanyReport(id).Select(o => EntityDTO.EntityToDTO(o));
        }

        public IEnumerable<OrderDTO> GetClientUncommentedOrders(string email)
        {
            throw new NotImplementedException();
        }
    }
}
