using System;
using System.Linq;
using StajAppCore.Models;
using StajAppCore.Models.Auth;
using System.Collections.Generic;

namespace StajAppCore.Services
{
    public class RoleMaster
    {
        private static List<Role> AllRoles = new List<Role>();
        private static List<string> RolesException = new List<string>();
        private static Dictionary<string, Tuple<string, string, IEnumerable<MenuItem>>> RoleForView = new Dictionary<string, Tuple<string, string, IEnumerable<MenuItem>>>();

        public RoleMaster()
        { }

        public Tuple<string, string, IEnumerable<MenuItem>> GetViewRole(string name) => RoleForView.FirstOrDefault(i => i.Key == name).Value;

        public bool ValidationAllowed(string name)
        {
            if (AllRoles.FirstOrDefault(i => i.Name == name) == null || RolesException.FirstOrDefault(i => i == name) != null)
                return false;

            return true;
        }

        public bool ValidationAllowed(int id)
        {
            Role role = AllRoles.FirstOrDefault(i => i.Id == id);
            if (role == null || RolesException.FirstOrDefault(i => i == role.Name) != null)
                return false;

            return true;
        }

        public Role GetRole(int id) => AllRoles.FirstOrDefault(i => i.Id == id);

        public Role[] GetRoles() => AllRoles.ToArray();

        public static void AddRoles(params Role[] roles) => AllRoles.AddRange(roles);

        public static void AddRoleException(string roleName) => RolesException.Add(roleName);

        public static void AddRoleForView(string role, string controller, string view, IEnumerable<MenuItem> meny) => RoleForView.Add(role, new Tuple<string, string, IEnumerable<MenuItem>>(view, controller, meny));
    }
}
