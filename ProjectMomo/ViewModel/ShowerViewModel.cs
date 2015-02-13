using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Win32;
using ProjectMomo.Helpers;
using ProjectMomo.Model;
using ProjectMomo.Properties;

namespace ProjectMomo.ViewModel
{
  public class ShowerViewModel : TabViewModel
  {
    #region Properties
    public string ShowerName
    {
      get { return _showerModel.ShowerName; }
    }
    public ObservableCollection<ShowerPicture> ShowerPictures
    {
      get { return _showerModel.MiscPictures; }
    }
    #endregion

    private Shower _showerModel;

    #region Commands
    public RelayCommand ExportShowerCommand { get; set; }
    public RelayCommand LoadShowerCommand { get; set; }
    public RelayCommand ImportGuestsCommand { get; set; }
    public RelayCommand DisplayPicCommand { get; set; }
    #endregion

    public ShowerViewModel(Shower shower)
    {
      _showerModel = shower;
      Header = App.Current.FindResource("ShowerHeader").ToString();

      ExportShowerCommand = new RelayCommand(new Action<object>(OnExportShower));
      LoadShowerCommand = new RelayCommand(new Action<object>(OnLoadShowerDialog));
      ImportGuestsCommand = new RelayCommand(new Action<object>(OnImportGuests));
      DisplayPicCommand = new RelayCommand(new Action<object>(DisplayPicture));
    }

    #region Load/Save Operations
    /// <summary>
    /// todo - The load and save shower methods need to be refactored and put in
    /// a repository instead of just being embedded into the view model.
    /// Bad Angelina Bad. -.-;
    /// </summary>
    /// <param name="filepath"></param>
    public void SaveShower(string filepath)
    {
      XmlSerializer serializer = new XmlSerializer(typeof(Shower));
      TextWriter writer = new StreamWriter(filepath);
      serializer.Serialize(writer, _showerModel);
      writer.Close();
    }

    public void SaveShower()
    {
      SaveShower(Settings.Default.ShowerBackupFile);
    }
    
    public void LoadShower(string filepath)
    {
      XmlSerializer deserializer = new XmlSerializer(typeof (Shower));
      XmlReader reader = XmlReader.Create(filepath);
      try
      {
        Shower shower = (Shower) deserializer.Deserialize(reader);

        // A cheap copy in order to move on.  
        // would need to clean this up at a later time
        _showerModel.ShallowCopy(shower);
        Settings.Default.ShowerBackupFile = filepath;
      }
      catch (Exception e)
      {
        Console.WriteLine("Failed to load XML document.");
      }
    }
    #endregion

    #region File Operations
    private void OnExportShower(object obj)
    {
      SaveFileDialog saveDialog = new SaveFileDialog
        {
          FileName = Settings.Default.ShowerBackupFile,
          DefaultExt = "*.xml",
          Filter = "Shower XML documents (.xml)|*.xml"
        };

      Nullable<bool> result = saveDialog.ShowDialog();

      if (result != true)
        return;

      SaveShower( saveDialog.FileName );
    }

    public void OnLoadShowerDialog(object obj)
    {
      OpenFileDialog openDialog = new OpenFileDialog
      {
        FileName = Settings.Default.ShowerBackupFile,
        DefaultExt = "*.xml",
        Filter = "Shower XML documents (.xml)|*.xml"
      };

      Nullable<bool> result = openDialog.ShowDialog();
      if (result != true)
        return;
      
      LoadShower(openDialog.FileName);
    }

    private void OnImportGuests(object obj)
    {
      OpenFileDialog openDialog = new OpenFileDialog
      {
        FileName = Settings.Default.ShowerBackupFile,
        DefaultExt = "*.csv",
        Filter = "Shower Guests csv documents (.csv)|*.csv"
      };

      Nullable<bool> result = openDialog.ShowDialog();
      if (result != true)
        return;

      string[] lines = File.ReadAllLines(openDialog.FileName);

      foreach (var line in lines)
      {
        string[] data = line.Split(';');
        // We return a person with the data in order.

        if (string.IsNullOrEmpty(data[0]))
          continue;

        Guest guest = new Guest {Name = data[0]};

        bool containsAddress = (data.Length == 2);
        if (containsAddress)
          guest.Address = data[1];

        if (_showerModel.ContainGuestNmae(guest.Name))
          continue;

        _showerModel.Guests.Add(guest);
      }
    }
    #endregion

    #region Display Operations
    private void DisplayPicture(object obj)
    {
      if (obj == null)
        return;

      var win = new ShowerPictureDialog
      {
        Picture = (ShowerPicture)obj
      };
      win.Show();
    }
    #endregion
  }
}
