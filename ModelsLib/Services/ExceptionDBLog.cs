using System;
using StajAppCore.Models;
using Microsoft.AspNetCore.Mvc.Filters;

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
            error.Name = context.HttpContext?.User.Identity.Name;
            error.Exception = context.Exception.Message;
            error.StackTrace = context.Exception.StackTrace;
            error.Data = DateTime.Now;
            error.Url = context.HttpContext?.Request.Host + context.HttpContext?.Request.Path;

            OnException(error);
        }

        public void OnException(DBEroorModel error)
        {
            AppDB.Errors.Add(error);
            AppDB.SaveChanges();
        }
    }
}
