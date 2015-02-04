using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ProjectMomo_LiveCamera
{
  public abstract class AProjectMomoCameraSdk : IDisposable
  {
    public Canvas LiveViewCanvas { get; set; }

    public bool IsCameraSet { get; protected set; }
    public bool IsCanvasSet()
    {
      return (null != LiveViewCanvas);
    }
    
    public abstract bool IsCameraLive();

    public abstract List<CanonCamera> GetCameraList();
    public abstract void OpenSession(CanonCamera openCamera);
    public abstract void CloseSession();
    public abstract void StartLiveView();
    public abstract void StopLiveView();
    public abstract void Dispose();
  }
}
