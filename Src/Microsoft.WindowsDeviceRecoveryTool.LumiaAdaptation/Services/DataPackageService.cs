using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Contracts;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services.DataPackageRules;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;
using Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;
using Nokia.Mira;

namespace Microsoft.WindowsDeviceRecoveryTool.LumiaAdaptation.Services
{
	// Token: 0x02000004 RID: 4
	[Export(typeof(IUseProxy))]
	[Export(typeof(DataPackageService))]
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class DataPackageService : IUseProxy, IDisposable
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000230E File Offset: 0x0000050E
		[ImportingConstructor]
		public DataPackageService(FileChecker fileChecker)
		{
			this.fileChecker = fileChecker;
			this.speedCalculator = new SpeedCalculator();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000009 RID: 9 RVA: 0x0000232C File Offset: 0x0000052C
		// (remove) Token: 0x0600000A RID: 10 RVA: 0x00002364 File Offset: 0x00000564
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int> IntegrityCheckProgressEvent;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600000B RID: 11 RVA: 0x0000239C File Offset: 0x0000059C
		// (remove) Token: 0x0600000C RID: 12 RVA: 0x000023D4 File Offset: 0x000005D4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<DownloadingProgressChangedEventArgs> DownloadProgressChanged;

		// Token: 0x0600000D RID: 13 RVA: 0x00002409 File Offset: 0x00000609
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000241C File Offset: 0x0000061C
		public void IntegrityCheckProgressChanged(double progress)
		{
			Action<int> handle = this.IntegrityCheckProgressEvent;
			bool flag = handle != null;
			if (flag)
			{
				AppDispatcher.Execute(delegate
				{
					handle((int)progress);
				}, false);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002465 File Offset: 0x00000665
		public void IntegrityCheckDuringDownloadProgressChanged(double progress)
		{
			Tracer<DataPackageService>.WriteInformation(string.Format("Current integrity check progress is {0} ", progress));
			this.lastProgressPercentage = 95 + (int)(progress / 20.0);
			this.RaiseDownloadProgressChangedEvent();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000249C File Offset: 0x0000069C
		public List<VariantInfo> FindLocalVariants()
		{
			List<VariantInfo> list = new List<VariantInfo>();
			this.FindLocalVariants(list, Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.DefaultProductsPath);
			this.FindLocalVariants(list, Microsoft.WindowsDeviceRecoveryTool.Model.FileSystemInfo.NokiaProductsPath);
			return list;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000024D0 File Offset: 0x000006D0
		public void FindLocalVariants(List<VariantInfo> allVariants, string localPath)
		{
			Tracer<DataPackageService>.LogEntry("FindLocalVariants");
			bool flag = !Directory.Exists(localPath);
			if (flag)
			{
				Tracer<DataPackageService>.WriteInformation("Directory doesn't exist: {0}", new object[] { localPath });
			}
			else
			{
				string[] directories = Directory.GetDirectories(localPath, "rm-*", SearchOption.AllDirectories);
				foreach (string text in directories)
				{
					string[] files = Directory.GetFiles(Path.Combine(localPath, text), "*.vpl");
					allVariants.AddRange(files.Select(new Func<string, VariantInfo>(VariantInfo.GetVariantInfo)));
				}
				Tracer<DataPackageService>.LogExit("FindLocalVariants");
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000256C File Offset: 0x0000076C
		public void CheckVariantIntegrity(string vplPath, CancellationToken cancellationToken)
		{
			Tracer<DataPackageService>.LogEntry("CheckVariantIntegrity");
			Tracer<DataPackageService>.WriteInformation("Checking: {0}", new object[] { vplPath });
			this.fileChecker.SetProgressHandler(new Action<double>(this.IntegrityCheckProgressChanged));
			VplContent vplContent = new VplContent();
			vplContent.ParseVplFile(vplPath);
			string directoryName = Path.GetDirectoryName(vplPath);
			bool flag = string.IsNullOrEmpty(directoryName);
			if (flag)
			{
				throw new DirectoryNotFoundException("Vpl directory not found");
			}
			List<string> list = (from file in vplContent.FileList
				where !string.IsNullOrEmpty(file.Name) && !file.Optional
				select file.Name).ToList<string>();
			foreach (string text in list)
			{
				bool flag2 = File.Exists(Path.Combine(directoryName, text));
				bool flag3 = !flag2;
				if (flag3)
				{
					throw new FirmwareFileNotFoundException();
				}
			}
			List<FileCrcInfo> list2 = new List<FileCrcInfo>();
			foreach (VplFile vplFile in vplContent.FileList)
			{
				bool flag4 = !string.IsNullOrEmpty(vplFile.Name) && !string.IsNullOrEmpty(vplFile.Crc) && list.Contains(vplFile.Name, StringComparer.OrdinalIgnoreCase);
				if (flag4)
				{
					list2.Add(new FileCrcInfo
					{
						FileName = vplFile.Name,
						Directory = directoryName,
						Crc = vplFile.Crc,
						Optional = vplFile.Optional
					});
				}
			}
			this.fileChecker.CheckFiles(list2, cancellationToken);
			Tracer<DataPackageService>.LogExit("CheckVariantIntegrity");
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002770 File Offset: 0x00000970
		public ReadOnlyCollection<string> FindLocalVariantPaths(string productType, string productCode, string searchPath, CancellationToken token)
		{
			bool flag = !Directory.Exists(searchPath);
			ReadOnlyCollection<string> readOnlyCollection;
			if (flag)
			{
				Tracer<DataPackageService>.WriteInformation("Directory doesn't exist: {0}", new object[] { searchPath });
				readOnlyCollection = new ReadOnlyCollection<string>(new List<string>());
			}
			else
			{
				readOnlyCollection = this.fileChecker.FindLocalVplFilePaths(productType, productCode, searchPath);
			}
			return readOnlyCollection;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000027C0 File Offset: 0x000009C0
		public Tuple<long, long, bool> GetDownloadPackageInformation()
		{
			return new Tuple<long, long, bool>(this.speedCalculator.CurrentDownloadedSize, this.totalFilesSize, this.speedCalculator.IsResumed);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000027F4 File Offset: 0x000009F4
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.disposed;
			if (!flag)
			{
				this.disposed = true;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002818 File Offset: 0x00000A18
		private void Download(List<File4> filesToDownload, string path, CancellationToken token, bool verifyChecksum = true)
		{
			this.lastProgressMessage = "DownloadingFiles";
			Action<DownloadProgressInfo> action = new Action<DownloadProgressInfo>(this.DownloadTaskProgress);
			Nokia.Mira.Progress<DownloadProgressInfo> progress = new Nokia.Mira.Progress<DownloadProgressInfo>(action);
			List<Task> list = new List<Task>();
			DownloadSettings downloadSettings = new DownloadSettings(5, 3145728L, true, true);
			foreach (File4 file in filesToDownload)
			{
				Uri uri = new Uri(file.DownloadUrl);
				HttpWebRequestFactory httpWebRequestFactory = new HttpWebRequestFactory(uri)
				{
					Proxy = this.Proxy()
				};
				WebFile webFile = new WebFile(httpWebRequestFactory);
				Task task = webFile.DownloadAsync(Path.Combine(path, file.FileName), token, progress, downloadSettings);
				task.ContinueWith(new Action<Task>(this.DownloadTaskFinished), token);
				list.Add(task);
			}
			try
			{
				Task.WaitAll(list.ToArray());
			}
			catch (Exception ex)
			{
				bool flag = ex.InnerException is AggregateException && ex.InnerException.InnerException is IOException && (long)ex.InnerException.InnerException.HResult == -2147024784L;
				if (!flag)
				{
					throw;
				}
				string pathRoot = Path.GetPathRoot(path);
				bool flag2 = pathRoot == null;
				if (flag2)
				{
					throw;
				}
				long availableFreeSpace = new DriveInfo(pathRoot).AvailableFreeSpace;
				throw new NotEnoughSpaceException
				{
					Available = availableFreeSpace,
					Needed = this.speedCalculator.TotalFilesSize - this.speedCalculator.TotalDownloadedSize,
					Disk = pathRoot
				};
			}
			token.ThrowIfCancellationRequested();
			if (verifyChecksum)
			{
				this.lastProgressPercentage = 95;
				this.lastProgressMessage = "VerifyingDownloadedFiles";
				this.RaiseDownloadProgressChangedEvent();
				this.fileChecker.SetProgressHandler(new Action<double>(this.IntegrityCheckDuringDownloadProgressChanged));
				this.fileChecker.CheckFilesCorrectness(path, filesToDownload, token);
			}
			this.lastProgressPercentage = 100;
			this.lastProgressMessage = string.Empty;
			this.RaiseDownloadProgressChangedEvent();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002A30 File Offset: 0x00000C30
		private void DownloadTaskFinished(Task task)
		{
			TaskStatus status = task.Status;
			bool flag = status.Equals(TaskStatus.Faulted);
			if (flag)
			{
				Tracer<DataPackageService>.WriteError("Downloading variant file failed.", new object[] { task.Exception });
			}
			else
			{
				bool flag2 = status.Equals(TaskStatus.Canceled);
				if (flag2)
				{
					Tracer<DataPackageService>.WriteInformation("Download cancelled on the variant file.");
				}
				else
				{
					Tracer<DataPackageService>.WriteInformation("Variant file successfully downloaded.");
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002AAC File Offset: 0x00000CAC
		private void DownloadTaskProgress(DownloadProgressInfo info)
		{
			bool flag = !this.speedCalculator.IsDownloadStarted;
			if (flag)
			{
				this.speedCalculator.PreviousDownloadedSize += info.BytesReceived;
				this.speedCalculator.IsDownloadStarted = true;
			}
			else
			{
				this.speedCalculator.CurrentDownloadedSize += info.BytesReceived;
			}
			bool flag2 = this.totalFilesSize > 0L;
			if (flag2)
			{
				this.lastProgressPercentage = (int)(this.speedCalculator.TotalDownloadedSize * 95L / this.totalFilesSize);
			}
			this.RaiseDownloadProgressChangedEvent();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002B48 File Offset: 0x00000D48
		private void RaiseDownloadProgressChangedEvent()
		{
			Action<DownloadingProgressChangedEventArgs> downloadProgressChanged = this.DownloadProgressChanged;
			bool flag = downloadProgressChanged != null;
			if (flag)
			{
				downloadProgressChanged(new DownloadingProgressChangedEventArgs(this.lastProgressPercentage, this.speedCalculator.TotalDownloadedSize, this.speedCalculator.TotalFilesSize, this.speedCalculator.BytesPerSecond, this.speedCalculator.RemaingSeconds, this.lastProgressMessage));
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002BAC File Offset: 0x00000DAC
		public EmergencyPackageInfo DownloadEmergencyPackage(string typeDesignator, string destinationPath, CancellationToken cancellationToken)
		{
			EmergencyPackageInfo emergencyPackageInfo;
			try
			{
				emergencyPackageInfo = this.TryDownloadEmergencyPackage(typeDesignator, destinationPath, cancellationToken);
			}
			catch (AggregateException ex)
			{
				Exception ex2 = ex;
				while (ex2.InnerException != null)
				{
					ex2 = ex2.InnerException;
					bool flag = (ex2 is IOException || ex2.InnerException is IOException) && ((long)ex2.HResult == 39L || (long)ex2.HResult == 112L);
					if (flag)
					{
						throw ex2;
					}
					bool flag2 = ex2 is AggregateException && ex2.InnerException is IOException && ((long)ex2.InnerException.HResult == -2146233088L || (long)ex2.InnerException.HResult == -2146232800L);
					if (flag2)
					{
						throw new WebException(ex2.GetBaseException().Message);
					}
				}
				throw new DownloadPackageException(ex2.GetBaseException().Message, ex);
			}
			catch (Exception ex3)
			{
				bool flag3 = ex3 is OperationCanceledException || ex3 is NotEnoughSpaceException || ex3 is PlannedServiceBreakException || ex3 is CannotAccessDirectoryException || ex3.InnerException is TaskCanceledException || ex3.InnerException is OperationCanceledException;
				if (flag3)
				{
					throw;
				}
				throw new DownloadPackageException(ex3.GetBaseException().Message, ex3);
			}
			return emergencyPackageInfo;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002D18 File Offset: 0x00000F18
		private EmergencyPackageInfo TryDownloadEmergencyPackage(string typeDesignator, string destinationPath, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			string text = string.Format("https://repairavoidance.blob.core.windows.net/packages/EmergencyFlash/{0}/{1}", typeDesignator, "emergency_flash_config.xml");
			File4 file = new File4("emergency_flash_config.xml", string.Empty, this.QueryFileLength(text), text, string.Empty, 0);
			List<File4> list = new List<File4> { file };
			this.totalFilesSize = 0L;
			this.speedCalculator.Reset();
			FileChecker.ValidateSpaceAvailability(destinationPath, list.Sum((File4 f) => f.FileSize));
			this.Download(list, destinationPath, cancellationToken, false);
			EmergencyPackageInfo emergencyPackageInfo = new EmergencyPackageInfo
			{
				ConfigFilePath = Path.Combine(destinationPath, "emergency_flash_config.xml")
			};
			list = this.GetEmergencyFilesList(Path.Combine(destinationPath, "emergency_flash_config.xml"), typeDesignator, destinationPath, ref emergencyPackageInfo);
			this.totalFilesSize = list.Sum((File4 f) => f.FileSize);
			FileChecker.ValidateSpaceAvailability(destinationPath, list.Sum((File4 f) => f.FileSize));
			try
			{
				this.speedCalculator.Start(this.totalFilesSize, this.totalFilesSize - list.Sum((File4 f) => f.FileSize));
				this.Download(list, destinationPath, cancellationToken, false);
			}
			finally
			{
				this.speedCalculator.Stop();
			}
			cancellationToken.ThrowIfCancellationRequested();
			return emergencyPackageInfo;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002EB8 File Offset: 0x000010B8
		private List<File4> GetEmergencyFilesList(string emergencyConfigFilePath, string typeDesignator, string destinationPath, ref EmergencyPackageInfo emergencyPackageInfo)
		{
			List<File4> list = new List<File4>();
			using (FileStream fileStream = new FileStream(emergencyConfigFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				XElement xelement = XElement.Load(fileStream);
				List<File4> list2 = (from file in xelement.Descendants("first_boot_image")
					select new File4(file.Attribute("image_path").Value, string.Empty, this.QueryFileLength(string.Format("https://repairavoidance.blob.core.windows.net/packages/EmergencyFlash/{0}/{1}", typeDesignator, file.Attribute("image_path").Value)), string.Format("https://repairavoidance.blob.core.windows.net/packages/EmergencyFlash/{0}/{1}", typeDesignator, file.Attribute("image_path").Value), string.Empty, 0)).ToList<File4>();
				list.AddRange(list2);
				bool flag = list2.Any<File4>();
				if (flag)
				{
					emergencyPackageInfo.HexFlasherFilePath = Path.Combine(destinationPath, list2.First<File4>().FileName);
				}
				List<File4> list3 = (from file in xelement.Descendants("firehose_image")
					select new File4(file.Attribute("image_path").Value, string.Empty, this.QueryFileLength(string.Format("https://repairavoidance.blob.core.windows.net/packages/EmergencyFlash/{0}/{1}", typeDesignator, file.Attribute("image_path").Value)), string.Format("https://repairavoidance.blob.core.windows.net/packages/EmergencyFlash/{0}/{1}", typeDesignator, file.Attribute("image_path").Value), string.Empty, 0)).ToList<File4>();
				list.AddRange(list3);
				bool flag2 = list3.Any<File4>();
				if (flag2)
				{
					emergencyPackageInfo.EdpImageFilePath = Path.Combine(destinationPath, list3.First<File4>().FileName);
				}
				List<File4> list4 = (from file in xelement.Descendants("hex_flasher")
					select new File4(file.Attribute("image_path").Value, string.Empty, this.QueryFileLength(string.Format("https://repairavoidance.blob.core.windows.net/packages/EmergencyFlash/{0}/{1}", typeDesignator, file.Attribute("image_path").Value)), string.Format("https://repairavoidance.blob.core.windows.net/packages/EmergencyFlash/{0}/{1}", typeDesignator, file.Attribute("image_path").Value), string.Empty, 0)).ToList<File4>();
				list.AddRange(list4);
				bool flag3 = list4.Any<File4>();
				if (flag3)
				{
					emergencyPackageInfo.HexFlasherFilePath = Path.Combine(destinationPath, list4.First<File4>().FileName);
				}
				List<File4> list5 = (from file in xelement.Descendants("mbn_image")
					select new File4(file.Attribute("image_path").Value, string.Empty, this.QueryFileLength(string.Format("https://repairavoidance.blob.core.windows.net/packages/EmergencyFlash/{0}/{1}", typeDesignator, file.Attribute("image_path").Value)), string.Format("https://repairavoidance.blob.core.windows.net/packages/EmergencyFlash/{0}/{1}", typeDesignator, file.Attribute("image_path").Value), string.Empty, 0)).ToList<File4>();
				list.AddRange(list5);
				bool flag4 = list5.Any<File4>();
				if (flag4)
				{
					emergencyPackageInfo.MbnImageFilePath = Path.Combine(destinationPath, list5.First<File4>().FileName);
				}
			}
			return list;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003090 File Offset: 0x00001290
		public void SetProxy(IWebProxy settings)
		{
			this.proxySettings = settings;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000309C File Offset: 0x0000129C
		private IWebProxy Proxy()
		{
			return this.proxySettings ?? WebRequest.GetSystemWebProxy();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000030C0 File Offset: 0x000012C0
		private long QueryFileLength(string downloadUrl)
		{
			WebRequest webRequest = WebRequest.Create(downloadUrl);
			webRequest.Proxy = this.Proxy();
			webRequest.Method = "HEAD";
			using (WebResponse response = webRequest.GetResponse())
			{
				long num;
				bool flag = long.TryParse(response.Headers.Get("Content-Length"), out num);
				if (flag)
				{
					return num;
				}
			}
			return 0L;
		}

		// Token: 0x04000007 RID: 7
		private const int DefaultChunkSize = 3145728;

		// Token: 0x04000008 RID: 8
		private const int DefaultMaxChunks = 5;

		// Token: 0x04000009 RID: 9
		private const long ErrorHandleDiskFull = 39L;

		// Token: 0x0400000A RID: 10
		private const long ErrorEmptyDiskSpace = -2147024784L;

		// Token: 0x0400000B RID: 11
		private const long ErrorInternetConnection = -2146233088L;

		// Token: 0x0400000C RID: 12
		private const long ErrorServerConnection = -2146232800L;

		// Token: 0x0400000D RID: 13
		private const long ErrorDiskFull = 112L;

		// Token: 0x0400000E RID: 14
		private readonly FileChecker fileChecker;

		// Token: 0x0400000F RID: 15
		private readonly SpeedCalculator speedCalculator;

		// Token: 0x04000010 RID: 16
		private bool disposed;

		// Token: 0x04000011 RID: 17
		private int lastProgressPercentage;

		// Token: 0x04000012 RID: 18
		private string lastProgressMessage;

		// Token: 0x04000013 RID: 19
		private long totalFilesSize;

		// Token: 0x04000014 RID: 20
		private IWebProxy proxySettings;
	}
}
