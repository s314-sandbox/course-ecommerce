using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace evcommerce.Models
{
    public class OrderInfo
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public int PaymentId { get; set; }

        public int DeliveryId { get; set; }

        public int AdressId { get; set; }
    }
}
