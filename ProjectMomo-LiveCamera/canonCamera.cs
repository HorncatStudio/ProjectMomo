using System;
using EDSDKLib;

namespace ProjectMomo_LiveCamera
{
  public class CanonCamera
  {
    private Camera _canonCamera;

    private string _fakeName;
    public string Name
    {
      get
      {
        if( null == _canonCamera )
          return _fakeName;
        else
        {
          return _canonCamera.Info.szDeviceDescription;
        }
      }
    }

    public CanonCamera(string name)
    {
      _fakeName = name;
      _canonCamera = null;
    }

    public CanonCamera(IntPtr refrence)
    {
      _canonCamera = new Camera(refrence);  
    }

    public CanonCamera(Camera camera)
    {
      _canonCamera = camera;
    }

    public Camera GetCamera()
    {
      return _canonCamera;
    }
  }
}
