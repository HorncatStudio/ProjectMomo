using ProjectMomo.Model;

namespace ProjectMomo.Helpers
{
  /// <summary>
  /// An interface class to allow custom implementations of accessing a repository to retireve and save
  /// Shower information.
  /// </summary>
  public interface IShowerRepository
  {
    Shower GetShower();
  }
}
