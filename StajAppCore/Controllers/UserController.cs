using StajAppCore.Models;
using System.Threading.Tasks;
using StajAppCore.Models.Auth;
using StajAppCore.Models.Store;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using StajAppCore.Models.Store.StoreView;
using StajAppCore.Services.Repositories.RepositoryBuilder;

namespace StajAppCore.Controllers
{
    public class UserController : BaseController
    {
        private IRepositoryBuilder RepositoryBuilder;

        public UserController(IRepositoryBuilder rb)
        {
            RepositoryBuilder = rb;
        }

        [HttpPost]
        [Authorize(Roles = Client)]
        public async Task<IActionResult> AddUniqOrder([FromBody]OrderModel order)
        {
            if (ModelState.IsValid)
            {
                Order newOreder = (Order)order;
                List<Product> products = (List<Product>)order.Products.ToIEnumerableProduct();
                newOreder.UserId = (await RepositoryBuilder.AuthRepository.GetUserByEmailAsync(User.Identity.Name, false)).Id;

                var result = await RepositoryBuilder.OrderRepository.ActionQueueAsync( async i => 
                {                   
                    await i.AddObjAsync(newOreder);
                    await RepositoryBuilder.ProductRepository.AddRangeAsync(products);

                    for (int j = 0; j < products.Count; j++)
                    {
                        newOreder.OrderProduct.Add(new OrderProduct()
                        {
                            OrderId = newOreder.Id,
                            ProductId = products[j].Id,
                            CountProduct = order.Products[j].Count
                        });
                    }

                    return true;
                }, true );

                return Data("Ваш заказ успешно добавлен!");
            }

            return Data("Ваш заказ не прошёл проверку.", ModelState.Root.Children);
        }

        [HttpGet]
        [Authorize(Roles = Client)]
        public async Task<IActionResult> GetUserOrders()
        {
            User us = await RepositoryBuilder.AuthRepository.GetUserByEmailAsync(User.Identity.Name, false);
            IEnumerable<Order> orders = await RepositoryBuilder.OrderRepository.GetOrdersByUserAsync(us.Id);
            IEnumerable<OrderModel> vueOrders = orders.ToIenumerableOrderModel(i => i.Courier, i => i.CourierDelivered);

            return Json(vueOrders);
        }

        [HttpGet]
        [Authorize(Roles = Client)]
        public async Task<IActionResult> ConfirmUserOrder(int id, bool cancelled)
        {
            var result = await RepositoryBuilder.OrderRepository.ActionQueueAsync( async i => 
            {
                User us = await RepositoryBuilder.AuthRepository.GetUserByEmailAsync(User.Identity.Name, false);
                Order usOrder = await i.GetObjByIdAsync(id);
                if (usOrder != null && usOrder.UserId == us.Id)
                {
                    if (cancelled)
                        usOrder.UserCancelled = true;
                    else
                        usOrder.Delivered = true;
                    return true;
                }
                return false;

            }, true);

            if (result)
                return Data("Готово!");

            return Data(":(");
        }
    }
}