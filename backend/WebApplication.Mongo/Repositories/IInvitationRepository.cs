using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Mongo.Models;

namespace WebApplication.Mongo.Repositories
{
    public interface IInvitationRepository
    {
        Task<IEnumerable<InvitationDO>> GetUsersInvitationsAsync(string userId);
        Task AddInvitationAsync(InvitationDO invitation);
        Task DeleteInvitationAsync(string id);
        Task<InvitationDO> GetInvitationByIdAsync(string id);
    }
}
