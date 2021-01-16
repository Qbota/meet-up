using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication.Application.AIs.Food;
using WebApplication.Application.Meals.Models;
using WebApplication.Mongo.Models;

namespace WebApplication.Application.AIs
{
    public class FoodRecomendationService : IFoodRecomendationService
    {

        private const string _uri = "http://localhost:5002";
        private IRPCClient _rpcClient;
        public FoodRecomendationService(IRPCClient client)
        {
            _rpcClient = client;
        }
        public async Task<List<MealDO>> GetMealRecomendations(List<MealPreferenceDO> mealPreferences)
        {
            var request = GenerateFoodRequest(mealPreferences);
            return  await GetRecomendationFromAIRabbit(request);
        }

        private async Task<List<MealDO>> GetRecomendationFromAIAsync(FoodRequest request)
        {
            JArray array;
            using (var client = new HttpClient())
            {
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
            return GetMealList(array);
        }
        private async Task<List<MealDO>> GetRecomendationFromAIRabbit(FoodRequest request)
        {
            JArray array;
            string resultContent = await _rpcClient.SendAsync(JsonSerializer.Serialize(request));
            array = JArray.Parse(resultContent);
            return GetMealList(array);
        }
        private List<MealDO> GetMealList(JArray array)
        {
            var list = new List<MealDO>();
            if (array != null && array.Any())
            {
                foreach (var element in array)
                {
                    list.Add(new MealDO
                    {
                        ID = element.Value<string>("idMeal"),
                        Name = element.Value<string>("strMeal"),
                        Cuisine = element.Value<string>("strArea")
                    });
                }
            }
            return list;
        }

        private FoodRequest GenerateFoodRequest(List<MealPreferenceDO> mealPreferences)
        {
            var allergens = new List<string>();
            var cousines = new Dictionary<string, int>();
            foreach (var preference in mealPreferences)
            {
                allergens.AddRange(preference.Allergens);
                AddCouisnes(ref cousines, preference.Cousines.ToList());
            }
            return new FoodRequest
            {
                Allergens = allergens.Select(x => x).Distinct().ToList(),
                Cusines = cousines,
                MealsAmount = 10
            };
        }
        private FoodRequest GetMockRequest()
        {
            var allergens = new List<string>();
            allergens.Add("dairy");
            allergens.Add("eggs");
            var cousines = new Dictionary<string, int>();
            cousines.Add("british", 2);
            cousines.Add("polish", 2);
            cousines.Add("chinese", 2);
            return new FoodRequest
            {
                Allergens = allergens,
                Cusines = cousines,
                MealsAmount = 10
            };
        }

        private void AddCouisnes(ref Dictionary<string, int> cousines, List<string> userCousines)
        {
            foreach (var item in userCousines)
            {
                if (cousines.ContainsKey(item))
                {
                    cousines[item]++;
                }
                else cousines.Add(item, 1);
            }
        }
    }
}
