using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using ComponentAce.Compression.Archiver;
using ComponentAce.Compression.ZipForge;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.BusinessLogic;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Controls;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;
using Microsoft.WindowsDeviceRecoveryTool.Properties;

namespace Microsoft.WindowsDeviceRecoveryTool.Controllers
{
	// Token: 0x0200008F RID: 143
	[Export("Microsoft.WindowsDeviceRecoveryTool.Controllers.SettingsController", typeof(IController))]
	public class SettingsController : BaseController
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x00017628 File Offset: 0x00015828
		[ImportingConstructor]
		public SettingsController(ICommandRepository commandRepository, LogicContext logics, EventAggregator eventAggregator)
			: base(commandRepository, eventAggregator)
		{
			this.logics = logics;
			this.SetProxy(null);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00017644 File Offset: 0x00015844
		[CustomCommand(IsAsynchronous = true)]
		public void ChangePackagesPathDirectory(object parameter, CancellationToken token)
		{
			Tracer<SettingsController>.LogEntry("ChangePackagesPathDirectory");
			base.EventAggregator.Publish<SettingsPreviousStateMessage>(new SettingsPreviousStateMessage("PackagesState"));
			base.EventAggregator.Publish<SelectedPathMessage>(new SelectedPathMessage(Settings.Default.PackagesPath));
			base.Commands.Run((AppController c) => c.SwitchSettingsState("FolderBrowseAreaState"));
			Tracer<SettingsController>.LogExit("ChangePackagesPathDirectory");
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000176FC File Offset: 0x000158FC
		[CustomCommand(IsAsynchronous = true)]
		public void SetPackagesPathDirectory(string packagesPath, CancellationToken token)
		{
			Tracer<SettingsController>.LogEntry("SetPackagesPathDirectory");
			bool flag = string.IsNullOrWhiteSpace(packagesPath);
			if (!flag)
			{
				base.EventAggregator.Publish<PackageDirectoryMessage>(new PackageDirectoryMessage(packagesPath));
				Tracer<SettingsController>.LogExit("SetPackagesPathDirectory");
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00017740 File Offset: 0x00015940
		[CustomCommand(IsAsynchronous = true)]
		public void CollectLogs(string zipLogFilePath, CancellationToken token)
		{
			Tracer<SettingsController>.LogEntry("CollectLogs");
			bool flag = string.IsNullOrWhiteSpace(zipLogFilePath);
			if (!flag)
			{
				base.EventAggregator.Publish<IsBusyMessage>(new IsBusyMessage(true, LocalizationManager.GetTranslation("CollectingLogFilesInfo")));
				try
				{
					string appNamePrefix = Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.AppNamePrefix;
					string text = Path.Combine(zipLogFilePath, string.Format("{0}_{1}.zip", appNamePrefix, DateTime.UtcNow.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture)));
					this.CreateLogZipFile(text, Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.AppDataPath(SpecialFolder.Traces), appNamePrefix);
					Process.Start("explorer.exe", string.Format("/select, {0}", text));
				}
				catch (ArchiverException ex)
				{
					bool flag2 = ex.ErrorCode == ErrorCode.DiskIsFull;
					if (flag2)
					{
						throw new NotEnoughSpaceException(ex.Message, ex);
					}
					throw;
				}
				finally
				{
					base.EventAggregator.Publish<IsBusyMessage>(new IsBusyMessage(false, ""));
					base.EventAggregator.Publish<TraceParametersMessage>(new TraceParametersMessage(null, true));
				}
				Tracer<SettingsController>.LogExit("CollectLogs");
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001785C File Offset: 0x00015A5C
		[CustomCommand(IsAsynchronous = true)]
		public void DeleteLogs(bool skipDialogQuestion, CancellationToken token)
		{
			Tracer<SettingsController>.LogEntry("DeleteLogs");
			DialogMessageManager dialogMessageManager = new DialogMessageManager();
			bool flag3;
			if (!skipDialogQuestion)
			{
				bool? flag = dialogMessageManager.ShowQuestionDialog(LocalizationManager.GetTranslation("DeleteLogsQuestion"), null, true);
				bool flag2 = true;
				flag3 = (flag.GetValueOrDefault() == flag2) & (flag != null);
			}
			else
			{
				flag3 = true;
			}
			bool flag4 = flag3;
			if (flag4)
			{
				base.EventAggregator.Publish<IsBusyMessage>(new IsBusyMessage(true, LocalizationManager.GetTranslation("DeletingLogFilesInfo")));
				try
				{
					TraceManager.Instance.RemoveDiagnosticLogs(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.AppDataPath(SpecialFolder.Traces), Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.AppNamePrefix, Settings.Default.TraceEnabled);
				}
				finally
				{
					base.EventAggregator.Publish<ApplicationInvalidateSizeMessage>(new ApplicationInvalidateSizeMessage(ApplicationInvalidateSizeMessage.DataType.Logs));
					bool flag5 = !skipDialogQuestion;
					if (flag5)
					{
						base.EventAggregator.Publish<IsBusyMessage>(new IsBusyMessage(false, ""));
					}
				}
			}
			Tracer<SettingsController>.LogExit("DeleteLogs");
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00017944 File Offset: 0x00015B44
		[CustomCommand(IsAsynchronous = true)]
		public void SetTraceEnabled(bool traceEnabled, CancellationToken token)
		{
			Tracer<SettingsController>.LogEntry("SetTraceEnabled");
			if (traceEnabled)
			{
				TraceManager.Instance.EnableDiagnosticLogs(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.AppDataPath(SpecialFolder.Traces), Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.AppNamePrefix);
				Tracer<SettingsController>.WriteInformation("App version: {0} (running on: {1})", new object[]
				{
					AppInfo.Version,
					Environment.OSVersion
				});
			}
			else
			{
				TraceManager.Instance.DisableDiagnosticLogs(false);
			}
			Tracer<SettingsController>.LogExit("SetTraceEnabled");
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000179B6 File Offset: 0x00015BB6
		[CustomCommand(IsAsynchronous = true)]
		public void CalculateLogsSize(object parameter, CancellationToken token)
		{
			base.EventAggregator.Publish<ApplicationDataSizeMessage>(new ApplicationDataSizeMessage(ApplicationDataSizeMessage.DataType.Logs, Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetDirectorySize(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.AppDataPath(SpecialFolder.Traces))));
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000179D6 File Offset: 0x00015BD6
		[CustomCommand(IsAsynchronous = true)]
		public void CalculateReportsSize(object parameter, CancellationToken token)
		{
			base.EventAggregator.Publish<ApplicationDataSizeMessage>(new ApplicationDataSizeMessage(ApplicationDataSizeMessage.DataType.Reports, Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetDirectorySize(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.AppDataPath(SpecialFolder.Reports))));
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000179F8 File Offset: 0x00015BF8
		[CustomCommand(IsAsynchronous = true)]
		public void CalculatePackagesSize(object parameter, CancellationToken token)
		{
			token.ThrowIfCancellationRequested();
			bool flag = string.IsNullOrEmpty(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetCustomProductsPath());
			long num;
			if (flag)
			{
				num = Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetDirectorySize(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.DefaultPackagesPath);
				token.ThrowIfCancellationRequested();
				num += Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetDirectorySize(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.NokiaPackagesPath);
				token.ThrowIfCancellationRequested();
				num += Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetDirectorySize(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.HtcPackagesPath);
				token.ThrowIfCancellationRequested();
				num += Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetDirectorySize(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.LgePackagesPath);
				token.ThrowIfCancellationRequested();
				num += Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetDirectorySize(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.McjPackagesPath);
				token.ThrowIfCancellationRequested();
				num += Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetDirectorySize(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.BluPackagesPath);
				token.ThrowIfCancellationRequested();
				num += Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetDirectorySize(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.AlcatelPackagesPath);
			}
			else
			{
				num = Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetDirectorySize(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetCustomProductsPath());
			}
			token.ThrowIfCancellationRequested();
			base.EventAggregator.Publish<ApplicationDataSizeMessage>(new ApplicationDataSizeMessage(ApplicationDataSizeMessage.DataType.Packages, num));
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00017AD4 File Offset: 0x00015CD4
		[CustomCommand(IsAsynchronous = true)]
		public void ResetSettings(object parameter, CancellationToken token)
		{
			Tracer<SettingsController>.LogEntry("ResetSettings");
			string translation = LocalizationManager.GetTranslation("ResetSettingsQuestion");
			DialogMessageManager dialogMessageManager = new DialogMessageManager();
			bool? flag = dialogMessageManager.ShowQuestionDialog(translation, null, true);
			bool flag2 = true;
			bool flag3 = !((flag.GetValueOrDefault() == flag2) & (flag != null));
			if (!flag3)
			{
				ApplicationInfo.CurrentLanguageInRegistry = ApplicationInfo.DefaultLanguageInRegistry;
				base.EventAggregator.Publish<LanguageChangedMessage>(new LanguageChangedMessage(ApplicationInfo.DefaultLanguageInRegistry));
				base.EventAggregator.Publish<ThemeColorChangedMessage>(new ThemeColorChangedMessage((string)Settings.Default.Properties["Theme"].DefaultValue, (string)Settings.Default.Properties["Style"].DefaultValue));
				Settings.Default.Reset();
				Settings.Default.CallUpgrade = false;
				this.SetTraceEnabled(Settings.Default.TraceEnabled, CancellationToken.None);
				Settings.Default.Save();
				Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.CustomPackagesPath = string.Empty;
				Tracer<SettingsController>.LogExit("ResetSettings");
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00017BE8 File Offset: 0x00015DE8
		[CustomCommand(IsAsynchronous = true)]
		public void DeleteReports(bool skipDialogQuestion, CancellationToken token)
		{
			Tracer<SettingsController>.LogEntry("DeleteReports");
			DialogMessageManager dialogMessageManager = new DialogMessageManager();
			bool flag3;
			if (!skipDialogQuestion)
			{
				bool? flag = dialogMessageManager.ShowQuestionDialog(LocalizationManager.GetTranslation("DeleteReportsQuestion"), null, true);
				bool flag2 = true;
				flag3 = (flag.GetValueOrDefault() == flag2) & (flag != null);
			}
			else
			{
				flag3 = true;
			}
			bool flag4 = flag3;
			if (flag4)
			{
				base.EventAggregator.Publish<IsBusyMessage>(new IsBusyMessage(true, LocalizationManager.GetTranslation("DeletingReportsInfo")));
				try
				{
					this.DeleteAllReports();
				}
				finally
				{
					base.EventAggregator.Publish<ApplicationInvalidateSizeMessage>(new ApplicationInvalidateSizeMessage(ApplicationInvalidateSizeMessage.DataType.Reports));
					bool flag5 = !skipDialogQuestion;
					if (flag5)
					{
						base.EventAggregator.Publish<IsBusyMessage>(new IsBusyMessage(false, ""));
					}
				}
			}
			Tracer<SettingsController>.LogExit("DeleteReports");
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00017CB4 File Offset: 0x00015EB4
		[CustomCommand(IsAsynchronous = true)]
		public void DeletePackages(bool skipDialogQuestion, CancellationToken token)
		{
			Tracer<SettingsController>.LogEntry("DeletePackages");
			DialogMessageManager dialogMessageManager = new DialogMessageManager();
			bool flag3;
			if (!skipDialogQuestion)
			{
				bool? flag = dialogMessageManager.ShowQuestionDialog(LocalizationManager.GetTranslation("DeletePackagesQuestion"), null, true);
				bool flag2 = true;
				flag3 = (flag.GetValueOrDefault() == flag2) & (flag != null);
			}
			else
			{
				flag3 = true;
			}
			bool flag4 = flag3;
			if (flag4)
			{
				base.EventAggregator.Publish<IsBusyMessage>(new IsBusyMessage(true, LocalizationManager.GetTranslation("DeletingPackagesInfo")));
				try
				{
					this.DeleteAllPackages();
				}
				finally
				{
					base.EventAggregator.Publish<ApplicationInvalidateSizeMessage>(new ApplicationInvalidateSizeMessage(ApplicationInvalidateSizeMessage.DataType.Packages));
					bool flag5 = !skipDialogQuestion;
					if (flag5)
					{
						base.EventAggregator.Publish<IsBusyMessage>(new IsBusyMessage(false, ""));
					}
				}
			}
			Tracer<SettingsController>.LogExit("DeletePackages");
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00017D80 File Offset: 0x00015F80
		[CustomCommand(IsAsynchronous = true)]
		public void CleanUserData(object parameter, CancellationToken token)
		{
			Tracer<SettingsController>.LogEntry("CleanUserData");
			DialogMessageManager dialogMessageManager = new DialogMessageManager();
			bool? flag = dialogMessageManager.ShowQuestionDialog(LocalizationManager.GetTranslation("CleanUserDataQuestion"), null, true);
			bool flag2 = true;
			bool flag3 = (flag.GetValueOrDefault() == flag2) & (flag != null);
			if (flag3)
			{
				try
				{
					this.DeleteLogs(true, CancellationToken.None);
					this.DeleteReports(true, CancellationToken.None);
					this.DeletePackages(true, CancellationToken.None);
				}
				finally
				{
					base.EventAggregator.Publish<IsBusyMessage>(new IsBusyMessage(false, ""));
				}
			}
			Tracer<SettingsController>.LogExit("CleanUserData");
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00017E2C File Offset: 0x0001602C
		[CustomCommand]
		public void SetProxy(object parameter)
		{
			Tracer<SettingsController>.LogEntry("SetProxy");
			IWebProxy webProxy = null;
			bool useManualProxy = Settings.Default.UseManualProxy;
			if (useManualProxy)
			{
				bool flag = !string.IsNullOrEmpty(Settings.Default.ProxyAddress);
				if (flag)
				{
					webProxy = new WebProxy(Settings.Default.ProxyAddress, Settings.Default.ProxyPort)
					{
						Credentials = new NetworkCredential(Settings.Default.ProxyUsername, new Credentials().DecryptString(Settings.Default.ProxyPassword))
					};
				}
			}
			this.logics.SetProxy(webProxy);
			Settings.Default.Save();
			Tracer<SettingsController>.LogExit("SetProxy");
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00017ED8 File Offset: 0x000160D8
		[CustomCommand]
		public void SetApplicationSettings()
		{
			bool flag = Settings.Default.CustomPackagesPathEnabled && !string.IsNullOrWhiteSpace(Settings.Default.PackagesPath);
			if (flag)
			{
				Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.CustomPackagesPath = Settings.Default.PackagesPath;
			}
			else
			{
				Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.CustomPackagesPath = null;
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00017F28 File Offset: 0x00016128
		[CustomCommand]
		public void ChangeZipLogPath()
		{
			Tracer<SettingsController>.LogEntry("ChangeZipLogPath");
			base.EventAggregator.Publish<SettingsPreviousStateMessage>(new SettingsPreviousStateMessage("TraceState"));
			base.EventAggregator.Publish<SelectedPathMessage>(new SelectedPathMessage(Environment.ExpandEnvironmentVariables(Settings.Default.ZipFilePath)));
			base.Commands.Run((AppController c) => c.SwitchSettingsState("FolderBrowseAreaState"));
			Tracer<SettingsController>.LogExit("ChangeZipLogPath");
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00017FE4 File Offset: 0x000161E4
		private void DeleteAllReports()
		{
			Tracer<SettingsController>.LogEntry("DeleteAllReports");
			base.EventAggregator.Publish<IsBusyMessage>(new IsBusyMessage(true, LocalizationManager.GetTranslation("RemovingReports")));
			try
			{
				string text = Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.AppDataPath(SpecialFolder.Reports);
				this.DeleteDirContent(text);
			}
			finally
			{
				base.EventAggregator.Publish<IsBusyMessage>(new IsBusyMessage(false, ""));
			}
			Tracer<SettingsController>.LogExit("DeleteAllReports");
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00018064 File Offset: 0x00016264
		private void DeleteAllPackages()
		{
			Tracer<SettingsController>.LogEntry("DeleteAllPackages");
			base.EventAggregator.Publish<IsBusyMessage>(new IsBusyMessage(true, LocalizationManager.GetTranslation("RemovingPackages")));
			try
			{
				bool flag = string.IsNullOrEmpty(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetCustomProductsPath());
				if (flag)
				{
					this.DeleteDirContent(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.DefaultPackagesPath);
					this.DeleteDirContent(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.NokiaPackagesPath);
					this.DeleteDirContent(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.HtcPackagesPath);
					this.DeleteDirContent(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.LgePackagesPath);
					this.DeleteDirContent(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.McjPackagesPath);
					this.DeleteDirContent(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.BluPackagesPath);
					this.DeleteDirContent(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.AlcatelPackagesPath);
				}
				else
				{
					this.DeleteDirContent(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetCustomProductsPath());
				}
			}
			finally
			{
				base.EventAggregator.Publish<IsBusyMessage>(new IsBusyMessage(false, ""));
			}
			Tracer<SettingsController>.LogExit("DeleteAllPackages");
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00018148 File Offset: 0x00016348
		private void DeleteDirContent(string path)
		{
			Tracer<SettingsController>.LogEntry("DeleteDirContent");
			bool flag = !Directory.Exists(path);
			if (flag)
			{
				Tracer<SettingsController>.WriteWarning("Directory not found!", new object[0]);
				Tracer<SettingsController>.LogExit("DeleteDirContent");
			}
			else
			{
				foreach (DirectoryInfo directoryInfo in new DirectoryInfo(path).GetDirectories())
				{
					try
					{
						directoryInfo.Delete(true);
						string text = "Successfully removed directory: {0}";
						object[] array = new object[1];
						int num = 0;
						DirectoryInfo directoryInfo2 = directoryInfo;
						array[num] = path + ((directoryInfo2 != null) ? directoryInfo2.ToString() : null);
						Tracer<SettingsController>.WriteInformation(text, array);
					}
					catch (Exception ex)
					{
						Tracer<SettingsController>.WriteInformation("Skipped {0} directory when cleaning up data - {1}", new object[] { directoryInfo, ex.Message });
					}
				}
				foreach (FileInfo fileInfo in new DirectoryInfo(path).GetFiles())
				{
					try
					{
						fileInfo.Delete();
						string text2 = "Successfully removed file {0}";
						object[] array2 = new object[1];
						int num2 = 0;
						FileInfo fileInfo2 = fileInfo;
						array2[num2] = path + ((fileInfo2 != null) ? fileInfo2.ToString() : null);
						Tracer<SettingsController>.WriteInformation(text2, array2);
					}
					catch (Exception)
					{
						Tracer<SettingsController>.WriteInformation("Skipped {0} file when cleaning up data", new object[] { fileInfo });
					}
				}
				Tracer<SettingsController>.LogExit("DeleteDirContent");
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000182AC File Offset: 0x000164AC
		private void CreateLogZipFile(string zipFilePath, string logPath, string appNamePrefix)
		{
			Tracer<SettingsController>.LogEntry("CreateLogZipFile");
			Tracer<SettingsController>.WriteInformation("Creating log .zip file: {0}", new object[] { zipFilePath });
			using (ZipForge zipForge = new ZipForge())
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					zipForge.OpenArchive(memoryStream, true);
					zipForge.BaseDir = logPath;
					string[] files = Directory.GetFiles(logPath);
					var enumerable = files.Select((string filePath) => new
					{
						filePath = filePath,
						fileInfo = new FileInfo(filePath)
					});
					Func<<>f__AnonymousType1<string, FileInfo>, bool> <>9__1;
					var func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = <>h__TransparentIdentifier0 => <>h__TransparentIdentifier0.fileInfo.Name.StartsWith(appNamePrefix, true, CultureInfo.CurrentCulture));
					}
					foreach (string text in from <>h__TransparentIdentifier0 in enumerable.Where(func)
						where (DateTime.Now - <>h__TransparentIdentifier0.fileInfo.CreationTime).Days < 7
						select <>h__TransparentIdentifier0.filePath)
					{
						zipForge.AddFiles(text);
					}
					bool flag = zipForge.Size > new DriveInfo(Path.GetPathRoot(zipFilePath)).AvailableFreeSpace;
					if (flag)
					{
						throw new ArchiverException("The disk is full", ErrorCode.DiskIsFull, null, null);
					}
					zipForge.CloseArchive();
					using (FileStream fileStream = new FileStream(zipFilePath, FileMode.Create))
					{
						memoryStream.WriteTo(fileStream);
					}
				}
			}
			Tracer<SettingsController>.LogExit("CreateLogZipFile");
		}

		// Token: 0x0400022B RID: 555
		private readonly LogicContext logics;
	}
}
