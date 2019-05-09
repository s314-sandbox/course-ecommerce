using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evcommerce.Models
{
    public class AccountDetailsModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        public string Mail { get; set; }

        public string Login { get; set; }

        public List<AdressListModel> AdressListModel { get; set; }

        public List<OrderInfoEntryModel> OrderInfoEntryModel { get; set; }

    }
}
