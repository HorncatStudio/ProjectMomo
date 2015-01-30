using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ProjectMomo.View;
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
    private ProjectMomoTab _homePage;

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
      _fetchPictureService = new FakeFetchPictureService();
      _showerRepository = new FakeShowerRepository();
      _pictureRouter = new ShowerImageRouter();

      _fetchPictureService.registerListener(_pictureRouter);

      // Models
      _currentShower = new Shower();
      _guestBook = new PhotoGuestBook();

      // View Models
      _homePageViewModel = new HomePageViewModel();
      _guestBookViewModel = new PhotoGuestBookViewModel(_guestBook);
      _settingsViewModel = new SettingsViewModel();
      _mainWindowViewModel = new MainWindowViewModel(_pictureRouter);
      
      InitializeTabNavigation();
      InitializeImageRouting();

      // Views
      _mainWindow = new MainWindow();
      _mainWindow.DataContext = _mainWindowViewModel;
    }

    private void InitializeImageRouting()
    {
      _pictureRouter.RegisterDefaultRoute(_currentShower);
      _pictureRouter.RegisterRoute(_guestBookViewModel.Header, _guestBook);
    }

    public void Start()
    {
      _currentShower = _showerRepository.GetShower();
      _guestBook.Guests = _currentShower.Guests;

      // todo - make this data bound instead of manually setting it
      _mainWindow.SetStatusBarText(_currentShower.showerName());
      _mainWindow.Show();
    }

    private void InitializeTabNavigation()
    {
      _mainWindowViewModel.Tabs.Add(_homePageViewModel);
      _mainWindowViewModel.Tabs.Add(_guestBookViewModel);
      _mainWindowViewModel.Tabs.Add(_settingsViewModel);
      _mainWindowViewModel.SelectedTab = _homePageViewModel;

      _homePageViewModel.RegisterNavigation(_mainWindowViewModel);
    }
  }
}
