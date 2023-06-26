using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Contracts;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs;
using Nokia.Mira;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services
{
	// Token: 0x02000007 RID: 7
	[Export(typeof(IUseProxy))]
	[Export(typeof(AutoUpdateService))]
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class AutoUpdateService : IUseProxy, IDisposable
	{
		// Token: 0x0600004A RID: 74 RVA: 0x000027B8 File Offset: 0x000009B8
		[ImportingConstructor]
		public AutoUpdateService()
		{
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600004B RID: 75 RVA: 0x000027C4 File Offset: 0x000009C4
		// (remove) Token: 0x0600004C RID: 76 RVA: 0x000027FC File Offset: 0x000009FC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<DownloadingProgressChangedEventArgs> DownloadProgressChanged;

		// Token: 0x0600004D RID: 77 RVA: 0x00002834 File Offset: 0x00000A34
		public ApplicationUpdate ReadLatestAppVersion(int appId, string currentVersion, bool useTestServer)
		{
			try
			{
				Tracer<AutoUpdateService>.LogEntry("ReadLatestAppVersion");
				Tracer<AutoUpdateService>.WriteInformation("Checking - appId: {0}, current version: {1}", new object[] { appId, currentVersion });
				ApplicationUpdate appVersion = this.GetAppVersion(appId, useTestServer);
				bool flag = appVersion != null;
				if (flag)
				{
					Tracer<AutoUpdateService>.WriteInformation("Latest package version found: {0}", new object[] { appVersion.Version });
					int num = VersionComparer.CompareVersions(appVersion.Version, currentVersion);
					bool flag2 = num > 0;
					if (flag2)
					{
						Tracer<AutoUpdateService>.WriteInformation("Package on server is newer than installed!");
						this.CheckPackageSize(appVersion);
						return appVersion;
					}
					bool flag3 = num == 0;
					if (flag3)
					{
						Tracer<AutoUpdateService>.WriteInformation("Package on server is same as installed.");
					}
					else
					{
						Tracer<AutoUpdateService>.WriteError("Package on server is older than installed!", new object[0]);
					}
				}
				else
				{
					Tracer<AutoUpdateService>.WriteInformation("Update package not found.");
				}
			}
			finally
			{
				Tracer<AutoUpdateService>.LogExit("ReadLatestAppVersion");
			}
			return null;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000292C File Offset: 0x00000B2C
		public string DownloadAppPacket(ApplicationUpdate packageToDownload, string downloadPath, CancellationToken token)
		{
			token.ThrowIfCancellationRequested();
			bool flag = packageToDownload == null || string.IsNullOrWhiteSpace(packageToDownload.PackageUri);
			if (flag)
			{
				throw new InvalidOperationException("App update package is incorrect. It doesn't contain any file.");
			}
			this.Download(packageToDownload, downloadPath, token);
			return Path.Combine(downloadPath, packageToDownload.PackageFileName);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002980 File Offset: 0x00000B80
		private void Download(ApplicationUpdate packageToDownload, string path, CancellationToken token)
		{
			this.lastProgressPercentage = 0;
			this.lastProgressMessage = "DownloadingFiles";
			this.speedCalculator = new SpeedCalculator();
			this.speedCalculator.Start(packageToDownload.Size, 0L);
			Action<DownloadProgressInfo> action = new Action<DownloadProgressInfo>(this.DownloadTaskProgress);
			Nokia.Mira.Progress<DownloadProgressInfo> progress = new Nokia.Mira.Progress<DownloadProgressInfo>(action);
			DownloadSettings downloadSettings = new DownloadSettings(5, 3145728L, true, true);
			Uri uri = new Uri(packageToDownload.PackageUri);
			HttpWebRequestFactory httpWebRequestFactory = new HttpWebRequestFactory(uri)
			{
				Proxy = this.Proxy()
			};
			WebFile webFile = new WebFile(httpWebRequestFactory);
			Task task = webFile.DownloadAsync(path + packageToDownload.PackageFileName, token, progress, downloadSettings);
			task.ContinueWith(new Action<Task>(this.DownloadTaskFinished), token);
			Task.WaitAll(new Task[] { task });
			token.ThrowIfCancellationRequested();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A54 File Offset: 0x00000C54
		private void DownloadTaskProgress(DownloadProgressInfo info)
		{
			this.speedCalculator.CurrentDownloadedSize += info.BytesReceived;
			this.lastProgressPercentage = (int)(this.speedCalculator.TotalDownloadedSize * 100L / this.speedCalculator.TotalFilesSize);
			this.RaiseDownloadProgressChangedEvent();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002AA4 File Offset: 0x00000CA4
		private void RaiseDownloadProgressChangedEvent()
		{
			Action<DownloadingProgressChangedEventArgs> downloadProgressChanged = this.DownloadProgressChanged;
			bool flag = downloadProgressChanged != null;
			if (flag)
			{
				downloadProgressChanged(new DownloadingProgressChangedEventArgs(this.lastProgressPercentage, this.speedCalculator.TotalDownloadedSize, this.speedCalculator.TotalFilesSize, this.speedCalculator.BytesPerSecond, this.speedCalculator.RemaingSeconds, this.lastProgressMessage));
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002B08 File Offset: 0x00000D08
		private void DownloadTaskFinished(Task task)
		{
			TaskStatus status = task.Status;
			bool flag = status.Equals(TaskStatus.Faulted);
			if (flag)
			{
				Tracer<AutoUpdateService>.WriteInformation("Downloading App update failed.");
			}
			else
			{
				bool flag2 = status.Equals(TaskStatus.Canceled);
				if (flag2)
				{
					Tracer<AutoUpdateService>.WriteInformation("Download cancelled on the App update.");
				}
				else
				{
					Tracer<AutoUpdateService>.WriteInformation("App update succesfully downloaded.");
				}
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002B74 File Offset: 0x00000D74
		private void CheckPackageSize(ApplicationUpdate package)
		{
			WebRequest webRequest = WebRequest.Create(package.PackageUri);
			webRequest.Method = "HEAD";
			webRequest.Proxy = this.Proxy();
			using (WebResponse response = webRequest.GetResponse())
			{
				long num;
				bool flag = long.TryParse(response.Headers.Get("Content-Length"), out num);
				if (flag)
				{
					package.Size = num;
				}
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002BF4 File Offset: 0x00000DF4
		private ApplicationUpdate GetAppVersion(int appId, bool useTestServer)
		{
			Tracer<AutoUpdateService>.LogEntry("GetAppVersion");
			string text = (useTestServer ? "http://147.243.21.64/PackageSource/WDRT/AppUpdate/" : "https://repairavoidance.blob.core.windows.net/packages/WDRT/AppUpdate/");
			text = Path.Combine(text, "WDRT_Update.xml");
			try
			{
				Uri uri = new Uri(text);
				Stream stream = this.Download(uri);
				XmlReader xmlReader = XmlReader.Create(stream);
				XmlDocument xmlDocument = new XmlDocument
				{
					XmlResolver = null
				};
				xmlDocument.Load(xmlReader);
				XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("Package");
				foreach (object obj in elementsByTagName)
				{
					XmlNode xmlNode = (XmlNode)obj;
					ApplicationUpdate autoUpdateNode = this.GetAutoUpdateNode(xmlNode);
					bool flag = autoUpdateNode.AppId == appId;
					if (flag)
					{
						return autoUpdateNode;
					}
				}
			}
			catch (Exception ex)
			{
				Tracer<AutoUpdateService>.WriteError(ex, "Reading app update failed", new object[0]);
				throw;
			}
			Tracer<AutoUpdateService>.LogExit("GetAppVersion");
			return null;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002D10 File Offset: 0x00000F10
		private ApplicationUpdate GetAutoUpdateNode(XmlNode node)
		{
			ApplicationUpdate applicationUpdate = new ApplicationUpdate();
			foreach (object obj in node.ChildNodes)
			{
				XmlElement xmlElement = (XmlElement)obj;
				bool flag = xmlElement.Name == "Description";
				if (flag)
				{
					applicationUpdate.Description = xmlElement.InnerText;
				}
				else
				{
					bool flag2 = xmlElement.Name == "PackagePath";
					if (flag2)
					{
						applicationUpdate.PackageUri = xmlElement.InnerText;
					}
					else
					{
						bool flag3 = xmlElement.Name == "AppVersion";
						if (flag3)
						{
							applicationUpdate.Version = xmlElement.InnerText;
						}
						else
						{
							bool flag4 = xmlElement.Name == "AppId";
							if (flag4)
							{
								applicationUpdate.AppId = int.Parse(xmlElement.InnerText);
							}
						}
					}
				}
			}
			Tracer<AutoUpdateService>.WriteInformation("Found app update node: appId: {0} | appVersion {1}", new object[] { applicationUpdate.AppId, applicationUpdate.Version });
			return applicationUpdate;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002E44 File Offset: 0x00001044
		public void SetProxy(IWebProxy settings)
		{
			this.proxySettings = settings;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002E50 File Offset: 0x00001050
		private IWebProxy Proxy()
		{
			return this.proxySettings ?? WebRequest.GetSystemWebProxy();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002E74 File Offset: 0x00001074
		private Stream Download(Uri address)
		{
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
			Stream stream;
			using (WebClientEx webClientEx = new WebClientEx(30000)
			{
				Proxy = this.Proxy()
			})
			{
				try
				{
					stream = webClientEx.OpenRead(address);
				}
				catch (Exception ex)
				{
					Tracer<AutoUpdateService>.WriteError(ex);
					throw;
				}
			}
			return stream;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002EE4 File Offset: 0x000010E4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002EF8 File Offset: 0x000010F8
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.disposed;
			if (!flag)
			{
				this.disposed = true;
			}
		}

		// Token: 0x04000016 RID: 22
		private const int DefaultChunkSize = 3145728;

		// Token: 0x04000017 RID: 23
		private const int DefaultMaxChunks = 5;

		// Token: 0x04000018 RID: 24
		private const string QaServerAddress = "http://147.243.21.64/PackageSource/WDRT/AppUpdate/";

		// Token: 0x04000019 RID: 25
		private const string ProductionServerAddress = "https://repairavoidance.blob.core.windows.net/packages/WDRT/AppUpdate/";

		// Token: 0x0400001A RID: 26
		private const string AutoUpdateXmlFileName = "WDRT_Update.xml";

		// Token: 0x0400001B RID: 27
		private bool disposed;

		// Token: 0x0400001C RID: 28
		private IWebProxy proxySettings;

		// Token: 0x0400001D RID: 29
		private string lastProgressMessage;

		// Token: 0x0400001E RID: 30
		private int lastProgressPercentage;

		// Token: 0x0400001F RID: 31
		private SpeedCalculator speedCalculator;
	}
}
