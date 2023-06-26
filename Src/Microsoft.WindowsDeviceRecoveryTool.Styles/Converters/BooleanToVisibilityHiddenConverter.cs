using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Converters
{
	// Token: 0x02000003 RID: 3
	public class BooleanToVisibilityHiddenConverter : IValueConverter
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000246C File Offset: 0x0000066C
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool flag = false;
			bool flag2 = value is bool;
			if (flag2)
			{
				flag = (bool)value;
			}
			return flag ? Visibility.Visible : Visibility.Hidden;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000024A2 File Offset: 0x000006A2
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
