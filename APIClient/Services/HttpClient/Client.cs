using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClient.Services.Client
{
    internal class Client : IClient
    {
        public HttpClient HttpClient { get; }

        public Client()
        {
            HttpClient cli = new HttpClient();

            cli.BaseAddress = new Uri("https://localhost:7073/");
            cli.DefaultRequestHeaders.Accept.Clear();
            cli.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpClient = cli;
        }

    }
}
