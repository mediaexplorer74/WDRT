using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Converters
{
	// Token: 0x02000005 RID: 5
	public class EqualToVisibilityConverter : IValueConverter
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000024DC File Offset: 0x000006DC
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (parameter != null && parameter.Equals(value)) ? Visibility.Visible : Visibility.Collapsed;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024A2 File Offset: 0x000006A2
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
