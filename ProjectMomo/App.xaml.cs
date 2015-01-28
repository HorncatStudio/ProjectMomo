using System.Windows;

namespace ProjectMomo
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    ProjectMomoApp _mainApplication;

    private void Application_Startup(object sender, StartupEventArgs e)
    {
      _mainApplication = new ProjectMomoApp();
      _mainApplication.Start();
    }

    private void App_OnExit(object sender, ExitEventArgs e)
    {
      //Properties.Settings.Default.Save();
    }
  }
}
