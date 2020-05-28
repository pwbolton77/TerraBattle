using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using TerraBattle.UI.ViewModel;

namespace TerraBattle.UI.View
{
    public partial class MainWindow : Window
    {

        public UnitConfigEditPage UnitConfigEditPage { get; set; }

        public LandingPage LandingPage { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            UnitConfigEditPage = new UnitConfigEditPage(this);
            LandingPage = new LandingPage(this);

            Loaded += MainWindowLoaded;
        }

        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.NavigationService.Navigate(LandingPage);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            // _viewModel.OnClosing(e);
        }
    }
}
