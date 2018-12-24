using System.Collections.Generic;

namespace StajAppCore.Models.Store
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; }

        public Shop()
        {
            Products = new List<Product>();
        }
    }
}
