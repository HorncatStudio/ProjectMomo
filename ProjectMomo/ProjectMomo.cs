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
  public class ProjectMomo
  {
    // Infrastrucutre
    private IFetchPictureService _FetchPictureService;
    private IShowerRepository _ShowerRepository;
    private ShowerImageRouter _PictureRouter;

    // Models
    private Shower _currentShower;
    private PhotoGuestBook _guestBook;
    private ProjectMomoTab _homePage;

    // ViewModels
    private HomePageViewModel _homePageViewModel;
    private PhotoGuestBookViewModel _guestBookViewModel;
    private MainWindowViewModel _mainWindowViewModel_;

    // View
    MainWindow _mainWindow;

    public ProjectMomo()
    {
      // Services
      _FetchPictureService = new FakeFetchPictureService();
      _ShowerRepository = new FakeShowerRepository();
      _PictureRouter = new ShowerImageRouter();

      _FetchPictureService.registerListener(_PictureRouter);

      // Models
      _currentShower = new Shower();
      _guestBook = new PhotoGuestBook();

      // View Models
      _homePageViewModel = new HomePageViewModel();
      _guestBookViewModel = new PhotoGuestBookViewModel(_guestBook);
      _mainWindowViewModel_ = new MainWindowViewModel(_PictureRouter);
      
      InitializeTabNavigation();
      InitializeImageRouting();

      // Views
      _mainWindow = new MainWindow();
      _mainWindow.DataContext = _mainWindowViewModel_;
    }

    private void InitializeImageRouting()
    {
      _PictureRouter.RegisterDefaultRoute(_currentShower);
      _PictureRouter.RegisterRoute(_guestBookViewModel.Header, _guestBook);
    }

    public void Start()
    {
      _currentShower = _ShowerRepository.GetShower();
      _guestBook.Guests = _currentShower.Guests;

      // todo - make this data bound instead of manually setting it
      _mainWindow.SetStatusBarText(_currentShower.showerName());
      _mainWindow.Show();
    }

    private void InitializeTabNavigation()
    {
      _mainWindowViewModel_.Tabs.Add(_homePageViewModel);
      _mainWindowViewModel_.Tabs.Add(_guestBookViewModel);
      _mainWindowViewModel_.SelectedTab = _homePageViewModel;

      _homePageViewModel.RegisterNavigation(_mainWindowViewModel_);
    }
  }
}
