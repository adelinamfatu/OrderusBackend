using System;
using System.Collections.Generic;
using System.Text;

namespace App.DTO
{
    public enum DiscountType
    {
        Value,
        Percentage
    }

    public class OfferDTO
    {
        public int Discount { get; set; }

        public DiscountType Type { get; set; }

        public string ClientEmail { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
