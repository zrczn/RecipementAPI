using System.ComponentModel.DataAnnotations;

namespace RecipementsAPI.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
