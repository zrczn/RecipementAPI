using System.ComponentModel.DataAnnotations;

namespace RecipementsAPI.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
