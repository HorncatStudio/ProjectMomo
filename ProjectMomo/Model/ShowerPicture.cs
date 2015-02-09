using System;
using System.IO;
using System.Xml.Serialization;

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

    public string FileName
    {
      get { return Path.GetFileName(PicturePath.LocalPath); ; }
      set {}
    }    

    /// <summary>
    /// Returns the absolute file path of the URI provided. </summary>
    public string AbsolutePath
    {
      get { return PicturePath.AbsolutePath; }
      set
      {
        if( File.Exists(value) )
          PicturePath = new Uri(value);
      }
    }

    /// <summary>
    /// The path of the image.  </summary>
    [XmlIgnore]
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
