using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace ProjectMomo.Helpers
{
  /// <summary>
  /// Interaction logic for CreateImportDialog.xaml
  /// </summary>
  public partial class CreateImportDialog : Window
  {
    #region Code Neccesary for disabling system bar
    private const int GWL_STYLE = -16;
    private const int WS_SYSMENU = 0x80000;
    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    #endregion

    public enum LoadResult { Import, Create, None };

    public RelayCommand CreateShowerCommand { get; set; }
    public RelayCommand ImportShowerCommand { get; set; }

    public LoadResult Result { get; set; }

    public CreateImportDialog()
    {
      InitializeComponent();
      CreateShowerCommand = new RelayCommand(new Action<object>(SetCreateShowerOption));
      ImportShowerCommand = new RelayCommand(new Action<object>(SetImportShowerOption));

      DataContext = this;
      Result = LoadResult.None;

      this.Loaded += OnWindowLoaded;
    }

    private void OnWindowLoaded(object sender, RoutedEventArgs e)
    {
      var hwnd = new WindowInteropHelper(this).Handle;
      SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
    }

    private void SetImportShowerOption(object obj)
    {
      Result = LoadResult.Import;
      DialogResult = true;
      Close();
    }

    private void SetCreateShowerOption(object obj)
    {
      Result = LoadResult.Create;
      DialogResult = true;
      Close();
    }
  }
}
