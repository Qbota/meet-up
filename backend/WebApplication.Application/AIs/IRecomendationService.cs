using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Mongo.Models;

namespace WebApplication.Application.AIs
{
    public interface IRecomendationService
    {
        Task<MeetingDO> GetRecomendations(MeetingDO meeting);
    }
}
