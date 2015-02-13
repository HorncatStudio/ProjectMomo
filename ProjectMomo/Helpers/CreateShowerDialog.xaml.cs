using System;
using System.Windows;
using ProjectMomo.Model;

namespace ProjectMomo.Helpers
{
  /// <summary>
  /// Interaction logic for CreateShowerDialog.xaml
  /// </summary>
  public partial class CreateShowerDialog : Window
  {
    public Shower CreatedShower { get; set; }

    public RelayCommand CreateShowerCommand { get; set; }

    public CreateShowerDialog()
    {
      InitializeComponent();
      CreatedShower = new Shower();
      CreateShowerCommand = new RelayCommand(new Action<object>(CreateShowerAction));
      DataContext = this;
    }

    private void CreateShowerAction(object obj)
    {
      DialogResult = true;
      Close();
    }
  }
}
