using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StajAppCore.Services.Repositories
{
    public interface IRepository<T, U>
    {
        bool AddObj(T obj);
        Task<bool> AddObjAsync(T obj);

        IEnumerable<T> GetListObj();
        Task<IEnumerable<T>> GetListObjAsync();

        T GetObjById(int id);
        Task<T> GetObjByIdAsync(int id);

        bool DeleteObj(int id);
        Task<bool> DeleteObjAsync(int id);

        void SaveChanges();
        Task SaveChangesAsync();
        
        bool ActionQueue(Func<U, bool> queue, bool invokeSaveChanges);
        Task<bool> ActionQueueAsync(Func<U, Task<bool>> queue, bool invokeSaveChanges);
    }
}
