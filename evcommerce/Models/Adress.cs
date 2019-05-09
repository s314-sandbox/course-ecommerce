using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evcommerce.Models
{
    public class Adress
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int House { get; set; }

        public string Flat { get; set; }

        public string Info { get; set; }

        public int UserId { get; set; }

    }
}
