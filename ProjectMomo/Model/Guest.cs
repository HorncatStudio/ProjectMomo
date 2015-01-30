using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMomo.Model
{
  /// <summary>
  /// Guest model for all unique guest information
  /// </summary>
  public class Guest
  {
    /// <summary>
    /// Unique identifier that shal be used for retrieving and storing information
    /// related to a guest in the repository. </summary>
    private int _id;

    public string Name { get; set; }
    public string Address { get; set; }

    /// <summary>
    /// Container of guest book images that are associated with the guest. </summary>
    public List<ShowerPicture> GuestBookPictures { get; set; }

    public Guest()
    {
      GuestBookPictures = new List<ShowerPicture>();
    }

    public void AddGuestBookPicture( ShowerPicture picture )
    {
      GuestBookPictures.Add(picture);
    }

    public ShowerPicture GetLastGuestBookPicture()
    {
      //! TODO: Another place for exception probably
      if (!GuestBookPictures.Any())
        return new ShowerPicture();

      return GuestBookPictures.ElementAt(GuestBookPictures.Count - 1);
    }
  }
}
