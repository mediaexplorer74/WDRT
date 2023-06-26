using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Converters
{
	// Token: 0x0200000C RID: 12
	public class InvertedBooleanToVisibilityConverter : IValueConverter
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002724 File Offset: 0x00000924
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool flag = false;
			bool flag2 = value is bool;
			if (flag2)
			{
				flag = (bool)value;
			}
			return flag ? Visibility.Collapsed : Visibility.Visible;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000275C File Offset: 0x0000095C
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool flag = value is Visibility;
			object obj;
			if (flag)
			{
				obj = (Visibility)value > Visibility.Visible;
			}
			else
			{
				obj = false;
			}
			return obj;
		}
	}
}
