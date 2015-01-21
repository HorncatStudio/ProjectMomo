using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
