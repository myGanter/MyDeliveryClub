using StajAppCore.Models;
using System.Threading.Tasks;
using StajAppCore.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using StajAppCore.Models.Store;
using System.Collections.Generic;
using StajAppCore.Models.Store.StoreView;
using Microsoft.AspNetCore.Authorization;
using StajAppCore.Services.Repositories.RepositoryBuilder;

namespace StajAppCore.Controllers
{
    public class CourierController : BaseController
    {
        private IRepositoryBuilder RepositoryBuilder;

        public CourierController(IRepositoryBuilder rb)
        {
            RepositoryBuilder = rb;
        }

        [HttpGet]
        [Authorize(Roles = Courier)]
        public async Task<IActionResult> GetAllOrders()
        {
            IEnumerable<Order> orders = await RepositoryBuilder.OrderRepository.GetAllOrdersAsync();
            IEnumerable<OrderModel> vueOrders = orders.ToIenumerableOrderModel(i => i.User, i => i.Delivered);

            return Json(vueOrders);
        }

        [HttpGet]
        [Authorize(Roles = Courier)]
        public async Task<IActionResult> GetCourierOrders()
        {
            User us = await RepositoryBuilder.AuthRepository.GetUserByEmailAsync(User.Identity.Name, false);
            IEnumerable<Order> orders = await RepositoryBuilder.OrderRepository.GetOrdersByCourierAsync(us.Id);
            IEnumerable<OrderModel> vueOrders = orders.ToIenumerableOrderModel(i => i.User, i => i.Delivered);

            return Json(vueOrders);
        }

        [HttpGet]
        [Authorize(Roles = Courier)]
        public async Task<IActionResult> TakeOrder(int id)
        {
            var result = await RepositoryBuilder.OrderRepository.ActionQueueAsync(async i => 
            {
                Order order = await i.GetObjByIdAsync(id);
                if (order.CourierId != null)
                    return false;

                User us = await RepositoryBuilder.AuthRepository.GetUserByEmailAsync(User.Identity.Name, false);
                order.CourierId = us.Id;

                return true;
            }, true);

            if (result)
                return Data("Заказ взят!");

            return Data(":(");
        }

        [HttpGet]
        [Authorize(Roles = Courier)]
        public async Task<IActionResult> ConfirmOrderCourier(int id)
        {
            var result = await RepositoryBuilder.OrderRepository.ActionQueueAsync(async i =>
            {
                User us = await RepositoryBuilder.AuthRepository.GetUserByEmailAsync(User.Identity.Name, false);
                Order usOrder = await i.GetObjByIdAsync(id);
                if (usOrder != null && usOrder.CourierId == us.Id)
                {
                    usOrder.CourierDelivered = true;
                    return true;
                }
                return false;

            }, true);

            if (result)
                return Data("Доставка подтверждена!");

            return Data(":("); 
        }
    }
}