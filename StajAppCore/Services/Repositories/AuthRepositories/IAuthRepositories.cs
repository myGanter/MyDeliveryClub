using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StajAppCore.Models.Auth;

namespace StajAppCore.Services.Repositories.AuthRepositories
{
    public interface IAuthRepositories : IRepository<User, IAuthRepositories>
    {
        User GetUserByEmail(string email, bool loadRole);

        Task<User> GetUserByEmailAsync(string email, bool loadRole);
    }
}
