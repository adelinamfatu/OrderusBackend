using System;
using System.Collections.Generic;
using System.Text;

namespace App.DTO
{
    public class ClientDTO
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public string Building { get; set; }

        public string Staircase { get; set; }

        public int? ApartmentNumber { get; set; }

        public int? Floor { get; set; }

        public string Picture { get; set; }
    }
}
