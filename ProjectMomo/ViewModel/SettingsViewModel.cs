using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using ProjectMomo.Annotations;
using ProjectMomo.Helpers;
using ProjectMomo.Model;

namespace ProjectMomo.ViewModel
{
  class SettingsViewModel : TabViewModel, INotifyPropertyChanged
  {
    private string _imageFilePath;
    public string ImageFilePath
    {
      get { return _imageFilePath; }
      set
      {
        _imageFilePath = value;
        OnPropertyChanged("ImageFilePath");
      }
    }

    public ICommand FindImageFilePathButton { get; set; }

    public SettingsViewModel()
    {
      Header = App.Current.FindResource("SettingsHeader").ToString();
      FindImageFilePathButton = new RelayCommand(new Action<object>(GetImageFilePath));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Updates the ImageFilePath property of the view model which 
    /// is used for searching where images are stored.
    /// </summary>
    /// <param name="obj"></param>
    private void GetImageFilePath(object obj)
    {
      // todo - Should fake this out for testing, but in the mean time,
      // will be leaving it this way.
      
      // Configure open file dialog box
      // Ew forms. 
      var dialog = new FolderBrowserDialog();
      dialog.Description = "Select Directory Images Are Sent";

      if (string.IsNullOrEmpty(_imageFilePath))
      {
        // Sets it to the MyDocuments directory
        dialog.RootFolder = Environment.SpecialFolder.Personal;
      }
      else
      {
        dialog.SelectedPath = _imageFilePath;
      }

      // Show open file dialog box
      var result = dialog.ShowDialog();
      if (result != DialogResult.OK)
        return;

      ImageFilePath = dialog.SelectedPath;
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      var handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
