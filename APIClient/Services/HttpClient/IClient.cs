using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClient.Services.Client
{
    internal interface IClient
    {
        HttpClient HttpClient { get; }
    }
}
