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
                            .Find(group => group.ID == id)
                            .FirstOrDefaultAsync();
        }

        public async Task<List<GroupDO>> GetGroupByUserIdAsync(string userId)
        {
            return await _context.Groups
                            .Find(group => group.MemberIDs.Contains(userId))
                            .ToListAsync();
        }

        public async Task AddGroupAsync(GroupDO group)
        {
            await _context.Groups.InsertOneAsync(group);
        }

        public async Task UpdateGroupAsync(GroupDO group)
        {
            await _context.Groups.ReplaceOneAsync(x => x.ID == group.ID, group);
        }

        public async Task DeleteGroupAsync(string id)
        {
            await _context.Groups.DeleteOneAsync(x => x.ID == id);
        }
    }
}
