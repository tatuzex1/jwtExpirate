using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JwtApiTest.Domain.Models
{
    public class User
    {

        private readonly IList<string> _roles = new List<string>();
        private readonly IList<string> _permissions = new List<string>();

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles => _roles;
        public IEnumerable<string> Permissions => _permissions;

        [JsonIgnore]
        public Password Password { get; private set; }

        protected User() { }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = new Password(password);
        }

        //Todo Fluent Validation after...
        public void ChangePassword(string newPassword, string newPasswordConfirmation)
        {
            if (newPassword != newPasswordConfirmation) return;
            Password = new Password(newPassword);
        }

        public void AddRole(string role)
        {
            _roles.Add(role);
        }

        public void AddPermission(string permission)
        {
            _permissions.Add(permission);
        }


    }
}
