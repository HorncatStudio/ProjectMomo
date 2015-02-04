using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EDSDKLib;

namespace ProjectMomo_LiveCamera
{
  public class CanonCameraSdkSdk : AProjectMomoCameraSdk
  {
    public SDKHandler CanonSdkHandler;

    #region LiveViewMembers

    /// <summary>
    /// Thread  responsible for maintaing the live view update. </summary>
    private ThreadStart _setImageAction;

    private ImageBrush BackgroundBrush;
    private ImageSource LiveImage;
    private JpegBitmapDecoder _decoder;
    #endregion

    public CanonCameraSdkSdk()
    {
      CanonSdkHandler = new EDSDKLib.SDKHandler();
      CanonSdkHandler.CameraAdded += new SDKHandler.CameraAddedHandler(SDK_CameraAdded);
      CanonSdkHandler.LiveViewUpdated += new SDKHandler.StreamUpdate(SDK_LiveViewUpdated);
      CanonSdkHandler.ProgressChanged += new SDKHandler.ProgressHandler(SDK_ProgressChanged);
      CanonSdkHandler.CameraHasShutdown += CloseSession;

      BackgroundBrush = new ImageBrush();

      _setImageAction = new ThreadStart(delegate { BackgroundBrush.ImageSource = LiveImage; });

      LiveViewCanvas = null;
    }

    public override void Dispose()
    {
      CanonSdkHandler.Dispose();
    }
     
    #region AProjectMomoCameraSdk Implementaiton

    void InitializeCamera(Canvas liveViewCanvas)
    {
      LiveViewCanvas = liveViewCanvas;
    }

    public override List<CanonCamera> GetCameraList()
    {
      List<CanonCamera> canonCameras = new List<CanonCamera>();

      List<Camera> cameras = CanonSdkHandler.GetCameraList();
      foreach (var camera in cameras)
      {
        canonCameras.Add(new CanonCamera(camera));
      }

      return canonCameras;
    }

    public override void OpenSession(CanonCamera openCamera)
    {
      if (openCamera.GetCamera() == null)
        return;

      if (CanonSdkHandler.CameraSessionOpen) 
        CloseSession();
        
      CanonSdkHandler.OpenSession(openCamera.GetCamera());
      IsCameraSet = CanonSdkHandler.CameraSessionOpen;
    }

    public override void CloseSession()
    {
      if (CanonSdkHandler.CameraSessionOpen)
      {
        CanonSdkHandler.CloseSession();
        IsCameraSet = CanonSdkHandler.CameraSessionOpen;
      }
    }

    public override void StartLiveView()
    {
      if (!IsCanvasSet())
        return;

      if (!CanonSdkHandler.CameraSessionOpen)
        return;

      LiveViewCanvas.Background = Brushes.Red;
      
      LiveViewCanvas.Background = BackgroundBrush;
      CanonSdkHandler.StartLiveView();
    }

    public override void StopLiveView()
    {
      if (!IsCanvasSet())
        return; 
      CanonSdkHandler.StopLiveView();
      LiveViewCanvas.Background = Brushes.LightGray;
    }

    public override bool IsCameraLive()
    {
      return CanonSdkHandler.IsLiveViewOn;
    }

    #endregion

    #region ESDK Unique Events
    private void CloseSession(object sender, EventArgs e)
    {
      CloseSession();
    }

    private void SDK_LiveViewUpdated(Stream img)
    {
      if (CanonSdkHandler.IsLiveViewOn)
      {
        img.Position = 0;
        _decoder = new JpegBitmapDecoder(img, BitmapCreateOptions.None, BitmapCacheOption.None);
        LiveImage = _decoder.Frames[0];
        Application.Current.Dispatcher.Invoke(_setImageAction);
      }
    }

    /// <summary>
    /// May not need this
    /// </summary>
    /// <param name="Progress"></param>
    private void SDK_ProgressChanged(int Progress)
    {
      //if (Progress == 100) Progress = 0;
      //MainProgressBar.Value = Progress;
    }

    private void SDK_CameraAdded()
    {
      Console.WriteLine("New camera founded.");
    }
    #endregion

  }
}