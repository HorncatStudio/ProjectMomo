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

    public User()
    {
      RegisteredShowers = new List<Shower>();
    }
  }
}
