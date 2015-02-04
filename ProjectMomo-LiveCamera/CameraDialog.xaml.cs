using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using EDSDKLib;

namespace ProjectMomo_LiveCamera
{
  /// <summary>
  /// Interaction logic for CameraDialog.xaml
  /// </summary>
  public partial class CameraDialog : Window, INotifyPropertyChanged
  {
    private AProjectMomoCameraSdk _cameraSdk { get; set; }
    public List<CanonCamera> CameraList
    {
      get { return _cameraSdk.GetCameraList(); }
    }
    public CanonCamera SelectedCamera { get; set; }

    public CameraDialog( AProjectMomoCameraSdk cameraSdk )
    {
      InitializeComponent();
      DataContext = this;
      _cameraSdk = cameraSdk;
    }

    private void OnOkButtonClick(object sender, RoutedEventArgs e)
    {
      if (SelectedCamera == null)
        this.DialogResult = false;
      else
      {
        this.DialogResult = true;
      }

      Close();
    }

    private void OnRefreshButtonClick(object sender, RoutedEventArgs e)
    {
      OnPropertyChanged("CameraList");
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      var handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
