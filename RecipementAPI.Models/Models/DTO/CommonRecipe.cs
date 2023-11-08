using System.ComponentModel.DataAnnotations;

namespace RecipementAPI.Models.DTO
{
    public interface ICommonRecipe
    {
        public int Id { get; set; }
        public string Title { get; set; }

    }
}
