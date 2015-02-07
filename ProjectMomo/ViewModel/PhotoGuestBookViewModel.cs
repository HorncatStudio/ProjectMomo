using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectMomo.Model;
using System.Windows;
using ProjectMomo.Annotations;
using ProjectMomo.Helpers;

namespace ProjectMomo.ViewModel 
{

  /// <summary>
  /// View model for the tab that displays the photo guest book for the event.
  /// It manages guests checking in and picture additions.
  /// 
  /// If a Guest is selected, the on the OnFetchPicture event, the image will be added to that guest.
  /// 
  /// </summary>
  public class PhotoGuestBookViewModel : TabViewModel, FetchPictureListener, INotifyPropertyChanged
  {
    public List<Guest> Guests { get; set; }
    public Guest CurrentGuest { get; set; }

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


    public RelayCommand AssignGiftPicture { get; set; }
    /// <summary>
    /// The guests that the view model is responsible for displaying and operating on.
    /// </summary>
    /// <param name="guests"></param>
    public PhotoGuestBookViewModel( List<Guest> guests )
    {
      Header = App.Current.FindResource("GuestBookHeader").ToString();
      Guests = guests;
      AssignGiftPicture = new RelayCommand(new Action<object>(OnAssignGiftPicture));
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
      OnPropertyChanged("CurrentGuest");
    }

    public void CheckInCurrentGuest()
    {
    }

    #region INotifyProperChanged 
    public event PropertyChangedEventHandler PropertyChanged;
    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      var handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    private void OnAssignGiftPicture(object obj)
    {
      Console.WriteLine("Calling ASIGN AT LEASTS");
      ShowerPicture picture = obj as ShowerPicture;
      if (picture == null)
        Console.WriteLine("FAILED TO SET PICTURE");
    }
  }
}