using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Contracts;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Enums;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;
using Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions.HTC;

namespace Microsoft.WindowsDeviceRecoveryTool.HtcAdaptation.Services
{
	// Token: 0x02000006 RID: 6
	[ExportAdaptation(Type = PhoneTypes.Htc)]
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class HtcAdaptation : BaseAdaptation
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002378 File Offset: 0x00000578
		[ImportingConstructor]
		internal HtcAdaptation(MsrService msrService, ReportingService reportingService, HtcModelsCatalog htcModelsCatalog)
		{
			this.salesNameProvider = new SalesNameProvider();
			this.reportingService = reportingService;
			this.htcModelsCatalog = htcModelsCatalog;
			this.msrService = msrService;
			this.msrService.ProgressChanged += this.MsrDownloadProgressEvent;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000023C8 File Offset: 0x000005C8
		public override PhoneTypes PhoneType
		{
			get
			{
				return PhoneTypes.Htc;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000023DC File Offset: 0x000005DC
		public override string PackageExtension
		{
			get
			{
				return "nbh";
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000023F4 File Offset: 0x000005F4
		public override bool RecoverySupport
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002408 File Offset: 0x00000608
		public override string ManufacturerName
		{
			get
			{
				return "HTC";
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002420 File Offset: 0x00000620
		public override string ReportManufacturerName
		{
			get
			{
				return "HTC";
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002438 File Offset: 0x00000638
		public override ISalesNameProvider SalesNameProvider()
		{
			return this.salesNameProvider;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002450 File Offset: 0x00000650
		protected override void FillSupportedDeviceIdentifiers()
		{
			foreach (VidPidPair vidPidPair in this.htcModelsCatalog.Models.SelectMany((ModelInfo m) => m.VidPidPairs))
			{
				this.SupportedNormalModeIds.Add(new DeviceIdentifier(vidPidPair.Vid, vidPidPair.Pid));
			}
			this.SupportedFlashModeIds.Add(new DeviceIdentifier("0BB4", "00CE"));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024FC File Offset: 0x000006FC
		public override void DownloadEmergencyPackage(Phone currentPhone, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002504 File Offset: 0x00000704
		public override SwVersionComparisonResult CompareFirmwareVersions(Phone phone)
		{
			Tracer<HtcAdaptation>.LogEntry("CompareFirmwareVersions");
			Tracer<HtcAdaptation>.LogExit("CompareFirmwareVersions");
			return SwVersionComparisonResult.UnableToCompare;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002530 File Offset: 0x00000730
		public override bool IsDeviceInFlashModeConnected(Phone phone, CancellationToken cancellationToken)
		{
			int num = 15;
			HtcDeviceInfo htcDeviceInfo;
			do
			{
				htcDeviceInfo = this.ReadDeviceInfo(phone.InstanceId);
				bool flag = htcDeviceInfo == null || string.IsNullOrEmpty(htcDeviceInfo.Mid) || string.IsNullOrEmpty(htcDeviceInfo.Cid);
				if (flag)
				{
					Tracer<HtcAdaptation>.WriteInformation("Unable to read MID and CID from HTCDeviceInfo.exe");
					Thread.Sleep(1000);
				}
			}
			while (!cancellationToken.IsCancellationRequested && (htcDeviceInfo == null || string.IsNullOrEmpty(htcDeviceInfo.Mid)) && num-- > 0);
			return htcDeviceInfo != null;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025BC File Offset: 0x000007BC
		public override void DownloadPackage(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<HtcAdaptation>.LogEntry("DownloadPackage");
			base.RaiseProgressPercentageChanged(0, null);
			try
			{
				this.reportingService.OperationStarted(phone, ReportOperationType.DownloadPackage);
				QueryParameters queryParameters = HtcMsrQuery.CreateQueryParameters(phone.Mid, phone.Cid);
				string htcProductsPath = Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.GetHtcProductsPath(string.Format("{0}-{1}", queryParameters.ManufacturerHardwareModel, queryParameters.ManufacturerHardwareVariant));
				DownloadParameters downloadParameters = new DownloadParameters
				{
					DiscoveryParameters = queryParameters,
					DestinationFolder = htcProductsPath,
					FilesVersioned = true
				};
				Tracer<HtcAdaptation>.WriteInformation("Download Params: {0}", new object[] { downloadParameters });
				phone.PackageFiles = this.msrService.DownloadLatestPackage(downloadParameters, cancellationToken);
				Tuple<long, long, bool> downloadPackageInformation = this.msrService.GetDownloadPackageInformation();
				this.reportingService.SetDownloadByteInformation(phone, ReportOperationType.DownloadPackage, downloadPackageInformation.Item1, downloadPackageInformation.Item2, downloadPackageInformation.Item3);
				this.reportingService.OperationSucceded(phone, ReportOperationType.DownloadPackage);
			}
			catch (Exception ex)
			{
				bool flag = true;
				bool flag2 = ex is OperationCanceledException || ex.GetBaseException() is TaskCanceledException;
				UriData uriData;
				if (flag2)
				{
					uriData = UriData.DownloadVariantPackageAbortedByUser;
					flag = false;
				}
				else
				{
					uriData = UriData.FailedToDownloadVariantPackage;
				}
				Tuple<long, long, bool> downloadPackageInformation2 = this.msrService.GetDownloadPackageInformation();
				this.reportingService.SetDownloadByteInformation(phone, ReportOperationType.DownloadPackage, downloadPackageInformation2.Item1, downloadPackageInformation2.Item2, downloadPackageInformation2.Item3);
				this.reportingService.OperationFailed(phone, ReportOperationType.DownloadPackage, uriData, ex);
				Tracer<HtcAdaptation>.WriteError(ex);
				bool flag3 = flag;
				if (flag3)
				{
					throw;
				}
			}
			finally
			{
				Tracer<HtcAdaptation>.LogExit("DownloadPackage");
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002778 File Offset: 0x00000978
		private void MsrDownloadProgressEvent(ProgressChangedEventArgs progressArgs)
		{
			base.RaiseProgressPercentageChanged(progressArgs.Percentage, progressArgs.Message, progressArgs.DownloadedSize, progressArgs.TotalSize, progressArgs.BytesPerSecond, progressArgs.SecondsLeft);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000027A8 File Offset: 0x000009A8
		public override void FlashDevice(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<HtcAdaptation>.LogEntry("FlashDevice");
			base.RaiseProgressPercentageChanged(-1, null);
			this.lastProgressPercentage = 0;
			Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.CheckAndCreatePath(Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.HtcProductsPath);
			bool flag = string.IsNullOrEmpty(phone.Mid) && (string.IsNullOrEmpty(phone.Cid) || !ApplicationInfo.IsInternal());
			if (flag)
			{
				throw new FlashException("Device data is missing (Mid and/or Cid)");
			}
			string text = string.Empty;
			string text2 = string.Empty;
			foreach (string text3 in phone.PackageFiles)
			{
				string fileName = Path.GetFileName(text3);
				bool flag2 = fileName != null && fileName.ToLower().StartsWith("uefi");
				if (flag2)
				{
					text = text3;
				}
				else
				{
					bool flag3 = fileName != null && fileName.ToLower().StartsWith("ruu");
					if (flag3)
					{
						text2 = text3;
					}
				}
			}
			this.CheckForMissingFiles(new string[]
			{
				text,
				text2,
				Path.Combine(this.GetWorkingDirectoryPath(), "HTCRomUpdater.exe")
			});
			try
			{
				this.FlashDevice("HTCRomUpdater.exe", text, text2, phone.Path);
			}
			finally
			{
				Tracer<HtcAdaptation>.LogExit("FlashDevice");
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002910 File Offset: 0x00000B10
		private void CheckForMissingFiles(params string[] files)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in files)
			{
				bool flag = !Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.CheckIfFileIsValid(text);
				if (flag)
				{
					stringBuilder.Append(text + "\n");
				}
			}
			bool flag2 = stringBuilder.Length > 0;
			if (flag2)
			{
				throw new FileNotFoundException(stringBuilder.ToString());
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000297C File Offset: 0x00000B7C
		private void FlashDevice(string appName, string uefiFile, string ruuFile, string phonePath)
		{
			this.flashingResult = null;
			ProcessStartInfo processStartInfo = new ProcessStartInfo
			{
				FileName = appName,
				Arguments = string.Format("\"{0}\" \"{1}\" -s \"{2}\"", uefiFile, ruuFile, phonePath),
				UseShellExecute = false,
				RedirectStandardOutput = true,
				CreateNoWindow = true,
				WorkingDirectory = this.GetWorkingDirectoryPath()
			};
			Tracer<HtcAdaptation>.WriteInformation("filename: {0} | working directory: {1} | arguments: {2}", new object[] { appName, processStartInfo.WorkingDirectory, processStartInfo.Arguments });
			ProcessHelper processHelper = new ProcessHelper
			{
				EnableRaisingEvents = true,
				StartInfo = processStartInfo
			};
			processHelper.OutputDataReceived += this.FlashOnOutputDataReceived;
			Tracer<HtcAdaptation>.WriteInformation("Starting flash process");
			processHelper.Start();
			processHelper.BeginOutputReadLine();
			processHelper.WaitForExit();
			Tracer<HtcAdaptation>.WriteInformation("Flash process finished");
			this.CheckForPossibleFlashErrors();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002A5C File Offset: 0x00000C5C
		private void CheckForPossibleFlashErrors()
		{
			bool flag = string.IsNullOrWhiteSpace(this.flashingResult) || this.flashingResult == "0";
			if (flag)
			{
				return;
			}
			bool flag2 = this.flashingResult.StartsWith("8007");
			if (flag2)
			{
				throw new HtcUsbPortOpenException();
			}
			bool flag3 = this.flashingResult.StartsWith("A001");
			if (flag3)
			{
				throw new HtcDeviceCommunicationException();
			}
			bool flag4 = this.flashingResult.StartsWith("A002");
			if (flag4)
			{
				throw new HtcServiceControlException();
			}
			bool flag5 = this.flashingResult.StartsWith("A003");
			if (flag5)
			{
				throw new HtcUsbCommunicationException();
			}
			bool flag6 = this.flashingResult.StartsWith("A011");
			if (flag6)
			{
				throw new HtcDeviceHandshakingException();
			}
			bool flag7 = this.flashingResult.StartsWith("A012");
			if (flag7)
			{
				throw new HtcPackageFileCheckException();
			}
			throw new FlashException(this.lastProgressMessage);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002B48 File Offset: 0x00000D48
		public override void CheckPackageIntegrity(Phone phone, CancellationToken cancellationToken)
		{
			Tracer<HtcAdaptation>.LogEntry("CheckPackageIntegrity");
			Tracer<HtcAdaptation>.WriteWarning("NOT IMPLEMENTED!!!", new object[0]);
			Tracer<HtcAdaptation>.LogExit("CheckPackageIntegrity");
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002B74 File Offset: 0x00000D74
		private string GetWorkingDirectoryPath()
		{
			string directoryName = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
			bool flag = string.IsNullOrWhiteSpace(directoryName);
			if (flag)
			{
				Tracer<HtcAdaptation>.WriteError("Could not find working directory path", new object[0]);
				throw new Exception("Could not find working directory path");
			}
			return directoryName;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024FC File Offset: 0x000006FC
		public override List<PackageFileInfo> FindAllPackages(string directory, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public override PackageFileInfo CheckLatestPackage(Phone phone, CancellationToken cancellationToken)
		{
			PackageFileInfo result;
			try
			{
				Tracer<HtcAdaptation>.LogEntry("CheckLatestPackage");
				Task<PackageFileInfo> task = this.msrService.CheckLatestPackage(this.DeviceQueryParameters(phone), cancellationToken);
				task.Wait(cancellationToken);
				result = task.Result;
			}
			catch (Exception ex)
			{
				bool flag = ex.InnerException is PackageNotFoundException;
				if (flag)
				{
					throw ex.InnerException;
				}
				bool flag2 = ex.InnerException != null && ex.InnerException.GetBaseException() is WebException;
				if (flag2)
				{
					throw new WebException();
				}
				bool flag3 = ex is OperationCanceledException || ex.InnerException is TaskCanceledException;
				if (flag3)
				{
					throw;
				}
				throw;
			}
			finally
			{
				Tracer<HtcAdaptation>.LogExit("CheckLatestPackage");
			}
			return result;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public override List<PackageFileInfo> FindPackage(string directory, Phone currentPhone, CancellationToken cancellationToken)
		{
			Tracer<HtcAdaptation>.LogEntry("FindPackage");
			Tracer<HtcAdaptation>.LogExit("FindPackage");
			return new List<PackageFileInfo>();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public override void ReadDeviceInfo(Phone phone, CancellationToken token)
		{
			HtcDeviceInfo htcDeviceInfo;
			do
			{
				htcDeviceInfo = this.ReadDeviceInfo(phone.InstanceId);
				bool flag = htcDeviceInfo == null || string.IsNullOrEmpty(htcDeviceInfo.Mid) || string.IsNullOrEmpty(htcDeviceInfo.Cid);
				if (flag)
				{
					Tracer<HtcAdaptation>.WriteInformation("Unable to read MID and CID from HTCDeviceInfo.exe");
					Thread.Sleep(1000);
				}
				else
				{
					base.RaiseDeviceInfoRead(new Phone(phone.PortId, phone.Vid, phone.Pid, phone.LocationPath, phone.HardwareModel, phone.HardwareVariant, phone.SalesName, phone.SoftwareVersion, phone.Path, phone.Type, phone.InstanceId, phone.SalesNameProvider, true, htcDeviceInfo.Mid, htcDeviceInfo.Cid));
				}
			}
			while (!token.IsCancellationRequested && (htcDeviceInfo == null || string.IsNullOrEmpty(htcDeviceInfo.Mid)));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002DB8 File Offset: 0x00000FB8
		private HtcDeviceInfo ReadDeviceInfo(string instanceId)
		{
			Tracer<HtcAdaptation>.LogEntry("ReadDeviceInfo");
			this.deviceInfos = new Dictionary<string, HtcDeviceInfo>();
			ProcessStartInfo processStartInfo = new ProcessStartInfo
			{
				FileName = "HTCDeviceInfo.exe",
				Arguments = "all",
				UseShellExecute = false,
				RedirectStandardError = true,
				RedirectStandardOutput = true,
				CreateNoWindow = true,
				WorkingDirectory = this.GetWorkingDirectoryPath()
			};
			ProcessHelper processHelper = new ProcessHelper
			{
				EnableRaisingEvents = true,
				StartInfo = processStartInfo
			};
			processHelper.OutputDataReceived += this.ReadDeviceInfoOnOutputDataReceived;
			processHelper.Start();
			processHelper.BeginOutputReadLine();
			processHelper.WaitForExit();
			Tracer<HtcAdaptation>.LogExit("ReadDeviceInfo");
			string deviceId = instanceId.Substring(instanceId.LastIndexOf('\\') + 2).ToLower();
			return this.deviceInfos.Values.FirstOrDefault((HtcDeviceInfo di) => di.Path.ToLower().Contains(deviceId));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002EB4 File Offset: 0x000010B4
		private void ReadDeviceInfoOnOutputDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
		{
			try
			{
				string data = dataReceivedEventArgs.Data;
				bool flag = string.IsNullOrEmpty(data);
				if (!flag)
				{
					bool flag2 = data.ToLower().Contains("error");
					if (flag2)
					{
						Tracer<HtcAdaptation>.WriteError("Unable to read device info.", new object[0]);
					}
					else
					{
						string text;
						string text2;
						this.TryReadNumberValue(data, out text, out text2);
						bool flag3 = text2.ToLower().Contains("usb#");
						if (flag3)
						{
							this.deviceInfos.Add(text, new HtcDeviceInfo(text2));
							Tracer<HtcAdaptation>.WriteInformation("{0} usb Path found: {1}.", new object[] { text, text2 });
						}
						else
						{
							bool flag4 = text2.ToLower().Contains("mid");
							if (flag4)
							{
								int num = text2.Trim().IndexOf(" ", StringComparison.Ordinal);
								string text3 = text2.Trim().Substring(num + 1, 8);
								string text4 = Path.GetInvalidFileNameChars().Aggregate(text3, (string current, char c) => current.Replace(c.ToString(CultureInfo.InvariantCulture), "0"));
								this.deviceInfos[text].Mid = text4;
								Tracer<HtcAdaptation>.WriteInformation("{0} mid found: {1}, replaced with {2}.", new object[] { text, text3, text4 });
							}
							else
							{
								bool flag5 = text2.ToLower().Contains("cid");
								if (flag5)
								{
									int num2 = text2.Trim().IndexOf(" ", StringComparison.Ordinal);
									string text5 = text2.Trim().Substring(num2 + 1);
									string text6 = Path.GetInvalidFileNameChars().Aggregate(text5, (string current, char c) => current.Replace(c.ToString(CultureInfo.InvariantCulture), "0"));
									this.deviceInfos[text].Cid = text6;
									Tracer<HtcAdaptation>.WriteInformation("{0} cid found: {1}, replaced with {2}.", new object[] { text, text5, text6 });
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Tracer<HtcAdaptation>.WriteWarning("Error parsing HTCDeviceInfo output. Unable to parse string: {0}, exception {1}", new object[] { dataReceivedEventArgs.Data, ex });
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000030E4 File Offset: 0x000012E4
		private void TryReadNumberValue(string input, out string number, out string value)
		{
			number = null;
			value = null;
			try
			{
				number = input.Substring(0, 3);
				value = input.Substring(4);
			}
			catch (Exception ex)
			{
				Tracer<HtcAdaptation>.WriteWarning("Error parsing HTCDeviceInfo output. Unable to parse string: {0}, exception {1}", new object[] { input, ex });
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003140 File Offset: 0x00001340
		private void FlashOnOutputDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
		{
			try
			{
				string data = dataReceivedEventArgs.Data;
				Tracer<HtcAdaptation>.WriteInformation(data);
				bool flag = data.Contains("[MESSAGE]");
				if (flag)
				{
					this.lastProgressMessage = data.Substring(9, data.IndexOf("[/MESSAGE]", StringComparison.Ordinal) - 9);
				}
				bool flag2 = data.Contains("[PERCENTAGE]");
				if (flag2)
				{
					string text = data.Substring(12, data.IndexOf("[/PERCENTAGE]", StringComparison.Ordinal) - 12);
					this.lastProgressPercentage = int.Parse(text);
				}
				bool flag3 = data.Contains("[RESULT]");
				if (flag3)
				{
					this.flashingResult = data.Substring(8, data.IndexOf("[/RESULT]", StringComparison.Ordinal) - 8);
				}
				base.RaiseProgressPercentageChanged(this.lastProgressPercentage, this.lastProgressMessage);
			}
			catch (Exception ex)
			{
				Tracer<HtcAdaptation>.WriteWarning("Error parsing HTCRomUpdater output. Unable to parse string: {0}, exception {1}", new object[] { dataReceivedEventArgs.Data, ex });
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000323C File Offset: 0x0000143C
		protected override Stream GetImageDataStream(Phone phone)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
			string text = string.Empty;
			bool flag = phone.Vid.ToUpper() == "0BB4" && (phone.Pid.ToUpper() == "0BAC" || phone.Pid.ToUpper() == "0BAD" || phone.Pid.ToUpper() == "0BAE");
			if (flag)
			{
				text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("HTCOne.png"));
			}
			else
			{
				bool flag2 = phone.Vid.ToUpper() == "0BB4";
				if (flag2)
				{
					text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("HTC8X.png"));
				}
			}
			bool flag3 = !string.IsNullOrEmpty(text);
			Stream stream;
			if (flag3)
			{
				stream = executingAssembly.GetManifestResourceStream(text);
			}
			else
			{
				stream = base.GetImageDataStream(phone);
			}
			return stream;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003358 File Offset: 0x00001558
		private QueryParameters DeviceQueryParameters(Phone phone)
		{
			return new QueryParameters
			{
				ManufacturerName = "HTC",
				ManufacturerHardwareModel = phone.Mid,
				ManufacturerHardwareVariant = phone.Cid
			};
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003398 File Offset: 0x00001598
		protected override Stream GetManufacturerImageDataStream()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
			string text = manifestResourceNames.FirstOrDefault((string resourceName) => resourceName.Contains("HtcLogo.png"));
			bool flag = !string.IsNullOrEmpty(text);
			Stream stream;
			if (flag)
			{
				stream = executingAssembly.GetManifestResourceStream(text);
			}
			else
			{
				stream = null;
			}
			return stream;
		}

		// Token: 0x0400000E RID: 14
		private const string RuuExeFile = "HTCRomUpdater.exe";

		// Token: 0x0400000F RID: 15
		private const int NumberOfTrials = 15;

		// Token: 0x04000010 RID: 16
		private readonly MsrService msrService;

		// Token: 0x04000011 RID: 17
		private readonly ReportingService reportingService;

		// Token: 0x04000012 RID: 18
		private readonly HtcModelsCatalog htcModelsCatalog;

		// Token: 0x04000013 RID: 19
		private readonly SalesNameProvider salesNameProvider;

		// Token: 0x04000014 RID: 20
		private Dictionary<string, HtcDeviceInfo> deviceInfos;

		// Token: 0x04000015 RID: 21
		private string lastProgressMessage;

		// Token: 0x04000016 RID: 22
		private int lastProgressPercentage;

		// Token: 0x04000017 RID: 23
		private string flashingResult;
	}
}
