using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evcommerce.Models
{
    public class Basket
    {

        private BasketContext context;

        public int Id { get; set; }

        public int UserId { get; set; }

        public int ItemId { get; set; }

        public int Amount { get; set; }

    }
}
