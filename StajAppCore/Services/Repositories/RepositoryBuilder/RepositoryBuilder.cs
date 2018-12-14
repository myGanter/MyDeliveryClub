using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StajAppCore.Models;
using StajAppCore.Services.Repositories.AuthRepositories;
using StajAppCore.Services.Repositories.StoreRepositories;

namespace StajAppCore.Services.Repositories.RepositoryBuilder
{
    public class RepositoryBuilder : IRepositoryBuilder
    {
        private ApplicationContext DBContext;

        public RepositoryBuilder(ApplicationContext db)
        {
            DBContext = db;
        }
        private IAuthRepositories authRepository;
        public IAuthRepositories AuthRepository
        {
            get
            {
                if (authRepository == null)
                    authRepository = new AuthRepositories.AuthRepositories(DBContext);
                return authRepository;
            }
        }

        private IOrderRepositories orderRepository;
        public IOrderRepositories OrderRepository
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepositorise(DBContext);
                return orderRepository;
            }
        }

        private IProductRepositories productRepository;
        public IProductRepositories ProductRepository
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepositories(DBContext);
                return productRepository;
            }
        }

        public void Dispose()
        {
            DBContext.Dispose();
            //System.Diagnostics.Debug.WriteLine("DBContext destroed!!!");
        }
    }
}
