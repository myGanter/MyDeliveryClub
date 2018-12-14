using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StajAppCore.Services.Repositories.AuthRepositories;
using StajAppCore.Services.Repositories.StoreRepositories;

namespace StajAppCore.Services.Repositories.RepositoryBuilder
{
    public interface IRepositoryBuilder : IDisposable
    {
        IAuthRepositories AuthRepository { get; }
        IOrderRepositories OrderRepository { get; }
        IProductRepositories ProductRepository { get; }
    }
}
