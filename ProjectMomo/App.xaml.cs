using System.Windows;

namespace ProjectMomo
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    ProjectMomo _mainApplication;

    private void Application_Startup(object sender, StartupEventArgs e)
    {
      _mainApplication = new ProjectMomo();
      _mainApplication.Start();
    }
  }
}
