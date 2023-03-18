using System;
using System.Collections.Generic;
using System.Text;

namespace App.DTO
{
    public class CompanyServiceOptionDTO
    {
        public CompanyDTO Company { get; set; }

        public float Price { get; set; }

        public List<CommentDTO> Comments { get; set; }
    }
}
