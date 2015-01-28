using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using ProjectMomo.Helpers;

namespace ProjectMomo.ViewModel
{
  public class HomePageViewModel : TabViewModel
  {
    private List<SectionNavigation> _navigation;

    public ICommand ButtonCommand { get; set; }

    public HomePageViewModel()
    {
      _navigation = new List<SectionNavigation>();
      Header = Application.Current.FindResource("HomeHeader").ToString();
      ButtonCommand = new RelayCommand(new Action<object>(Navigate));
    }

    public void RegisterNavigation(SectionNavigation navigation)
    {
      _navigation.Add(navigation);
    }

    private void Navigate(object obj)
    {
      foreach (var nav in _navigation)
      {
        nav.DisplaySection(obj.ToString());
      }
    }
  }
}