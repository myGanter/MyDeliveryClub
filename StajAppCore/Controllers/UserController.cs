using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StajAppCore.Models;
using Microsoft.EntityFrameworkCore;
using StajAppCore.Models.Store;
using StajAppCore.Models.Auth;
using StajAppCore.Models.Store.StoreView;

namespace StajAppCore.Controllers
{
    public class UserController : Controller
    {
        private ApplicationContext bd;
        public UserController(ApplicationContext bd)
        {
            this.bd = bd;
        }

        [HttpPost]
        [Authorize(Roles = "Пользователь")]
        public async Task<IActionResult> AddUniqOrder([FromBody]OrderModel order)
        {
            if (ModelState.IsValid)
            {
                Order newOreder = (Order)order;
                List<Product> products = new List<Product>();
                foreach (var i in order.Products)
                    products.Add((Product)i);

                newOreder.UserId = (await bd.GetUserAsync(User.Identity.Name)).Id;
                await bd.Products.AddRangeAsync(products);
                await bd.Orders.AddAsync(newOreder);
                for (int i = 0; i < products.Count; i++)
                {
                    newOreder.OrderProduct.Add(new OrderProduct() { OrderId = newOreder.Id, ProductId = products[i].Id, CountProduct = order.Products[i].Count });
                }

                await bd.SaveChangesAsync();
            }

            return Json(new { error = ModelState.IsValid ? false : true });
        }

        [HttpGet]
        [Authorize(Roles = "Пользователь")]
        public async Task<IActionResult> GetUserOrders()
        {
            User us = await bd.GetUserAsync(User.Identity.Name);
            var orders = await bd.Orders.Where(i => i.UserId == us.Id && !i.Delivered).Include(i => i.OrderProduct).ThenInclude(sc => sc.Product).ToListAsync();
            List<OrderModel> vueOrders = new List<OrderModel>();
            foreach (var i in orders)
            {
                OrderModel bar = (OrderModel)i;
                bar.Products = (List<ProductModel>)i.OrderProduct.ToIEnumerableProduct();
                vueOrders.Add(bar);
            }

            return Json(vueOrders);
        }

        [HttpGet]
        [Authorize(Roles = "Пользователь")]
        public async Task<IActionResult> ConfirmOrder(int id)
        {
            User us = await bd.GetUserAsync(User.Identity.Name);
            var order = await bd.Orders.FirstOrDefaultAsync(i => i.Id == id );
            if (order != null)
                order.Delivered = true;

            await bd.SaveChangesAsync();
            return Json(new { error = false });
        }
    }
}