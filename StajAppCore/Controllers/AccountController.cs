using System;
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
using StajAppCore.Services.MessageSending;
using Microsoft.AspNetCore.Authentication.Cookies;
using StajAppCore.Services.Repositories.RepositoryBuilder;

namespace StajAppCore.Controllers
{
    public class AccountController : BaseController
    {
        private IRepositoryBuilder RepositoryBuilder;
        private RoleMaster RM;
        private PasswdHesher<IHesh> PasswdHesh;
        private IMass MailSender;

        public AccountController(RoleMaster rm, PasswdHesher<IHesh> ph, IRepositoryBuilder rb, IMass sm)
        {
            MailSender = sm;
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
                UserGuid usg = new UserGuid();
                string guideValue = "";
                var result = await RepositoryBuilder.AuthRepository.ActionQueueAsync(async i =>
                {
                   User user = await i.GetUserByEmailAsync(model.Email, false);                   
                   if (user == null)
                   {
                       User nConfirmUs = await i.GetUserByEmailAsync(model.Email, false, false);
                       if (nConfirmUs != null)
                       {
                           await i.DeleteUserGuideInUserIdAsync(nConfirmUs.Id);
                           await i.DeleteObjAsync(nConfirmUs.Id);
                           await i.SaveChangesAsync();
                       }

                       PasswdHesh.SetHeshContSalt(newUser, newUser.Password);
                       await i.AddObjAsync(newUser);
                       
                       usg.UserId = newUser.Id;
                       Guid uguid = Guid.NewGuid();
                       guideValue = uguid.ToString();
                       PasswdHesh.SetHeshContSalt(usg, guideValue);
                       await i.AddUserGuideAsync(usg);                      
                                             
                       return true;
                   }
                   else
                   {
                       ModelState.AddModelError("err", "Такой пользователь уже существует!");
                       return false;
                   }
                }, true);

                if (result)
                {
                    var task = MailSender.SendMessage(
                           newUser.Email,
                           "Подтверждение почты для учётной записи.",
                           $"https://{HttpContext.Request.Host.Value.ToString()}/Account/AccountConfirm?id={usg.Id}&guid={guideValue}".TegLinq());

                    return GetMainVue(new MsgVue("В течении 5 минут на указанную почту поступит письмо для подтверждения аккаунта!"));                
                }          
            }

            return GetMainVue(new MsgVue("Что-то не так!", ModelState.Root.Children));
        }

        [HttpGet]
        public async Task<IActionResult> AccountConfirm(int id, string guid)
        {
            var result = await RepositoryBuilder.AuthRepository.ActionQueueAsync(async i =>
            {
                UserGuid ug = await i.GetUserGuideByIdAsync(id);
                if (ug == null || ug.UserId == null)
                    return false;

                User us = await i.GetObjByIdAsync((int)ug.UserId);
                if (us == null)
                    return false;

                if (PasswdHesh.VerifyPasswd(ug, guid))
                {
                    us.Active = true;
                    await Authenticate(us);
                    await i.DeleteUserGuideAsync(id);
                    return true;
                }

                return false;
            }, true);

            if (!result)
                return GetMainVue(new MsgVue("Ссылка недействительна!"));

            return RedirectToAction("Index", "Main");
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