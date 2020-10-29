using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Mongo.Models;

namespace WebApplication.Mongo.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDO>> GetUsersAsync();
        Task<UserDO> GetUserByIdAsync(string id);
        Task AddUserAsync(UserDO user);
        Task UpdateUserAsync(UserDO user);
        Task DeleteUserAsync(string id);
    }
}
