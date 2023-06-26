using System;
using System.Windows;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Settings
{
	// Token: 0x0200002B RID: 43
	public sealed class SettingsPageListItem : DependencyObject
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000BEC4 File Offset: 0x0000A0C4
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000BEE6 File Offset: 0x0000A0E6
		public string Title
		{
			get
			{
				return (string)base.GetValue(SettingsPageListItem.TitleProperty);
			}
			set
			{
				base.SetValue(SettingsPageListItem.TitleProperty, value);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
		// (set) Token: 0x060001EE RID: 494 RVA: 0x0000BF1A File Offset: 0x0000A11A
		public SettingsPage Page
		{
			get
			{
				return (SettingsPage)base.GetValue(SettingsPageListItem.PageProperty);
			}
			set
			{
				base.SetValue(SettingsPageListItem.PageProperty, value);
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000BF30 File Offset: 0x0000A130
		public override string ToString()
		{
			return this.Title;
		}

		// Token: 0x040000EC RID: 236
		public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(SettingsPageListItem));

		// Token: 0x040000ED RID: 237
		public static readonly DependencyProperty PageProperty = DependencyProperty.Register("Page", typeof(SettingsPage), typeof(SettingsPageListItem));
	}
}
