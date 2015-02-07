﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
      shower.Parter = new User { FirstName = "Shinichi", Lastname = "Uno", Email = "uno.shinichi11@gmail.com" };
      
      string fullPicturePath = Path.GetFullPath("Angelina&Shinichi_056.jpg");

      shower.MiscPictures = new List<ShowerPicture> {
        new ShowerPicture { PicturePath = new Uri(fullPicturePath) },
        new ShowerPicture { PicturePath = new Uri(fullPicturePath) },
        new ShowerPicture { PicturePath = new Uri(fullPicturePath) }
      };

      List<Guest> guestList = new List<Guest> {
        new Guest{ Name="Anastasia Antonison", Address="12013 Mount Charron Road NW, Huntsville AL 35806", GiftPicture =  new ShowerPicture { PicturePath = new Uri(fullPicturePath) },
          GuestBookPictures = new List<ShowerPicture>
          {
            new ShowerPicture { PicturePath = new Uri(fullPicturePath) },
            new ShowerPicture { PicturePath = new Uri(fullPicturePath) },
            new ShowerPicture { PicturePath = new Uri(fullPicturePath) }
          }
        },
        new Guest{ Name="Nancy Morris", Address="227 Ryland Pike, Huntsville AL 35811", GiftPicture =  new ShowerPicture { PicturePath = new Uri(fullPicturePath) },
          GuestBookPictures = new List<ShowerPicture>
          {
            new ShowerPicture { PicturePath = new Uri(fullPicturePath) },
            new ShowerPicture { PicturePath = new Uri(fullPicturePath) },
          }
        },
        new Guest{ Name="Nancy Perry", Address="12774 Wall Triana Blvd, Ardmore AL 35739", GiftPicture =  new ShowerPicture { PicturePath = new Uri(fullPicturePath) },
          GuestBookPictures = new List<ShowerPicture>
          {
            new ShowerPicture { PicturePath = new Uri(fullPicturePath) },
            new ShowerPicture { PicturePath = new Uri(fullPicturePath) },
            new ShowerPicture { PicturePath = new Uri(fullPicturePath) },
            new ShowerPicture { PicturePath = new Uri(fullPicturePath) }
          }
        },
        new Guest{ Name="Amy Helser", Address="2702 Bronte Cir., Owens Cross Roads, AL 35763", GiftPicture =  new ShowerPicture { PicturePath = new Uri(fullPicturePath) },
          GuestBookPictures = new List<ShowerPicture>
          {
            new ShowerPicture { PicturePath = new Uri(fullPicturePath) }
          }
        },
        new Guest{ Name="Janey Pearson", Address="1001 Water Hill Road Apartment #3004, Madison AL 35758", GiftPicture =  new ShowerPicture { PicturePath = new Uri(fullPicturePath) },
          GuestBookPictures = new List<ShowerPicture>
          {
            new ShowerPicture { PicturePath = new Uri(fullPicturePath) },
            new ShowerPicture { PicturePath = new Uri(fullPicturePath) },
            new ShowerPicture { PicturePath = new Uri(fullPicturePath) },
          }
        }    
      };
      shower.Guests = guestList;

      return shower;
    }
  }
}
