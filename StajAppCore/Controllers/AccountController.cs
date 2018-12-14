using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using StajAppCore.Models;
using StajAppCore.Models.Account;
using StajAppCore.Models.Auth;
using StajAppCore.Services;
using StajAppCore.Services.Repositories.RepositoryBuilder;

namespace StajAppCore.Controllers
{
    public class AccountController : Controller
    {
        private IRepositoryBuilder RepositoryBuilder;
        private RoleMaster RM;

        public AccountController(RoleMaster rm, IRepositoryBuilder rb)
        {
            RepositoryBuilder = rb;
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
                        await i.AddObjAsync(newUser);
                        return true;
                    }
                    else
                        return false;
                }, true);

                if (result)
                    await Authenticate(newUser);
                else
                    ModelState.AddModelError("err", "error");
            }

            return RedirectToAction("Index", "Main", new { EROOR = !ModelState.IsValid });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await RepositoryBuilder.AuthRepository.GetUserByEmailAsync(model.Email, true);

                if (user != null && user.Password == model.Password)
                    await Authenticate(user);
                else
                    ModelState.AddModelError("err", "error");
            }

            return RedirectToAction("Index", "Main", new { EROOR = !ModelState.IsValid });
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
    }
}