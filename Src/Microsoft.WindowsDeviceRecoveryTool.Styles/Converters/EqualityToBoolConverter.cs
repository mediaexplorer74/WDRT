using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Converters
{
	// Token: 0x0200000A RID: 10
	public class EqualityToBoolConverter : IValueConverter
	{
		// Token: 0x06000028 RID: 40 RVA: 0x0000268C File Offset: 0x0000088C
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return parameter != null && parameter.Equals(value);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026B0 File Offset: 0x000008B0
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool? flag = value as bool?;
			bool? flag2 = flag;
			bool flag3 = true;
			return ((flag2.GetValueOrDefault() == flag3) & (flag2 != null)) ? parameter : DependencyProperty.UnsetValue;
		}
	}
}
