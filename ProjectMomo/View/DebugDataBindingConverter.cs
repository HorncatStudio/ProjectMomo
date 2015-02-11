using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace ProjectMomo
{
  /// <summary>
  /// A Helper class recommended from a blog post 
  /// http://spin.atomicobject.com/2013/12/11/wpf-data-binding-debug/
  ///  in order to help track data binding issues.
  /// 
  /// Is super helpful!
  ///  
  /// Add the , Converter={StaticResource DebugBinding} to the datababding, and include this class as a resource 
  ///     <Grid.Resources>
  ///    <projectMomo:DebugDataBindingConverter x:Key="DebugBinding"/>
  ///    </Grid.Resources>
  /// </summary>
  public class DebugDataBindingConverter : IValueConverter
  {
    public object Convert(object value, Type targetType,
        object parameter, CultureInfo culture)
    {
      Debugger.Break();
      return value;
    }

    public object ConvertBack(object value, Type targetType,
        object parameter, CultureInfo culture)
    {
      Debugger.Break();
      return value;
    }
  }
}