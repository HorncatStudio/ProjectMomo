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

    public string Message { get; set; }

    public GuestDialog(string messaeg)
    {
      Message = messaeg;
    }

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
        MessageBox.Show(this,"Name field required.", "Name Required", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }
      else if (string.IsNullOrEmpty(CachedGuest.Address) && !IsGroup)
      {
        var result = MessageBox.Show(this, "Recommended to provide address. Would you like to provide an address?", "Address Recommended", MessageBoxButton.YesNo,
          MessageBoxImage.Warning, MessageBoxResult.Yes);

        if (result == MessageBoxResult.Yes)
          return;
      }

      DialogResult = true;
      Close();
    }
  }
}
