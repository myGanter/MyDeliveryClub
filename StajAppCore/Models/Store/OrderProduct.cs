using StajAppCore.Models.Store.StoreView;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StajAppCore.Models.Store
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Range(1, Int32.MaxValue)]
        public int CountProduct { get; set; }
    }

    public static class OrderProductEX
    {
        public static IEnumerable<ProductModel> ToIEnumerableProduct(this IEnumerable<OrderProduct> products)
        {
            List<ProductModel> newProducts = new List<ProductModel>();
            foreach (var i in products)
                newProducts.Add(
                    new ProductModel()
                    {
                        Name = i.Product.Name,
                        Description = i.Product.Description,
                        Price = i.Product.Price,
                        Count = i.CountProduct
                    });
            return newProducts;
        }
    }
}
