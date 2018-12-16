using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StajAppCore.Models
{
    public class MsgVue
    {
        public MsgVue()
        {
            Information = new List<string>();
        }

        public MsgVue(string message) : this()
        {
            Message = message;
        }

        public MsgVue(string message, IReadOnlyList<ModelStateEntry> info) : this(message)
        {
            foreach(var i in info)
            {
                if (i.ValidationState == ModelValidationState.Invalid)
                {   
                    foreach (var er in i.Errors)
                        Information.Add(er.ErrorMessage);
                }
            }
        }

        public string Message { get; set; }

        public List<string> Information { get; set; }
    }
}
