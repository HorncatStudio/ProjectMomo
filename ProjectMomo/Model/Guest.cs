using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMomo.Model
{
  public class Guest
  {
    private int _Id;

    public string Name { get; set; }
    public string Address { get; set; }
    public List<ShowerPicture> GuestBookPictures;

    public Guest()
    {
      GuestBookPictures = new List<ShowerPicture>();
    }

    public Guest( string name, string address )
    {
      Name = name;
      Address = address;
      GuestBookPictures = new List<ShowerPicture>();
    }


    public void addGuestBookPicture( ShowerPicture picture )
    {
      GuestBookPictures.Add(picture);
    }

    public ShowerPicture getLastGuestBookPicture()
    {
      //! TODO: Another place for exception probably
      if (!GuestBookPictures.Any())
        return new ShowerPicture();

      return GuestBookPictures.ElementAt(GuestBookPictures.Count - 1);
    }
  }
}
