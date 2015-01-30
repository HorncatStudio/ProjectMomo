﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using ProjectMomo.Model;

namespace ProjectMomo.Helpers
{
  public class FetchPictureService : IFetchPictureService
  {
    private FileSystemWatcher _directoryWatcher = null;
    private List<FetchPictureListener> _listeners = null;
    private static string LocalDataDirectoryName = ".projectmomo";
    private string _localDataDirectoryPath;

    public FetchPictureService()
    {
      _listeners = new List<FetchPictureListener>();

      _directoryWatcher = new FileSystemWatcher();

      _directoryWatcher.Path = Properties.Settings.Default.FetchImageFilePath;
      _directoryWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName ;
      _directoryWatcher.Filter = "*.png";
      _directoryWatcher.Created += new FileSystemEventHandler(OnCreated);

      _localDataDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                                    + "//"  + LocalDataDirectoryName;
    }

    public void UpdateDirectoryToWatch(string directory)
    {
      _directoryWatcher.Path = directory;
    }

    [PermissionSet(SecurityAction.Demand, Name="FullTrust")]
    public void Start()
    {
      _directoryWatcher.EnableRaisingEvents = true;

      CreateTempDirectory();
    }

    private void CreateTempDirectory()
    {
      if (!Directory.Exists(_localDataDirectoryPath))
        Directory.CreateDirectory(_localDataDirectoryPath);
    }

    public void Stop()
    {
      _directoryWatcher.EnableRaisingEvents = false;
    }

    private void OnCreated(object source, FileSystemEventArgs e)
    {
      Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
      string newFilePath = _localDataDirectoryPath + "//" + e.Name;
      File.Move(e.FullPath, newFilePath);

      ShowerPicture picture = new ShowerPicture
      {
        PicturePath = new Uri(newFilePath)
      };

      foreach (var listener in _listeners)
      {
        listener.OnFetchPicture(picture);
      }
    }

    public void RegisterListener(FetchPictureListener listener)
    {
      _listeners.Add(listener);
    }

    public void UnregisterListener(FetchPictureListener listener)
    {
      if (_listeners.Contains(listener))
        _listeners.Remove(listener);
    }
  }
}
