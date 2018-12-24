using System.Linq;
using StajAppCore.Services;
using System.Threading.Tasks;
using StajAppCore.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using StajAppCore.Services.Repositories.RepositoryBuilder;

namespace StajAppCore.Controllers
{
    public class MainController : Controller
    {
        private IRepositoryBuilder RepositoryBuilder;
        private RoleMaster RoleM;

        public MainController(IRepositoryBuilder rb, RoleMaster rM)
        {
            RepositoryBuilder = rb;
            RoleM = rM;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {            
            User us = await RepositoryBuilder.AuthRepository.GetUserByEmailAsync(User.Identity.Name, true);
            if (us != null)
            {
                ViewData["UserName"] = us.Name == null ? us.Email : us.Name;
                string roleName = us.Role.Name;
                return View(RoleM.GetViewRole(roleName).Item1, RoleM.GetViewRole(roleName).Item2);
            }

            ViewData["Roles"] = RoleM.GetRoles().Where(i => RoleM.ValidationAllowed(i.Id));
            return View("Hello", null);
        }
    }
}