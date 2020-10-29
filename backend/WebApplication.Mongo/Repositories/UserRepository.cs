using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
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
           return await _context.Users.Find(_ => true).ToListAsync();
        }

        public async Task<UserDO> GetUserByIdAsync(string id)
        {
            return await _context.Users
                            .Find(user => user.ID == id)
                            .FirstOrDefaultAsync();
        }

        public async Task AddUserAsync(UserDO user)
        {
            await _context.Users.InsertOneAsync(user);
        }

        public async Task UpdateUserAsync(UserDO user)
        {
            await _context.Users.ReplaceOneAsync(Builders<UserDO>.Filter.Eq("_id", user.ID), user);
        }

        public  async Task DeleteUserAsync(string id)
        {
            var objectId = new ObjectId(id);
            await _context.Users.DeleteOneAsync(Builders<UserDO>.Filter.Eq("_id", objectId));
        }
    }
}
