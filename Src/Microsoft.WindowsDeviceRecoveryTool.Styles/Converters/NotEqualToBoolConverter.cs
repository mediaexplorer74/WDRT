using System;
using System.Globalization;
using System.Windows.Data;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Converters
{
	// Token: 0x02000007 RID: 7
	public class NotEqualToBoolConverter : IValueConverter
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002548 File Offset: 0x00000748
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return parameter != null && !parameter.Equals(value);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024A2 File Offset: 0x000006A2
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
