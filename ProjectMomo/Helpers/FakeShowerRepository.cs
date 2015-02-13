using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using ProjectMomo.Model;

namespace ProjectMomo.Helpers
{
  /// <summary>
  /// A faked repository in order to develop application without the neccesity of a real implemntation at this time.
  /// </summary>
  public class FakeShowerRepository : IShowerRepository
  {
    public Shower GetShower()
    {
      Shower shower = new Shower();
      shower.Host = new User {FirstName = "Ashley", Lastname = "Grenon", Email = "ashley.grenon@gmail.com"};
      shower.Mama = new User{FirstName = "Angelina", Lastname = "Uno-Antonison", Email = "ange.unoantonison@gmail.com"};
      shower.Partner = new User { FirstName = "Shinichi", Lastname = "Uno", Email = "uno.shinichi11@gmail.com" };
      
      string fullPicturePath = Path.GetFullPath("Angelina&Shinichi_056.jpg");

      shower.MiscPictures = new ObservableCollection<ShowerPicture> {
        new ShowerPicture { AbsolutePath = fullPicturePath },
        new ShowerPicture { AbsolutePath = fullPicturePath },
        new ShowerPicture { AbsolutePath = fullPicturePath }
      };

      ObservableCollection<Guest> guestList = new ObservableCollection<Guest> {
        new Guest{ Name="Anastasia Antonison", Address="12013 Mount Charron Road NW, Huntsville AL 35806", GiftPicture = new ShowerPicture { AbsolutePath = fullPicturePath },
          GuestBookPictures = new ObservableCollection<ShowerPicture>
          {
            new ShowerPicture { AbsolutePath = fullPicturePath },
            new ShowerPicture { AbsolutePath = fullPicturePath },
            new ShowerPicture { AbsolutePath = fullPicturePath }
          }
        },
        new Guest{ Name="Nancy Morris", Address="227 Ryland Pike, Huntsville AL 35811", GiftPicture =  new ShowerPicture { AbsolutePath = fullPicturePath },
          GuestBookPictures = new ObservableCollection<ShowerPicture>
          {
            new ShowerPicture { AbsolutePath = fullPicturePath },
            new ShowerPicture { AbsolutePath = fullPicturePath }
          }
        },
        new Guest{ Name="Nancy Perry", Address="12774 Wall Triana Blvd, Ardmore AL 35739", GiftPicture = new ShowerPicture { AbsolutePath = fullPicturePath },
          GuestBookPictures = new ObservableCollection<ShowerPicture>
          {
            new ShowerPicture { AbsolutePath = fullPicturePath },
            new ShowerPicture { AbsolutePath = fullPicturePath },
            new ShowerPicture { AbsolutePath = fullPicturePath },
            new ShowerPicture { AbsolutePath = fullPicturePath },
            new ShowerPicture { AbsolutePath = fullPicturePath },
            new ShowerPicture { AbsolutePath = fullPicturePath }
          }
        },
        new Guest{ Name="Amy Helser", Address="2702 Bronte Cir., Owens Cross Roads, AL 35763", GiftPicture =  new ShowerPicture { AbsolutePath = fullPicturePath },
          GuestBookPictures = new ObservableCollection<ShowerPicture>
          {
            new ShowerPicture { AbsolutePath = fullPicturePath }
          }
        },
        new Guest{ Name="Janey Pearson", Address="1001 Water Hill Road Apartment #3004, Madison AL 35758", GiftPicture = new ShowerPicture { AbsolutePath = fullPicturePath },
          GuestBookPictures = new ObservableCollection<ShowerPicture>
          {
            new ShowerPicture { AbsolutePath = fullPicturePath },
            new ShowerPicture { AbsolutePath = fullPicturePath },
            new ShowerPicture { AbsolutePath = fullPicturePath }
          }
        }    
      };
      shower.Guests = guestList;

      return shower;
    }
  }
}
