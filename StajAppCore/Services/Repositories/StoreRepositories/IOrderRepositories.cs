using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StajAppCore.Models.Store;
using StajAppCore.Services.Repositories.AuthRepositories;

namespace StajAppCore.Services.Repositories.StoreRepositories
{
    public interface IOrderRepositories : IRepository<Order, IOrderRepositories>
    {
        Task<IEnumerable<Order>> GetOrdersByUserAsync(int id);

        Task<IEnumerable<Order>> GetAllOrdersAsync();

        Task<IEnumerable<Order>> GetOrdersByCourierAsync(int id);
    }
}
