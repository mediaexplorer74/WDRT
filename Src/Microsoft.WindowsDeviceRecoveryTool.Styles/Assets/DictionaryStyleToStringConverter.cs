using System;
using System.Globalization;
using System.Windows.Data;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Assets
{
	// Token: 0x02000018 RID: 24
	public class DictionaryStyleToStringConverter : IValueConverter
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00003620 File Offset: 0x00001820
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return StyleLogic.GetStyle((string)value);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003640 File Offset: 0x00001840
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			DictionaryStyle dictionaryStyle = (DictionaryStyle)value;
			return dictionaryStyle.Name;
		}
	}
}
