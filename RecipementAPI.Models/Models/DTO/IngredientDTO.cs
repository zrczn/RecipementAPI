using System.ComponentModel.DataAnnotations;

namespace RecipementAPI.Models.DTO
{
    public class IngredientDTO : ICommonRecipe
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Quanity { get; set; }
    }
}
