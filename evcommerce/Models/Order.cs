using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evcommerce.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int OrderInfoId { get; set; }

        public int ItemId { get; set; }

        public int Amount { get; set; }
    }
}
