﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ProjectMomo.Annotations;

namespace ProjectMomo.Model
{
  /// <summary>
  /// Guest model for all unique guest information
  /// </summary>
  public class Guest : INotifyPropertyChanged
  {
    /// <summary>
    /// Unique identifier that shal be used for retrieving and storing information
    /// related to a guest in the repository. </summary>
    private int _id;

    public string Name { get; set; }
    public string Address { get; set; }

    private bool _isCheckedIn;
    public bool IsCheckedIn
    {
      get { return _isCheckedIn; }
      set
      {
        _isCheckedIn = value;
        OnPropertyChanged();
      }
    }

    /// <summary>
    /// Container of guest book images that are associated with the guest. </summary>
    private List<ShowerPicture> _guestBookPictures; 
    public List<ShowerPicture> GuestBookPictures
    {
      get { return _guestBookPictures; }
      set
      {
        _guestBookPictures = value;
        OnPropertyChanged();
      }
    }

    public ShowerPicture GiftPicture { get; set; }

    public Guest()
    {
      GuestBookPictures = new List<ShowerPicture>();
      IsCheckedIn = false;
    }

    public void AddGuestBookPicture( ShowerPicture picture )
    {
      GuestBookPictures.Add(picture);

      if (GuestBookPictures.Count == 1)
        GiftPicture = picture;

      OnPropertyChanged("GuestBookPictures");
      OnPropertyChanged("CurrentGuest");
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
