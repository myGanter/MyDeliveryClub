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
using StajAppCore.Models.Auth.AuthView;
using StajAppCore.Models.Store.StoreView;
using StajAppCore.Services.Repositories.RepositoryBuilder;

namespace StajAppCore.Controllers
{
    public class CourierController : Controller
    {
        private IRepositoryBuilder RepositoryBuilder;

        public CourierController(IRepositoryBuilder rb)
        {
            RepositoryBuilder = rb;
        }

        [HttpGet]
        [Authorize(Roles = "Курьер")]
        public async Task<IActionResult> GetAllOrders()
        {
            IEnumerable<Order> orders = await RepositoryBuilder.OrderRepository.GetAllOrdersAsync();
            IEnumerable<OrderModel> vueOrders = orders.ToIenumerableOrderModel(i => i.User, i => i.Delivered);

            return Json(vueOrders);
        }

        [HttpGet]
        [Authorize(Roles = "Курьер")]
        public async Task<IActionResult> GetCourierOrders()
        {
            User us = await RepositoryBuilder.AuthRepository.GetUserByEmailAsync(User.Identity.Name, false);
            IEnumerable<Order> orders = await RepositoryBuilder.OrderRepository.GetOrdersByCourierAsync(us.Id);
            IEnumerable<OrderModel> vueOrders = orders.ToIenumerableOrderModel(i => i.User, i => i.Delivered);

            return Json(vueOrders);
        }

        [HttpGet]
        [Authorize(Roles = "Курьер")]
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
            
            return Json(new { error = !result });
        }

        [HttpGet]
        [Authorize(Roles = "Курьер")]
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
                return Json(new { error = false });

            return Json(new { error = true });
        }
    }
}