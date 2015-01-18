using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMomo.Model
{
  public class Shower
  {
    public User Host { get; set; }
    public User Parter { get; set; }
    public User Mama { get; set; }
    public List<Guest> Guests;

    public Shower()
    {
      Guests = new List<Guest>();
    }

    public string showerName()
    {
      return Mama.FirstName + " & " + Parter.FirstName + " Baby Shower";
    }

    
  }
}
