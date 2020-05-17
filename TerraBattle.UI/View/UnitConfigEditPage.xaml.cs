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

namespace TerraBattle.UI.View
{
    /// <summary>
    /// Interaction logic for UnitConfigEditPage.xaml
    /// </summary>
    public partial class UnitConfigEditPage : Page
    {
        private MainViewModel _viewModel;

        public UnitConfigEditPage(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }
    }
}
