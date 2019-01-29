using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace StajAppCore.Models
{
    public class MsgVue
    {
        public MsgVue()
        {
            Information = new List<string>();
            Errors = null;
        }

        public MsgVue(string message) : this()
        {
            Message = message;
        }

        public MsgVue(string message, IReadOnlyList<ModelStateEntry> info) : this(message)
        {
            InitInformation(info);
        }

        public MsgVue(string message, ValueEnumerable errors) : this(message)
        {
            Errors = errors.ToDictionary(
                i => (string)i.GetType().GetProperty("Key").GetValue(i), 
                i => i.Errors.Select(
                    j => j.ErrorMessage).ToList());
        }

        private void InitInformation(IReadOnlyList<ModelStateEntry> info)
        {
            foreach (var i in info)
            {
                if (i.Children != null)
                    InitInformation(i.Children);

                if (i.ValidationState == ModelValidationState.Invalid)
                {
                    foreach (var er in i.Errors)
                        Information.Add(er.ErrorMessage);
                }
            }
        }

        public string Message { get; set; }

        public List<string> Information { get; set; }

        public Dictionary<string, List<string>> Errors { get; set; }
    }
}
