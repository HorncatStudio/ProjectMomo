using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectMomo.Model;
using System.Windows;
using ProjectMomo.Annotations;

namespace ProjectMomo.ViewModel 
{

  /// <summary>
  /// View model for the tab that displays the photo guest book for the event.
  /// </summary>
  public class PhotoGuestBookViewModel : TabViewModel, INotifyPropertyChanged
  {
    /// <summary>
    /// Model associated with this view model.  </summary>
    private PhotoGuestBook _guestBookModel = null;

    public List<Guest> Guests
    {
      get{ return _guestBookModel.Guests; }
    }

    /// <summary>
    /// The selected image that is displayed in another smaller window to show a larger preview of it. </summary>
    ShowerPicture _SelectedImage;
    public ShowerPicture SelectedImage
    {
      get { return _SelectedImage; }
      set
      {
        if (_SelectedImage != value)
        {
          _SelectedImage = value;
          //! Temporary placeholder for code where a smaller window with the image would
          //! open if selected.
          if (value != null)
          {
            var win = new Window(); 
            win.Show();
          }
        }
      }
    }

    private Guest _selectedGuest;
    public Guest SelectedGuest
    {
      get { return _selectedGuest; }
      set
      {
        _selectedGuest = value;
        if (null != value)
        {
          _guestBookModel.SetCurrentGuest(value);
        }
      }
    }

    /// <summary>
    /// Must inject the guest book model into the view model to display information in the view.
    /// </summary>
    /// <param name="book"></param>
    public PhotoGuestBookViewModel(PhotoGuestBook book)
    {
      Header = App.Current.FindResource("GuestBookHeader").ToString();
      _guestBookModel = book;
      _guestBookModel.PropertyChanged += ModelChanged;
    }

    private void ModelChanged(object sender, PropertyChangedEventArgs e)
    {
      OnPropertyChanged(String.Empty);
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