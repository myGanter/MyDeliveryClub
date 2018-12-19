using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StajAppCore.Models.Store;
using StajAppCore.Models.Auth.AuthView;

namespace StajAppCore.Models.Auth
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set;}
        public string Phone { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }

        public List<Order> Orders { get; set; }

        public List<Order> CourierOrders { get; set; }

        public User()
        {
            Orders = new List<Order>();
            CourierOrders = new List<Order>();
        }

        public static explicit operator UserModel(User us) =>
            new UserModel()
            {
                Name = us.Name,
                Email = us.Email,
                Phone = us.Phone
            };
    }
}
