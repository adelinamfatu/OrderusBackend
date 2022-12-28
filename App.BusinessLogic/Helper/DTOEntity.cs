using App.Domain;
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
                //Category = DTOEntity.DTOtoEntity(service.Category)
            };
        }
    }
}
