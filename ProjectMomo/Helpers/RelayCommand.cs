using System;
using System.Windows.Input;

namespace ProjectMomo.Helpers
{
  /// <summary>
  /// Sample RelayCommand to forward commands in the application.
  /// 
  /// Borrowed this implementation from the internets somewhere.  So commonly referenced, it is difficult
  /// to find a source for it.
  /// 
  /// Would be replaced if using a MVVM framework in the future.
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