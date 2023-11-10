using APIClient.Services.APICalls;
using APIClient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClient.MainApp
{
    internal class App
    {
        private readonly ICombinedView _view;

        public App(ICombinedView view)
        {
            _view = view;
        }

        public void Run()
        {
            _view.RunView(default);
        }
    }
}
