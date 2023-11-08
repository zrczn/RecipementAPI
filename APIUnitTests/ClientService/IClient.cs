using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.ClientConfiguration
{
    public interface IClient
    {
        HttpClient HttpClient { get; }
    }
}
