using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
  public class PhotoGuestBookViewModel : TabViewModel, FetchPictureListener
  {
    private Shower _showerModel;

    #region Properties
    public ObservableCollection<Guest> Guests {
      get { return _showerModel.Guests; }
      set { }
    }
    public Guest CurrentGuest { get; set; }

    /// <summary>
    /// The selected image that is displayed in another smaller window to show a larger preview of it. </summary>
    public ShowerPicture SelectedImage { get; set; }
    #endregion

    #region Commands
    public RelayCommand AssignGiftPicture { get; set; }
    public RelayCommand AddGuestCommand { get; set; }
    public RelayCommand DisplayPicCommand { get; set; }
    #endregion

    /// <summary>
    /// The guests that the view model is responsible for displaying and operating on.
    /// </summary>
    /// <param name="guests"></param>
    public PhotoGuestBookViewModel( Shower shower )
    {
      Header = App.Current.FindResource("GuestBookHeader").ToString();
      _showerModel = shower;
      AssignGiftPicture = new RelayCommand(new Action<object>(OnAssignGiftPicture));
      AddGuestCommand = new RelayCommand( new Action<object>(AddGuest));
      DisplayPicCommand = new RelayCommand(new Action<object>(DisplayPicture));
    }



    #region Command Methods
    private void AddGuest(object obj)
    {
      GuestDialog dialog = new GuestDialog();
      if (dialog.ShowDialog() != true)
        return;

      DispatchService.Invoke(() =>
      { 
        Guests.Add(dialog.CachedGuest);
      });
    }

    private void DisplayPicture(object obj)
    {
      if (obj == null)
        return;

      var win = new ShowerPictureDialog
      {
        Picture =  (ShowerPicture) obj
      };
      win.Show();
    }

    private void OnAssignGiftPicture(object obj)
    {
      if (null == SelectedImage)
        return;

      CurrentGuest.GiftPicture = SelectedImage;
    }

    #endregion
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
    }
  }
}