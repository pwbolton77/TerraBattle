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
        LandingPage _landingPage;

        public MainWindow()
        {
            InitializeComponent();

            _unitConfigEditPage = new UnitConfigEditPage();
            _landingPage = new LandingPage(_unitConfigEditPage);

            Loaded += MainWindowLoaded;
        }

        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.NavigationService.Navigate(_landingPage);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            // _viewModel.OnClosing(e);
        }
    }
}
