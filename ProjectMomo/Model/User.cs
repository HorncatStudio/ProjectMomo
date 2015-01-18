using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMomo.Model
{
  public class User
  {
    public string FirstName { get; set; }
    public string Lastname { get; set; }
    public string Email { get;set; }

    List<Shower> _RegisteredShowers;

    public User( string firstName, string lastName, string email )
    {
      FirstName = firstName;
      Lastname = lastName;
      Email = email;

      _RegisteredShowers = new List<Shower>();
    }
  }
}
