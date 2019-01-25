using StajAppCore.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StajAppCore.Services.Repositories.RepositoryBuilder;

namespace StajAppCore.Controllers
{
    public class MainController : BaseController
    {
        public MainController(IRepositoryBuilder rb, RoleMaster rM) : base(rb, rM)
        { }

        [HttpGet]
        public async Task<IActionResult> Index() => await GetCurrentView("Main", "Index");        
    }
}