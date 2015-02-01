using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMomo.Model;

namespace ProjectMomo.ViewModel
{
  public class ShowerViewModel : TabViewModel
  {
    public string ShowerName
    {
      get { return _showerModel.ShowerName; }
    }

    public List<ShowerPicture> ShowerPictures
    {
      get { return _showerModel.MiscPictures; }
    }

    private Shower _showerModel;

    public ShowerViewModel(Shower shower)
    {
      _showerModel = shower;
      Header = App.Current.FindResource("ShowerHeader").ToString();
    }
  }
}
