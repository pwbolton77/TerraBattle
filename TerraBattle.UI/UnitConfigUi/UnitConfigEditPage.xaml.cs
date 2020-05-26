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
using Autofac;
using Microsoft.Practices.Prism.PubSubEvents;
using TerraBattle.DataAccess;
using TerraBattle.Model;
using TerraBattle.UI.DataProvider;
using TerraBattle.UI.DataProvider.Lookups;
using TerraBattle.UI.View.Services;
using TerraBattle.UI.Startup;

namespace TerraBattle.UI.View
{
    /// <summary>
    /// Interaction logic for UnitConfigEditPage.xaml
    /// </summary>
    public partial class UnitConfigEditPage : Page
    {
        private MainViewModel _viewModel;

        public UnitConfigEditPage()
        {
            InitializeComponent();

            var bootstrapper = new Bootstrapper();
            IContainer container = bootstrapper.Bootstrap();

            _viewModel = container.Resolve<MainViewModel>();
            _viewModel.Load();
            DataContext = _viewModel;
        }
    }
}
