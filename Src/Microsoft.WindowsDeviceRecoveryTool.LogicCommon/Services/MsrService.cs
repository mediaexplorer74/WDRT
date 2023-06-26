using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Common.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Contracts;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Msr;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services.DataPackageRules;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.DataPackage;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;
using SoftwareRepository.Discovery;
using SoftwareRepository.Streaming;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services
{
	// Token: 0x02000010 RID: 16
	[Export(typeof(IUseProxy))]
	[Export(typeof(MsrService))]
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class MsrService : BaseRemoteRepository, IUseProxy
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x000044B1 File Offset: 0x000026B1
		[ImportingConstructor]
		public MsrService(FileChecker fileChecker)
		{
			this.fileChecker = fileChecker;
			this.fileChecker.SetProgressHandler(new Action<double>(this.IntegrityCheckProgressChanged));
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060000C3 RID: 195 RVA: 0x000044E8 File Offset: 0x000026E8
		// (remove) Token: 0x060000C4 RID: 196 RVA: 0x00004520 File Offset: 0x00002720
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int> IntegrityCheckProgressEvent;

		// Token: 0x060000C5 RID: 197 RVA: 0x00004558 File Offset: 0x00002758
		public override async Task<PackageFileInfo> CheckLatestPackage(QueryParameters queryParameters, CancellationToken cancellationToken)
		{
			SoftwarePackage softwarePackage = await this.CheckLatestPackageInternal(queryParameters, cancellationToken);
			SoftwarePackage package = softwarePackage;
			softwarePackage = null;
			return new MsrPackageInfo(string.Empty, package.PackageTitle, package.PackageRevision)
			{
				ManufacturerModelName = package.ManufacturerModelName.FirstOrDefault<string>(),
				PackageFileData = this.ExtractMsrPackageFiles(package)
			};
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000045AC File Offset: 0x000027AC
		private IEnumerable<MsrPackageInfo.MsrFileInfo> ExtractMsrPackageFiles(SoftwarePackage package)
		{
			return package.Files.Select((SoftwareFile f) => new MsrPackageInfo.MsrFileInfo
			{
				FileName = f.FileName,
				FileType = (this.ReadFileTypeFromPackage(f, package) ?? f.FileType),
				FileNameWithRevision = this.GenerateSoftwareVersionFile(f.FileName, package.PackageRevision, true),
				FileVersion = this.ReadFileVersionFromPackage(f, package)
			});
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000045F0 File Offset: 0x000027F0
		private string ReadFileVersionFromPackage(SoftwareFile softwareFile, SoftwarePackage originPackage)
		{
			string text = null;
			bool flag = originPackage.ExtendedAttributes != null;
			if (flag)
			{
				Dictionary<string, string> dictionary = originPackage.ExtendedAttributes.Dictionary;
				bool flag2 = dictionary != null;
				if (flag2)
				{
					KeyValuePair<string, string> keyValuePair = dictionary.FirstOrDefault((KeyValuePair<string, string> kv) => string.Equals(softwareFile.FileName, kv.Value, StringComparison.InvariantCultureIgnoreCase));
					string text2 = "Component(?<component_number>\\d+)_FileName";
					bool flag3 = !string.IsNullOrWhiteSpace(keyValuePair.Key) && Regex.IsMatch(keyValuePair.Key, text2);
					if (flag3)
					{
						Match match = Regex.Match(keyValuePair.Key, text2);
						string value = match.Groups["component_number"].Value;
						string text3 = string.Format("Component{0}_Version", value);
						bool flag4 = dictionary.ContainsKey(text3);
						if (flag4)
						{
							text = dictionary[text3];
						}
					}
				}
			}
			return text;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000046D4 File Offset: 0x000028D4
		private string ReadFileTypeFromPackage(SoftwareFile softwareFile, SoftwarePackage originPackage)
		{
			string text = null;
			bool flag = originPackage.ExtendedAttributes != null;
			if (flag)
			{
				Dictionary<string, string> dictionary = originPackage.ExtendedAttributes.Dictionary;
				bool flag2 = dictionary != null;
				if (flag2)
				{
					KeyValuePair<string, string> keyValuePair = dictionary.FirstOrDefault((KeyValuePair<string, string> kv) => string.Equals(softwareFile.FileName, kv.Value, StringComparison.InvariantCultureIgnoreCase));
					string text2 = "Component(?<component_number>\\d+)_FileName";
					bool flag3 = !string.IsNullOrWhiteSpace(keyValuePair.Key) && Regex.IsMatch(keyValuePair.Key, text2);
					if (flag3)
					{
						Match match = Regex.Match(keyValuePair.Key, text2);
						string value = match.Groups["component_number"].Value;
						string text3 = string.Format("Component{0}_Type", value);
						bool flag4 = dictionary.ContainsKey(text3);
						if (flag4)
						{
							text = dictionary[text3];
						}
					}
				}
			}
			return text;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000047B8 File Offset: 0x000029B8
		public Tuple<long, long, bool> GetDownloadPackageInformation()
		{
			return new Tuple<long, long, bool>(base.SpeedCalculator.CurrentDownloadedSize, base.TotalFilesSize, base.SpeedCalculator.IsResumed);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000047EC File Offset: 0x000029EC
		private async Task<SoftwarePackage> CheckLatestPackageInternal(QueryParameters queryParameters, CancellationToken cancellationToken)
		{
			DiscoveryQueryParameters discoveryQueryParameters = new DiscoveryQueryParameters
			{
				ManufacturerName = queryParameters.ManufacturerName,
				ManufacturerModelName = queryParameters.ManufacturerModelName,
				ManufacturerProductLine = queryParameters.ManufacturerProductLine,
				PackageType = queryParameters.PackageType,
				PackageClass = queryParameters.PackageClass,
				ManufacturerHardwareModel = queryParameters.ManufacturerHardwareModel,
				ManufacturerHardwareVariant = queryParameters.ManufacturerHardwareVariant
			};
			bool flag = queryParameters.ExtendedAttributes != null && queryParameters.ExtendedAttributes.Count > 0;
			if (flag)
			{
				discoveryQueryParameters.ExtendedAttributes = new ExtendedAttributes
				{
					Dictionary = queryParameters.ExtendedAttributes
				};
			}
			Tracer<MsrService>.WriteInformation("MSR Query: {0}", new object[] { queryParameters });
			DiscoveryParameters discovererParams = new DiscoveryParameters
			{
				Query = discoveryQueryParameters
			};
			Discoverer discoverer = new Discoverer
			{
				SoftwareRepositoryAlternativeBaseUrl = (ApplicationInfo.UseTestServer() ? "https://pvprepo.azurewebsites.net" : "https://api.swrepository.com"),
				SoftwareRepositoryAuthenticationToken = null,
				SoftwareRepositoryProxy = base.Proxy()
			};
			DiscoveryJsonResult discoveryJsonResult = await discoverer.DiscoverJsonAsync(discovererParams, cancellationToken);
			DiscoveryJsonResult result = discoveryJsonResult;
			discoveryJsonResult = null;
			cancellationToken.ThrowIfCancellationRequested();
			if (result.StatusCode != HttpStatusCode.OK)
			{
				throw new PackageNotFoundException();
			}
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SoftwarePackages));
			using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(result.Result)))
			{
				SoftwarePackages packages = (SoftwarePackages)serializer.ReadObject(stream);
				if (packages != null)
				{
					Tracer<MsrService>.WriteInformation("Packages found: {0}", new object[] { packages.SoftwarePackageList.Count });
					SoftwarePackage correctPackage = packages.SoftwarePackageList.FirstOrDefault<SoftwarePackage>();
					if (correctPackage != null)
					{
						Tracer<MsrService>.WriteInformation("Version: {0}", new object[] { correctPackage.PackageRevision });
						return correctPackage;
					}
					correctPackage = null;
				}
				packages = null;
			}
			MemoryStream stream = null;
			throw new PackageNotFoundException();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004840 File Offset: 0x00002A40
		public override List<string> DownloadLatestPackage(DownloadParameters downloadParameters, CancellationToken cancellationToken)
		{
			List<string> list;
			try
			{
				this.progressUpdateResetTimer = new IntervalResetAccessTimer(MsrDownloadConfig.Instance.ReportingProgressIntervalMillis, true);
				this.progressUpdateResetTimer.StartTimer();
				list = this.DownloadPackage(downloadParameters, cancellationToken);
			}
			catch (DownloadPackageException)
			{
				throw;
			}
			catch (NotEnoughSpaceException)
			{
				throw;
			}
			catch (AggregateException ex)
			{
				bool flag = ex.GetBaseException() is PackageNotFoundException;
				if (flag)
				{
					throw ex.GetBaseException();
				}
				bool flag2 = ex.GetBaseException() is DownloadPackageException;
				if (flag2)
				{
					throw ex.GetBaseException();
				}
				Exception ex2 = ex;
				while (ex2.InnerException != null)
				{
					ex2 = ex2.InnerException;
					bool flag3 = ex2 is IOException && ((long)ex2.HResult == 39L || (long)ex2.HResult == 112L);
					if (flag3)
					{
						throw ex2;
					}
					bool flag4 = ex2.InnerException is IOException;
					if (flag4)
					{
						throw new NotEnoughSpaceException(ex2.InnerException.Message, ex2.InnerException);
					}
				}
				throw new DownloadPackageException("Downloading variant file failed.", ex);
			}
			catch (Exception ex3)
			{
				bool flag5 = ex3 is OperationCanceledException || ex3 is CannotAccessDirectoryException || ex3.InnerException is TaskCanceledException || ex3.InnerException is OperationCanceledException;
				if (flag5)
				{
					throw;
				}
				throw new DownloadPackageException("Downloading variant file failed.", ex3);
			}
			finally
			{
				base.SpeedCalculator.Stop();
				this.progressUpdateResetTimer.StopTimer();
			}
			return list;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004A28 File Offset: 0x00002C28
		private List<string> DownloadPackage(DownloadParameters downloadParameters, CancellationToken cancellationToken)
		{
			base.RaiseProgressChangedEvent(0, "CheckingAlreadyDownloadedFiles");
			Task<SoftwarePackage> task = this.CheckLatestPackageInternal(downloadParameters.DiscoveryParameters, cancellationToken);
			task.Wait(cancellationToken);
			cancellationToken.ThrowIfCancellationRequested();
			SoftwarePackage package = task.Result;
			List<SoftwareFile> list = package.Files;
			this.lastDownloadedSize = 0L;
			bool flag = !string.IsNullOrEmpty(downloadParameters.FileExtension);
			if (flag)
			{
				list = package.Files.Where((SoftwareFile file) => file.FileName.ToLower().EndsWith(downloadParameters.FileExtension.ToLower())).ToList<SoftwareFile>();
			}
			base.TotalFilesSize = list.Sum((SoftwareFile f) => f.FileSize);
			List<SoftwareFile> notDownloadedFiles = this.GetNotDownloadedFiles(downloadParameters.DestinationFolder, list, package.PackageRevision, downloadParameters.FilesVersioned, cancellationToken);
			bool flag2 = notDownloadedFiles.Count > 0;
			if (flag2)
			{
				long num = notDownloadedFiles.Sum((SoftwareFile file) => file.FileSize);
				FileChecker.ValidateSpaceAvailability(downloadParameters.DestinationFolder, num);
				base.RaiseProgressChangedEvent(0, "DownloadingFiles");
				base.SpeedCalculator.Start(base.TotalFilesSize, base.TotalFilesSize - num);
				Dictionary<string, long> dictionary = new Dictionary<string, long>();
				List<Task> list2 = new List<Task>();
				foreach (SoftwareFile softwareFile in notDownloadedFiles)
				{
					string text = this.GenerateSoftwareVersionFile(softwareFile.FileName, package.PackageRevision, downloadParameters.FilesVersioned);
					string text2 = Path.Combine(downloadParameters.DestinationFolder, text);
					Task task2 = this.DownloadAsync(package.Id, softwareFile.FileName, text2, downloadParameters.DestinationFolder, dictionary, cancellationToken);
					dictionary.Add(softwareFile.FileName, 0L);
					task2.ContinueWith(new Action<Task>(this.DownloadTaskFinished), cancellationToken);
					list2.Add(task2);
				}
				Task.WaitAll(list2.ToArray(), cancellationToken);
				cancellationToken.ThrowIfCancellationRequested();
			}
			base.RaiseProgressChangedEvent(95, "VerifyingDownloadedFiles");
			this.CheckFilesCorrectness(downloadParameters.DestinationFolder, notDownloadedFiles, package.PackageRevision, downloadParameters.FilesVersioned, cancellationToken);
			base.RaiseProgressChangedEvent(100, null);
			return list.Select((SoftwareFile file) => Path.Combine(downloadParameters.DestinationFolder, this.GenerateSoftwareVersionFile(file.FileName, package.PackageRevision, downloadParameters.FilesVersioned))).ToList<string>();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004D00 File Offset: 0x00002F00
		private async Task DownloadAsync(string packageId, string fileName, string downloadPath, string destinationFolder, Dictionary<string, long> filesBeingDownloaded, CancellationToken cancellationToken)
		{
			Downloader download = new Downloader
			{
				SoftwareRepositoryAlternativeBaseUrl = (ApplicationInfo.UseTestServer() ? "https://pvprepo.azurewebsites.net" : "https://api.swrepository.com"),
				SoftwareRepositoryProxy = base.Proxy(),
				ChunkSize = (long)MsrDownloadConfig.Instance.ChunkSizeBytes,
				MaxParallelConnections = MsrDownloadConfig.Instance.MaxNumberOfParallelDownloads
			};
			using (FileStreamer streamer = new FileStreamer(downloadPath, packageId, destinationFolder, false))
			{
				try
				{
					await download.GetFileAsync(packageId, fileName, streamer, cancellationToken, new DownloadProgress<DownloadProgressInfo>(delegate(DownloadProgressInfo dpi)
					{
						this.OnProgress(dpi, filesBeingDownloaded);
					}));
				}
				catch (Exception ex)
				{
					throw new DownloadPackageException(string.Format("Downloading file {0} failed.", fileName), ex);
				}
			}
			FileStreamer streamer = null;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004D74 File Offset: 0x00002F74
		private void DownloadTaskFinished(Task task)
		{
			TaskStatus status = task.Status;
			bool flag = status.Equals(TaskStatus.Faulted);
			if (flag)
			{
				Tracer<MsrService>.WriteInformation("Downloading file failed.");
			}
			else
			{
				bool flag2 = status.Equals(TaskStatus.Canceled);
				if (flag2)
				{
					Tracer<MsrService>.WriteInformation("Download cancelled on the file.");
				}
				else
				{
					Tracer<MsrService>.WriteInformation("File succesfully downloaded.");
				}
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004DE0 File Offset: 0x00002FE0
		private async void OnProgress(DownloadProgressInfo progressInfo, Dictionary<string, long> filesBeingDownloaded)
		{
			bool flag = await this.progressUpdateResetTimer.TryAccessSectionAndSetAsync(CancellationToken.None);
			bool flag2 = !flag;
			if (!flag2)
			{
				try
				{
					await this.downloadProgressRaiseSemaphoreSlim.WaitAsync();
					if (filesBeingDownloaded[progressInfo.FileName] == 0L)
					{
						base.SpeedCalculator.CurrentPartlyDownloadedSize += progressInfo.BytesReceived;
					}
					else
					{
						base.SpeedCalculator.CurrentDownloadedSize = filesBeingDownloaded.Sum((KeyValuePair<string, long> c) => c.Value) - base.SpeedCalculator.CurrentPartlyDownloadedSize;
					}
					filesBeingDownloaded[progressInfo.FileName] = progressInfo.BytesReceived;
					int percentage = (int)(base.SpeedCalculator.TotalDownloadedSize * 95L / base.TotalFilesSize);
					long downloadSizeRequiredDeltaBytes = (long)MsrDownloadConfig.Instance.MinimalReportedDownloadedBytesIncrease;
					if (base.SpeedCalculator.TotalDownloadedSize - this.lastDownloadedSize >= downloadSizeRequiredDeltaBytes)
					{
						this.lastDownloadedSize = base.SpeedCalculator.TotalDownloadedSize;
						base.RaiseProgressChangedEvent(percentage, null);
					}
				}
				finally
				{
					this.downloadProgressRaiseSemaphoreSlim.Release();
				}
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004E28 File Offset: 0x00003028
		private List<SoftwareFile> GetNotDownloadedFiles(string targetFolder, IEnumerable<SoftwareFile> files, string softwareVersion, bool filesVersioned, CancellationToken cancellationToken)
		{
			List<SoftwareFile> list = files.Select((SoftwareFile file) => new SoftwareFile
			{
				Checksum = file.Checksum,
				FileName = file.FileName,
				FileSize = file.FileSize,
				FileType = file.FileType
			}).ToList<SoftwareFile>();
			List<SoftwareFile> list2 = new List<SoftwareFile>(list);
			foreach (SoftwareFile softwareFile in list)
			{
				cancellationToken.ThrowIfCancellationRequested();
				string text = this.GenerateSoftwareVersionFile(softwareFile.FileName, softwareVersion, filesVersioned);
				string text2 = Path.Combine(targetFolder, text);
				bool flag = File.Exists(text2);
				if (flag)
				{
					SoftwareFileChecksum softwareFileChecksum = softwareFile.Checksum.First<SoftwareFileChecksum>();
					byte[] array = this.fileChecker.CheckFile(softwareFileChecksum.ChecksumType, text2, cancellationToken);
					bool flag2 = array != null && Convert.ToBase64String(array) == softwareFileChecksum.Value;
					if (flag2)
					{
						list2.Remove(softwareFile);
					}
				}
			}
			return list2;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004F34 File Offset: 0x00003134
		private string GenerateSoftwareVersionFile(string fileName, string softwareVersion, bool appendVersion)
		{
			string text4;
			if (appendVersion)
			{
				string text = fileName.Substring(0, fileName.LastIndexOf('.'));
				string text2 = fileName.Substring(fileName.LastIndexOf('.'));
				string text3 = text + "_" + softwareVersion + text2;
				text4 = text3;
			}
			else
			{
				text4 = fileName;
			}
			return text4;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004F84 File Offset: 0x00003184
		private void IntegrityCheckProgressChanged(double progress)
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

		// Token: 0x060000D3 RID: 211 RVA: 0x00004FD0 File Offset: 0x000031D0
		public void CheckFilesCorrectness(string targetFolder, IEnumerable<SoftwareFile> files, string softwareVersion, bool filesVersioned, CancellationToken cancellationToken)
		{
			foreach (SoftwareFile softwareFile in files)
			{
				cancellationToken.ThrowIfCancellationRequested();
				string text = this.GenerateSoftwareVersionFile(softwareFile.FileName, softwareVersion, filesVersioned);
				SoftwareFileChecksum softwareFileChecksum = softwareFile.Checksum.First<SoftwareFileChecksum>();
				byte[] array = this.fileChecker.CheckFile(softwareFileChecksum.ChecksumType, Path.Combine(targetFolder, text), cancellationToken);
				bool flag = array != null && Convert.ToBase64String(array) != softwareFileChecksum.Value;
				if (flag)
				{
					throw new Crc32Exception(targetFolder + softwareFile.FileName);
				}
			}
		}

		// Token: 0x0400003E RID: 62
		private const string RepositoryBaseUri = "https://api.swrepository.com";

		// Token: 0x0400003F RID: 63
		private const string TestRepositoryBaseUri = "https://pvprepo.azurewebsites.net";

		// Token: 0x04000040 RID: 64
		private readonly SemaphoreSlim downloadProgressRaiseSemaphoreSlim = new SemaphoreSlim(1, 1);

		// Token: 0x04000041 RID: 65
		private long lastDownloadedSize;

		// Token: 0x04000042 RID: 66
		private readonly FileChecker fileChecker;

		// Token: 0x04000043 RID: 67
		private IntervalResetAccessTimer progressUpdateResetTimer;
	}
}
