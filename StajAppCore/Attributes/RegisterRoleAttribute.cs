using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using StajAppCore.Services;

namespace StajAppCore.Attributes
{
    public class RegisterRoleAttribute : ValidationAttribute
    {
        private RoleMaster RM = new RoleMaster(null);

        public override bool IsValid(object value) => RM.ValidationAllowed((int)value);        
    }
}
