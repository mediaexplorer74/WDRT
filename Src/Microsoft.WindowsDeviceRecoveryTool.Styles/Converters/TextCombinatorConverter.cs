using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Converters
{
	// Token: 0x0200000F RID: 15
	public class TextCombinatorConverter : IMultiValueConverter
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002834 File Offset: 0x00000A34
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			bool flag = values == null || !values.Any<object>();
			object obj;
			if (flag)
			{
				obj = string.Empty;
			}
			else
			{
				object[] array = new object[values.Length - 1];
				Array.Copy(values, array, values.Length - 1);
				bool flag2 = array.All((object value) => !string.IsNullOrEmpty(value as string));
				if (flag2)
				{
					obj = string.Join(string.Empty, array);
				}
				else
				{
					obj = values.Last<object>();
				}
			}
			return obj;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000024A2 File Offset: 0x000006A2
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
