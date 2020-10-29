using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Mongo.Models;

namespace WebApplication.Mongo.Repositories
{
    public interface IMeetingRepository 
    {
        Task<IEnumerable<MeetingDO>> GetMeetingsAsync();
        Task<MeetingDO> GetMeetingByIdAsync(string id);
        Task AddMeetingAsync(MeetingDO meeting);
        Task UpdateMeetingAsync(MeetingDO meeting);
        Task DeleteMeetingAsync(string id);
    }
}
