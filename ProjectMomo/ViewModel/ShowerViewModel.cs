using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using ProjectMomo.Helpers;
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

    public RelayCommand SaveShowerCommand { get; set; }
    public ShowerViewModel(Shower shower)
    {
      _showerModel = shower;
      Header = App.Current.FindResource("ShowerHeader").ToString();
      SaveShowerCommand = new RelayCommand(new Action<object>(OnSaveShower));
    }

    private void OnSaveShower(object obj)
    {
      XmlSerializer serializer = new XmlSerializer(typeof(Shower));
      TextWriter writer = new StreamWriter("test_shower_name.xml");
      serializer.Serialize(writer, _showerModel);
      writer.Close();
    }
  }
}
