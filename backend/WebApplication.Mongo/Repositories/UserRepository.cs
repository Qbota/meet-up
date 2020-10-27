using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Mongo.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDBContext _context;
        public UserRepository(MongoDBContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
           return await _context.Users.Find(_ => true).ToListAsync();
        }
        public async Task<User> GetUserAsync(string id)
        {
            return await _context.Users
                            .Find(user => user.ID == id)
                            .FirstOrDefaultAsync();
        }
        public async Task AddUserAsync(User user)
        {
            await _context.Users.InsertOneAsync(user);
        }
        //add lock


    }
}
