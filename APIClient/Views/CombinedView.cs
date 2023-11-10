using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIClient.Views
{
    internal class CombinedView : ICombinedView
    {
        private readonly IMainView _mainView;
        private readonly IInnerView _innerView;

        public CombinedView(IMainView MainView, IInnerView InnerView)
        {
            _mainView = MainView;
            _innerView = InnerView;
        }

        public void RunView(int id)
        {
            do
            {
                int extractVal = _mainView.RunView();
                _innerView.RunView(extractVal);

            } while (true);
        }
    }
}
