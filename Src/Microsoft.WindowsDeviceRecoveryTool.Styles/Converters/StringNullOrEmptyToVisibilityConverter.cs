using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Converters
{
	// Token: 0x0200000E RID: 14
	[ValueConversion(typeof(string), typeof(Visibility))]
	public sealed class StringNullOrEmptyToVisibilityConverter : IValueConverter
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000027B2 File Offset: 0x000009B2
		public StringNullOrEmptyToVisibilityConverter()
		{
			this.Collapse = true;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000027C4 File Offset: 0x000009C4
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000027CC File Offset: 0x000009CC
		public bool Collapse { get; set; }

		// Token: 0x06000037 RID: 55 RVA: 0x000027D8 File Offset: 0x000009D8
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string text = value as string;
			bool flag = !string.IsNullOrEmpty(text);
			Visibility visibility;
			if (flag)
			{
				visibility = Visibility.Visible;
			}
			else
			{
				visibility = (this.Collapse ? Visibility.Collapsed : Visibility.Hidden);
			}
			return visibility;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000281C File Offset: 0x00000A1C
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
