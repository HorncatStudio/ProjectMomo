using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProjectMomo.Model;

namespace ProjectMomo.ViewModel
{
  public class PhotoGuestBookViewModel : TabViewModel
  {
    private PhotoGuestBook _guestBookModel = null;

    public PhotoGuestBookViewModel()
      : this(null)
    {
    }

    public PhotoGuestBookViewModel(PhotoGuestBook book)
    {
      Header = App.Current.FindResource("GuestBookHeader").ToString();
      _guestBookModel = book;
    }
  }
}