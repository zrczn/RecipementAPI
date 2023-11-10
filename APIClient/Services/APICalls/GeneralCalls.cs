using APIClient.Services.Client;
using RecipementAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace APIClient.Services.APICalls
{
    internal class GeneralCalls : IGeneralCalls
    {
        private readonly HttpClient _cli;

        public GeneralCalls(IClient cli)
        {
            _cli = cli.HttpClient;
        }

        public async Task<(int, string)> Create(RecipeDTO recipeDTO)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string intoJSON = JsonConvert.SerializeObject(recipeDTO, settings);
            StringContent content = new StringContent(intoJSON, Encoding.UTF8, "application/json");
            HttpResponseMessage respMessage = await _cli.PostAsync("recipementapi/recipe", content);

            return ((int)respMessage.StatusCode, respMessage.Content.ReadAsStringAsync().Result);
        }

        public async Task<(int, IEnumerable<RecipeHeader>)> GetHeaders()
        {
            IEnumerable<RecipeHeader> recipeHeaders = new List<RecipeHeader>();
            int responseCode;

            HttpResponseMessage response = await _cli.GetAsync("recipementapi/recipe");

            if (response.IsSuccessStatusCode)
            {
                string getContent = await response.Content.ReadAsStringAsync();
                recipeHeaders = JsonConvert.DeserializeObject<IEnumerable<RecipeHeader>>(getContent);
            }

            responseCode = (int)response.StatusCode;


            return (responseCode, recipeHeaders);
        }
    }
}
