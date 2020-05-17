using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using TerraBattle.UI.ViewModel;

namespace TerraBattle.UI.View
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        UnitConfigEditPage _unitConfigEditPage;
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;   // @@ Dont think this is needed.
            _unitConfigEditPage = new UnitConfigEditPage(_viewModel);   // @@ New could be done with AutoFac (?)
            Loaded += MainWindowLoaded;
        }

        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.NavigationService.Navigate(_unitConfigEditPage);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _viewModel.OnClosing(e);
        }
    }
}
