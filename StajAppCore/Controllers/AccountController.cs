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

namespace StajAppCore.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext DBContext;

        public AccountController(ApplicationContext context)
        {
            DBContext = context;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await DBContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    user = (User)model;
                    Role userRole = await DBContext.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId);
                    if (userRole != null)
                        user.Role = userRole;

                    DBContext.Users.Add(user);
                    await DBContext.SaveChangesAsync();

                    await Authenticate(user);
                }
            }

            return RedirectToAction("Index", "Main", new { EROOR = !ModelState.IsValid });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await DBContext.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                    await Authenticate(user);
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
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
 
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
  
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}