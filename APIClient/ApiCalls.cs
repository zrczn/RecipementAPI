//using Newtonsoft.Json;
//using RecipeApp.ClientConfiguration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RecipeApp
//{
//    public class ApiCalls
//    {
//        private readonly HttpClient _client;

//        public ApiCalls(IClient cli)
//        {
//            _client = cli.HttpClient;
//        }

//        public async Task<(bool, List<Recipe.DataAccess.Models.Recipe>)> GetRecipeHeaders()
//        {
//            List<Recipe.DataAccess.Models.Recipe> recipes = new List<Recipe.DataAccess.Models.Recipe>();
//            bool flag = false;

//            using (_client)
//            {
//                HttpResponseMessage resp = await _client.GetAsync("API/recipe/");

//                if (resp.IsSuccessStatusCode)
//                {
//                    string getContent = await resp.Content.ReadAsStringAsync();

//                    recipes = JsonConvert.DeserializeObject<List<Recipe.DataAccess.Models.Recipe>>(getContent);
//                }
//            }

//            if (recipes != null)
//                flag = true;

//            return (flag, recipes);
//        }

//        public async Task<CompletedRecipe> GetSingleRecipe(int id)
//        {
//            Recipe.DataAccess.Models.Recipe recipe = null;
//            IEnumerable<Enrollment> enrollments = null;
//            bool flag = false;

//            using (_client)
//            {
//                HttpResponseMessage response = await _client.GetAsync($"API/recipe/{id}");

//                if (response.IsSuccessStatusCode)
//                {
//                    string getContent = await response.Content.ReadAsStringAsync();

//                    recipe = JsonConvert.DeserializeObject<Recipe.DataAccess.Models.Recipe>(getContent);
//                    enrollments = recipe.Enrollments.Select(x => x);
//                }
//            }

//            List<Ingredient> inr = new List<Ingredient>();

//            foreach (var enrollment in enrollments)
//            {
//                inr.Add(enrollment.Ingredient);
//            }

//            CompletedRecipe completedRecipe = new()
//            {
//                Name = recipe.RecipeName,
//                Description = recipe.RecipeDescription,
//                Ingredients = inr
//            };

//            return completedRecipe;
//        }

//        public async Task<string> CreateIngredient(Ingredient obj)
//        {
//            HttpResponseMessage response;

//            using (_client)
//            {
//                string intoJSON = JsonConvert.SerializeObject(obj);
//                StringContent content = new StringContent(intoJSON, Encoding.UTF8, "application/json");
//                response = await _client.PostAsync("API/ingredient", content);
//            }

//            if (response.IsSuccessStatusCode)
//            {
//                string responseContent = await response.Content.ReadAsStringAsync();

//                return responseContent.Where(x => char.IsNumber(x)).Aggregate("", (total, y) => $"{total}{y}");
//            }

//            return "err";
//        }


//        //do przerobienia
//        public async Task<bool> CreateRecipe(CompletedRecipe completedRecipe)
//        {
//            List<int> ingredientIds = new List<int>();

//            foreach (var ingredient in completedRecipe.Ingredients)
//            {
//                string responseId = await CreateIngredient(ingredient);
//                ingredientIds.Add(Convert.ToInt32(responseId));
//            }

//            RecipeDTO recipeDTO = new()
//            {
//                RecipeName = completedRecipe.Name,
//                RecipeDescription = completedRecipe.Description,
//                IngredientIds = ingredientIds
//            };

            //var settings = new JsonSerializerSettings
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //};

//            //string intoJSON = JsonConvert.SerializeObject(recObj, settings);

//            //StringContent cntnt = new StringContent(intoJSON, Encoding.UTF8, "application/json");

//            //HttpResponseMessage responseMessage = await _client.PostAsync("API/recipe", cntnt);

//            //if (responseMessage.IsSuccessStatusCode)
//            //    return true;

//            return false;
//        }

//    }
//}
