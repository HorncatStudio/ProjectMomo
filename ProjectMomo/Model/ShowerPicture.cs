using System.IO;
using System.Reflection;

namespace ProjectMomo.Model
{
  /// <summary>
  /// A picture model that holds all information unique to a picture in the application.
  /// 
  /// todo - Cache the picture for display only in the future
  /// </summary>
  public class ShowerPicture
  {
    public string FileName
    {
      get { return Path.GetFileName(_absoluteFilePath); }
      set {}
    }

    private string _absoluteFilePath;
    public string AbsolutePath
    {
      get
      {
        if( string.IsNullOrEmpty(_absoluteFilePath) )
          return "../Resources/NoGiftPicture.png";

        return _absoluteFilePath;
      }
      set
      {
        if (File.Exists(value))
          _absoluteFilePath = value;
      }
    }
  }
}
