using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Mongo.Models;

namespace WebApplication.Mongo.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly MongoDBContext _context;
        public InvitationRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InvitationDO>> GetUsersInvitationsAsync(string userId)
        {
            return await _context.Invitations
                            .Find(invitation => invitation.UserId == userId)
                            .ToListAsync();
        }
        public async Task<InvitationDO> GetInvitationByIdAsync(string id)
        {
            return await _context.Invitations
                            .Find(invitation => invitation.Id == id)
                            .FirstOrDefaultAsync();
        }

        public async Task AddInvitationAsync(InvitationDO invitation)
        {
            await _context.Invitations.InsertOneAsync(invitation);
        }

        public async Task DeleteInvitationAsync(string id)
        {
            await _context.Invitations.DeleteOneAsync(Builders<InvitationDO>.Filter.Eq("id", id));
        }
    }
}
