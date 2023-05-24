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
    public static class EntityDTO
    {
        public static CategoryDTO EntityToDTO(ServiceCategory serviceCategory)
        {
            return new CategoryDTO() 
            { 
                ID = serviceCategory.ID,
                Name = serviceCategory.Name 
            };
        }

        public static ServiceDTO EntityToDTO(Service service)
        {
            return new ServiceDTO()
            {
                ID = service.ID,
                Name = service.Name
            };
        }

        public static CompanyDTO EntityToDTO(Company company)
        {
            return new CompanyDTO()
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
                Logo = company.Logo,
                Site = company.Site,
                Description = company.Description
            };
        }

        public static CommentDTO EntityToDTO(Comment comment)
        {
            return new CommentDTO()
            {
                ID = comment.ID,
                Content = comment.Content,
                Score = comment.Score,
                ClientEmail = comment.ClientEmail
            };
        }

        public static CompanyServiceOptionDTO EntityToDTO(CompanyServiceOption cso)
        {
            return new CompanyServiceOptionDTO()
            {
                Service = EntityToDTO(cso.Service),
                Price = cso.Price
            };
        }

        public static ClientDTO EntityToDTO(Client client)
        {
            return new ClientDTO()
            {
                Email = client.Email,
                Name = client.Name,
                Surname = client.Surname,
                Phone = client.Phone,
                City = client.City,
                Street = client.Street,
                StreetNumber = client.Street,
                Building = client.Building,
                Staircase = client.Staircase,
                ApartmentNumber = client.ApartmentNumber,
                Floor = client.Floor
            };
        }

        public static EmployeeDTO EntityToDTO(Employee employee)
        {
            return new EmployeeDTO()
            {
                Email = employee.Email,
                Name = employee.Name,
                Surname = employee.Surname,
                Phone = employee.Phone,
                IsConfirmed = employee.IsConfirmed,
                Picture = employee.Picture,
                Services = new List<ServiceDTO>(),
                Orders = new List<OrderDTO>()
            };
        }

        public static MaterialDTO EntityToDTO(Material material)
        {
            return new MaterialDTO()
            {
                Name = material.Name,
                Price = material.Price,
                Quantity = material.Quantity
            };
        }

        public static OrderDTO EntityToDTO(Order order)
        {
            return new OrderDTO()
            {
                DateTime = order.DateTime,
                Duration = order.Duration,
                PaymentAmount = order.PaymentAmount,
                ServiceName = order.Service.Name
            };
        }
    }
}
