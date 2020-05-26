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
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);
      MainWindow = new MainWindow();
      MainWindow.Show();
    }
  }
}
