using System;
using System.Globalization;
using System.Windows.Data;
using Microsoft.WindowsDeviceRecoveryTool.Localization;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Converters
{
	// Token: 0x02000009 RID: 9
	public class BoolToOffOnConverter : IValueConverter
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002640 File Offset: 0x00000840
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string text = string.Empty;
			bool flag = value is bool;
			if (flag)
			{
				text = (((bool)value) ? LocalizationManager.GetTranslation("ButtonOn") : LocalizationManager.GetTranslation("ButtonOff"));
			}
			return text;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024A2 File Offset: 0x000006A2
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
