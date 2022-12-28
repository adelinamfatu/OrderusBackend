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
            return new CategoryDTO() { Name = serviceCategory.Name };
        }
    }
}
