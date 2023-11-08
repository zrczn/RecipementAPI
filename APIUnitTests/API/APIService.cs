using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RecipeApp.ClientConfiguration;
using RecipementAPI.Models.DTO;
using RecipementsAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.API
{
    [TestFixture]
    internal class APIService
    {
        private HttpClient _client;
        private ApplicationContext _dbCon;

        public void SetUp()
        {
            _client = new Client().HttpClient;

            var optBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optBuilder.UseSqlServer("server=(localdb)\\Local;database=RecipementDB;trusted_connection=true;");
            _dbCon = new ApplicationContext(optBuilder.Options);
        }

        [Test]
        public async Task ExistingRecipe()
        {
            RecipeDTO recipeDTO = new RecipeDTO //existingRecipe
            {
                Id = 1,
                Title = "Jajcówa",
                Description = "mniam",
                Ingredients = new List<IngredientDTO>
                    {
                        new IngredientDTO { Title = "jajko" }
                    }
            };

            string intoJSON = JsonConvert.SerializeObject(recipeDTO);
            StringContent cntnt = new StringContent(intoJSON, Encoding.UTF8, "application/json");

            HttpResponseMessage resp = await _client.PostAsync("recipementapi/recipe", cntnt);

            Assert.IsTrue((int)resp.StatusCode == 404);

        }

        [Test]
        public async Task TitleMissing()
        {
            var recipeDTO = new RecipeDTO
            {
                Id = 1,
                Title = "",
                Description = "testcase1",
                Ingredients = new List<IngredientDTO>
                {
                    new IngredientDTO { Title = "testcase1" }
                }
            };

            string intoJSON = JsonConvert.SerializeObject(recipeDTO);
            StringContent cntnt = new StringContent(intoJSON, Encoding.UTF8, "application/json");

            HttpResponseMessage resp = await _client.PostAsync("recipementapi/recipe", cntnt);

            Assert.IsTrue((int)resp.StatusCode == 404);
        }

        [Test]
        public async Task DescriptionMissing()
        {
            var recipeDTO = new RecipeDTO
            {
                Id = 1,
                Title = "testcase2",
                Description = "",
                Ingredients = new List<IngredientDTO>
                {
                    new IngredientDTO { Title = "testcase2" }
                }
            };

            string intoJSON = JsonConvert.SerializeObject(recipeDTO);
            StringContent cntnt = new StringContent(intoJSON, Encoding.UTF8, "application/json");

            HttpResponseMessage resp = await _client.PostAsync("recipementapi/recipe", cntnt);

            Assert.IsTrue((int)resp.StatusCode == 404);
        }

        [Test]
        public async Task IngredientsMissing()
        {
            var recipeDTO = new RecipeDTO
            {
                Id = 1,
                Title = "testcase3",
                Description = "testcase3",
                Ingredients = null
            };

            string intoJSON = JsonConvert.SerializeObject(recipeDTO);
            StringContent cntnt = new StringContent(intoJSON, Encoding.UTF8, "application/json");

            HttpResponseMessage resp = await _client.PostAsync("recipementapi/recipe", cntnt);

            Assert.IsTrue((int)resp.StatusCode == 404);
        }

        [Test]
        public async Task ExistingIngredient()
        {
            var recipeDTO = new RecipeDTO
            {
                Id = 1,
                Title = "testcase4",
                Description = "testcase4",
                Ingredients = new List<IngredientDTO>
                {
                    new IngredientDTO { Title = "jajko" }
                }
            };

            string intoJSON = JsonConvert.SerializeObject(recipeDTO);
            StringContent cntnt = new StringContent(intoJSON, Encoding.UTF8, "application/json");

            HttpResponseMessage resp = await _client.PostAsync("recipementapi/recipe", cntnt);

            Assert.IsTrue((int)resp.StatusCode == 404);

        }

        [TearDown]
        public void TearDown()
        {
            _client = new Client().HttpClient;
        }
    }
}
