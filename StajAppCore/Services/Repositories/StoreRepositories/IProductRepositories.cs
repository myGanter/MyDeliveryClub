using System.Threading.Tasks;
using StajAppCore.Models.Store;
using System.Collections.Generic;

namespace StajAppCore.Services.Repositories.StoreRepositories
{
    public interface IProductRepositories : IRepository<Product, IProductRepositories>
    {
        bool AddRange(IEnumerable<Product> products);

        Task<bool> AddRangeAsync(IEnumerable<Product> products);
    }
}
