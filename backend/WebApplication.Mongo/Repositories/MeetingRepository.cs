using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Mongo.Models;

namespace WebApplication.Mongo.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly MongoDBContext _context;
        public MeetingRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MeetingDO>> GetMeetingsAsync()
        {
            var meetings =  await _context.Meetings.Find(_ => true).ToListAsync();
            DatesToLocalTime(meetings);
            return meetings;
        }
        private void DatesToLocalTime(List<MeetingDO> meetings)
        {
            foreach (var meeting in meetings)
            {
                meeting.DateProposition = meeting.DateProposition.ToLocalTime();
            }
        }
        public async Task<IEnumerable<MeetingDO>> GetMeetingsByGroupIdAsync(string groupId)
        {
            var meetings =  await _context.Meetings
                            .Find(meeting => meeting.GroupID == groupId)
                            .ToListAsync();
            DatesToLocalTime(meetings);
            return meetings;
        }

        public async Task<MeetingDO> GetMeetingByIdAsync(string id)
        {
           var meeting =  await _context.Meetings
                            .Find(meeting => meeting.ID == id)
                            .FirstOrDefaultAsync();
            meeting.DateProposition = meeting.DateProposition.ToLocalTime();
            return meeting;
        }

        public async Task AddMeetingAsync(MeetingDO meeting)
        {
            meeting.DateProposition = meeting.DateProposition.ToUniversalTime();
            await _context.Meetings.InsertOneAsync(meeting);
        }

        public async Task UpdateMeetingAsync(MeetingDO meeting)
        {
            meeting.DateProposition = meeting.DateProposition.ToUniversalTime();
            await _context.Meetings.ReplaceOneAsync(x => x.ID == meeting.ID, meeting);
        }

        public async Task DeleteMeetingAsync(string id)
        {
            await _context.Meetings.DeleteOneAsync(x => x.ID == id);
        }
    }
}
