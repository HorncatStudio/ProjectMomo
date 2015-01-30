namespace ProjectMomo.Helpers
{
  /// <summary>
  /// The interface a class must implement in order to support receiving navigation commands within the application.
  /// </summary>
  public interface ISectionNavigation
  {
    void DisplaySection(string sectionHeader);
  }
}