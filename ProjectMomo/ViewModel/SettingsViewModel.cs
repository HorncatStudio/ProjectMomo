using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Input;
using ProjectMomo.Annotations;
using ProjectMomo.Helpers;

namespace ProjectMomo.ViewModel
{
  /// <summary>
  /// Section that displays user settings for the application.
  /// </summary>
  class SettingsViewModel : TabViewModel, INotifyPropertyChanged
  {
    /// <summary>
    /// Service that fetches images.
    /// </summary>
    private IFetchPictureService _fetchPictureService;

    /// <summary>
    /// The file path where the fetched pictuers are gathered. </summary>
    private string _imageFilePath;
    public string ImageFilePath
    {
      get { return _imageFilePath; }
      set
      {
        _imageFilePath = value;
        Properties.Settings.Default.FetchImageFilePath = _imageFilePath;
        _fetchPictureService.UpdateDirectoryToWatch(_imageFilePath);
        OnPropertyChanged("ImageFilePath");
      }
    }

    /// <summary>
    /// Commands the retrieval of getting the file path for image fetching.
    /// </summary>
    public ICommand FindImageFilePathButton { get; set; }

    public SettingsViewModel( IFetchPictureService fetchService )
    {
      _fetchPictureService = fetchService;

      Header = App.Current.FindResource("SettingsHeader").ToString();
      FindImageFilePathButton = new RelayCommand(new Action<object>(GetImageFilePath));
      ImageFilePath = Properties.Settings.Default.FetchImageFilePath;

    }

    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Updates the ImageFilePath property of the view model which  is used for searching where images are stored.
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

      // Kind of a hacky way to do this but leaving it for now the saving of the applications settings
      // todo - move this to be in a proper place elsewhere later
      Properties.Settings.Default.Save();
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      var handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
