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
            return await _context.Meetings.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<MeetingDO>> GetMeetingsByGroupIdAsync(string groupId)
        {
            return await _context.Meetings
                            .Find(meeting => meeting.GroupID == groupId)
                            .ToListAsync();
        }

        public async Task<MeetingDO> GetMeetingByIdAsync(string id)
        {
            return await _context.Meetings
                            .Find(meeting => meeting.ID == id)
                            .FirstOrDefaultAsync();
        }

        public async Task AddMeetingAsync(MeetingDO meeting)
        {
            await _context.Meetings.InsertOneAsync(meeting);
        }

        public async Task UpdateMeetingAsync(MeetingDO meeting)
        {
            await _context.Meetings.ReplaceOneAsync(x => x.ID == meeting.ID, meeting);
        }

        public async Task DeleteMeetingAsync(string id)
        {
            await _context.Meetings.DeleteOneAsync(Builders<MeetingDO>.Filter.Eq("id", id));
        }
    }
}
