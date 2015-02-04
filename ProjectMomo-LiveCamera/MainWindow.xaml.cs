using System;
using System.Collections.Generic;
using System.Windows;

namespace ProjectMomo_LiveCamera
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    /// <summary>
    /// Saving the reference to the SDK to ensure it is cleaned up at the end of the application. </summary>
    private AProjectMomoCameraSdk storedCameraSdk;
    public MainWindow()
    {
      InitializeComponent();
      
      // Temproary fake SDK to see if the actions are followed in the application
      //FakeCameraSdkSdk cameraSdk = new FakeCameraSdkSdk();
      //List<CanonCamera> fakeCameras = new List<CanonCamera>();
      //fakeCameras.Add(new CanonCamera("Camera1"));
      //fakeCameras.Add(new CanonCamera("fake2"));
      //cameraSdk.CameraList = fakeCameras;

      CanonCameraSdkSdk cameraSdk = new CanonCameraSdkSdk();
      
      storedCameraSdk = cameraSdk;
      DataContext = new LiveCameraViewModel(cameraSdk);
      cameraSdk.LiveViewCanvas = this.liveViewCanvas;
    }

    private void MainWindow_OnClosed(object sender, EventArgs e)
    {
      storedCameraSdk.Dispose();
    }
  }

 
}
