using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Mongo.Models;

namespace WebApplication.Mongo.Repositories
{
    public interface IGroupRepository
    {
        Task<IEnumerable<GroupDO>> GetGroupsAsync();
        Task<GroupDO> GetGroupByIdAsync(string id);
        Task<List<GroupDO>> GetGroupByUserIdAsync(string userId);
        Task AddGroupAsync(GroupDO group);
        Task UpdateGroupAsync(GroupDO group);
        Task DeleteGroupAsync(string id);
    }
}
