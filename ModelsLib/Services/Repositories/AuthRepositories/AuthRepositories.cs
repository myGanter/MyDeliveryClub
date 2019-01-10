using System;
using System.Linq;
using StajAppCore.Models;
using System.Threading.Tasks;
using StajAppCore.Models.Auth;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> ActionQueueAsync(Func<IAuthRepositories, Task<bool>> queue, bool invokeSaveChanges)
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

        public User GetUserByEmail(string email, bool loadRole, bool confirmd = true)
        {
            var user = loadRole ? 
                DBContext.Users.Include(i => i.Role).FirstOrDefault(u => u.Email == email && u.Active == confirmd) : 
                DBContext.Users.FirstOrDefault(u => u.Email == email && u.Active == confirmd);
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email, bool loadRole, bool confirmd = true)
        {
            var user = loadRole ?
                await DBContext.Users.Include(i => i.Role).FirstOrDefaultAsync(u => u.Email == email && u.Active == confirmd) :
                await DBContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Active == confirmd);
            return user;
        }

        public async Task<bool> AddUserGuideAsync(UserGuid ug)
        {
            await DBContext.UserGuids.AddAsync(ug);
            return true;
        }

        public async Task<UserGuid> GetUserGuideByIdAsync(int id)
        {
            return await DBContext.UserGuids.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> DeleteUserGuideAsync(int id)
        {
            var obj = await GetUserGuideByIdAsync(id);
            DBContext.UserGuids.Remove(obj);
            return true;
        }

        public async Task<bool> DeleteUserGuideInUserIdAsync(int id)
        {
            var obj = await DBContext.UserGuids.FirstOrDefaultAsync(i => i.UserId == id);
            if (obj != null)
                DBContext.UserGuids.Remove(obj);
            return true;
        }
    }
}
