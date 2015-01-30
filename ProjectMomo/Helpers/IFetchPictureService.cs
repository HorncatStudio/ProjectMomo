using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMomo.Helpers
{
  /// <summary>
  /// Fetching images service interface that needs to be implemented in order to support images to be fetched.
  /// </summary>
  public interface IFetchPictureService
  {
    void RegisterListener(FetchPictureListener listener);
    void UnregisterListener(FetchPictureListener listener);

    void UpdateDirectoryToWatch(string directory);

    void Start();
    void Stop();
  }
}
