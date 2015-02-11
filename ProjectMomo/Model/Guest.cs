using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectMomo.Annotations;
using ProjectMomo.Helpers;

namespace ProjectMomo.Model
{
  /// <summary>
  /// Guest model for all unique guest information
  /// </summary>
  public class Guest : INotifyPropertyChanged
  {
    #region Properties
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }

   
    /// <summary>
    /// Container of guest book images that are associated with the guest. </summary>
    public ObservableCollection<ShowerPicture> GuestBookPictures { get; set; }
    public ObservableCollection<ShowerPicture> ShowerGiftPictures { get; set; }
    public string GiftsText { get; set; }
    public ShowerPicture GiftPicture { get; set; }
    #endregion

    public Guest()
    {
      GuestBookPictures = new ObservableCollection<ShowerPicture>();
      ShowerGiftPictures = new ObservableCollection<ShowerPicture>();
    }

    public void AddGuestBookPicture( ShowerPicture picture )
    {
      DispatchService.Invoke(() =>
      {
       GuestBookPictures.Add(picture);
      });

      if (GuestBookPictures.Count == 1)
        GiftPicture = picture;
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
