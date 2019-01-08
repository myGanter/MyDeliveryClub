using StajAppCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StajAppCore.Controllers
{
    public class BaseController : Controller
    {
        protected const string Admin = "Администратор";
        protected const string Client = "Пользователь";
        protected const string Courier = "Курьер";

        protected JsonResult Data(string msg) => Json(new MsgVue(msg));

        protected JsonResult Data(string msg, IReadOnlyList<ModelStateEntry> info) => Json(new MsgVue(msg, info));
    }
}
