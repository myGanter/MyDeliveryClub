using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StajAppCore.Models.Auth
{
    public interface IUser
    {
        string Email { get; set; }
        string Password { get; set; }
        string Salt { get; set; }
    }
}
