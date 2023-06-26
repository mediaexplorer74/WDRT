using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool
{
	// Token: 0x02000008 RID: 8
	[Export]
	public class MainWindowViewModel : BaseViewModel
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002578 File Offset: 0x00000778
		public MainWindowViewModel()
		{
			this.ConfigureWindowButton();
			this.AppName = string.Format("{0} {1}", AppInfo.AppTitle(), AppInfo.AppVersion());
			bool flag = ApplicationInfo.IsInternal();
			if (flag)
			{
				this.AppName = "[INT] " + this.AppName;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000025D4 File Offset: 0x000007D4
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000025EC File Offset: 0x000007EC
		public string AppName
		{
			get
			{
				return this.appName;
			}
			set
			{
				base.SetValue<string>(() => this.AppName, ref this.appName, value);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000262C File Offset: 0x0000082C
		private void ConfigureWindowButton()
		{
			Type typeFromHandle = typeof(MainWindow);
			CommandBinding commandBinding = new CommandBinding(SystemCommands.MinimizeWindowCommand, new ExecutedRoutedEventHandler(this.MinimizeWindowCommandOnExecuted), new CanExecuteRoutedEventHandler(this.MinimizeWindowCommandOnCanExecute));
			CommandBinding commandBinding2 = new CommandBinding(SystemCommands.MaximizeWindowCommand, new ExecutedRoutedEventHandler(this.MaximizeWindowCommandOnExecuted), new CanExecuteRoutedEventHandler(this.MaximizeWindowCommandOnCanExecute));
			CommandBinding commandBinding3 = new CommandBinding(SystemCommands.RestoreWindowCommand, new ExecutedRoutedEventHandler(this.RestoreWindowCommandOnExecuted), new CanExecuteRoutedEventHandler(this.RestoreWindowCommandOnCanExecute));
			CommandBinding commandBinding4 = new CommandBinding(SystemCommands.CloseWindowCommand, new ExecutedRoutedEventHandler(this.CloseWindowCommandOnExecuted), new CanExecuteRoutedEventHandler(this.CloseWindowCommandOnCanExecute));
			CommandManager.RegisterClassCommandBinding(typeFromHandle, commandBinding);
			CommandManager.RegisterClassCommandBinding(typeFromHandle, commandBinding2);
			CommandManager.RegisterClassCommandBinding(typeFromHandle, commandBinding3);
			CommandManager.RegisterClassCommandBinding(typeFromHandle, commandBinding4);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026F3 File Offset: 0x000008F3
		private void MinimizeWindowCommandOnCanExecute(object sender, CanExecuteRoutedEventArgs args)
		{
			args.CanExecute = true;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002700 File Offset: 0x00000900
		private void MinimizeWindowCommandOnExecuted(object sender, ExecutedRoutedEventArgs args)
		{
			MainWindow mainWindow = sender as MainWindow;
			bool flag = mainWindow != null;
			if (flag)
			{
				SystemCommands.MinimizeWindow(mainWindow);
				args.Handled = true;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026F3 File Offset: 0x000008F3
		private void MaximizeWindowCommandOnCanExecute(object sender, CanExecuteRoutedEventArgs args)
		{
			args.CanExecute = true;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002730 File Offset: 0x00000930
		private void MaximizeWindowCommandOnExecuted(object sender, ExecutedRoutedEventArgs args)
		{
			MainWindow mainWindow = sender as MainWindow;
			bool flag = mainWindow != null;
			if (flag)
			{
				SystemCommands.MaximizeWindow(mainWindow);
				args.Handled = true;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026F3 File Offset: 0x000008F3
		private void RestoreWindowCommandOnCanExecute(object sender, CanExecuteRoutedEventArgs args)
		{
			args.CanExecute = true;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002760 File Offset: 0x00000960
		private void RestoreWindowCommandOnExecuted(object sender, ExecutedRoutedEventArgs args)
		{
			MainWindow mainWindow = sender as MainWindow;
			bool flag = mainWindow != null;
			if (flag)
			{
				SystemCommands.RestoreWindow(mainWindow);
				args.Handled = true;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026F3 File Offset: 0x000008F3
		private void CloseWindowCommandOnCanExecute(object sender, CanExecuteRoutedEventArgs args)
		{
			args.CanExecute = true;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002790 File Offset: 0x00000990
		private void CloseWindowCommandOnExecuted(object sender, ExecutedRoutedEventArgs args)
		{
			MainWindow mainWindow = sender as MainWindow;
			bool flag = mainWindow != null;
			if (flag)
			{
				SystemCommands.CloseWindow(mainWindow);
				args.Handled = true;
			}
		}

		// Token: 0x0400000A RID: 10
		private string appName;
	}
}
