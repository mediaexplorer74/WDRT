using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000CD RID: 205
	public sealed partial class NotificationControl : Border
	{
		// Token: 0x06000650 RID: 1616 RVA: 0x0001DFD2 File Offset: 0x0001C1D2
		public NotificationControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x0001DFE4 File Offset: 0x0001C1E4
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x0001E006 File Offset: 0x0001C206
		public Brush Foreground
		{
			get
			{
				return (Brush)base.GetValue(NotificationControl.ForegroundProperty);
			}
			set
			{
				base.SetValue(NotificationControl.ForegroundProperty, value);
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0001E018 File Offset: 0x0001C218
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x0001E03A File Offset: 0x0001C23A
		public bool? ShowNotification
		{
			get
			{
				return (bool?)base.GetValue(NotificationControl.ShowNotificationProperty);
			}
			set
			{
				base.SetValue(NotificationControl.ShowNotificationProperty, value);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x0001E050 File Offset: 0x0001C250
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x0001E072 File Offset: 0x0001C272
		public string NotificationHeader
		{
			get
			{
				return (string)base.GetValue(NotificationControl.NotificationHeaderProperty);
			}
			set
			{
				base.SetValue(NotificationControl.NotificationHeaderProperty, value);
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x0001E084 File Offset: 0x0001C284
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x0001E0A6 File Offset: 0x0001C2A6
		public string NotificationText
		{
			get
			{
				return (string)base.GetValue(NotificationControl.NotificationTextProperty);
			}
			set
			{
				base.SetValue(NotificationControl.NotificationTextProperty, value);
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x0001E0B8 File Offset: 0x0001C2B8
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x0001E0DA File Offset: 0x0001C2DA
		public Style HeaderStyle
		{
			get
			{
				return (Style)base.GetValue(NotificationControl.HeaderStyleProperty);
			}
			set
			{
				base.SetValue(NotificationControl.HeaderStyleProperty, value);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x0001E0EC File Offset: 0x0001C2EC
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x0001E10E File Offset: 0x0001C30E
		public Style MessageStyle
		{
			get
			{
				return (Style)base.GetValue(NotificationControl.MessageStyleProperty);
			}
			set
			{
				base.SetValue(NotificationControl.MessageStyleProperty, value);
			}
		}

		// Token: 0x040002DC RID: 732
		public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(NotificationControl), new PropertyMetadata(null));

		// Token: 0x040002DD RID: 733
		public static readonly DependencyProperty NotificationHeaderProperty = DependencyProperty.Register("NotificationHeader", typeof(string), typeof(NotificationControl), new PropertyMetadata(null));

		// Token: 0x040002DE RID: 734
		public static readonly DependencyProperty NotificationTextProperty = DependencyProperty.Register("NotificationText", typeof(string), typeof(NotificationControl), new PropertyMetadata(null));

		// Token: 0x040002DF RID: 735
		public static readonly DependencyProperty HeaderStyleProperty = DependencyProperty.Register("HeaderStyle", typeof(Style), typeof(NotificationControl), new PropertyMetadata(null));

		// Token: 0x040002E0 RID: 736
		public static readonly DependencyProperty MessageStyleProperty = DependencyProperty.Register("MessageStyle", typeof(Style), typeof(NotificationControl), new PropertyMetadata(null));

		// Token: 0x040002E1 RID: 737
		public static readonly DependencyProperty ShowNotificationProperty = DependencyProperty.Register("ShowNotification", typeof(bool?), typeof(NotificationControl), new PropertyMetadata(null));
	}
}
