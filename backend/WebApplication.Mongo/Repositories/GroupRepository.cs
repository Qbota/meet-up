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
    public class GroupRepository : IGroupRepository
    {
        private readonly MongoDBContext _context;
        public GroupRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GroupDO>> GetGroupsAsync()
        {
            return await _context.Groups.Find(_ => true).ToListAsync();
        }

        public async Task<GroupDO> GetGroupByIdAsync(string id)
        {
            return await _context.Groups
                            .Find(user => user.ID == id)
                            .FirstOrDefaultAsync();
        }

        public async Task AddGroupAsync(GroupDO group)
        {
            await _context.Groups.InsertOneAsync(group);
        }

        public async Task UpdateGroupAsync(GroupDO group)
        {
            await _context.Groups.ReplaceOneAsync(Builders<GroupDO>.Filter.Eq("_id", group.ID), group);
        }

        public async Task DeleteGroupAsync(string id)
        {
            var objectId = new ObjectId(id);
            await _context.Groups.DeleteOneAsync(Builders<GroupDO>.Filter.Eq("_id", objectId));
        }
    }
}
