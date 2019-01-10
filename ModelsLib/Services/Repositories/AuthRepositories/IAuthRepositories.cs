using System.Threading.Tasks;
using StajAppCore.Models.Auth;

namespace StajAppCore.Services.Repositories.AuthRepositories
{
    public interface IAuthRepositories : IRepository<User, IAuthRepositories>
    {
        User GetUserByEmail(string email, bool loadRole, bool confirmd = true);
        Task<User> GetUserByEmailAsync(string email, bool loadRole, bool confirmd = true);

        Task<bool> AddUserGuideAsync(UserGuid ug);
        Task<UserGuid> GetUserGuideByIdAsync(int id);
        Task<bool> DeleteUserGuideAsync(int id);
        Task<bool> DeleteUserGuideInUserIdAsync(int id);
    }
}
