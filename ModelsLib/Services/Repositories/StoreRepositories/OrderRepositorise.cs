using System;
using System.Linq;
using StajAppCore.Models;
using System.Threading.Tasks;
using StajAppCore.Models.Store;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StajAppCore.Services.Repositories.StoreRepositories
{
    public class OrderRepositorise : IOrderRepositories
    {
        private ApplicationContext DBContext;

        public OrderRepositorise(ApplicationContext db)
        {
            DBContext = db;
        }

        public bool ActionQueue(Func<IOrderRepositories, bool> queue, bool invokeSaveChanges)
        {
            bool resalt = false;
            using (var transaction = DBContext.Database.BeginTransaction())
            {
                resalt = queue(this);

                if (invokeSaveChanges && resalt)
                    SaveChanges();
                
                if (resalt)
                    transaction.Commit();
            }

            return resalt;
        }

        public async Task<bool> ActionQueueAsync(Func<IOrderRepositories, Task<bool>> queue, bool invokeSaveChanges)
        {
            bool resalt = false;
            using (var transaction = await DBContext.Database.BeginTransactionAsync())
            {
                resalt = await queue(this);

                if (invokeSaveChanges && resalt)
                    await SaveChangesAsync();

                if (resalt)
                    transaction.Commit();
            }

            return resalt;
        }

        public bool AddObj(Order obj)
        {
            DBContext.Orders.Add(obj);
            return true;
        }

        public async Task<bool> AddObjAsync(Order obj)
        {
            await DBContext.Orders.AddAsync(obj);
            return true;
        }

        public bool DeleteObj(int id)
        {
            var obj = GetObjById(id);
            DBContext.Orders.Remove(obj);
            return true;
        }

        public async Task<bool> DeleteObjAsync(int id)
        {
            var obj = await GetObjByIdAsync(id);
            DBContext.Orders.Remove(obj);
            return true;
        }

        public IEnumerable<Order> GetListObj()
        {
            return DBContext.Orders.ToList();
        }

        public async Task<IEnumerable<Order>> GetListObjAsync()
        {
            return await DBContext.Orders.ToListAsync();
        }

        public Order GetObjById(int id)
        {
            return DBContext.Orders.FirstOrDefault(i => i.Id == id);
        }

        public async Task<Order> GetObjByIdAsync(int id)
        {
            return await DBContext.Orders.FirstOrDefaultAsync(i => i.Id == id);
        }

        public void SaveChanges()
        {
            DBContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await DBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserAsync(int id)
        {           
            var orders = await DBContext.Orders
                .Where(i => i.UserId == id && !i.Delivered && !i.UserCancelled)
                .Include(i => i.Courier)
                .Include(i => i.OrderProduct)
                .ThenInclude(sc => sc.Product)
                .ToListAsync();

            return orders;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = await DBContext.Orders
                .Where(i => !i.Delivered && i.CourierId == null && !i.UserCancelled)
                .Include(i => i.User)
                .Include(i => i.OrderProduct)
                .ThenInclude(sc => sc.Product)
                .ToListAsync();

            return orders;
        }

        public async Task<IEnumerable<Order>> GetOrdersByCourierAsync(int id)
        {
            var orders = await DBContext.Orders
                .Where(i => i.CourierId == id && !i.CourierDelivered)
                .Include(i => i.User)
                .Include(i => i.OrderProduct)
                .ThenInclude(sc => sc.Product)
                .ToListAsync();

            return orders;
        }
    }
}
