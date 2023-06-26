using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Microsoft.WindowsDeviceRecoveryTool.Localization;

namespace Microsoft.WindowsDeviceRecoveryTool.Controls
{
	// Token: 0x020000CC RID: 204
	public class MetroTextBlockDialog : MetroDialog
	{
		// Token: 0x06000648 RID: 1608 RVA: 0x0001DEA4 File Offset: 0x0001C0A4
		public MetroTextBlockDialog()
		{
			base.YesButtonFocused = true;
			this.BodyMessagePanel.Visibility = Visibility.Collapsed;
			this.textBox = new TextBox
			{
				VerticalAlignment = VerticalAlignment.Center
			};
			this.textBox.GotKeyboardFocus += delegate(object s, KeyboardFocusChangedEventArgs e)
			{
				((TextBoxBase)s).SelectAll();
			};
			AutomationProperties.SetName(this.textBox, LocalizationManager.GetTranslation("FolderName"));
			base.Loaded += delegate
			{
				this.textBox.Focus();
			};
			this.GridContent.Children.Add(this.textBox);
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0001DF4C File Offset: 0x0001C14C
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x0001DF69 File Offset: 0x0001C169
		public string InputText
		{
			get
			{
				return this.textBox.Text;
			}
			set
			{
				this.textBox.Text = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0001DF7C File Offset: 0x0001C17C
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x0001DF94 File Offset: 0x0001C194
		public new string NoButtonText
		{
			get
			{
				return base.NoButtonText;
			}
			set
			{
				base.NoButtonText = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0001DFA0 File Offset: 0x0001C1A0
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x0001DFB8 File Offset: 0x0001C1B8
		public new string YesButtonText
		{
			get
			{
				return base.YesButtonText;
			}
			set
			{
				base.YesButtonText = value;
			}
		}

		// Token: 0x040002DB RID: 731
		private readonly TextBox textBox;
	}
}
