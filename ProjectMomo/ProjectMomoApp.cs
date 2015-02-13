using System;
using System.IO;
using ProjectMomo.Helpers;
using ProjectMomo.Model;
using ProjectMomo.ViewModel;

namespace ProjectMomo
{
  /// <summary>
  /// Core of the application which manages the relationships between the models, view models, and views.
  /// 
  /// Infrastructure classes shall be faked in the mean time.  At a later time, a fake core could be created
  /// for acceptance testing purposes as practice.
  /// 
  /// question - Should all of this be migrated into the App class?
  /// </summary>
  public class ProjectMomoApp
  {
    // Infrastrucutre
    private IFetchPictureService _fetchPictureService;
    private ShowerImageRouter _pictureRouter;

    // Models
    private Shower _currentShower;

    // ViewModels
    private PhotoGuestBookViewModel _guestBookViewModel;
    private MainWindowViewModel _mainWindowViewModel;
    private SettingsViewModel _settingsViewModel;
    private ShowerViewModel _showerViewModel;
    private GiftViewModel _gitViewModel;

    // View
    MainWindow _mainWindow;

    public ProjectMomoApp()
    {
      // Services
      _fetchPictureService = new FetchPictureService();
      _pictureRouter = new ShowerImageRouter();

      _fetchPictureService.RegisterListener(_pictureRouter);

      // Models
      _currentShower = new Shower();

      // View Models
      _showerViewModel = new ShowerViewModel(_currentShower);
      _guestBookViewModel = new PhotoGuestBookViewModel(_currentShower);
      _settingsViewModel = new SettingsViewModel(_fetchPictureService);
      _mainWindowViewModel = new MainWindowViewModel(_pictureRouter);
      _gitViewModel = new GiftViewModel(_currentShower);

      LoadShowerModel();

      InitializeTabNavigation();
      InitializeImageRouting();

      // Views
      _mainWindow = new MainWindow { DataContext = _mainWindowViewModel };
    }

    private void LoadShowerModel()
    {
      if (!File.Exists(Properties.Settings.Default.ShowerBackupFile))
      {
        DefaultShowerRepository defaultShowerRepository = new DefaultShowerRepository();
        _currentShower.ShallowCopy(defaultShowerRepository.GetShower());
        return;
      }
      
      try
      {
        _showerViewModel.LoadShower(Properties.Settings.Default.ShowerBackupFile);
      }
      catch (Exception)
      {
        Console.WriteLine("Debug - Using empty shower since backupfile does not exist.");
      }
    }

    /// <summary>
    /// Initialized all of the routing paths to sections that will do operations when
    /// an image is fetched.
    /// </summary>
    private void InitializeImageRouting()
    {
      _pictureRouter.RegisterDefaultRoute(_currentShower);
      _pictureRouter.RegisterRoute(_guestBookViewModel.Header, _guestBookViewModel);
      _pictureRouter.RegisterRoute(_gitViewModel.Header, _gitViewModel);
    }

    /// <summary>
    /// Initializes the tab navigation dictated by the view models.
    /// </summary>
    private void InitializeTabNavigation()
    {
      _mainWindowViewModel.Tabs.Add(_showerViewModel);
      _mainWindowViewModel.Tabs.Add(_guestBookViewModel);
      _mainWindowViewModel.Tabs.Add(_gitViewModel);
      _mainWindowViewModel.Tabs.Add(_settingsViewModel);
      _mainWindowViewModel.SelectedTab = _showerViewModel;
    }


    /// <summary>
    /// Starts the primary application.
    /// </summary>
    public void Start()
    {
      // todo - make this data bound instead of manually setting it
      _mainWindow.SetShowerName(_currentShower.ShowerName);
      _mainWindow.Show();

      _fetchPictureService.Start();
    }


    public void Stop()
    {
      _showerViewModel.SaveShower();
      _fetchPictureService.Stop();
    }
  }
}
