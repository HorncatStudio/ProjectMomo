using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMomo.Helpers;

namespace ProjectMomo.Model
{
  /// <summary>
  /// Manages guests checking in and picture additions.
  /// 
  /// To use this model, a current guest must be selected using the SetCurrentGuest() method.
  /// Pictures can then be assigned to that current guest by the OnFetchPicture method.
  /// 
  /// note - Checking in aspect has not been added yet.
  /// question - is this class even neccesary, may be able to just pass around the
  ///            shower object instead which contains the guests.
  /// </summary>
  public class PhotoGuestBook : FetchPictureListener
  {
    public List<Guest> Guests { get; set; }

    //! Should maybe be moved to the view model? 
    private Guest _currentGuest;

    public PhotoGuestBook()
    {
      Guests = new List<Guest>();
      _currentGuest = null;
    }

    /// <summary>
    ///  Sets the current guest to be the one the new pictures get added to.
    /// </summary>
    /// <param name="guest"></param>
    public void SetCurrentGuest( Guest guest )
    {
      //! TODO: throw exception here maybe?
      if (!Guests.Contains(guest))
        return;

      _currentGuest = guest;
    }

    /// <summary>
    /// Sample Empty method that could be used for checking in guests at a later time.
    /// </summary>
    /// <param name="guest"></param>
    public void CheckinGuest(Guest guest)
    {
    }
  
    /// <summary>
    /// Method from interface to be implemented in order for a new shower image
    /// to be assigned to the current guest.  If there is no current guest, no action occurs.
    /// </summary>
    /// <param name="image"></param>
    public void OnFetchPicture(ShowerPicture image)
    {
      if (null == _currentGuest)
        return;

      _currentGuest.AddGuestBookPicture(image);
    }
  }
}
