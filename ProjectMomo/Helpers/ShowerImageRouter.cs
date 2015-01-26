using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;

namespace ProjectMomo.Helpers
{
  /// <summary>
  /// A router the sends images to the current listener (model in our case) that is actively being used
  /// </summary>
  public class ShowerImageRouter : FetchPictureListener
  {
    private Dictionary<string, FetchPictureListener> _registeredRoutes;
    public const string DEFAULT_ROUTE_NAME = "DEFAULT";

    private string _currentState;
    public string CurrentRoute
    {
      private get { return _currentState; }
      set {
        _currentState = _registeredRoutes.Keys.Contains(value) ? value : DEFAULT_ROUTE_NAME;
      }
    }
    

    public ShowerImageRouter()
    {
      _registeredRoutes = new Dictionary<string, FetchPictureListener>();
    }

    public void RegisterDefaultRoute(FetchPictureListener listener)
    {
      if (_registeredRoutes.ContainsKey(DEFAULT_ROUTE_NAME))
        _registeredRoutes[DEFAULT_ROUTE_NAME] = listener;
      else
        _registeredRoutes.Add(DEFAULT_ROUTE_NAME, listener);
    }

    public void RegisterRoute(string routeName, FetchPictureListener listener )
    {
      _registeredRoutes.Add(routeName, listener);
    }

    public void onFetchPicture(Model.ShowerPicture image)
    {
      _registeredRoutes[CurrentRoute].onFetchPicture(image);
    }
  }
}