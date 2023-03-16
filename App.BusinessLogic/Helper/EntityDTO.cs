using App.Domain;
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
                Name = service.Name,
                //Category = EntityDTO.EntityToDTO(service.Category)
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
                ApartmentNumber = (int)company.ApartmentNumber,
                Floor = (int)company.Floor,
                Logo = company.Logo,
                Site = company.Site
            };
        }
    }
}
