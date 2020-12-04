using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Mongo.Models;

namespace WebApplication.Application.AIs
{
    public interface IMovieRecomendationService
    {
        Task<List<MovieDO>> GetMovieRecomendations(List<MoviePreferenceDO> moviePreferences);
    }
}
