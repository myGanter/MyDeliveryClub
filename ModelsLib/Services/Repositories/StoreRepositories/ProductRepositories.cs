﻿using System;
using System.Linq;
using StajAppCore.Models;
using System.Threading.Tasks;
using StajAppCore.Models.Store;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StajAppCore.Services.Repositories.StoreRepositories
{
    public class ProductRepositories : IProductRepositories
    {
        private ApplicationContext DBContext;

        public ProductRepositories(ApplicationContext db)
        {
            DBContext = db;
        }

        public bool ActionQueue(Func<IProductRepositories, bool> queue, bool invokeSaveChanges)
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

        public async Task<bool> ActionQueueAsync(Func<IProductRepositories, Task<bool>> queue, bool invokeSaveChanges)
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

        public bool AddObj(Product obj)
        {
            DBContext.Products.Add(obj);
            return true;
        }

        public async Task<bool> AddObjAsync(Product obj)
        {
            await DBContext.Products.AddAsync(obj);
            return true;
        }

        public bool AddRange(IEnumerable<Product> products)
        {
            DBContext.AddRange(products);
            return true;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<Product> products)
        {
            await DBContext.AddRangeAsync(products);
            return true;
        }

        public bool DeleteObj(int id)
        {
            var obj = GetObjById(id);
            DBContext.Products.Remove(obj);
            return true;
        }

        public async Task<bool> DeleteObjAsync(int id)
        {
            var obj = await GetObjByIdAsync(id);
            DBContext.Products.Remove(obj);
            return true;
        }

        public IEnumerable<Product> GetListObj()
        {
            return DBContext.Products.ToList();
        }

        public async Task<IEnumerable<Product>> GetListObjAsync()
        {
            return await DBContext.Products.ToListAsync();
        }

        public Product GetObjById(int id)
        {
            return DBContext.Products.FirstOrDefault(i => i.Id == id);
        }

        public async Task<Product> GetObjByIdAsync(int id)
        {
            return await DBContext.Products.FirstOrDefaultAsync(i => i.Id == id);
        }

        public void SaveChanges()
        {
            DBContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await DBContext.SaveChangesAsync();
        }
    }
}
