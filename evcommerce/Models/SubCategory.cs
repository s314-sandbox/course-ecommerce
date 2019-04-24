using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evcommerce.Models
{
    public class SubCategory
    {

        private SubCategoryContext context;

        public int Id { get; set; }

        public int Category { get; set; }

        public string Name { get; set; }

    }
}
