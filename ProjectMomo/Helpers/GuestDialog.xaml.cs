using System.Windows;
using ProjectMomo.Model;

namespace ProjectMomo.Helpers
{
  /// <summary>
  /// Interaction logic for GuestDialog.xaml
  /// </summary>
  public partial class GuestDialog : Window
  {
    public Guest CachedGuest { get; set; }
    public bool IsGroup { get; set; }

    public GuestDialog( bool isGroup = false )
    {
      InitializeComponent();
      CachedGuest = new Guest();
      IsGroup = isGroup;
      DataContext = this;
    }

    private void OnCancelButtonClick(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
      Close();
    }

    private void OnAddButtonClick(object sender, RoutedEventArgs e)
    {
      if (string.IsNullOrEmpty(CachedGuest.Name)) 
      {
        // Display popup window requiring name if Add button clicked
        return;
      }
      else if (string.IsNullOrEmpty(CachedGuest.Address) && !IsGroup)
      {
        // Recommend an address is provided
        // if decides do not, then continue, if so, exists
        return;
      }

      DialogResult = true;
      Close();
    }
  }
}
