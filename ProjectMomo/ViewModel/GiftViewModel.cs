using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public List<Guest> Guests
    {
      get { return _shower.Guests; }
    }

    public Guest SelectedGuest { get; set; }
    #endregion

    public GiftViewModel(Shower shower)
    {
      _shower = shower;
      Header = App.Current.FindResource("GiftHeader").ToString();
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
  }
}
