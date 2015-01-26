using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents.DocumentStructures;
using ProjectMomo.Helpers;

namespace ProjectMomo.Model
{
  public class Shower : FetchPictureListener
  {
    public User Host { get; set; }
    public User Parter { get; set; }
    public User Mama { get; set; }
    public List<Guest> Guests;
    public List<ShowerPicture> MiscPictures; 

    public Shower()
    {
      Guests = new List<Guest>();
      MiscPictures = new List<ShowerPicture>();
    }

    public string showerName()
    {
      return Mama.FirstName + " & " + Parter.FirstName + " Baby Shower";
    }


    public void onFetchPicture(ShowerPicture image)
    {
      MiscPictures.Add(image);
    }
  }
}
