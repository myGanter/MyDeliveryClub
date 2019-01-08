using System.Linq;
using StajAppCore.Models;
using StajAppCore.Services;
using System.Threading.Tasks;
using System.Security.Claims;
using StajAppCore.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using StajAppCore.Models.Account;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using StajAppCore.Services.Repositories.RepositoryBuilder;

namespace StajAppCore.Controllers
{
    public class AccountController : BaseController
    {
        private IRepositoryBuilder RepositoryBuilder;
        private RoleMaster RM;
        private PasswdHesher<User> PasswdHesh;

        public AccountController(RoleMaster rm, PasswdHesher<User> ph, IRepositoryBuilder rb)
        {
            RepositoryBuilder = rb;
            PasswdHesh = ph;
            RM = rm;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = (User)model;
                var result = await RepositoryBuilder.AuthRepository.ActionQueueAsync( async i => 
                {
                    User user = await i.GetUserByEmailAsync(model.Email, false);
                    if (user == null)
                    {
                        PasswdHesh.SetHeshContSalt(newUser, newUser.Password);
                        await i.AddObjAsync(newUser);
                        return true;
                    }
                    else
                        return false;
                }, true);

                if (result)
                {
                    await Authenticate(newUser);
                    return RedirectToAction("Index", "Main");
                }
                else
                    ModelState.AddModelError("err", "Такой пользователь уже существует!");
            }

            return GetMainVue(new MsgVue("Что-то не так!", ModelState.Root.Children));
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await RepositoryBuilder.AuthRepository.GetUserByEmailAsync(model.Email, true);

                if (user != null && PasswdHesh.VerifyPasswd(user, model.Password))
                {
                    await Authenticate(user);
                    return RedirectToAction("Index", "Main");
                }
                else
                    ModelState.AddModelError("err", "Неверный логин или пароль");
            }

            return GetMainVue(new MsgVue("Что-то не так!", ModelState.Root.Children));          
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Main");
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, RM.GetRole((int)user.RoleId)?.Name)
            };
 
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
  
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        private IActionResult GetMainVue(MsgVue msg)
        {
            ViewData["Roles"] = RM.GetRoles().Where(i => RM.ValidationAllowed(i.Id));           
            ViewData["ERROR"] = msg;
            return View("~/Views/Main/Hello.cshtml", null);
        }
    }
}