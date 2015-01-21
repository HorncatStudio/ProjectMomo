using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using ProjectMomo.Helpers;

namespace ProjectMomo.ViewModel
{
  public class HomePageViewModel : TabViewModel
  {
    private SectionNavigation navigation_;

    public ICommand ButtonCommand { get; set; }

    public HomePageViewModel()
    {
      navigation_ = null;
      Header = "HOME";
      ButtonCommand = new RelayCommand(new Action<object>(Navigate));
    }

    public void RegisterNavigation(SectionNavigation navigation)
    {
      navigation_ = navigation;

    }

    private void Navigate(object obj)
    {
      if (null == navigation_)
        return;

      //MessageBox.Show(obj.ToString());

      switch (obj.ToString())
      {
        case "GuestBook":
          navigation_.DisplayGuestBook();
          break;
        case "Home":
          navigation_.DisplayHomePage();
          break;
      }
    }
  }
}