using StajAppCore.Models;
using StajAppCore.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StajAppCore.Services.Repositories.RepositoryBuilder;

namespace StajAppCore.Controllers
{
    public class AdminController : BaseController
    {
        public AdminController(IRepositoryBuilder rb, RoleMaster rm) : base(rb, rm)
        { }

        [HttpGet]
        public async Task<IActionResult> Couriers() => await GetCurrentView("Admin", "Couriers", new MsgVue("Авторизируйтесь"));

        [HttpGet]
        public async Task<IActionResult> Shops() => await GetCurrentView("Admin", "Shops", new MsgVue("Авторизируйтесь"));

        [HttpGet]
        public async Task<IActionResult> Users() => await GetCurrentView("Admin", "Users", new MsgVue("Авторизируйтесь")); 
    }
}