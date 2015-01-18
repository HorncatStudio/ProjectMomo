using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMomo.View;
using ProjectMomo.Helpers;
using ProjectMomo.Model;

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
    IFetchPictureService _FetchPitureService;
    IShowerRepository _ShowerRepository;

    // Models
    Shower _CurrentShower;
    PhotoGuestBook _GuestBook;

    // ViewModels
      
    // View
    MainWindow _MainWindow;

    public ProjectMomo()
    {
      _FetchPitureService = new FakeFetchPictureService();
      _ShowerRepository = new FakeShowerRepository();

      _CurrentShower = new Shower();
      _GuestBook = new PhotoGuestBook();

      _MainWindow = new MainWindow();
    }

    public void Start()
    {
      _CurrentShower = _ShowerRepository.GetShower();
      _GuestBook.loadGuests(_CurrentShower.Guests);
      _MainWindow.SetStatusBarText(_CurrentShower.showerName());
      _MainWindow.Show();
    }
  }
}
