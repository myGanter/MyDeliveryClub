using System.Collections.Generic;

namespace StajAppCore.Models.Store
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public int? ShopId { get; set; }
        public Shop Shop { get; set; }

        public List<OrderProduct> OrderProduct { get; set; }

        public Product()
        {
            OrderProduct = new List<OrderProduct>();
        }
    }
}
