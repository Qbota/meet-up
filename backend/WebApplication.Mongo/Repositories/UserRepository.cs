using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Mongo.Models;

namespace WebApplication.Mongo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDBContext _context;
        public UserRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDO>> GetUsersAsync()
        {
           var users =  await _context.Users.Find(_ => true).ToListAsync();
           DatesToLocalTime(users);
           return users; 
        }

        private void DatesToLocalTime(List<UserDO> users)
        {
            foreach (var user in users)
            {
                user.AvailableDates = user.AvailableDates.Select(x => x.ToLocalTime()).ToList();
            }
        }

        public async Task<IEnumerable<UserDO>> GetUsersByGroupIdAsync(string groupId)
        {
            var users =  await _context.Users
                            .Find(user => user.GroupIDs.Contains(groupId))
                            .ToListAsync();
            DatesToLocalTime(users);
            return users;

        }
        public async Task<UserDO> GetUserByLoginAsync(string login)
        {
            var user =  await _context.Users
                            .Find(user => user.Login == login)
                            .FirstOrDefaultAsync();
            user.AvailableDates = user.AvailableDates.Select(x => x.ToLocalTime()).ToList();
            return user; 
        }

        public async Task<UserDO> GetUserByIdAsync(string id)
        {
            var user =  await _context.Users
                            .Find(user => user.ID == id)
                            .FirstOrDefaultAsync();
            user.AvailableDates = user.AvailableDates.Select(x => x.ToLocalTime()).ToList();
            return user;
        }

        public async Task AddUserAsync(UserDO user)
        {
            user.AvailableDates = user.AvailableDates.Select(x => x.ToUniversalTime()).ToList();
            await _context.Users.InsertOneAsync(user);
        }

        public async Task UpdateUserAsync(UserDO user)
        {
            user.AvailableDates = user.AvailableDates.Select(x => x.ToUniversalTime()).ToList();
            await _context.Users.ReplaceOneAsync(x => x.ID == user.ID, user); 
        }

        public  async Task DeleteUserAsync(string id)
        {
            await _context.Users.DeleteOneAsync(x => x.ID == id);
        }
    }
}
