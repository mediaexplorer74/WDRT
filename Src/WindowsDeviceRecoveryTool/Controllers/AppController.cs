using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.BusinessLogic;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Controls;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;
using Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;
using Microsoft.WindowsDeviceRecoveryTool.Properties;
using Microsoft.WindowsDeviceRecoveryTool.States.Help;
using Microsoft.WindowsDeviceRecoveryTool.States.Settings;
using Microsoft.WindowsDeviceRecoveryTool.States.Shell;

namespace Microsoft.WindowsDeviceRecoveryTool.Controllers
{
	// Token: 0x02000093 RID: 147
	[Export("Microsoft.WindowsDeviceRecoveryTool.Controllers.AppController", typeof(IController))]
	public class AppController : BaseController, ICanHandle<BlockWindowMessage>, ICanHandle
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x0001A7FC File Offset: 0x000189FC
		[ImportingConstructor]
		public AppController(ICommandRepository commandRepository, LogicContext logics, EventAggregator eventAggregator)
			: base(commandRepository, eventAggregator)
		{
			this.logics = logics;
			bool flag = base.EventAggregator != null;
			if (flag)
			{
				base.EventAggregator.Subscribe(this);
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001A853 File Offset: 0x00018A53
		[CustomCommand]
		public void EndCurrentState()
		{
			this.shellState.CurrentState.Finish(string.Empty);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001A86C File Offset: 0x00018A6C
		[CustomCommand]
		public void SwitchSettingsState(string stateName)
		{
			SettingsState settingsState = this.shellState.CurrentState as SettingsState;
			bool flag = settingsState != null;
			if (flag)
			{
				settingsState.CurrentState.Finish(stateName);
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001A8A4 File Offset: 0x00018AA4
		[CustomCommand]
		public void SwitchHelpState(string stateName)
		{
			HelpState helpState = this.shellState.CurrentState as HelpState;
			bool flag = helpState != null;
			if (flag)
			{
				helpState.CurrentState.Finish(stateName);
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001A8DA File Offset: 0x00018ADA
		[CustomCommand]
		public void SwitchToState(string stateName)
		{
			this.shellState.CurrentState.Finish(stateName);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001A8F0 File Offset: 0x00018AF0
		[CustomCommand]
		public void ExitApplication()
		{
			Tracer<AppController>.WriteInformation("Shutdown the application");
			this.logics.Dispose();
			Application.Current.Dispatcher.BeginInvoke(new Action(delegate
			{
				Application.Current.Shutdown();
			}), new object[0]);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001A94C File Offset: 0x00018B4C
		[CustomCommand]
		public void PreviousState(object parameter)
		{
			BaseViewModel baseViewModel = parameter as BaseViewModel;
			bool flag = baseViewModel != null;
			if (flag)
			{
				this.SwitchToState(baseViewModel.PreviousStateName);
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001A978 File Offset: 0x00018B78
		[CustomCommand(IsAsynchronous = true)]
		public void CheckForAppUpdate(object parameter, CancellationToken cancellationToken)
		{
			try
			{
				bool flag = ApplicationBuildSettings.SkipApplicationUpdate || !this.IsAvailableAppUpdate();
				if (flag)
				{
					base.Commands.Run((AppController c) => c.SwitchToState("AutomaticManufacturerSelectionState"));
				}
			}
			catch (Exception ex)
			{
				bool flag2 = ex is PlannedServiceBreakException || ex is IOException;
				if (flag2)
				{
					throw;
				}
				throw new AutoUpdateException("Checking for application auto update failed.", ex);
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001AA40 File Offset: 0x00018C40
		[CustomCommand(IsAsynchronous = true)]
		public void SendNotification(NotificationMessage notificationData, CancellationToken cancellationToken)
		{
			object obj = this.notificationLock;
			lock (obj)
			{
				Tracer<AppController>.WriteInformation("Sending notification - Header: {0}    message: {1}", new object[] { notificationData.Header, notificationData.Text });
				base.EventAggregator.Publish<NotificationMessage>(new NotificationMessage(true, notificationData.Header, notificationData.Text));
				Thread.Sleep(5000);
				base.EventAggregator.Publish<NotificationMessage>(new NotificationMessage(false, null, null));
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001AAE0 File Offset: 0x00018CE0
		[CustomCommand(IsAsynchronous = true)]
		public void InstallAppUpdate(object parameter, CancellationToken cancellationToken)
		{
			base.Commands.Run((AppController c) => c.SwitchToState("AppAutoUpdateState"));
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001AB50 File Offset: 0x00018D50
		[CustomCommand(IsAsynchronous = true)]
		public void UpdateApplication(object parameter, CancellationToken token)
		{
			Tracer<AppController>.WriteInformation("Start update application");
			string text = Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.AppDataPath(SpecialFolder.AppUpdate);
			try
			{
				this.CheckFreeDiskSpace();
				this.logics.AutoUpdateService.DownloadProgressChanged += this.AutoUpdateServiceOnDownloadProgressChanged;
				string text2 = this.logics.AutoUpdateService.DownloadAppPacket(this.packageToDownload, text, token);
				bool flag = !string.IsNullOrEmpty(text2);
				if (flag)
				{
					this.InstallPacket(text2);
				}
			}
			catch (AutoUpdateNotEnoughSpaceException)
			{
				throw;
			}
			catch (Exception ex)
			{
				Tracer<AppController>.WriteError(ex);
				bool flag2 = !token.IsCancellationRequested;
				if (flag2)
				{
					throw new AutoUpdateException("Application auto update failed.", ex);
				}
			}
			finally
			{
				this.logics.AutoUpdateService.DownloadProgressChanged -= this.AutoUpdateServiceOnDownloadProgressChanged;
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001AC40 File Offset: 0x00018E40
		[CustomCommand]
		public void StartSoftwareInstall(SwVersionComparisonResult softwareComparisonStatus)
		{
			this.StartSoftwareInstallStatus(new Tuple<SwVersionComparisonResult, string>(softwareComparisonStatus, "DisclaimerState"));
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001AC58 File Offset: 0x00018E58
		[CustomCommand]
		public void StartSoftwareInstallStatus(Tuple<SwVersionComparisonResult, string> comparisonStatusAndStateTuple)
		{
			bool flag = comparisonStatusAndStateTuple.Item1 == SwVersionComparisonResult.FirstIsGreater;
			if (flag)
			{
				DialogMessageManager dialogMessageManager = new DialogMessageManager();
				bool? flag2 = dialogMessageManager.ShowQuestionDialog(LocalizationManager.GetTranslation("DowngradeSoftwareQuestion"), null, true);
				bool flag3 = false;
				bool flag4 = (flag2.GetValueOrDefault() == flag3) & (flag2 != null);
				if (flag4)
				{
					return;
				}
			}
			base.Commands.Run((AppController c) => c.SwitchToState(comparisonStatusAndStateTuple.Item2));
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001AD4C File Offset: 0x00018F4C
		[CustomCommand]
		public void CancelDownloadAppUpdate()
		{
			((IAsyncDelegateCommand)base.Commands["UpdateApplication"]).Cancel();
			base.Commands.Run((AppController c) => c.SwitchToState("CheckAppAutoUpdateState"));
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001ADD7 File Offset: 0x00018FD7
		public void Handle(BlockWindowMessage blockWindowMessage)
		{
			this.isBlock = blockWindowMessage.Block;
			this.message = blockWindowMessage.Message;
			this.title = blockWindowMessage.Title;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001AE00 File Offset: 0x00019000
		[CustomCommand(IsAsynchronous = true)]
		public void CloseAppOperations(MainWindow mainWindow, CancellationToken token)
		{
			Tracer<AppController>.LogEntry("CloseAppOperations");
			object obj = this.appClosingLock;
			lock (obj)
			{
				bool flag2 = this.appClosing;
				if (flag2)
				{
					Tracer<AppController>.WriteInformation("App is already being closed. Skipping operation");
					return;
				}
				this.appClosing = true;
			}
			Settings.Default.Save();
			bool flag3 = this.CanAppClose(mainWindow);
			if (flag3)
			{
				this.logics.ReportingService.MsrReportingService.SessionReportsSendingCompleted += this.OnSessionReportsSendingCompleted;
				this.logics.ReportingService.SendSessionReports();
			}
			else
			{
				this.appClosing = false;
			}
			Tracer<AppController>.LogExit("CloseAppOperations");
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001AECC File Offset: 0x000190CC
		private bool CanAppClose(MainWindow mainWindow)
		{
			bool flag = this.isBlock;
			if (flag)
			{
				Application.Current.Dispatcher.Invoke(delegate
				{
					this.RestoreWindow(mainWindow);
				});
				DialogMessageManager dialogMessageManager = new DialogMessageManager();
				bool? flag2 = dialogMessageManager.ShowQuestionDialog(this.message, this.title, true);
				bool? flag3 = flag2;
				bool flag4 = false;
				bool flag5 = (flag3.GetValueOrDefault() == flag4) & (flag3 != null);
				if (flag5)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001AF5C File Offset: 0x0001915C
		private void RestoreWindow(MainWindow mainWindow)
		{
			bool flag = mainWindow.WindowState == WindowState.Minimized;
			if (flag)
			{
				SystemCommands.RestoreWindow(mainWindow);
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001AF80 File Offset: 0x00019180
		private void OnSessionReportsSendingCompleted()
		{
			this.ExitApplication();
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001AF8A File Offset: 0x0001918A
		private void AutoUpdateServiceOnDownloadProgressChanged(DownloadingProgressChangedEventArgs args)
		{
			base.EventAggregator.Publish<ProgressMessage>(new ProgressMessage(args.Percentage, args.Message, args.DownloadedSize, args.TotalSize, args.BytesPerSecond, args.SecondsLeft));
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001AFC4 File Offset: 0x000191C4
		private void InstallPacket(string path)
		{
			bool flag = this.IsInstallFile(path);
			if (flag)
			{
				try
				{
					Process.Start(path);
					Application.Current.Dispatcher.BeginInvoke(new Action(delegate
					{
						Application.Current.Shutdown();
					}), new object[0]);
				}
				catch (Exception ex)
				{
					Tracer<AppController>.WriteError(ex);
				}
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001B03C File Offset: 0x0001923C
		private bool IsInstallFile(string path)
		{
			return !string.IsNullOrEmpty(path) && (path.EndsWith(".exe", StringComparison.OrdinalIgnoreCase) || path.EndsWith(".msi", StringComparison.OrdinalIgnoreCase) || path.EndsWith(".com", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001B084 File Offset: 0x00019284
		private void CheckFreeDiskSpace()
		{
			long size = this.packageToDownload.Size;
			long availableFreeSpace = this.GetAvailableFreeSpace();
			long num = Math.Max(size, 157286400L);
			bool flag = num > availableFreeSpace;
			if (flag)
			{
				Tracer<AppController>.WriteError("Not enough space on the disk", new object[0]);
				throw new AutoUpdateNotEnoughSpaceException
				{
					Available = availableFreeSpace,
					Needed = num,
					Disk = this.driveInfo.Name
				};
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001B0F4 File Offset: 0x000192F4
		private long GetAvailableFreeSpace()
		{
			this.driveInfo = new DriveInfo(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.AppDataPath(SpecialFolder.AppUpdate));
			return this.driveInfo.AvailableFreeSpace;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001B124 File Offset: 0x00019324
		private bool IsAvailableAppUpdate()
		{
			bool flag = this.isDebugSession;
			bool flag2;
			if (flag)
			{
				Tracer<AppController>.WriteInformation("Debug session: Skipping looking for app updates");
				flag2 = false;
			}
			else
			{
				int applicationId = AppInfo.ApplicationId;
				string version = AppInfo.Version;
				bool flag3 = this.CheckIfUseTestServer();
				ApplicationUpdate applicationUpdate = this.logics.AutoUpdateService.ReadLatestAppVersion(applicationId, version, flag3);
				bool flag4 = applicationUpdate != null && !string.IsNullOrWhiteSpace(applicationUpdate.PackageUri);
				if (flag4)
				{
					base.EventAggregator.Publish<ApplicationUpdateMessage>(new ApplicationUpdateMessage(applicationUpdate));
					this.packageToDownload = applicationUpdate;
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001B1B8 File Offset: 0x000193B8
		private bool CheckIfUseTestServer()
		{
			string registryValue = ApplicationInfo.GetRegistryValue("UseTestServer");
			bool flag = !string.IsNullOrEmpty(registryValue);
			if (flag)
			{
				Tracer<AppController>.WriteInformation("Registry value data: {0}", new object[] { registryValue });
				bool flag3;
				bool flag2 = bool.TryParse(registryValue, out flag3);
				if (flag2)
				{
					Tracer<AppController>.WriteInformation("Registry value parsed succesfully");
					return flag3;
				}
			}
			return false;
		}

		// Token: 0x04000236 RID: 566
		public const string IsTestServer = "UseTestServer";

		// Token: 0x04000237 RID: 567
		private readonly bool isDebugSession = false;

		// Token: 0x04000238 RID: 568
		private readonly object appClosingLock = new object();

		// Token: 0x04000239 RID: 569
		private readonly object notificationLock = new object();

		// Token: 0x0400023A RID: 570
		private readonly LogicContext logics;

		// Token: 0x0400023B RID: 571
		private ApplicationUpdate packageToDownload;

		// Token: 0x0400023C RID: 572
		private DriveInfo driveInfo;

		// Token: 0x0400023D RID: 573
		private string message;

		// Token: 0x0400023E RID: 574
		private string title;

		// Token: 0x0400023F RID: 575
		private bool isBlock;

		// Token: 0x04000240 RID: 576
		[Import]
		private ShellState shellState;

		// Token: 0x04000241 RID: 577
		private bool appClosing;
	}
}
