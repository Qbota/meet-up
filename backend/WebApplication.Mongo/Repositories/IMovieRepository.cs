using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Mongo.Models;

namespace WebApplication.Mongo.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<MovieDO>> GetMoviesAsync();
        Task<IEnumerable<MovieDO>> GetMoviesBasicSetAsync();
        Task<MovieDO> GetMovieByIdAsync(string id);
        Task AddMovieAsync(MovieDO movie);
        Task DeleteMovieAsync(string id);
    }
}
