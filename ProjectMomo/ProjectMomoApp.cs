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
    private IShowerRepository _showerRepository;
    private ShowerImageRouter _pictureRouter;

    // Models
    private Shower _currentShower;
    private PhotoGuestBook _guestBook;

    // ViewModels
    private HomePageViewModel _homePageViewModel;
    private PhotoGuestBookViewModel _guestBookViewModel;
    private MainWindowViewModel _mainWindowViewModel;
    private SettingsViewModel _settingsViewModel;

    // View
    MainWindow _mainWindow;

    public ProjectMomoApp()
    {
      // Services
      _fetchPictureService = new FetchPictureService();
      _showerRepository = new FakeShowerRepository();
      _pictureRouter = new ShowerImageRouter();

      _fetchPictureService.RegisterListener(_pictureRouter);

      // Models
      _currentShower = new Shower();
      _guestBook = new PhotoGuestBook();

      // View Models
      _homePageViewModel = new HomePageViewModel();
      _guestBookViewModel = new PhotoGuestBookViewModel(_guestBook);
      _settingsViewModel = new SettingsViewModel(_fetchPictureService);
      _mainWindowViewModel = new MainWindowViewModel(_pictureRouter);
      
      InitializeTabNavigation();
      InitializeImageRouting();

      // Views
      _mainWindow = new MainWindow { DataContext = _mainWindowViewModel };
    }

    /// <summary>
    /// Initialized all of the routing paths to sections that will do operations when
    /// an image is fetched.
    /// </summary>
    private void InitializeImageRouting()
    {
      _pictureRouter.RegisterDefaultRoute(_currentShower);
      _pictureRouter.RegisterRoute(_guestBookViewModel.Header, _guestBook);
    }

    /// <summary>
    /// Initializes the tab navigation dictated by the view models.
    /// </summary>
    private void InitializeTabNavigation()
    {
      _mainWindowViewModel.Tabs.Add(_homePageViewModel);
      _mainWindowViewModel.Tabs.Add(_guestBookViewModel);
      _mainWindowViewModel.Tabs.Add(_settingsViewModel);
      _mainWindowViewModel.SelectedTab = _homePageViewModel;

      _homePageViewModel.RegisterNavigation(_mainWindowViewModel);
    }


    /// <summary>
    /// Starts the primary application.
    /// </summary>
    public void Start()
    {
      _currentShower = _showerRepository.GetShower();
      _guestBook.Guests = _currentShower.Guests;

      // todo - make this data bound instead of manually setting it
      _mainWindow.SetStatusBarText(_currentShower.ShowerName);
      _mainWindow.Show();

      _fetchPictureService.Start();
    }

    public void Stop()
    {
      _fetchPictureService.Stop();
    }
  }
}
