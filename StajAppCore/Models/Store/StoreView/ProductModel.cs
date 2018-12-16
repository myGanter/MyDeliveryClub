using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StajAppCore.Models.Store.StoreView
{
    public class ProductModel
    {
        [Required(ErrorMessage = "У товара должно быть название")]
        public string Name { get; set; }
        [Required(ErrorMessage = "У товара должно быть описание")]
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

    public static class ProductModelEX
    {
        public static IEnumerable<Product> ToIEnumerableProduct(this IEnumerable<ProductModel> products)
        {
            List<Product> newProducts = new List<Product>();
            foreach (var i in products)
                newProducts.Add((Product)i);
            return newProducts;
        }
    }
}
