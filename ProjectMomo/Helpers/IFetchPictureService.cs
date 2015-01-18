using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMomo.Helpers
{
  public interface IFetchPictureService
  {
    void registerListener(FetchPictureListener listener);
    void unregisterListener(FetchPictureListener listener);
  }
}
