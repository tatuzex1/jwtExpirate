using JwtApiTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JwtApiTest.Application.Interfaces.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> Get(Guid id);
        Task<User> Get(string email);
        Task<User> Authenticate(string email, string password);
        Task<bool> ExistsUser(string email);
        Task Save(User user);
        Task Remove(User user);
    }
}
