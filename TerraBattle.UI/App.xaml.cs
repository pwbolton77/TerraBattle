using System.Windows;
using Autofac;
using TerraBattle.UI.Startup;
using TerraBattle.UI.View;
using TerraBattle.UI.ViewModel;
using TerraBattle.UI.View.Services;

namespace TerraBattle.UI
{
  public partial class App : Application
  {
    private MainViewModel _mainViewModel;

    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);
      var bootstrapper = new Bootstrapper();
      IContainer container = bootstrapper.Bootstrap();

      _mainViewModel = container.Resolve<MainViewModel>();
      MainWindow = new MainWindow(_mainViewModel);
      MainWindow.Show();
      _mainViewModel.Load();
    }
  }
}
