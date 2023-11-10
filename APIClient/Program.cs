using APIClient.MainApp;
using APIClient.Services.APICalls;
using APIClient.Services.Client;
using APIClient.Views;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace APIClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IClient, Client>();
            serviceCollection.AddSingleton<IGeneralCalls, GeneralCalls>();
            serviceCollection.AddSingleton<IInnerCalls, InnerCalls>();


            serviceCollection.AddSingleton<IInnerView, InnerView>();
            serviceCollection.AddSingleton<IMainView, MainView>();
            serviceCollection.AddSingleton<ICombinedView, CombinedView>();


            serviceCollection.AddSingleton<App>();

            using (var serviceProvider = serviceCollection.BuildServiceProvider())
            {
                var app = serviceProvider.GetService<App>();
                app.Run();
            }
        }
    }
}