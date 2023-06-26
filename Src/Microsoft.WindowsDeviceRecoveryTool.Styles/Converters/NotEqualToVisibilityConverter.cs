using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Converters
{
	// Token: 0x02000004 RID: 4
	public class NotEqualToVisibilityConverter : IValueConverter
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000024B4 File Offset: 0x000006B4
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (parameter != null && !parameter.Equals(value)) ? Visibility.Visible : Visibility.Collapsed;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024A2 File Offset: 0x000006A2
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
