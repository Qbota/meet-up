using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Mongo.Models;

namespace WebApplication.Mongo.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MongoDBContext _context;
        public MovieRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task AddMovieAsync(MovieDO movie)
        {
            await _context.Movies.InsertOneAsync(movie);
        }

        public async Task DeleteMovieAsync(string id)
        {
            await _context.Movies.DeleteOneAsync(Builders<MovieDO>.Filter.Eq("id", id));
        }

        public async Task<MovieDO> GetMovieByIdAsync(string id)
        {
            return await _context.Movies
                             .Find(movie => movie.ID == id)
                             .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MovieDO>> GetMoviesAsync()
        {
            return await _context.Movies.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<MovieDO>> GetMoviesBasicSetAsync()
        {
            return await _context.Movies
                             .Find(movie => movie.IsBasicSet == true)
                             .ToListAsync();
        }
    }
}
