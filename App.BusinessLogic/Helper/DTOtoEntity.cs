using App.Domain;
using App.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BusinessLogic.Helper
{
    public static class DTOtoEntity
    {
        public static CategoryDTO EntitytoDTO(ServiceCategory serviceCategory)
        {
            return new CategoryDTO() { name = serviceCategory.Name };
        }
    }
}
