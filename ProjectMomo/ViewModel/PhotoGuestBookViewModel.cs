using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls.Primitives;
using ProjectMomo.Model;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media.Imaging;
using System.Windows;

namespace ProjectMomo.ViewModel
{
  public class PhotoGuestBookViewModel : TabViewModel
  {
    private PhotoGuestBook _guestBookModel = null;

    public List<Guest> Guests
    {
      get
      {
        return _guestBookModel.Guests;
      }
    }

    ShowerPicture _SelectedImage;
    public ShowerPicture SelectedImage
    {
      get
      {
        return _SelectedImage;
      }
      set
      {
        if (_SelectedImage != value)
        {
          _SelectedImage = value;
          if (value != null)
          {
            var win = new Window(); // This would be your enlarged view control, inherited from Window.
            win.Show();
          }
        }
      }
    }

    public PhotoGuestBookViewModel(PhotoGuestBook book)
    {
      Header = App.Current.FindResource("GuestBookHeader").ToString();
      _guestBookModel = book;
    }
  }
}