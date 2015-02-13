using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using ProjectMomo.Model;

namespace ProjectMomo.Helpers
{
  public class FetchPictureService : IFetchPictureService
  {
    /// <summary>
    /// \todo - if adding more file system watchers, store them in a list
    /// </summary>
    private FileSystemWatcher _pngFileSystemWatcher = null;
    private FileSystemWatcher _jpgFileSystemWatcher = null;

    private List<FetchPictureListener> _listeners = null;
    private static string LocalDataDirectoryName = ".projectmomo";
    private string _localDataDirectoryPath;

    private string DefaultFetchPictureDirectory 
    {
      get { return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ProjectMomo"; }
    }

    public FetchPictureService()
    {
      _listeners = new List<FetchPictureListener>();

      if (string.IsNullOrEmpty(Properties.Settings.Default.FetchImageFilePath))
      {
        if (!Directory.Exists(DefaultFetchPictureDirectory))
          Directory.CreateDirectory(DefaultFetchPictureDirectory);

        Properties.Settings.Default.FetchImageFilePath = DefaultFetchPictureDirectory;
      }

      _pngFileSystemWatcher = new FileSystemWatcher
      {
        Path = Properties.Settings.Default.FetchImageFilePath,
        NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName,
        Filter = "*.png"
      };
      _pngFileSystemWatcher.Created += new FileSystemEventHandler(OnCreated);

      _jpgFileSystemWatcher = new FileSystemWatcher
      {
        Path = Properties.Settings.Default.FetchImageFilePath,
        NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName,
        Filter = "*.jpg"
      };
      _jpgFileSystemWatcher.Created += new FileSystemEventHandler(OnCreated);

      _localDataDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                                    + "//"  + LocalDataDirectoryName;
    }

    #region State Operations
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public void Start()
    {
      if (!String.IsNullOrEmpty(_pngFileSystemWatcher.Path))
        _pngFileSystemWatcher.EnableRaisingEvents = true;

      if (!String.IsNullOrEmpty(_jpgFileSystemWatcher.Path))
        _jpgFileSystemWatcher.EnableRaisingEvents = true;

      CreateTempDirectory();
    }

    public void Stop()
    {
      _pngFileSystemWatcher.EnableRaisingEvents = false;
      _jpgFileSystemWatcher.EnableRaisingEvents = false;
    }
    #endregion

    public void UpdateDirectoryToWatch(string directory)
    {
      if (String.IsNullOrEmpty(directory))
          return;

      _pngFileSystemWatcher.Path = directory;
      _pngFileSystemWatcher.EnableRaisingEvents = true;

      _jpgFileSystemWatcher.Path = directory;
      _jpgFileSystemWatcher.EnableRaisingEvents = true;
    }

    #region Listener operations
    public void RegisterListener(FetchPictureListener listener)
    {
      _listeners.Add(listener);
    }

    public void UnregisterListener(FetchPictureListener listener)
    {
      if (_listeners.Contains(listener))
        _listeners.Remove(listener);
    }
    #endregion

    #region Internal Guts
    private void OnCreated(object source, FileSystemEventArgs e)
    {
      Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
      string newFilePath = _localDataDirectoryPath + "//" + e.Name;

      if (!File.Exists(newFilePath) && File.Exists(e.FullPath) )
        File.Move(e.FullPath, newFilePath);
      else
        File.Delete(e.FullPath);

      ShowerPicture picture = new ShowerPicture
      {
        AbsolutePath = newFilePath
      };

      foreach (var listener in _listeners)
      {
        listener.OnFetchPicture(picture);
      }
    }
    #endregion

    #region Helpers
    private void CreateTempDirectory()
    {
      if (!Directory.Exists(_localDataDirectoryPath))
        Directory.CreateDirectory(_localDataDirectoryPath);
    }
    #endregion
  }
}
