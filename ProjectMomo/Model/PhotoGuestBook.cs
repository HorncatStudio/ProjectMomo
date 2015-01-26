using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMomo.Helpers;

namespace ProjectMomo.Model
{
  public class PhotoGuestBook : ProjectMomoTab, FetchPictureListener
  {
    public List<Guest> Guests { get; set; }
    private Guest _currentGuest;

    public PhotoGuestBook()
    {
      Header = "GUESTBOOK";
      Guests = new List<Guest>();
      _currentGuest = null;
    }

    public void SetCurrentGuest( Guest guest )
    {
      //! TODO: throw exception here maybe?
      if (!Guests.Contains(guest))
        return;

      _currentGuest = guest;
    }
  
    public void onFetchPicture(ShowerPicture image)
    {
      if (null == _currentGuest)
        return;

      _currentGuest.addGuestBookPicture(image);
    }
  }
}
