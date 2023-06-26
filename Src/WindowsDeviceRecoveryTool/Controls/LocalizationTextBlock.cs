using System;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000CA RID: 202
	public class LocalizationTextBlock : TextBlock
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0001DC64 File Offset: 0x0001BE64
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x0001DC86 File Offset: 0x0001BE86
		public string LocalizationText
		{
			get
			{
				return (string)base.GetValue(LocalizationTextBlock.LocalizationTextProperty);
			}
			set
			{
				base.SetValue(LocalizationTextBlock.LocalizationTextProperty, value);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0001DC98 File Offset: 0x0001BE98
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x0001DCBA File Offset: 0x0001BEBA
		public string NullValue
		{
			get
			{
				return (string)base.GetValue(LocalizationTextBlock.NullValueProperty);
			}
			set
			{
				base.SetValue(LocalizationTextBlock.NullValueProperty, value);
			}
		}

		// Token: 0x040002D4 RID: 724
		public static readonly DependencyProperty LocalizationTextProperty = DependencyProperty.Register("LocalizationText", typeof(string), typeof(LocalizationTextBlock), new PropertyMetadata(null));

		// Token: 0x040002D5 RID: 725
		public static readonly DependencyProperty NullValueProperty = DependencyProperty.Register("NullValue", typeof(string), typeof(LocalizationTextBlock), new PropertyMetadata(null));
	}
}
