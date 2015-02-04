using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace ProjectMomo_LiveCamera
{
  public class FakeCameraSdkSdk : AProjectMomoCameraSdk
  {
    public List<CanonCamera> CameraList { get; set; }

    public override bool IsCameraLive()
    {
      return _isLive;
    }

    public override List<CanonCamera> GetCameraList()
    {
      return CameraList;
    }

    private bool _isOpened { get; set; }
    private bool _isLive { get; set; }

    public FakeCameraSdkSdk()
    {
      _isOpened = false;
      _isLive = false;
    }

    public override void OpenSession(CanonCamera openCamera)
    {
      if ( _isOpened )
        CloseSession();

      IsCameraSet = true;
      Console.WriteLine("Opening sessions for cameraSdk: {0}", openCamera.Name);
    }

    public override void CloseSession()
    {
      IsCameraSet = false;
      Console.WriteLine("Closing the cameraSdk Sessoin");
    }

    public override void StartLiveView()
    {
      if (!IsCanvasSet())
        return;

      LiveViewCanvas.Background = Brushes.Red;
      _isLive = true;
    }

    public override void StopLiveView()
    {
      if (!IsCanvasSet())
        return;

      LiveViewCanvas.Background = Brushes.LightGray;
      _isLive = false;
    }

    public override void Dispose()
    {
    }
  }
}