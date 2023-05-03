using App.Domain;
using App.Domain.Entities;
using App.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BusinessLogic.Helper
{
    public static class DTOEntity
    {
        public static ServiceCategory DTOtoEntity(CategoryDTO category)
        {
            return new ServiceCategory()
            {
                ID = category.ID,
                Name = category.Name
            };
        }

        public static Service DTOtoEntity(ServiceDTO service)
        {
            return new Service()
            {
                ID = service.ID,
                Name = service.Name,
            };
        }

        public static Company DTOtoEntity(CompanyDTO company)
        {
            return new Company()
            {
                ID = company.ID,
                Name = company.Name,
                City = company.City,
                Street = company.Street,
                StreetNumber = company.StreetNumber,
                Building = company.Building,
                Staircase = company.Staircase,
                ApartmentNumber = company.ApartmentNumber,
                Floor = company.Floor,
                Site = company.Site,
                Description = company.Description,
                RepresentativeEmail = company.RepresentativeEmail
            };
        }

        public static CompanyServiceOption DTOtoEntity(CompanyServiceOptionDTO cso)
        {
            return new CompanyServiceOption()
            {
                ServiceID = cso.Service.ID,
                Price = cso.Price,
                CompanyID = cso.Company.ID
            };
        }

        public static Client DTOtoEntity(ClientDTO client)
        {
            return new Client()
            {
                Email = client.Email,
                Password = client.Password,
                Name = client.Name,
                Surname = client.Surname,
                Phone = client.Phone,
                City = client.City,
                Street = client.Street,
                StreetNumber = client.StreetNumber,
                Building = client.Building,
                Staircase = client.Staircase,
                ApartmentNumber = client.ApartmentNumber,
                Floor = client.Floor
            };
        }

        public static Representative DTOtoEntityRepr(CompanyDTO company)
        {
            return new Representative()
            {
                Email = company.RepresentativeEmail,
                Password = company.RepresentativePassword,
                Name = company.RepresentativeName,
                Surname = company.RepresentativeSurname
            };
        }

        public static Employee DTOtoEntity(EmployeeDTO employee)
        {
            return new Employee()
            {
                Email = employee.Email,
                Name = employee.Email,
                Surname = employee.Surname,
                Phone = employee.Phone,
                Password = employee.Password,
                CompanyID = employee.CompanyID
            };
        }
    }
}
