using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using ProjectMomo.Helpers;

namespace ProjectMomo_LiveCamera
{
  /// <summary>
  /// Responsible for the displaying of camera live view that is currently selected.
  /// </summary>
  public class LiveCameraViewModel : INotifyPropertyChanged
  {
    private AProjectMomoCameraSdk _cameraSdkModel;

    #region Image Resources
    /// <summary>
    /// Resource for the > arrow button image  </summary>
    private ImageSource _playImage;
    /// <summary>
    /// Resource for the > arrow button image  </summary>
    private ImageSource _stopImage;
    #endregion

    /// <summary>
    /// Image that is used for the Play/Stop button. </summary>
    private ImageSource _playStopImage;
    public ImageSource PlayStopButtonImage
    {
      get { return _playStopImage; }
      set
      {
        _playStopImage = value;
        OnPropertyChanged();
      } 
    }

    /// <summary>
    /// Commands to do the start/stop action and open camera settings action. </summary>
    public RelayCommand StartStopLiveViewCommand { get; set; }
    public RelayCommand CameraSettingsCommand { get; set; }

    /// <summary>
    /// Public attribute that determines if a camera has been chosen to be displayed. </summary>
    public bool IsCameraSet
    {
      get { return _cameraSdkModel.IsCameraSet; }
    }

    /// <summary>
    /// Constructor of the view model.  Injects the CameraSDK to be used for displaying the live view and retrieve available cameras. </summary>
    /// <param name="cameraSdk"></param>
    public LiveCameraViewModel( AProjectMomoCameraSdk cameraSdk )
    {
      _cameraSdkModel = cameraSdk;
      StartStopLiveViewCommand = new RelayCommand(new Action<object>(OnStartStopLiveView));

      _stopImage = Application.Current.Resources["StopImage"] as ImageSource;
      _playImage = Application.Current.Resources["StartImage"] as ImageSource;
      PlayStopButtonImage = _playImage;
      CameraSettingsCommand = new RelayCommand(new Action<object>(OnCameraSettingsDisplay));
    }

    #region Camera Actions 
    private void OnCameraSettingsDisplay(object obj)
    {
      CameraDialog dialog = new CameraDialog(_cameraSdkModel);

      if (dialog.ShowDialog() != true)
        return;
  
      _cameraSdkModel.OpenSession(dialog.SelectedCamera);
      OnPropertyChanged("IsCameraSet");        
    }

    private void OnStartStopLiveView(object obj)
    {
      if ( _cameraSdkModel.IsCameraLive() )
      {
        StopLiveView();
      }
      else
      {
        StartLiveView();
      }
    }

    private void StartLiveView()
    {
      PlayStopButtonImage = _stopImage;
      _cameraSdkModel.StartLiveView();
    }

    private void StopLiveView()
    {
      PlayStopButtonImage = _playImage;
      _cameraSdkModel.StopLiveView();
    }
    #endregion

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      var handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
