using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectMomo.Helpers;
using ProjectMomo.ViewModel;

namespace ProjectMomo
{
  public class MainWindowViewModel : SectionNavigation, INotifyPropertyChanged
  {
    public ObservableCollection<TabViewModel> Tabs { get; set; }

    private TabViewModel _selectedTab;
    public TabViewModel SelectedTab
    {
      get { return _selectedTab; }
      set
      {
        _selectedTab = value;
        OnPropertyChanged("SelectedTab");
      }
    }

    private HomePageViewModel _homePageViewModel;
    private PhotoGuestBookViewModel _guestBookViewModel;

    public MainWindowViewModel()
    {
      Tabs = new ObservableCollection<TabViewModel>();
      _homePageViewModel = new HomePageViewModel();
      _guestBookViewModel = new PhotoGuestBookViewModel();

      Tabs.Add(_homePageViewModel);
      Tabs.Add(_guestBookViewModel);

      _selectedTab = _homePageViewModel;

      _homePageViewModel.RegisterNavigation(this);
    }

    public void DisplayHomePage()
    {
      SelectedTab = _homePageViewModel;
    }

    public void DisplayGuestBook()
    {
      SelectedTab = _guestBookViewModel;
    }

    public void DisplayGiftPage()
    {
      throw new System.NotImplementedException();
    }

    public void DisplayManagePage()
    {
      throw new System.NotImplementedException();
    }

    public void DisplaySettingsPage()
    {
      throw new System.NotImplementedException();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      var handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}