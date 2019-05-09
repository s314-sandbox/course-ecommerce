using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evcommerce.Models
{
    public class BasketPositionView
    {
        
        public int Id { get; set; }

        public string ItemName { get; set; }

        public int Amount { get; set; }

        public string VendorCode { get; set; }

        public int Cost { get; set; }

        public string SubCategory { get; set; }

        public string Category { get; set; }

        public string Provider { get; set; }

    }
}
