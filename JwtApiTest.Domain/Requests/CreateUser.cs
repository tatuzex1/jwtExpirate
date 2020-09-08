using System;
using System.Collections.Generic;
using System.Text;

namespace JwtApiTest.Domain.Requests
{
    public class CreateUser
    {
        private readonly List<string> _roles = new List<string>();
        private readonly List<string> _permissions = new List<string>();

        public CreateUser(string name, string email, string password, IEnumerable<string> roles, IEnumerable<string> permissions)
        {
            Name = name;
            Email = email;
            Password = password;


            if (roles != null)
            {
                _roles.AddRange(roles);
            }

            if (permissions != null)
            {
                _permissions.AddRange(permissions);
            }


        }

        public string Name { get; }
        public string Email { get; }
        public string Password { get; }
        public IEnumerable<string> Roles => _roles;
        public IEnumerable<string> Permissions => _permissions;
    }
}
