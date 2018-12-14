using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StajAppCore.Models;
using StajAppCore.Models.Auth;
using StajAppCore.Services;
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

        public async Task<IActionResult> Index(bool EROOR = false)
        {
            ViewData["ERROR"] = EROOR;

            User us = await RepositoryBuilder.AuthRepository.GetUserByEmailAsync(User.Identity.Name, true);
            if (us != null)
            {
                ViewData["UserName"] = us.Name == null ? us.Email : us.Name;
                string roleName = us.Role.Name;
                return View(RoleM.GetViewRole(roleName).Item1, RoleM.GetViewRole(roleName).Item2);
            }

            ViewData["Roles"] = RoleM.GetRoles().Where(i => RoleM.ValidationAllowed(i.Id));
            return View(RoleM.GetViewRole("Пользователь").Item1, RoleM.GetViewRole("Пользователь").Item2);
        }
    }
}