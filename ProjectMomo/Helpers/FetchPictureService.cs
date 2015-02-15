using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using ProjectMomo.Model;
using System.Collections.Concurrent;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ProjectMomo.Helpers
{
  public class FetchPictureService : IFetchPictureService
  {
    #region Members
    #region File System watching
    /// <summary>
    /// \todo - if adding more file system watchers, store them in a list
    /// </summary>
    private FileSystemWatcher _pngFileSystemWatcher = null;
    private FileSystemWatcher _jpgFileSystemWatcher = null;
    #endregion

    #region Listeners Waiting for images
    private List<FetchPictureListener> _listeners = null;
    #endregion

    #region Directory paths
    private static string LocalDataDirectoryName = ".projectmomo";
    private string _localDataDirectoryPath;
    private string _cachedImageDataDirectoryPath;
    private string DefaultFetchPictureDirectory 
    {
      get { return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ProjectMomo"; }
    }
    #endregion

    #region ConcurrentProcessing
    ConcurrentQueue<string> tsFilesToProcess;
    private volatile bool _isRunning;
    Thread _sendPictureProcess;
    #endregion

    #endregion

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
      _cachedImageDataDirectoryPath = _localDataDirectoryPath + "//thumbs";

      tsFilesToProcess = new ConcurrentQueue<string>();
      _isRunning = false;
      _sendPictureProcess = new Thread(this.ProcessImage);
    }

    #region State Operations
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public void Start()
    {
      CreateTempDirectory();

      StartThread();

      if (!String.IsNullOrEmpty(_pngFileSystemWatcher.Path))
        _pngFileSystemWatcher.EnableRaisingEvents = true;

      if (!String.IsNullOrEmpty(_jpgFileSystemWatcher.Path))
        _jpgFileSystemWatcher.EnableRaisingEvents = true;
    }

    public void Stop()
    {
      _pngFileSystemWatcher.EnableRaisingEvents = false;
      _jpgFileSystemWatcher.EnableRaisingEvents = false;

      StopThread();
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
      Console.WriteLine("Enqueing File To Be Processed: " + e.FullPath + " " + e.ChangeType);
      tsFilesToProcess.Enqueue(e.FullPath);
    }
    #endregion

    #region AsyncConsumer

    private void StartThread()
    {
      _isRunning = true;
      _sendPictureProcess.Start();
    }
    private void StopThread()
    {
      _isRunning = false;
      _sendPictureProcess.Join();
    }

    private void ProcessImage()
    {
      string fileToProess;
      FileInfo fileInfo;

      while (_isRunning)
      {
        while( tsFilesToProcess.TryDequeue(out fileToProess) )
        {
          fileInfo = new FileInfo(fileToProess);
          while( IsFileLocked(fileInfo) )
          {
            Console.WriteLine("Waiting for to be available : " + fileInfo.FullName);
            //wait here - sleep thread for a few miliseconds maybe?
          }

          Console.WriteLine("File available sending picture : " + fileInfo.FullName);
          SendPicture(fileToProess);
        }
      }
    }

    private void SendPicture( string imageFilepath)
    {
      FileInfo imageInfo = new FileInfo(imageFilepath);
      string newFilePath = _localDataDirectoryPath + "//" + imageInfo.Name;
      long originalImageFileSize = imageInfo.Length;

      if (!File.Exists(newFilePath) && File.Exists(imageFilepath))
        File.Move(imageFilepath, newFilePath);
      else
        File.Delete(imageFilepath);

      // only resize the image if it s larger then a Megabyte
      string cachedImageFilePath;
      const long oneMegabytesInBytes = 1000000;
      if (originalImageFileSize >= oneMegabytesInBytes)
      {
        cachedImageFilePath = _cachedImageDataDirectoryPath + "//" + imageInfo.Name;
        CacheImage(newFilePath, cachedImageFilePath);
      }
      else
      {
        cachedImageFilePath = newFilePath;
      }

      ShowerPicture picture = new ShowerPicture
      {
        AbsolutePath = newFilePath,
        CachedImageFilePath = cachedImageFilePath
      };

      foreach (var listener in _listeners)
      {
        listener.OnFetchPicture(picture);
      }
    }

    private void CacheImage( string movedPicture, string cachedPicturePath )
    {
      if (File.Exists(cachedPicturePath) )
        return;

      Image srcImage = Image.FromFile(movedPicture);
      int cachedWidth = srcImage.Width / 10;
      int cachedHeight = srcImage.Height / 10;
      Bitmap newImage = new Bitmap(cachedWidth, cachedHeight);
      using (Graphics gr = Graphics.FromImage(newImage))
      {
        gr.SmoothingMode = SmoothingMode.HighQuality;
        gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
        gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
        gr.DrawImage(srcImage, new Rectangle(0, 0, cachedWidth, cachedHeight));
      }
      newImage.Save(cachedPicturePath);
    }
    /// <summary>
    /// A method to ping if the file is still being used by the FileSystemWatcher.
    /// Seems a bit intensive as a check but will be good enough for now.
    /// Ref: http://stackoverflow.com/questions/876473/is-therehttp://stackoverflow.com/questions/876473/is-there-a-way-to-check-if-a-file-is-in-use-a-way-to-check-if-a-file-is-in-use
    /// note - may cause race condition if file is acquired again after it was accessed.
    ///        should look into other methods maybe?
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    protected virtual bool IsFileLocked(FileInfo file)
    {
      FileStream stream = null;
      try
      {
        stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
      }
      catch (IOException)
      {
        //the file is unavailable because it is:
        //still being written to
        //or being processed by another thread
        //or does not exist (has already been processed)
        return true;
      }
      finally
      {
        if (stream != null)
          stream.Close();
      }

      //file is not locked
      return false;
    }
    #endregion

    #region Helpers
    private void CreateTempDirectory()
    {
      if (!Directory.Exists(_localDataDirectoryPath))
        Directory.CreateDirectory(_localDataDirectoryPath);

      if (!Directory.Exists(_cachedImageDataDirectoryPath))
        Directory.CreateDirectory(_cachedImageDataDirectoryPath);
    }
    #endregion
  }
}
