using System;
using StajAppCore.Models.Auth;
using System.Collections.Generic;
using StajAppCore.Models.Auth.AuthView;
using StajAppCore.Models.Store.StoreView;

namespace StajAppCore.Models.Store
{
    public class Order
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string DeliveryAddress { get; set; }
        public bool Delivered { get; set; } = false;
        public bool CourierDelivered { get; set; } = false;

        public int? UserId { get; set; }
        public User User { get; set; }

        public int? CourierId { get; set; }
        public User Courier { get; set; }

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

    public static class OrderEX
    {
        public static OrderModel ToOrderModel(this Order order, Func<Order, User> getUser, Func<Order, bool> userDelivered)
        {
            OrderModel newOrderModel = (OrderModel)order;
            User us = getUser(order);
            newOrderModel.UserOppositeSide = us == null ? null : (UserModel)us;
            newOrderModel.DeliveredOppositeSide = userDelivered(order);
            newOrderModel.Products = (List<ProductModel>)order.OrderProduct.ToIEnumerableProduct();

            return newOrderModel;
        }

        public static IEnumerable<OrderModel> ToIenumerableOrderModel(this IEnumerable<Order> orders, Func<Order, User> getUser, Func<Order, bool> userDelivered)
        {
            List<OrderModel> orderModels = new List<OrderModel>();
            foreach (var i in orders)
                orderModels.Add(i.ToOrderModel(getUser, userDelivered));

            return orderModels;
        }
    }
}
