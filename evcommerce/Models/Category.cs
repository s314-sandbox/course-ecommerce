using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evcommerce.Models
{
    public class Category
    {

        private CategoryContext context;

        public int Id { get; set; }

        public string Name { get; set; }

    }
}
