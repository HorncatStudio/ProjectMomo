using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls.Primitives;
using ProjectMomo.Model;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace ProjectMomo.ViewModel
{
  public sealed class ImageConverter : IValueConverter
  {
    public object Convert(object value, Type targetType,
                          object parameter, CultureInfo culture)
    {
      try
      {
        return new BitmapImage(new Uri((string)value));
      }
      catch
      {
        return new BitmapImage();
      }
    }

    public object ConvertBack(object value, Type targetType,
                              object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }

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

    private ObservableCollection<Uri> items;

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