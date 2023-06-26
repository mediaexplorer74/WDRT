using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Converters
{
	// Token: 0x0200000D RID: 13
	public class ObjectNullToVisibilityConverter : IValueConverter
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002794 File Offset: 0x00000994
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (value == null) ? Visibility.Collapsed : Visibility.Visible;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000024A2 File Offset: 0x000006A2
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
