using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RecipementAPI.Models.DTO;
using RecipementsAPI.Database;
using RecipementsAPI.Models;
using System.Net.Mail;

namespace RecipementsAPI.Controllers
{
    [Route("recipementapi")]
    [ApiController]
    public class RecipementsController : ControllerBase
    {
        private readonly ApplicationContext _dbCon;

        public RecipementsController(ApplicationContext dbcon)
        {
            _dbCon = dbcon;
        }


        [HttpGet("recipe")]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<RecipeHeader>>> GetHeaders()
        {
            var headers = await _dbCon.Recipes.Select(x => new RecipeHeader()
            {
                Id = x.Id,
                Title = x.Name
            
            }).ToListAsync();

            if (headers.Count == 0)
                return NoContent();

            return Ok(headers);
        }

        [HttpGet("reicpe/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<RecipeDTO>> GetSingleRecipe(int id)
        {
            Recipe singleRecipe = await _dbCon.Recipes.Include(x => x.Enrollments)
                .ThenInclude(y => y.Ingredient).FirstOrDefaultAsync(x => x.Id == id);

            if (singleRecipe == null)
                return NotFound("There's no such recipement");

            var getCorrespondentIngredients = singleRecipe.Enrollments.Where(x => x.RecipeId == id)
                .Select(x => new IngredientDTO()
                {
                    Id = x.Ingredient.Id,
                    Title = x.Ingredient.Name,
                    Quanity = x.Quanity
                }).ToList();

            RecipeDTO recipeDTO = new RecipeDTO()
            {
                Id = singleRecipe.Id,
                Description = singleRecipe.Description,
                Title = singleRecipe.Name,
                Ingredients = getCorrespondentIngredients
            };

            return Ok(recipeDTO);
        }

        [HttpPost("recipe")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeDTO DtoObj)
        {
            var getExistingRecipe = await _dbCon.Recipes.FirstOrDefaultAsync(x => x.Name.ToLower() == DtoObj.Title.ToLower());
            if (getExistingRecipe != null)
            {
                ModelState.AddModelError("RecipeError", "Such recipe already exists");
                return BadRequest(ModelState);
            }

            Recipe newRecipe = new Recipe() { Name = DtoObj.Title, Description = DtoObj.Description };

            await _dbCon.Recipes.AddAsync(newRecipe);
            await _dbCon.SaveChangesAsync();

            foreach (var ingredient in DtoObj.Ingredients)
            {
                Ingredient addIngredient;

                var getExistingIngredient = await _dbCon.Ingredients.FirstOrDefaultAsync(x => x.Name.ToLower() == ingredient.Title.ToLower());

                if (getExistingIngredient == null)
                {
                    addIngredient = 
                        new Ingredient()
                        {
                            Name = ingredient.Title
                        };

                    await _dbCon.Ingredients.AddAsync(addIngredient);
                    await _dbCon.SaveChangesAsync();
                }
                else
                {
                    addIngredient = getExistingIngredient;
                }

                await _dbCon.Enrollments.AddAsync(new Enrollment()
                { RecipeId = newRecipe.Id, IngredientId = addIngredient.Id, Quanity = ingredient.Quanity });

            }

            await _dbCon.SaveChangesAsync();

            return Ok();

        }

        [HttpDelete("recipe/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var getRecipe = await _dbCon.Recipes.FirstOrDefaultAsync(x => x.Id == id);

            if (getRecipe != null)
            {
                var RecipeIncluded = _dbCon.Recipes.Include(x => x.Enrollments).ThenInclude(y => y.Ingredient).FirstOrDefault(x => x.Id == id);

                foreach (var item in RecipeIncluded.Enrollments)
                {
                    _dbCon.Ingredients.Remove(item.Ingredient);
                }

                _dbCon.Recipes.Remove(RecipeIncluded);

                await _dbCon.SaveChangesAsync();
                return Ok();
            }

            ModelState.AddModelError("InsertError", "Theres no such recipe");
            return BadRequest(ModelState);
        }

        [HttpPut("recipe/{id:int}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateRecipe(int id, [FromBody] RecipeDTO DtoObj)
        {
            var getRecipe = await _dbCon.Recipes.FirstOrDefaultAsync(x => x.Id == id);

            if (getRecipe == null)
            {
                ModelState.AddModelError("GetError", "Theres no such recipe");
                return BadRequest(ModelState);
            }

            getRecipe.Name = DtoObj.Title;
            getRecipe.Description = DtoObj.Description;

            foreach (var ingredient in DtoObj.Ingredients)
            {
                var getIngredient = await _dbCon.Ingredients.FirstOrDefaultAsync(x => x.Id == ingredient.Id);

                if(getIngredient != null)
                {
                    getIngredient.Name = ingredient.Title;
                }
                else
                {
                    var newIngredient = new Ingredient() { Name = ingredient.Title };

                    _dbCon.Add<Ingredient>(newIngredient);

                    var newEnrollment = new Enrollment() { RecipeId = id, IngredientId = newIngredient.Id, Quanity = ingredient.Quanity };

                    await _dbCon.SaveChangesAsync();
                }
            }

            await _dbCon.SaveChangesAsync();
            return Ok();
        }
    }
}
