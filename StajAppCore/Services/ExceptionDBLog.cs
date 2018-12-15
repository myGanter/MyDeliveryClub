﻿using Microsoft.AspNetCore.Mvc.Filters;
using StajAppCore.Models;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StajAppCore.Services
{
    public class ExceptionDBLog : IExceptionFilter
    {
        private ApplicationContext AppDB;

        public ExceptionDBLog(ApplicationContext db)
        {
            AppDB = db;
        }

        public void OnException(ExceptionContext context)
        {
            DBEroorModel error = new DBEroorModel();
            error.Name = context.HttpContext.User.Identity.Name;
            error.Exception = context.Exception.Message;
            error.StackTrace = context.Exception.StackTrace;
            error.Data = DateTime.Now;
            error.Url = context.HttpContext.Request.Host + context.HttpContext.Request.Path;
            AppDB.Errors.Add(error);
            AppDB.SaveChanges();
        }
    }
}
