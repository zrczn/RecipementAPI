using System.ComponentModel.DataAnnotations;

namespace RecipementsAPI.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }


        public int RecipementId { get; set; }
        public Recipe Recipe { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public int Quanity { get; set; }
    }
}
