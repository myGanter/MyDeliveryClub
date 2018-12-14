using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StajAppCore.Models.Auth;
using Microsoft.EntityFrameworkCore;
using StajAppCore.Models;

namespace StajAppCore.Services.Repositories.AuthRepositories
{
    public class AuthRepositories : IAuthRepositories
    {
        private ApplicationContext DBContext;

        public AuthRepositories(ApplicationContext db)
        {
            DBContext = db;
        }

        public bool ActionQueue(Func<IAuthRepositories, bool> queue, bool invokeSaveChanges)
        {
            var resalt = queue(this);

            if (invokeSaveChanges)
                SaveChanges();

            return resalt;
        }

        public async Task<bool> ActionQueueAsync(Func<IAuthRepositories, Task<bool>> queue, bool invokeSaveChanges)
        {
            var resalt = await queue(this);

            if (invokeSaveChanges)
                await SaveChangesAsync();

            return resalt;
        }

        public bool AddObj(User obj)
        {
            DBContext.Users.Add(obj);
            return true;
        }

        public async Task<bool> AddObjAsync(User obj)
        {
            await DBContext.Users.AddAsync(obj);
            return true;
        }

        public void SaveChanges()
        {
            DBContext.SaveChanges();
        }

        public bool DeleteObj(int id)
        {
            var obj = GetObjById(id);
            DBContext.Users.Remove(obj);
            return true;
        }

        public async Task<bool> DeleteObjAsync(int id)
        {
            var obj = await GetObjByIdAsync(id);
            DBContext.Users.Remove(obj);
            return true;
        }

        public IEnumerable<User> GetListObj()
        {
            return DBContext.Users.ToList();
        }

        public async Task<IEnumerable<User>> GetListObjAsync()
        {
            return await DBContext.Users.ToListAsync();
        }

        public User GetObjById(int id)
        {
            return DBContext.Users.FirstOrDefault(i => i.Id == id);
        }

        public async Task<User> GetObjByIdAsync(int id)
        {
            return await DBContext.Users.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await DBContext.SaveChangesAsync();
        }

        public User GetUserByEmail(string email, bool loadRole)
        {
            var user = loadRole ? 
                DBContext.Users.Include(i => i.Role).FirstOrDefault(u => u.Email == email) : 
                DBContext.Users.FirstOrDefault(u => u.Email == email);
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email, bool loadRole)
        {
            var user = loadRole ?
                await DBContext.Users.Include(i => i.Role).FirstOrDefaultAsync(u => u.Email == email) :
                await DBContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}
