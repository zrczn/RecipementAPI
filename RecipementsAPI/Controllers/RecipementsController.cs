using Microsoft.AspNetCore.Mvc;
using RecipementsAPI.Models;

namespace RecipementsAPI.Controllers
{
    [Route("recipementapi")]
    [ApiController]
    public class RecipementsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Ingredient> GetIngredient()
        {
            

            return Ok(new Ingredient() {Id = 0, Name = "sksk" });
        }
    }
}
