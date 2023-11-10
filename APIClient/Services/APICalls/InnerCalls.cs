using APIClient.Services.Client;
using Newtonsoft.Json;
using RecipementAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClient.Services.APICalls
{
    internal class InnerCalls : IInnerCalls
    {
        private readonly HttpClient _cli;

        public InnerCalls(IClient client)
        {
            _cli = client.HttpClient;
        }

        public async Task<(int, string)> DeleteItem(int id)
        {
            HttpResponseMessage response = await _cli.DeleteAsync($"recipementapi/recipe/{id}");

            return ((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        public async Task<(int, RecipeDTO)> GetSingleItem(int id)
        {
            RecipeDTO recipeDTO;

            HttpResponseMessage response = await _cli.GetAsync($"recipementapi/recipe/{id}");

            if (response.IsSuccessStatusCode)
            {
                string getContent = await response.Content.ReadAsStringAsync();
                recipeDTO = JsonConvert.DeserializeObject<RecipeDTO>(getContent);
            }
            else
            {
                recipeDTO = new RecipeDTO { Title = await response.Content.ReadAsStringAsync() };
            }

            return ((int)response.StatusCode, recipeDTO);

        }

        public async Task<(int, string)> ModifyItem(int id, RecipeDTO recipeDTO)
        {
            string intoJSON = JsonConvert.SerializeObject(recipeDTO);
            StringContent cntnt = new StringContent(intoJSON, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _cli.PutAsync($"recipementapi/recipe/{id}", cntnt);

            return ((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
