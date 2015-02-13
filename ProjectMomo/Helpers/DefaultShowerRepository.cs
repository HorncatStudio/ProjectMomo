using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using ProjectMomo.Model;

namespace ProjectMomo.Helpers
{
  /// <summary>
  /// A faked repository in order to develop application without the neccesity of a real implemntation at this time.
  /// </summary>
  public class DefaultShowerRepository : IShowerRepository
  {
    public Shower GetShower()
    {
      Shower shower = new Shower();
      shower.Host = new User {FirstName = "Ashley", Lastname = "Grenon", Email = "ashley.grenon@gmail.com"};
      shower.Mama = new User{FirstName = "Angelina", Lastname = "Uno-Antonison", Email = "ange.unoantonison@gmail.com"};
      shower.Partner = new User { FirstName = "Shinichi", Lastname = "Uno", Email = "uno.shinichi11@gmail.com" };
      
      shower.MiscPictures = new ObservableCollection<ShowerPicture>();

      shower.Guests = new ObservableCollection<Guest>();

      return shower;
    }
  }
}
