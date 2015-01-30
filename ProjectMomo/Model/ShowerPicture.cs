using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ProjectMomo.Model
{
  /// <summary>
  /// A picture model that holds all information unique to a picture in the application.
  /// </summary>
  public class ShowerPicture
  {
    private const int InvalidId = -1;
    /// <summary>
    /// A unique identifier that shall be used when an image is read and saved into the repository.  </summary>
    private int _id;
    
    /// <summary>
    /// Returns the absolute file path of the URI provided. </summary>
    public string AbsolutePath
    {
      get { return PicturePath.AbsolutePath; }
    }

    /// <summary>
    /// The path of the image.  </summary>
    public Uri PicturePath { get; set; }

    public ShowerPicture()
      : this(InvalidId)
    {
    }

    public ShowerPicture( int id )
    {
      _id = id;
    }

    public bool IsNull()
    {
      return (InvalidId == _id);
    }
  }
}
