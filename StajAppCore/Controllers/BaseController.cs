using System.Linq;
using StajAppCore.Models;
using StajAppCore.Services;
using System.Threading.Tasks;
using StajAppCore.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StajAppCore.Services.Repositories.RepositoryBuilder;
using static Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace StajAppCore.Controllers
{
    public class BaseController : Controller
    {
        protected const string Admin = "Администратор";
        protected const string Client = "Пользователь";
        protected const string Courier = "Курьер";

        protected RoleMaster RoleM;
        protected IRepositoryBuilder RepositoryBuilder;

        public BaseController(IRepositoryBuilder rb, RoleMaster rM)
        {
            RepositoryBuilder = rb;
            RoleM = rM;
        }

        protected JsonResult Data(string msg) => Json(new MsgVue(msg));

        protected JsonResult Data(string msg, IReadOnlyList<ModelStateEntry> info) => Json(new MsgVue(msg, info));

        protected JsonResult Data(string msg, ValueEnumerable errors) => Json(new MsgVue(msg, errors));

        protected async Task<IActionResult> GetCurrentView(string controller, string method, MsgVue ifHello = null)
        {
            User us = await RepositoryBuilder.AuthRepository.GetUserByEmailAsync(User.Identity.Name, true);
            if (us != null)
            {
                var vueInfo = RoleM.GetViewRole(us.Role.Name);
                ViewData["UserName"] = us.Name == null ? us.Email : us.Name;

                if (controller == vueInfo.Item2 && ifHello != null)
                    return CreateView(
                        vueInfo.Item1, 
                        vueInfo.Item3, 
                        controller, method);
                else 
                    return CreateView(
                        vueInfo.Item1, 
                        vueInfo.Item3, 
                        vueInfo.Item2, 
                        vueInfo.Item3.FirstOrDefault().Url, 
                        ifHello != null ? new MsgVue("Недостаточно прав") : null);
            }

            return GetHelloView(ifHello);
        }

        protected IActionResult GetHelloView(MsgVue msg = null)
        {
            return CreateView("Hello", null, "Main", "Index", msg);
        }

        private IActionResult CreateView(string viewName, IEnumerable<MenuItem> menu, string controller, string method, MsgVue msg = null)
        {
            ViewData["Controller"] = controller;
            ViewData["Method"] = method;

            ViewData["Roles"] = RoleM.GetRoles().Where(i => RoleM.ValidationAllowed(i.Id));
            if (msg != null)
                ViewData["ERROR"] = msg;
            return GetView(viewName, menu);
        }

        private IActionResult GetView(string viewName, object model) => View($"~/Views/Main/{viewName}.cshtml", model);
    }
}
