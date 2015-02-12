using System.Windows;
using ProjectMomo.Model;

namespace ProjectMomo.Helpers
{
  /// <summary>
  /// Interaction logic for ShowerPictureDialog.xaml
  /// </summary>
  public partial class ShowerPictureDialog : Window
  {
    public ShowerPicture Picture { get; set; }

    public ShowerPictureDialog()
    {
      InitializeComponent();
      DataContext = this;
    }

  }
}
