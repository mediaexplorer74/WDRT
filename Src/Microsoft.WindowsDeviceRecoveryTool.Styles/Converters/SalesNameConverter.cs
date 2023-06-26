using System;
using System.Globalization;
using System.Windows.Data;
using Microsoft.WindowsDeviceRecoveryTool.Localization;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Converters
{
	// Token: 0x02000006 RID: 6
	public class SalesNameConverter : IValueConverter
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002504 File Offset: 0x00000704
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string text = value as string;
			bool flag = text != null;
			if (flag)
			{
				bool flag2 = text == "DeviceInUefiMode";
				if (flag2)
				{
					return LocalizationManager.GetTranslation("DeviceInUefiMode");
				}
			}
			return value;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024A2 File Offset: 0x000006A2
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
