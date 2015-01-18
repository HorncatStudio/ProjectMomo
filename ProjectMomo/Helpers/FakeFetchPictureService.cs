using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMomo.Model;

namespace ProjectMomo.Helpers
{
  public class FakeFetchPictureService : IFetchPictureService
  {
    private List<FetchPictureListener> _Listeners;

    public FakeFetchPictureService()
    {
      _Listeners = new List<FetchPictureListener>();
    }

    public void registerListener(FetchPictureListener listener)
    {
      _Listeners.Add(listener);
    }

    public void unregisterListener(FetchPictureListener listener)
    {
      _Listeners.Remove(listener);
    }

    public void sendPicture( ShowerPicture picture )
    {
      foreach( FetchPictureListener listener in _Listeners )
      {
        listener.onFetchPicture(picture);
      }
    }
  }
}
