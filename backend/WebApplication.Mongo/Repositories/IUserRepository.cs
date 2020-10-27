using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Mongo.Services
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(string id);
        Task AddUserAsync(User user);
    }
}
