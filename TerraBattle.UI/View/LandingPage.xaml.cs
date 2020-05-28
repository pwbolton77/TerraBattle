using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TerraBattle.UI.ViewModel;
using TerraBattle.UI.View.Services;

namespace TerraBattle.UI.View
{
    /// <summary>
    /// Interaction logic for LandingPage.xaml
    /// </summary>
    public partial class LandingPage : Page
    {
        private IPageManageMainWindow _mainWindowPageManager;

        public LandingPage(IPageManageMainWindow mainWindowPageManager)
        {
            InitializeComponent();
            _mainWindowPageManager = mainWindowPageManager;
        }

        private void UnitConfigurationMenuButton(object sender, RoutedEventArgs e)
        {
            _mainWindowPageManager.RequestUnitConfigEditPage();
        }
    }
}
