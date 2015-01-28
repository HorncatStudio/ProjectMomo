using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls.Primitives;
using ProjectMomo.Model;

namespace ProjectMomo.ViewModel
{
  public class PhotoGuestBookViewModel : TabViewModel
  {
    private PhotoGuestBook _guestBookModel = null;
    private ObservableCollection<Uri> items;

    public PhotoGuestBookViewModel()
      : this(null)
    {
    }

    public PhotoGuestBookViewModel(PhotoGuestBook book)
    {
      Header = App.Current.FindResource("GuestBookHeader").ToString();
      _guestBookModel = book;
      items = new ObservableCollection<Uri>();

      for (int i = 0; i < 4; i++)
      {
        var newOne = new Uri("ms-appx:///Angelina&Shinichi_056.jpg");
        items.Add(newOne);
      }
    }
  }
}