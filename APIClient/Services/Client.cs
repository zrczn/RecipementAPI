using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.ClientConfiguration
{
    public class Client : IClient
    {
        public HttpClient HttpClient { get; }

        public Client()
        {
            HttpClient cli = new HttpClient();

            cli.BaseAddress = new Uri("https://localhost:7174/");
            cli.DefaultRequestHeaders.Accept.Clear();
            cli.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpClient = cli;
        }

    }
}
