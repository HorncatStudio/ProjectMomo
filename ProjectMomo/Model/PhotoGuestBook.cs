using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMomo.Helpers;

namespace ProjectMomo.Model
{
  public class PhotoGuestBook : FetchPictureListener
  {
    List<Guest> _Guests;
    Guest _CurrentGuest;

    public void loadGuests( List<Guest> guests )
    {
      _Guests = guests;
    }

    public void setCurrentGuest( Guest guest )
    {
      //! TODO: throw exception here maybe?
      if (!_Guests.Contains(guest))
        return;

      _CurrentGuest = guest;
    }
  
    public void onFetchPicture(ShowerPicture image)
    {
      _CurrentGuest.addGuestBookPicture(image);
    }
  }
}
