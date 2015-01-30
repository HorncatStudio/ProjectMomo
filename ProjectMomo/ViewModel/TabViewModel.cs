namespace ProjectMomo.ViewModel
{
  /// <summary>
  /// An abstract class that all view models for views to be displayed in the primary tab control must implement.
  /// This is so that all tabs can have the same header information.
  /// 
  /// Names for each of the tabs need to be stored as a resource in the App.xaml resource section.
  /// Header names can then be retrieved progmatically like below.
  /// 
  /// Ex. App.Current.FindResource("GuestBookHeader").ToString();
  /// 
  /// Note - Remmeber the view must also be updated in order to map a Viewmodel and View together.
  /// </summary>
  public class TabViewModel
  {
    public string Header { get; set; }
  }
}