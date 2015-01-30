using System.Collections.Generic;

namespace ProjectMomo.Model
{
  /// <summary>
  /// Model that will contain user information for the users associated to the shower when that aspect of the program is added.
  /// </summary>
  public class User
  {
    public string FirstName { get; set; }
    public string Lastname { get; set; }
    public string Email { get;set; }
    public List<Shower> RegisteredShowers { get; set; }

    public User( string firstName, string lastName, string email )
    {
      FirstName = firstName;
      Lastname = lastName;
      Email = email;

      RegisteredShowers = new List<Shower>();
    }
  }
}
