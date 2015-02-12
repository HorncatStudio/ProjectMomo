using System.Collections.Generic;
using System.Collections.ObjectModel;
using ProjectMomo.Helpers;

namespace ProjectMomo.Model
{
  /// <summary>
  /// The primary model that contains all data in relation to a shower event.
  /// </summary>
  public class Shower : FetchPictureListener
  {
    #region Properties
    public User Host { get; set; }
    public User Parter { get; set; }
    public User Mama { get; set; }
    public ObservableCollection<Guest> Guests { get; set; }
    public ObservableCollection<ShowerPicture> MiscPictures { get; set; }

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
    #endregion

    public Shower()
    {
      Host = new User();
      Parter = new User();
      Mama = new User();

      Guests = new ObservableCollection<Guest>();
      MiscPictures = new ObservableCollection<ShowerPicture>();
    }

    /// <summary>
    /// A cheap method to resolve an issue with needing to update the
    /// reference of this object in the application.
    /// 
    /// Will refactor at a later time.
    /// </summary>
    /// <param name="shower"></param>
    public void ShallowCopy(Shower shower)
    {
      Host = shower.Host;
      Parter = shower.Parter;
      Mama = shower.Mama;

      //Guests.Clear();
      //foreach(Guest guest in shower.Guests)
      //{
      //  Guests.Add(guest);
      //}
      Guests = shower.Guests;
      MiscPictures = shower.MiscPictures;
    }

    #region Methods
    public bool ContainGuestNmae(string name)
    {
      foreach (var guest in Guests)
      {
        if (guest.Name == name)
          return true;
      }

      return false;
    }

    /// <summary>
    /// Method to an operation when a picture has been fetched and sent to this model.
    /// </summary>
    /// <param name="image"></param>
    public void OnFetchPicture(ShowerPicture image)
    {
      DispatchService.Invoke(() =>
      {
        MiscPictures.Add(image);
      });
    }
    #endregion
  }
}
