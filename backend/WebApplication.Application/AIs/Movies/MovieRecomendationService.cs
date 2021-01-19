using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        private IRPCClient _rpcClient;
        public MovieRecomendationService(IRPCClient client)
        {
            _rpcClient = client;
        }
        public async Task<List<MovieDO>> GetMovieRecomendations(List<MoviePreferenceDO> moviePreferences)
        {
            var request = GenerateMovieRequest(moviePreferences);
            return await GetRecomendationFromAIRabbit(request);
        }

        private async Task<List<MovieDO>> GetRecomendationFromAIRabbit(MovieRequest request)
        {
            JArray array;
            string resultContent = await _rpcClient.SendAsync(JsonSerializer.Serialize(request), "requestqueuemovies");
            array = JArray.Parse(resultContent);
            return GetMovieList(array);
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
            return GetMovieList(array);
        }

        private List<MovieDO> GetMovieList(JArray array)
        {
            var list = new List<MovieDO>();
            if (array != null && array.Any())
            {
                foreach (var element in array)
                {
                    list.Add(new MovieDO
                    {
                        ID = element.Value<string>("id"),
                        Title = element.Value<string>("title") ,
                        Genres = GetGenres(element.Value<string>("genres")),
                        Date = element.Value<string>("releaseDate"),
                        Poster = element.Value<string>("posterPath"),
                        Rating = GetRating(element.Value<string>("voteAverage")),
                        IsBasicSet = false
                    });
                }
            }
            return list;
        }

        private double GetRating(string rating)
        {
            double result = 0;
            if (rating != null && Double.TryParse(rating, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                return result;
            }
            return 0;
        }

        private List<string> GetGenres(string genresArray)
        {
            genresArray = genresArray.Trim(new Char[] { '[', ']', '\'' });
            genresArray = genresArray.Replace("\'",string.Empty);
            genresArray = genresArray.Replace(" ", string.Empty);
            List<string> result = genresArray.Split(new char[] { ',' }).ToList();
            return result;
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
