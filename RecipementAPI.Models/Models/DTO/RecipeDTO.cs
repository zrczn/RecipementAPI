using RecipementsAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace RecipementAPI.Models.DTO
{
    public class RecipeDTO : ICommonRecipe
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public ICollection<IngredientDTO> Ingredients { get; set; }
    }
}
