using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMomo.Model;

namespace ProjectMomo.Helpers
{
  /// <summary>
  /// The interface a class must inherit in order to recieved pictures from the fetch picture service.
  /// </summary>
  public interface FetchPictureListener
  {
    void OnFetchPicture( ShowerPicture image );
  }
}
