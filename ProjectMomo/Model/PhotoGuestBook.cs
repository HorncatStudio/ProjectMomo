using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProjectMomo.Annotations;
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
  public class PhotoGuestBook : FetchPictureListener, INotifyPropertyChanged
  {
    public List<Guest> Guests { get; set; }

    //! Should maybe be moved to the view model? 
    private Guest _currentGuest;
    public Guest CurrentGuest
    {
      get { return _currentGuest; }
      set
      {
        if (!Guests.Contains(value))
          return;
        _currentGuest = value;
        OnPropertyChanged();
      }
    }

    public PhotoGuestBook()
    {
      Guests = new List<Guest>();
      CurrentGuest = null;
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
      if (null == CurrentGuest)
        return;

      CurrentGuest.AddGuestBookPicture(image);
      OnPropertyChanged("GuestBookPictures");
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      var handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
