using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMomo.Model;

namespace ProjectMomo.Helpers
{
  /// <summary>
  /// Faked picture retrival service used for unit testing.
  /// 
  /// Use the SendPicture() method in order to force an image to be send to all of the registered listeners.
  /// </summary>
  public class FakeFetchPictureService : IFetchPictureService
  {
    private List<FetchPictureListener> _listeners;

    public FakeFetchPictureService()
    {
      _listeners = new List<FetchPictureListener>();
    }

    public void RegisterListener(FetchPictureListener listener)
    {
      _listeners.Add(listener);
    }

    public void UnregisterListener(FetchPictureListener listener)
    {
      _listeners.Remove(listener);
    }

    public void UpdateDirectoryToWatch(string directory)
    {
    }

    public void Start()
    {
    }

    public void Stop()
    {
    }

    /// <summary>
    /// A faked method in order to force an image to be "fetched" for testing purposes.
    /// </summary>
    /// <param name="picture"></param>
    public void SendPicture( ShowerPicture picture )
    {
      foreach( FetchPictureListener listener in _listeners )
      {
        listener.OnFetchPicture(picture);
      }
    }
  }
}
