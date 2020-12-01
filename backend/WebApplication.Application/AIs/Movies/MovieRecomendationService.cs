using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication.Application.AIs.Movies;
using WebApplication.Mongo.Models;

namespace WebApplication.Application.AIs
{
    public class MovieRecomendationService : IMovieRecomendationService
    {
        private const string _uri = "http://localhost:5004";
        public async Task<IEnumerable<MovieDO>> GetMovieRecomendations(List<MoviePreferenceDO> moviePreferences)
        {
            //var request = GenerateMovieRequest(moviePreferences);
            var request = GetMockRequest();
            return await GetRecomendationFromAIAsync(request);
        }

        private MovieRequest GetMockRequest()
        {
            var genres = new Dictionary<string, int>();
            genres.Add("Comedy", 2);
            genres.Add("Adventure", 1);
            var ratings = new List<Dictionary<string, double>>();
            var dict = new Dictionary<string, double>();
            dict.Add("3052", 4.5);
            dict.Add("365", 5);
            ratings.Add(dict);
            return new MovieRequest
            {
                Ratings = ratings,
                Genres = genres
            };
        }

        private async Task<List<MovieDO>> GetRecomendationFromAIAsync(MovieRequest request)
        {
            JArray array;
            using (var client = new HttpClient())
            {
                var a = JsonSerializer.Serialize(request);
                var httpRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(_uri + "/recomendation"),
                    Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
                };
                var result = await client.SendAsync(httpRequest);
                string resultContent = await result.Content.ReadAsStringAsync();
                array = JArray.Parse(resultContent);
            }
            throw new NotImplementedException();
        }
        private MovieRequest GenerateMovieRequest(List<MoviePreferenceDO> moviePreferences)
        {
            var ratings = new List<Dictionary<string, double>>();
            var genres = new Dictionary<string, int>();
            foreach (var preference in moviePreferences)
            {
                ratings.Add(preference.Ratings);
                AddGenres(ref genres, preference.MovieGenres.ToList());
            }
            return new MovieRequest
            { 
                Ratings = ratings,
                Genres = genres
            };
        }
        private void AddGenres(ref Dictionary<string, int> genres, List<string> userGenres)
        {
            foreach (var item in userGenres)
            {
                if (genres.ContainsKey(item))
                {
                    genres[item]++;
                }
                else genres.Add(item, 1);
            }
        }
    }
}
