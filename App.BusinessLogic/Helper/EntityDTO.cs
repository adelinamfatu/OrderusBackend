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
                Site = company.Site
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
    }
}
