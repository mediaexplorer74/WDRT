using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using Microsoft.WindowsDeviceRecoveryTool.Controls;
using Microsoft.WindowsDeviceRecoveryTool.Controls.TextBoxes;
using Microsoft.WindowsDeviceRecoveryTool.Controls.TextBoxes.Validation;
using Microsoft.WindowsDeviceRecoveryTool.Framework;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Settings
{
	// Token: 0x0200002F RID: 47
	[Export]
	[Region(new string[] { "SettingsMainArea" })]
	public partial class NetworkView : StackPanel
	{
		// Token: 0x060001FD RID: 509 RVA: 0x0000C323 File Offset: 0x0000A523
		public NetworkView()
		{
			this.InitializeComponent();
			this.capsLockTimer = (DispatcherTimer)base.FindResource("CapsLockTimer");
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000C34A File Offset: 0x0000A54A
		private void CapsLockTimer_OnTick(object sender, EventArgs e)
		{
			this.CapsLockTextBlock.IsEnabled = this.PasswordBox.IsKeyboardFocused && Keyboard.IsKeyToggled(Key.Capital);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000C36F File Offset: 0x0000A56F
		private void NetworkView_OnLoaded(object sender, RoutedEventArgs e)
		{
			this.capsLockTimer.Start();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000C37E File Offset: 0x0000A57E
		private void NetworkView_OnUnloaded(object sender, RoutedEventArgs e)
		{
			this.capsLockTimer.Stop();
		}

		// Token: 0x040000F8 RID: 248
		private readonly DispatcherTimer capsLockTimer;
	}
}
