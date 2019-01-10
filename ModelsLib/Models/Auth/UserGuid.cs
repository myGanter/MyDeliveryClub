using System;
using System.Collections.Generic;
using System.Text;

namespace StajAppCore.Models.Auth
{
    public class UserGuid : IHesh
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
