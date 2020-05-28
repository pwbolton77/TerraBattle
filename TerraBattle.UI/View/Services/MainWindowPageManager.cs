using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraBattle.UI.View.Services
{
    public class MainWindowPageManager : IPageManageMainWindow
    {
        System.Windows.Navigation.NavigationService _mainWindowNavigationService;

        public MainWindowPageManager(System.Windows.Navigation.NavigationService mainWindowNavigationService)
        {
            _mainWindowNavigationService = mainWindowNavigationService;
        }

        public void RequestLandingPage()
        {
            _mainWindowNavigationService.Navigate(new LandingPage (this));
        }

        public void RequestUnitConfigEditPage()
        {
            _mainWindowNavigationService.Navigate(new UnitConfigEditPage (this));
        }
    }
}
