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
    public List<Guest> Guests;
    private Guest _CurrentGuest;

    public PhotoGuestBook()
    {
      Header = "GUESTBOOK";
      Guests = new List<Guest>();
      _CurrentGuest = null;
    }

    public void loadGuests( List<Guest> guests )
    {
      Guests = guests;
    }

    public void setCurrentGuest( Guest guest )
    {
      //! TODO: throw exception here maybe?
      if (!Guests.Contains(guest))
        return;

      _CurrentGuest = guest;
    }
  
    public void onFetchPicture(ShowerPicture image)
    {
      if (null == _CurrentGuest)
        return;

      _CurrentGuest.addGuestBookPicture(image);
    }
  }
}
