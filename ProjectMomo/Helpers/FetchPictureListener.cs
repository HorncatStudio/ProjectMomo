using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMomo.Model;

namespace ProjectMomo.Helpers
{
  public interface FetchPictureListener
  {
    void onFetchPicture( ShowerPicture image );
  }
}
