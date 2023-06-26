using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Converters
{
	// Token: 0x02000008 RID: 8
	public sealed class GenericBoolConverter : IValueConverter
	{
		// Token: 0x0600001E RID: 30 RVA: 0x0000256F File Offset: 0x0000076F
		public GenericBoolConverter()
		{
			this.TrueValue = Visibility.Visible;
			this.FalseValue = Visibility.Collapsed;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002593 File Offset: 0x00000793
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000259B File Offset: 0x0000079B
		public object TrueValue { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000025A4 File Offset: 0x000007A4
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000025AC File Offset: 0x000007AC
		public object FalseValue { get; set; }

		// Token: 0x06000023 RID: 35 RVA: 0x000025B8 File Offset: 0x000007B8
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool flag = !(value is bool);
			object obj;
			if (flag)
			{
				obj = null;
			}
			else
			{
				obj = (((bool)value) ? this.TrueValue : this.FalseValue);
			}
			return obj;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025F8 File Offset: 0x000007F8
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool flag = object.Equals(value, this.TrueValue);
			object obj;
			if (flag)
			{
				obj = true;
			}
			else
			{
				bool flag2 = object.Equals(value, this.FalseValue);
				if (flag2)
				{
					obj = false;
				}
				else
				{
					obj = null;
				}
			}
			return obj;
		}
	}
}
