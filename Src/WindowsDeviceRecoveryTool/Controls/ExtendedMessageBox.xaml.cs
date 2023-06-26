using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000D5 RID: 213
	public sealed partial class ExtendedMessageBox : Window
	{
		// Token: 0x060006A8 RID: 1704 RVA: 0x0001EE06 File Offset: 0x0001D006
		public ExtendedMessageBox()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001EE18 File Offset: 0x0001D018
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x0001EE3A File Offset: 0x0001D03A
		public string MessageBoxText
		{
			get
			{
				return (string)base.GetValue(ExtendedMessageBox.MessageBoxTextProperty);
			}
			set
			{
				base.SetValue(ExtendedMessageBox.MessageBoxTextProperty, value);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0001EE4C File Offset: 0x0001D04C
		public BitmapSource BoxIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0001EE60 File Offset: 0x0001D060
		public Visibility AdvanceVisibility
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this.MessageBoxAdvance);
				Visibility visibility;
				if (flag)
				{
					visibility = Visibility.Collapsed;
				}
				else
				{
					visibility = Visibility.Visible;
				}
				return visibility;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0001EE88 File Offset: 0x0001D088
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x0001EEBC File Offset: 0x0001D0BC
		public string MessageBoxAdvance
		{
			get
			{
				bool flag = this.AdvanceTextBox != null;
				string text;
				if (flag)
				{
					text = this.AdvanceTextBox.Text;
				}
				else
				{
					text = string.Empty;
				}
				return text;
			}
			set
			{
				bool flag = this.AdvanceTextBox != null;
				if (flag)
				{
					this.AdvanceTextBox.Text = value;
				}
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0001EEE8 File Offset: 0x0001D0E8
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x0001EF0A File Offset: 0x0001D10A
		public Style ButtonPanelStyle
		{
			get
			{
				return (Style)base.GetValue(ExtendedMessageBox.ButtonPanelStyleProperty);
			}
			set
			{
				base.SetValue(ExtendedMessageBox.ButtonPanelStyleProperty, value);
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001EF1C File Offset: 0x0001D11C
		public static void Show(string messageBoxText, string caption, string messageBoxAdvance)
		{
			ExtendedMessageBox extendedMessageBox = new ExtendedMessageBox
			{
				Title = caption,
				MessageBoxText = messageBoxText,
				MessageBoxAdvance = messageBoxAdvance
			};
			bool isRightToLeft = Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft;
			if (isRightToLeft)
			{
				extendedMessageBox.FlowDirection = FlowDirection.RightToLeft;
			}
			else
			{
				extendedMessageBox.FlowDirection = FlowDirection.LeftToRight;
			}
			extendedMessageBox.ShowDialog();
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001EF7D File Offset: 0x0001D17D
		private void ButtonClick(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001EF87 File Offset: 0x0001D187
		private void ExtendedMessageBoxLoaded(object sender, RoutedEventArgs e)
		{
			base.MaxHeight = base.Height;
			base.MinHeight = base.Height;
		}

		// Token: 0x04000301 RID: 769
		public static readonly DependencyProperty MessageBoxTextProperty = DependencyProperty.Register("MessageBoxText", typeof(string), typeof(ExtendedMessageBox));

		// Token: 0x04000302 RID: 770
		public static readonly DependencyProperty ButtonPanelStyleProperty = DependencyProperty.Register("ButtonPanelStyle", typeof(Style), typeof(ExtendedMessageBox));
	}
}
