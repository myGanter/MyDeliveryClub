using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StajAppCore.Models.Store;

namespace StajAppCore.Services.Repositories.StoreRepositories
{
    public interface IProductRepositories : IRepository<Product, IProductRepositories>
    {
        bool AddRange(IEnumerable<Product> products);

        Task<bool> AddRangeAsync(IEnumerable<Product> products);
    }
}
