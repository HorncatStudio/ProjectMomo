using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMomo.Model;

namespace ProjectMomo.Helpers
{
  public class FakeShowerRepository : IShowerRepository
  {
    public FakeShowerRepository()
    {
    }

    public Shower GetShower()
    {
      Shower shower = new Shower();
      shower.Host = new User("Ashley", "Grenon", "ashley.grenon@gmail.com");
      shower.Mama = new User("Angelina", "Uno-Antonison", "ange.unoantonison@gmail.com");
      shower.Parter = new User("Shinichi", "Uno", "uno.shinichi11@gmail.com");

      List<Guest> guestList = new List<Guest>();
      guestList.Add(new Guest("Anastasia Antonison", "12013 Mount Charron Road NW, Huntsville AL 35806"));
      guestList.Add(new Guest("Nancy Morris", "227 Ryland Pike, Huntsville AL 35811"));
      guestList.Add(new Guest("Nancy Perry", "12774 Wall Triana Blvd, Ardmore AL 35739"));
      guestList.Add(new Guest("Amy Helser", "2702 Bronte Cir., Owens Cross Roads, AL 35763"));
      guestList.Add(new Guest("Janey Pearson", "1001 Water Hill Road Apartment #3004, Madison AL 35758"));
      shower.Guests = guestList;

      return shower;
    }

    public void clear()
    {

    }
  }
}
