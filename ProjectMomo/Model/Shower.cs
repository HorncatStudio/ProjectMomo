using System.Collections.Generic;
using ProjectMomo.Helpers;

namespace ProjectMomo.Model
{
  /// <summary>
  /// The primary model that contains all data in relation to a shower event.
  /// </summary>
  public class Shower : FetchPictureListener
  {
    public User Host { get; set; }
    public User Parter { get; set; }
    public User Mama { get; set; }
    public List<Guest> Guests { get; set; }
    public List<ShowerPicture> MiscPictures { get; set; }

    /// <summary>
    /// Returns a shower name.  At this time all showers are determined to be baby showers.
    /// </summary>
    public string ShowerName
    {
      get
      {
        string showerName = Mama.FirstName;
        if (!string.IsNullOrEmpty(Parter.FirstName))
          showerName += " & " + Parter.FirstName;
        showerName += " Baby Shower";
        return showerName;
      }
    }

    public Shower()
    {
      Guests = new List<Guest>();
      MiscPictures = new List<ShowerPicture>();
    }


    /// <summary>
    /// Method to an operation when a picture has been fetched and sent to this model.
    /// </summary>
    /// <param name="image"></param>
    public void OnFetchPicture(ShowerPicture image)
    {
      MiscPictures.Add(image);
    }
  }
}
