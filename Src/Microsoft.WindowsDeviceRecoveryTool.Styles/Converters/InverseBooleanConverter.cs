using System;
using System.Globalization;
using System.Windows.Data;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Converters
{
	// Token: 0x0200000B RID: 11
	public class InverseBooleanConverter : IValueConverter
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000026F0 File Offset: 0x000008F0
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool flag = false;
			bool flag2 = value is bool;
			if (flag2)
			{
				flag = (bool)value;
			}
			return !flag;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000024A2 File Offset: 0x000006A2
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
