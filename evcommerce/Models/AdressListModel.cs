using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evcommerce.Models
{
    public class AdressListModel
    {
        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int House { get; set; }

        public string Flat { get; set; }

        public string Info { get; set; }
    }
}
