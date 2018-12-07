using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StajAppCore.Models.Store.StoreView
{
    public class ProductModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Range(1, Int32.MaxValue)]
        public int Price { get; set; }
        [Range(1, Int32.MaxValue)]
        public int Count { get; set; }

        public static explicit operator Product (ProductModel ProdM) => 
            new Product()
            {
                Name = ProdM.Name,
                Description = ProdM.Description,
                Price = ProdM.Price
            };
    }
}
