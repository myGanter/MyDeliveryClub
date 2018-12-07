using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StajAppCore.Models.Auth;
using StajAppCore.Models.Store.StoreView;

namespace StajAppCore.Models.Store
{
    public class Order
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string DeliveryAddress { get; set; }
        public bool Delivered { get; set; } = false;

        public int? UserId { get; set; }
        public User User { get; set; }

        public List<OrderProduct> OrderProduct { get; set; }

        public Order()
        {
            OrderProduct = new List<OrderProduct>();
        }

        public static explicit operator OrderModel (Order order) =>
            new OrderModel()
            {
                Id = order.Id,
                DeliveryAddress = order.DeliveryAddress,
                Description = order.Description
            };
    }
}
