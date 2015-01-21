using System;
using System.Collections.Generic;
using System.Linq;
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
  /// </summary>
  public class ProjectMomo
  {
    // Infrastrucutre
    private IFetchPictureService _FetchPitureService;
    private IShowerRepository _ShowerRepository;

    // Models
    private Shower _currentShower;
    private PhotoGuestBook _guestBook;
    private ProjectMomoTab _homePage;

    // ViewModels
    private MainWindowViewModel _mainWindowViewModel_;

    // View
    MainWindow _mainWindow;

    public ProjectMomo()
    {
      _FetchPitureService = new FakeFetchPictureService();
      _ShowerRepository = new FakeShowerRepository();

      _currentShower = new Shower();
      _guestBook = new PhotoGuestBook();

      _mainWindowViewModel_ = new MainWindowViewModel();

      _mainWindow = new MainWindow();
      _mainWindow.DataContext = _mainWindowViewModel_;
    }

    public void Start()
    {
      _currentShower = _ShowerRepository.GetShower();
      _guestBook.loadGuests(_currentShower.Guests);
      _mainWindow.SetStatusBarText(_currentShower.showerName());
      _mainWindow.Show();
    }
  }
}
