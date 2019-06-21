using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ConwaysGameOfLife.Converters
{
	/// <summary>
	/// Converts boolean value to visibility value. For true sets Collapsed, for false sets Visible.
	/// </summary>
	public class InverseBooleanToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var booleanValue = value == null || (bool)value;

			return booleanValue ? Visibility.Collapsed : Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
