using RecipementAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClient.Services.APICalls
{
    internal interface IInnerCalls
    {
        Task<(int, RecipeDTO)> GetSingleItem(int id);
        Task<(int, string)> DeleteItem(int id);
        Task<(int, string)> ModifyItem(int id, RecipeDTO recipeDTO);

    }
}
