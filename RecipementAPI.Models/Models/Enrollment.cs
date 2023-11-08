using System.ComponentModel.DataAnnotations;

namespace RecipementsAPI.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }


        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public string Quanity { get; set; }
    }
}
