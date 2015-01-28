using System;
using System.Windows.Input;

namespace ProjectMomo.Helpers
{
  /// <summary>
  /// Sample RelayCommand to forward commands in the application.
  /// </summary>
  public class RelayCommand : ICommand
  {
    private Action<object> _action;
    public RelayCommand(Action<object> action)
    {
      _action = action;
    }
    
    public bool CanExecute(object parameter)
    {
      return true;
    }

    /// <summary>
    /// Executes the action according to the paramater.  If not parameter is provided then no action is executed.
    /// </summary>
    /// <param name="parameter"></param>
    public void Execute(object parameter)
    {
      _action(parameter);
    }

    public event EventHandler CanExecuteChanged;
  }
}