using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClickerUtilityLibrary;
using ClickerUtilityLibrary.Comm;
using ClickerUtilityLibrary.Misc;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;

namespace Microsoft.WindowsDeviceRecoveryTool.FawkesAdaptation.Services
{
	// Token: 0x02000007 RID: 7
	internal class FawkesFlasher
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002989 File Offset: 0x00000B89
		static FawkesFlasher()
		{
			CommandEngine.Instance.CommandEngineEvent += FawkesFlasher.InstanceOnCommandEngineEvent;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000029A4 File Offset: 0x00000BA4
		private static void InstanceOnCommandEngineEvent(object sender, FEvent fEvent)
		{
			EventType eventType = fEvent.EventType;
			if (eventType != EventType.UsbDeviceConnected)
			{
				if (eventType == EventType.UsbDeviceDisconnected)
				{
					FawkesFlasher.isDeviceConnected = false;
					return;
				}
			}
			else
			{
				FawkesFlasher.isDeviceConnected = true;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000029CD File Offset: 0x00000BCD
		public static bool IsDeviceConnected()
		{
			return FawkesFlasher.isDeviceConnected || FawkesFlasher.isSwitchingModes;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000029E0 File Offset: 0x00000BE0
		public static bool IsInFlashingMode()
		{
			string connectedDeviceFriendlyName = CommandEngine.Instance.ConnectedDeviceFriendlyName;
			Tracer<FawkesFlasher>.WriteInformation("Device name read: {0}", new object[] { connectedDeviceFriendlyName });
			if (string.IsNullOrEmpty(connectedDeviceFriendlyName))
			{
				Tracer<FawkesFlasher>.WriteWarning("Device name is empty", new object[0]);
				return false;
			}
			return ClickerFwUpdater.IsBootLoaderUsbFriendlyName(connectedDeviceFriendlyName);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002A2C File Offset: 0x00000C2C
		public static FawkesDeviceInfo ReadDeviceInfoFromNormalMode()
		{
			Tracer<FawkesFlasher>.LogEntry("ReadDeviceInfoFromNormalMode");
			FawkesDeviceInfo fawkesDeviceInfo = null;
			using (ClickerFwUpdater updater = FawkesFlasher.GetUpdater(new FawkesLogger()))
			{
				if (FawkesFlasher.IsInFlashingMode())
				{
					Tracer<FawkesFlasher>.WriteInformation("Device in bootloader mode. Need to reset before reading data.");
					ManualResetEvent deviceConnectedToNormalModeEvent = new ManualResetEvent(false);
					EventHandler<FwUpdaterEventArgs> eventHandler = delegate(object sender, FwUpdaterEventArgs args)
					{
						if (args.Type == FwUpdaterEventArgs.EventType.ConnectedToApplication)
						{
							Tracer<FawkesFlasher>.WriteVerbose("Device normal/APP mode connected.", new object[0]);
							deviceConnectedToNormalModeEvent.Set();
						}
					};
					try
					{
						updater.UpdaterEvent += eventHandler;
						FawkesFlasher.isSwitchingModes = true;
						ClickerFwUpdater.RunApplication();
						Tracer<FawkesFlasher>.WriteInformation("Device reset. Waiting for device to enter APP mode.");
						deviceConnectedToNormalModeEvent.WaitOne(10000);
					}
					finally
					{
						updater.UpdaterEvent -= eventHandler;
					}
				}
				int num;
				if (!FawkesFlasher.TryReadBoardId(updater, out num))
				{
					FawkesFlasher.TraceErrorAndThrowReadPhoneInfo("Could not read Hardware Id (Board Id). Reading device info skipped.");
				}
				Tracer<FawkesFlasher>.WriteVerbose("Hardware Id (Board id) read: {0}", new object[] { num });
				string connectedDeviceFriendlyName = CommandEngine.Instance.ConnectedDeviceFriendlyName;
				Tracer<FawkesFlasher>.WriteVerbose("Device name read: {0}", new object[] { connectedDeviceFriendlyName });
				string text = null;
				if (updater.GetFirmwareVersion(out text))
				{
					Tracer<FawkesFlasher>.WriteVerbose("Firmware version read: {0}", new object[] { text });
					fawkesDeviceInfo = new FawkesDeviceInfo(text, num.ToString(CultureInfo.InvariantCulture), connectedDeviceFriendlyName);
				}
				else
				{
					Tracer<FawkesFlasher>.WriteWarning("Could not read firmware version", new object[0]);
				}
			}
			Tracer<FawkesFlasher>.LogExit("ReadDeviceInfoFromNormalMode");
			return fawkesDeviceInfo;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public static void FlashDevice(Phone phone, FawkesProgress progress, CancellationToken cancellationToken)
		{
			Tracer<FawkesFlasher>.LogEntry("FlashDevice");
			Tracer<FawkesFlasher>.WriteVerbose("Package files: {0}", new object[] { string.Join(", ", phone.PackageFiles) });
			FawkesFlasher.ValidatePackage(phone);
			string text = FawkesFlasher.ReadFirmwarePackageFilePath(phone);
			ImageVersion imageVersion = FawkesFlasher.ReadPackageFirmwareVersion(phone.PackageFileInfo);
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new FlashException("Could not determine firmware package path");
			}
			Tracer<FawkesFlasher>.WriteInformation("Start firmware download: BL = {0}; package path = {1}; target version = {2}", new object[]
			{
				FawkesFlasher.IsInFlashingMode(),
				text,
				imageVersion
			});
			FawkesFlasher.FlashOperationData flashOperationData = new FawkesFlasher.FlashOperationData(text, imageVersion, progress);
			using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
			{
				Task task = Task.Factory.StartNew(delegate
				{
					FawkesFlasher.StartFirmwareDownload(flashOperationData);
				}, cancellationTokenSource.Token, TaskCreationOptions.AttachedToParent, TaskScheduler.Default);
				Task task2 = Task.Factory.StartNew(delegate
				{
					while (!flashOperationData.Finished && !flashOperationData.DeviceDisconnected)
					{
						Thread.Sleep(100);
						if (FawkesFlasher.isFlashing && !FawkesFlasher.isDeviceConnected && !flashOperationData.Finished)
						{
							flashOperationData.DeviceDisconnected = true;
							FawkesFlasher.isFlashing = false;
						}
					}
				}, cancellationTokenSource.Token);
				Task.WaitAny(new Task[] { task, task2 });
			}
			if (flashOperationData.DeviceDisconnected)
			{
				flashOperationData.Progress.CleanUpdaterEvents();
				throw new DeviceDisconnectedException();
			}
			if (!flashOperationData.Result)
			{
				string text2 = string.Format("Flashing failed with error:{0}{1}", Environment.NewLine, flashOperationData.FailMessage);
				Tracer<FawkesFlasher>.WriteError(text2, new object[0]);
				throw new FlashException(text2);
			}
			Tracer<FawkesFlasher>.WriteInformation("Flashing device: {0} completed. New firmware version: {1}", new object[] { phone, flashOperationData.FlashedFirmwareVersion });
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002D3C File Offset: 0x00000F3C
		public static void ValidatePackage(Phone phone)
		{
			MsrPackageInfo msrPackageInfo = phone.PackageFileInfo as MsrPackageInfo;
			if (msrPackageInfo == null || msrPackageInfo.OfflinePackage)
			{
				if (phone.PackageFiles.Count > 1)
				{
					FawkesFlasher.TraceErrorAndThrowFlash("Currently only one file per flashing is used. Please select single file.");
				}
				if (phone.PackageFiles.Count == 0)
				{
					FawkesFlasher.TraceErrorAndThrowFlash("No package files selected.");
					return;
				}
			}
			else
			{
				if (msrPackageInfo.PackageFileData == null && !msrPackageInfo.OfflinePackage)
				{
					FawkesFlasher.TraceErrorAndThrowFlash("No MSR package files data found. Flashing failed.");
					return;
				}
				if (msrPackageInfo.PackageFileData.Count<MsrPackageInfo.MsrFileInfo>() != 1)
				{
					string text = phone.PackageFiles[0];
					MsrPackageInfo.MsrFileInfo msrFileInfo = msrPackageInfo.PackageFileData.FirstOrDefault((MsrPackageInfo.MsrFileInfo f) => string.Equals(f.FileType, "APP", StringComparison.InvariantCultureIgnoreCase));
					if (msrFileInfo == null)
					{
						FawkesFlasher.TraceErrorAndThrowFlash("No APP package found. Flashing failed.");
					}
					string fileName = Path.GetFileName(text);
					if (!string.Equals(fileName, msrFileInfo.FileName))
					{
						FawkesFlasher.TraceErrorAndThrowFlash(string.Format("Found APP package file on MSR ({0}) does not match with local file: {1}", msrFileInfo.FileName, fileName));
					}
				}
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002E2C File Offset: 0x0000102C
		private static bool TryReadBoardId(ClickerFwUpdater updater, out int hwId)
		{
			hwId = -1;
			bool flag = updater.GetBoardId(out hwId);
			if (!flag)
			{
				Tracer<FawkesFlasher>.WriteWarning("Could not read board id. Reset devie and try again.", new object[0]);
				ClickerFwUpdater.ResetDevice();
				FawkesFlasher.WaitForEvent(updater, FwUpdaterEventArgs.EventType.ConnectedToApplication, 15000);
				flag = updater.GetBoardId(out hwId);
			}
			return flag;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002E74 File Offset: 0x00001074
		private static void WaitForEvent(ClickerFwUpdater updater, FwUpdaterEventArgs.EventType eventType, int timeoutMillis = -1)
		{
			ManualResetEvent resetEvent = new ManualResetEvent(false);
			EventHandler<FwUpdaterEventArgs> eventHandler = delegate(object o, FwUpdaterEventArgs e)
			{
				if (e.Type == eventType)
				{
					resetEvent.Set();
				}
			};
			try
			{
				updater.UpdaterEvent += eventHandler;
				resetEvent.WaitOne(timeoutMillis);
			}
			finally
			{
				updater.UpdaterEvent -= eventHandler;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002ED8 File Offset: 0x000010D8
		private static void StartFirmwareDownload(object operationStartData)
		{
			FawkesFlasher.FlashOperationData flashOperationData = operationStartData as FawkesFlasher.FlashOperationData;
			if (flashOperationData == null)
			{
				Tracer<FawkesFlasher>.WriteError("Flash operation data not found. Firmware download could not be started.", new object[0]);
				return;
			}
			try
			{
				FawkesLogger fawkesLogger = new FawkesLogger();
				using (ClickerFwUpdater updater = FawkesFlasher.GetUpdater(fawkesLogger))
				{
					flashOperationData.Progress.SetupUpdaterEvents(updater);
					flashOperationData.Result = updater.StartFirmwareDownload(flashOperationData.PackagePath, flashOperationData.TargetFirmwareVersion);
					if (!flashOperationData.Result)
					{
						string text = string.Join(Environment.NewLine, fawkesLogger.LoggedErrorMessages);
						flashOperationData.FailMessage = text;
					}
					else if (!flashOperationData.DeviceDisconnected)
					{
						string text2;
						updater.GetFirmwareVersion(out text2);
						flashOperationData.FlashedFirmwareVersion = text2;
					}
				}
			}
			finally
			{
				flashOperationData.Finished = true;
				if (!flashOperationData.DeviceDisconnected)
				{
					FawkesFlasher.isFlashing = false;
					flashOperationData.Progress.CleanUpdaterEvents();
				}
				Tracer<FawkesFlasher>.LogExit("StartFirmwareDownload");
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002FC0 File Offset: 0x000011C0
		private static ImageVersion ReadPackageFirmwareVersion(PackageFileInfo packageFileInfo)
		{
			ImageVersion imageVersion = null;
			if (!FawkesUtils.TryParseImageVersion(packageFileInfo.SoftwareVersion, out imageVersion))
			{
				imageVersion = new ImageVersion();
				Tracer<FawkesFlasher>.WriteWarning(string.Format("Could not read image version. Default version used: {0}", imageVersion), new object[0]);
			}
			else
			{
				Tracer<FawkesFlasher>.WriteVerbose(string.Format("Firmware version read from packet: {0}", imageVersion), new object[0]);
			}
			return imageVersion;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003013 File Offset: 0x00001213
		private static void TraceErrorAndThrowReadPhoneInfo(string errorMessage)
		{
			Tracer<FawkesFlasher>.WriteError(errorMessage, new object[0]);
			throw new ReadPhoneInformationException(errorMessage);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003027 File Offset: 0x00001227
		private static void TraceErrorAndThrowFlash(string errorMessage)
		{
			Tracer<FawkesFlasher>.WriteError(errorMessage, new object[0]);
			throw new FlashException(errorMessage);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000303C File Offset: 0x0000123C
		private static string ReadFirmwarePackageFilePath(Phone phone)
		{
			MsrPackageInfo msrPackageInfo = phone.PackageFileInfo as MsrPackageInfo;
			string text = null;
			if (msrPackageInfo != null)
			{
				if (msrPackageInfo.PackageFileData != null)
				{
					MsrPackageInfo.MsrFileInfo msrAppFile = msrPackageInfo.PackageFileData.FirstOrDefault((MsrPackageInfo.MsrFileInfo f) => string.Equals(f.FileType, "APP"));
					if (msrAppFile == null)
					{
						Tracer<FawkesFlasher>.WriteWarning("No APP file found in package. Fallback to single file check.", new object[0]);
						if (msrPackageInfo.PackageFileData.Count<MsrPackageInfo.MsrFileInfo>() == 1)
						{
							msrAppFile = msrPackageInfo.PackageFileData.First<MsrPackageInfo.MsrFileInfo>();
						}
						else
						{
							Tracer<FawkesFlasher>.WriteWarning("Package has more than one file. Could not determine firmware file. Fallback to downloaded file check.", new object[0]);
						}
					}
					if (msrAppFile != null)
					{
						text = phone.PackageFiles.FirstOrDefault((string p) => string.Equals(Path.GetFileName(p), msrAppFile.FileNameWithRevision));
						if (text == null)
						{
							text = phone.PackageFiles.FirstOrDefault((string p) => string.Equals(Path.GetFileName(p), msrAppFile.FileName));
							if (text == null)
							{
								Tracer<FawkesFlasher>.WriteWarning("Could not match any locally downloaded file to firmware file: {0}/{1}", new object[] { msrAppFile.FileName, msrAppFile.FileNameWithRevision });
							}
						}
					}
				}
				if (text == null && phone.PackageFiles.Count == 1)
				{
					text = phone.PackageFiles.First<string>();
				}
				else if (text == null)
				{
					Tracer<FawkesFlasher>.WriteError("There is more than one file in package. Could not determine firmware package path.", new object[0]);
				}
			}
			return text ?? phone.PackageFilePath;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003190 File Offset: 0x00001390
		private static ClickerFwUpdater GetUpdater(FawkesLogger logger)
		{
			ClickerFwUpdater clickerFwUpdater = new ClickerFwUpdater(logger);
			EventHandler<FwUpdaterEventArgs> eventHandler = delegate(object sender, FwUpdaterEventArgs args)
			{
				if (args.Type == FwUpdaterEventArgs.EventType.ConnectedToApplication || args.Type == FwUpdaterEventArgs.EventType.ConnectedToBootLoader)
				{
					FawkesFlasher.isSwitchingModes = false;
				}
				if (args.Type == FwUpdaterEventArgs.EventType.DeviceDisconnected)
				{
					FawkesFlasher.isSwitchingModes = true;
				}
				if (args.Type == FwUpdaterEventArgs.EventType.UpdateProgress)
				{
					FawkesFlasher.isFlashing = !object.Equals(args.Parameters, 1.0);
				}
				if (args.Type == FwUpdaterEventArgs.EventType.UpdateCompleted)
				{
					FawkesFlasher.isFlashing = false;
				}
			};
			clickerFwUpdater.UpdaterEvent += eventHandler;
			return clickerFwUpdater;
		}

		// Token: 0x0400000B RID: 11
		private static bool isDeviceConnected;

		// Token: 0x0400000C RID: 12
		private static bool isSwitchingModes;

		// Token: 0x0400000D RID: 13
		private static bool isFlashing;

		// Token: 0x0200000F RID: 15
		private class FlashOperationData
		{
			// Token: 0x17000012 RID: 18
			// (get) Token: 0x0600005F RID: 95 RVA: 0x00003546 File Offset: 0x00001746
			// (set) Token: 0x06000060 RID: 96 RVA: 0x0000354E File Offset: 0x0000174E
			public string PackagePath { get; private set; }

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000061 RID: 97 RVA: 0x00003557 File Offset: 0x00001757
			// (set) Token: 0x06000062 RID: 98 RVA: 0x0000355F File Offset: 0x0000175F
			public ImageVersion TargetFirmwareVersion { get; private set; }

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000063 RID: 99 RVA: 0x00003568 File Offset: 0x00001768
			// (set) Token: 0x06000064 RID: 100 RVA: 0x00003570 File Offset: 0x00001770
			public FawkesProgress Progress { get; private set; }

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000065 RID: 101 RVA: 0x00003579 File Offset: 0x00001779
			// (set) Token: 0x06000066 RID: 102 RVA: 0x00003581 File Offset: 0x00001781
			public bool Result { get; set; }

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000067 RID: 103 RVA: 0x0000358A File Offset: 0x0000178A
			// (set) Token: 0x06000068 RID: 104 RVA: 0x00003592 File Offset: 0x00001792
			public string FailMessage { get; set; }

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000069 RID: 105 RVA: 0x0000359B File Offset: 0x0000179B
			// (set) Token: 0x0600006A RID: 106 RVA: 0x000035A3 File Offset: 0x000017A3
			public string FlashedFirmwareVersion { get; set; }

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x0600006B RID: 107 RVA: 0x000035AC File Offset: 0x000017AC
			// (set) Token: 0x0600006C RID: 108 RVA: 0x000035B4 File Offset: 0x000017B4
			public bool DeviceDisconnected { get; set; }

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x0600006D RID: 109 RVA: 0x000035BD File Offset: 0x000017BD
			// (set) Token: 0x0600006E RID: 110 RVA: 0x000035C5 File Offset: 0x000017C5
			public bool Finished { get; set; }

			// Token: 0x0600006F RID: 111 RVA: 0x000035CE File Offset: 0x000017CE
			public FlashOperationData(string packagePath, ImageVersion targetFirmwareVersion, FawkesProgress progress)
			{
				this.PackagePath = packagePath;
				this.TargetFirmwareVersion = targetFirmwareVersion;
				this.Progress = progress;
			}
		}
	}
}
