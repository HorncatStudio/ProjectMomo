using System;
using System.Collections.ObjectModel;
using ProjectMomo.Helpers;
using ProjectMomo.Model;

namespace ProjectMomo.ViewModel
{
  /// <summary>
  /// Responsible for enabling the view to display elements of the shoewr gifts.
  /// </summary>
  class GiftViewModel : TabViewModel, FetchPictureListener
  {
    #region Members
    private Shower _shower;
    #endregion

    #region Properties
    public ObservableCollection<Guest> Guests
    {
      get { return _shower.Guests; }
    }

    public Guest SelectedGuest { get; set; }
    #endregion

    #region Commands
    public RelayCommand AddGroupCommand { get; set; }
    public RelayCommand DisplayPicCommand { get; set; }
    #endregion

    public GiftViewModel(Shower shower)
    {
      _shower = shower;
      Header = App.Current.FindResource("GiftHeader").ToString();

      AddGroupCommand = new RelayCommand(new Action<object>(AddGroup));
      DisplayPicCommand = new RelayCommand(new Action<object>(DisplayPicture));
    }

    private void AddGroup(object obj)
    {
      const bool isGroup = true;
      GuestDialog dialog = new GuestDialog(isGroup);
      if (dialog.ShowDialog() != true)
        return;

      DispatchService.Invoke(() =>
      {
        Guests.Add(dialog.CachedGuest);
      });
    }


    public void OnFetchPicture(ShowerPicture image)
    {
      if (null == SelectedGuest)
      {
        _shower.MiscPictures.Add(image);
        return;
      }

      DispatchService.Invoke(() =>
      {
        SelectedGuest.ShowerGiftPictures.Add(image);
      });
    }

    private void DisplayPicture(object obj)
    {
      if (obj == null)
        return;

      var win = new ShowerPictureDialog
      {
        Picture = (ShowerPicture)obj
      };
      win.Show();
    }

  }
}
