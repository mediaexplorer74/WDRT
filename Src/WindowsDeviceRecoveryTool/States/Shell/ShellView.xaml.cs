using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Microsoft.WindowsDeviceRecoveryTool.Controls;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Shell
{
	// Token: 0x0200000B RID: 11
	[Export]
	public sealed partial class ShellView : ContentControl
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00004870 File Offset: 0x00002A70
		public ShellView()
		{
			this.InitializeComponent();
			this.animation = Application.Current.Resources["StartAnimationStoryboard"] as Storyboard;
			this.focusDelayTimer = (DispatcherTimer)base.Resources["FocusDelayTimer"];
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000048C8 File Offset: 0x00002AC8
		private void FocusDelayTimer_OnTick(object sender, EventArgs e)
		{
			this.focusDelayTimer.Stop();
			bool flag = !this.GenericRoot.IsKeyboardFocusWithin || this.BackButton.IsKeyboardFocused;
			if (flag)
			{
				this.GenericRoot.Focus();
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000490F File Offset: 0x00002B0F
		private void ShellView_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.SettingsControl.SetCurrentValue(SettingsControl.IsOpenedProperty, !this.SettingsControl.IsOpened);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004938 File Offset: 0x00002B38
		private void ShellView_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			bool isOpened = this.SettingsControl.IsOpened;
			if (isOpened)
			{
				this.SettingsControl.SetCurrentValue(SettingsControl.IsOpenedProperty, false);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000496E File Offset: 0x00002B6E
		private void MainContent_OnContentChanged(object sender, RoutedEventArgs e)
		{
			this.animation.Begin(this.GenericRoot);
			Keyboard.ClearFocus();
			this.focusDelayTimer.Start();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004995 File Offset: 0x00002B95
		private void ShellView_OnPreviewKeyDown(object sender, KeyEventArgs e)
		{
			this.SettingsControl.SetCurrentValue(SettingsControl.IsOpenedProperty, false);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000049AF File Offset: 0x00002BAF
		private void ShellView_OnUnloaded(object sender, RoutedEventArgs e)
		{
			this.focusDelayTimer.Stop();
		}

		// Token: 0x0400005E RID: 94
		private readonly Storyboard animation;

		// Token: 0x0400005F RID: 95
		private readonly DispatcherTimer focusDelayTimer;
	}
}
