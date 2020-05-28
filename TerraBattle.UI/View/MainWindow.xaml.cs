using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using TerraBattle.UI.View.Services;
using TerraBattle.UI.ViewModel;

namespace TerraBattle.UI.View
{
    public partial class MainWindow : Window
    {

        private MainWindowPageManager _mainWindowPageManager;

        public MainWindow()
        {
            InitializeComponent();
            _mainWindowPageManager = new MainWindowPageManager(MainWindowFrame.NavigationService);

            Loaded += MainWindowLoaded;
        }

        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            _mainWindowPageManager.RequestLandingPage();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            // _viewModel.OnClosing(e);
        }
    }
}
