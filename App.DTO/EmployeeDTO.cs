using System;
using System.Collections.Generic;
using System.Text;

namespace App.DTO
{
    public class EmployeeDTO
    {
        public string Email { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public int CompanyID { get; set; }

        public string Picture { get; set; }

        public bool IsConfirmed { get; set; }
    }
}
