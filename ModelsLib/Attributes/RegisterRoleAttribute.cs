using StajAppCore.Services;
using System.ComponentModel.DataAnnotations;

namespace StajAppCore.Attributes
{
    public class RegisterRoleAttribute : ValidationAttribute
    {
        private RoleMaster RM = new RoleMaster();

        public override bool IsValid(object value) => RM.ValidationAllowed((int)value);        
    }
}
