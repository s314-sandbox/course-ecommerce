using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evcommerce.Models
{
    public class Item
    {
        private ItemContext context;

        public int Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public int Category { get; set; }

        public int Provider { get; set; }

        public int Cost { get; set; }
    }
}
