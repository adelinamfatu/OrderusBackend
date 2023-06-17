using System;
using System.Collections.Generic;
using System.Text;

namespace App.DTO
{
    public class OfferDTO
    {
        public int DiscountValue { get; set; }

        public int DiscountPercentage { get; set; }

        public string ClientEmail { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
