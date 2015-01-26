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
    private ShowerImageRouter _imageRouter = null;

    private TabViewModel _selectedTab;
    public TabViewModel SelectedTab
    {
      get { return _selectedTab; }
      set
      {
        _selectedTab = value;
        UpdateImageRouter(_selectedTab.Header); // kind of hacky way but oh well, it works for now
        OnPropertyChanged("SelectedTab");
      }
    }

    public MainWindowViewModel()
      : this(null)
    {
    }

    public MainWindowViewModel(ShowerImageRouter imageRouter)
    {
      Tabs = new ObservableCollection<TabViewModel>();
      _imageRouter = imageRouter;
    }

    private void UpdateImageRouter(string imageRoute)
    {
      if (null != _imageRouter)
        _imageRouter.CurrentRoute = imageRoute;
    }

    public void DisplaySection(string sectionHeader)
    {
      foreach (var tab in Tabs)
      {
        if (sectionHeader == tab.Header)
        {
          SelectedTab = tab;
          return;
        }
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      var handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }


  }
}