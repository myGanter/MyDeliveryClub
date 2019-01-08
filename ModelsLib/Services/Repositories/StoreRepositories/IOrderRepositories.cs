using System.Threading.Tasks;
using StajAppCore.Models.Store;
using System.Collections.Generic;

namespace StajAppCore.Services.Repositories.StoreRepositories
{
    public interface IOrderRepositories : IRepository<Order, IOrderRepositories>
    {
        Task<IEnumerable<Order>> GetOrdersByUserAsync(int id);

        Task<IEnumerable<Order>> GetAllOrdersAsync();

        Task<IEnumerable<Order>> GetOrdersByCourierAsync(int id);
    }
}
