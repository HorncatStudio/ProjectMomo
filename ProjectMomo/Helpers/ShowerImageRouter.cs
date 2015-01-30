using System.Collections.Generic;
using System.Linq;

namespace ProjectMomo.Helpers
{
  /// <summary>
  /// A router the sends images to the current listener (model in our case) that is actively being used.
  /// 
  /// If the route does not exist or a current route is not set, an image is sent to the "Default" route.
  /// </summary>
  public class ShowerImageRouter : FetchPictureListener
  {
    private Dictionary<string, FetchPictureListener> _registeredRoutes;
    public const string DefaultRouteName = "DEFAULT";

    private string _currentState;
    public string CurrentRoute
    {
      private get { return _currentState; }
      set {
        _currentState = _registeredRoutes.Keys.Contains(value) ? value : DefaultRouteName;
      }
    }
    
    public ShowerImageRouter()
    {
      _registeredRoutes = new Dictionary<string, FetchPictureListener>();
    }

    public void RegisterDefaultRoute(FetchPictureListener listener)
    {
      if (_registeredRoutes.ContainsKey(DefaultRouteName))
        _registeredRoutes[DefaultRouteName] = listener;
      else
        _registeredRoutes.Add(DefaultRouteName, listener);
    }

    public void RegisterRoute(string routeName, FetchPictureListener listener )
    {
      _registeredRoutes.Add(routeName, listener);
    }

    public void OnFetchPicture(Model.ShowerPicture image)
    {
      _registeredRoutes[CurrentRoute].OnFetchPicture(image);
    }
  }
}