using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using ProjectMomo.Helpers;

namespace ProjectMomo.ViewModel
{
  /// <summary>
  /// Homepage view model that is to provide navigation to other sections of the application.
  /// 
  /// Use the RegisterNavigation method to add an object that will handle navigation outside
  /// of the home page view model.  That class must inherit the ISectionNavigation interface.
  /// 
  /// In the future it will also be the primary "login" screen.
  /// </summary>
  public class HomePageViewModel : TabViewModel
  {
    /// <summary>
    /// A container of all of the sections that can be navigated to. </summary>
    private List<ISectionNavigation> _navigation;

    /// <summary>
    /// The navigation command that can be bound in order to execute the Navigate command. </summary>
    public ICommand NavigateButtonCommand { get; set; }

    public HomePageViewModel()
    {
      _navigation = new List<ISectionNavigation>();
      Header = Application.Current.FindResource("HomeHeader").ToString();
      NavigateButtonCommand = new RelayCommand(new Action<object>(Navigate));
    }

    /// <summary>
    /// Method to clal in order to register objects responsible for navigation in the application. 
    /// </summary>
    /// <param name="navigation"></param>
    public void RegisterNavigation(ISectionNavigation navigation)
    {
      _navigation.Add(navigation);
    }

    /// <summary>
    /// The Navigate command that gets executed when the NavaigateButtonCommand occurs.
    /// </summary>
    /// <param name="obj"></param>
    private void Navigate(object obj)
    {
      foreach (var nav in _navigation)
      {
        nav.DisplaySection(obj.ToString());
      }
    }
  }
}