using RecipementAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClient.Services.APICalls
{
    internal interface IGeneralCalls
    {
        Task<(int, IEnumerable<RecipeHeader>)> GetHeaders();
        Task<(int, string)> Create(RecipeDTO recipeDTO);

    }
}
