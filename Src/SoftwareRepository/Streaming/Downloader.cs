using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SoftwareRepository.Discovery;
using SoftwareRepository.Reporting;

namespace SoftwareRepository.Streaming
{
	// Token: 0x0200000E RID: 14
	public class Downloader
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002E3B File Offset: 0x0000103B
		public Downloader()
		{
			this.TimeoutInMilliseconds = 10000;
			this.MaxParallelConnections = 4;
			this.ChunkSize = this.DefaultChunkSize;
			this.AllowWindowsAuth = false;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002E7A File Offset: 0x0000107A
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002E82 File Offset: 0x00001082
		public long ChunkSize { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002E8B File Offset: 0x0000108B
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002E93 File Offset: 0x00001093
		public int MaxParallelConnections { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002E9C File Offset: 0x0000109C
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002EA4 File Offset: 0x000010A4
		[SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
		public string SoftwareRepositoryAlternativeBaseUrl { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002EAD File Offset: 0x000010AD
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002EB5 File Offset: 0x000010B5
		public string SoftwareRepositoryAuthenticationToken { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002EBE File Offset: 0x000010BE
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002EC6 File Offset: 0x000010C6
		public IWebProxy SoftwareRepositoryProxy { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002ECF File Offset: 0x000010CF
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002ED7 File Offset: 0x000010D7
		public string SoftwareRepositoryUserAgent { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002EE0 File Offset: 0x000010E0
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002EE8 File Offset: 0x000010E8
		public int TimeoutInMilliseconds { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002EF1 File Offset: 0x000010F1
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002EF9 File Offset: 0x000010F9
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Auth", Justification = "Acceptable abbreviation")]
		public bool AllowWindowsAuth { get; set; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600004A RID: 74 RVA: 0x00002F04 File Offset: 0x00001104
		// (remove) Token: 0x0600004B RID: 75 RVA: 0x00002F3C File Offset: 0x0000113C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event DownloadReadyEventHandler DownloadReady;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600004C RID: 76 RVA: 0x00002F74 File Offset: 0x00001174
		// (remove) Token: 0x0600004D RID: 77 RVA: 0x00002FAC File Offset: 0x000011AC
		[SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event BestUrlSelectionEventHandler OnUrlSelection;

		// Token: 0x0600004E RID: 78 RVA: 0x00002FE4 File Offset: 0x000011E4
		public async Task GetFileAsync(string packageId, string filename, Streamer streamer)
		{
			CancellationTokenSource cts = new CancellationTokenSource();
			DownloadReadyEventArgs downloadReadyEvent = null;
			DownloadReadyEventHandler readyHandler = delegate(object _, DownloadReadyEventArgs e)
			{
				downloadReadyEvent = e;
			};
			this.DownloadReady += readyHandler;
			Action<DownloadProgressInfo> progressHandler = delegate(DownloadProgressInfo info)
			{
				bool flag = info.BytesReceived >= info.TotalBytes;
				if (flag)
				{
					cts.Cancel();
				}
			};
			await this.GetFileAsync(packageId, filename, streamer, cts.Token, new DownloadProgress<DownloadProgressInfo>(progressHandler));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003040 File Offset: 0x00001240
		public async Task GetFileAsync(string packageId, string filename, Streamer streamer, bool ignoreFilePath)
		{
			CancellationTokenSource cts = new CancellationTokenSource();
			DownloadReadyEventArgs downloadReadyEvent = null;
			DownloadReadyEventHandler readyHandler = delegate(object _, DownloadReadyEventArgs e)
			{
				downloadReadyEvent = e;
			};
			this.DownloadReady += readyHandler;
			Action<DownloadProgressInfo> progressHandler = delegate(DownloadProgressInfo info)
			{
				bool flag = info.BytesReceived >= info.TotalBytes;
				if (flag)
				{
					cts.Cancel();
				}
			};
			await this.GetFileAsync(packageId, filename, streamer, cts.Token, new DownloadProgress<DownloadProgressInfo>(progressHandler), ignoreFilePath);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000030A4 File Offset: 0x000012A4
		public async Task GetFileAsync(string packageId, string filename, Streamer streamer, DownloadProgress<DownloadProgressInfo> progress)
		{
			await this.GetFileAsync(packageId, filename, streamer, CancellationToken.None, progress);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003108 File Offset: 0x00001308
		public async Task GetFileAsync(string packageId, string filename, Streamer streamer, DownloadProgress<DownloadProgressInfo> progress, bool ignoreFilePath)
		{
			await this.GetFileAsync(packageId, filename, streamer, CancellationToken.None, progress, ignoreFilePath);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003174 File Offset: 0x00001374
		public async Task GetFileAsync(string packageId, string filename, Streamer streamer, CancellationToken cancellationToken)
		{
			await this.GetFileAsync(packageId, filename, streamer, cancellationToken, null);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000031D8 File Offset: 0x000013D8
		public async Task GetFileAsync(string packageId, string filename, Streamer streamer, CancellationToken cancellationToken, bool ignoreFilePath)
		{
			await this.GetFileAsync(packageId, filename, streamer, cancellationToken, null, ignoreFilePath);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003244 File Offset: 0x00001444
		public async Task GetFileAsync(string packageId, string filename, Streamer streamer, CancellationToken cancellationToken, DownloadProgress<DownloadProgressInfo> progress)
		{
			await this.GetFileAsync(packageId, filename, streamer, cancellationToken, progress, false);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000032B0 File Offset: 0x000014B0
		public async Task GetFileAsync(string packageId, string filename, Streamer streamer, CancellationToken cancellationToken, DownloadProgress<DownloadProgressInfo> progress, bool ignoreFilePath)
		{
			Exception exception = null;
			int reportStatus = -1;
			DateTime reportTimeBegin = DateTime.Now;
			bool isSharedFile = false;
			string sharedFileURL = null;
			FileUrlResult fileUrlResult = null;
			string fileUrl = null;
			ChunkManager chunkManager = null;
			SemaphoreSlim syncLock = new SemaphoreSlim(1, 1);
			Downloader.ProgressWrapper progressWrapper = new Downloader.ProgressWrapper(progress);
			UrlSelectionResult urlSelectionResult = new UrlSelectionResult();
			try
			{
				try
				{
					FileUrlResult fileUrlResult2 = await this.GetFileUrlAsync(packageId, filename, cancellationToken);
					fileUrlResult = fileUrlResult2;
					fileUrlResult2 = null;
					List<string> fileUrls = fileUrlResult.GetFileUrls();
					if (fileUrlResult.StatusCode != HttpStatusCode.OK || fileUrls.Count <= 0)
					{
						throw new DownloadException(404, "File not found (" + filename + ").");
					}
					if (!ignoreFilePath)
					{
						foreach (string url in fileUrls)
						{
							if (url.ToLower().StartsWith("file://"))
							{
								isSharedFile = true;
								sharedFileURL = url;
								break;
							}
							url = null;
						}
						List<string>.Enumerator enumerator = default(List<string>.Enumerator);
					}
					if (isSharedFile)
					{
						urlSelectionResult.UrlResults.Add(new UrlResult
						{
							FileUrl = sharedFileURL,
							IsSelected = true
						});
						this.OnUrlSelection(urlSelectionResult);
						int num = await this.DownloadSharedFileAsync(packageId, filename, sharedFileURL, streamer.DownloadPath, fileUrlResult, progressWrapper);
						reportStatus = num;
						if (reportStatus != 200)
						{
							urlSelectionResult = new UrlSelectionResult();
							isSharedFile = false;
							sharedFileURL = null;
						}
					}
					if (!isSharedFile)
					{
						long streamSize = streamer.GetStream().Length;
						if (streamSize > fileUrlResult.FileSize)
						{
							throw new DownloadException(412, "Incorrect file size, can't resume download. Stream contains more data than expected.");
						}
						bool flag = streamSize == fileUrlResult.FileSize;
						bool flag2 = flag;
						if (flag2)
						{
							bool flag3 = await Downloader.FileIntegrityPreservedAsync(fileUrlResult.Checksum, streamer.GetStream());
							flag2 = flag3;
						}
						if (flag2)
						{
							this.ReportCompleted(progressWrapper, packageId, filename, fileUrlResult.FileSize);
							return;
						}
						byte[] savedMetadata = streamer.GetMetadata();
						if (savedMetadata != null)
						{
							chunkManager = new ChunkManager(DownloadMetadata.Deserialize(savedMetadata), this.ChunkSize, fileUrlResult.FileSize, filename, streamer, syncLock, cancellationToken);
							if (streamSize < chunkManager.ProgressBytes)
							{
								savedMetadata = null;
								streamer.ClearMetadata();
							}
						}
						if (savedMetadata == null)
						{
							chunkManager = new ChunkManager(this.ChunkSize, fileUrlResult.FileSize, filename, streamer, syncLock, cancellationToken);
						}
						chunkManager.SoftwareRepositoryProxy = this.SoftwareRepositoryProxy;
						chunkManager.ChunkTimeoutInMilliseconds = new int?(this.TimeoutInMilliseconds);
						chunkManager.AllowWindowsAuth = this.AllowWindowsAuth;
						this.OnUrlSelection += delegate(UrlSelectionResult result)
						{
							urlSelectionResult = result;
						};
						string text = await this.SelectBestUrlAsync(chunkManager.GetTestChunk(), fileUrlResult, streamer, cancellationToken, progressWrapper, chunkManager);
						fileUrl = text;
						text = null;
						chunkManager.FileUrl = fileUrl;
						if (chunkManager.IsDownloaded)
						{
							reportStatus = 200;
						}
						else
						{
							int num2 = await this.DownloadAsync(chunkManager, fileUrlResult, streamer, progressWrapper);
							reportStatus = num2;
						}
						savedMetadata = null;
					}
					fileUrls = null;
				}
				catch (OperationCanceledException ex)
				{
					reportStatus = 206;
					exception = ex;
				}
				catch (HttpRequestException ex2)
				{
					reportStatus = 999;
					exception = new DownloadException(reportStatus, "HttpRequestException.", ex2);
				}
				catch (DownloadException ex3)
				{
					reportStatus = ex3.StatusCode;
					exception = ex3;
				}
				if (!isSharedFile)
				{
					if (chunkManager != null)
					{
						if (chunkManager.IsDownloaded)
						{
							streamer.ClearMetadata();
						}
						else
						{
							chunkManager.SaveMetadata(streamer, true);
						}
					}
					await streamer.GetStream().FlushAsync();
				}
			}
			catch (Exception ex4)
			{
				if (reportStatus == -1)
				{
					reportStatus = 999;
				}
				exception = new DownloadException(999, "Unknown exception.", ex4);
			}
			if (exception == null)
			{
				bool flag4 = await Downloader.FileIntegrityPreservedAsync(fileUrlResult.Checksum, streamer.GetStream());
				if (!flag4)
				{
					reportStatus = 417;
					exception = new DownloadException(reportStatus, "File integrity error. MD5 checksum of the file does not match with data received from server.");
				}
				else
				{
					reportStatus = 200;
				}
			}
			try
			{
				TimeSpan downloadTime = DateTime.Now - reportTimeBegin;
				long downloadTimeInSeconds = (long)((int)downloadTime.TotalSeconds);
				if (downloadTimeInSeconds <= 0L)
				{
					downloadTimeInSeconds = 1L;
				}
				List<string> urls = new List<string>();
				urls.Add(urlSelectionResult.ToJson());
				Reporter reporter = new Reporter
				{
					SoftwareRepositoryAlternativeBaseUrl = this.SoftwareRepositoryAlternativeBaseUrl,
					SoftwareRepositoryUserAgent = this.SoftwareRepositoryUserAgent,
					SoftwareRepositoryProxy = this.SoftwareRepositoryProxy
				};
				int connections = 1;
				if (!isSharedFile)
				{
					connections = Math.Min(this.MaxParallelConnections, chunkManager.TotalChunks);
				}
				await reporter.SendDownloadReport(packageId, filename, urls, reportStatus, downloadTimeInSeconds, fileUrlResult.FileSize, connections, cancellationToken);
				downloadTime = default(TimeSpan);
				urls = null;
				reporter = null;
			}
			catch (Exception)
			{
			}
			if (exception != null)
			{
				throw exception;
			}
			this.ReportCompleted(progressWrapper, packageId, filename, fileUrlResult.FileSize);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003324 File Offset: 0x00001524
		private static async Task<bool> FileIntegrityPreservedAsync(List<SoftwareFileChecksum> checksums, Stream stream)
		{
			Downloader.<>c__DisplayClass57_0 CS$<>8__locals1 = new Downloader.<>c__DisplayClass57_0();
			CS$<>8__locals1.stream = stream;
			bool flag = checksums == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool ret = false;
				foreach (SoftwareFileChecksum checksum in checksums)
				{
					bool flag3 = checksum == null || checksum.ChecksumType == null || checksum.Value == null;
					if (!flag3)
					{
						bool flag4 = checksum.ChecksumType.ToUpperInvariant() == "MD5";
						if (flag4)
						{
							Downloader.<>c__DisplayClass57_1 CS$<>8__locals2 = new Downloader.<>c__DisplayClass57_1();
							CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
							CS$<>8__locals2.md5 = MD5.Create();
							try
							{
								CS$<>8__locals2.CS$<>8__locals1.stream.Seek(0L, SeekOrigin.Begin);
								byte[] array = await Task.Run<byte[]>(() => CS$<>8__locals2.md5.ComputeHash(CS$<>8__locals2.CS$<>8__locals1.stream));
								byte[] b = array;
								array = null;
								string base64MD5 = Convert.ToBase64String(b);
								string hexMD5 = BitConverter.ToString(b).Replace("-", string.Empty).ToLowerInvariant();
								if (!checksum.Value.Equals(base64MD5) && !checksum.Value.ToLowerInvariant().Equals(hexMD5))
								{
									return false;
								}
								ret = true;
								b = null;
								base64MD5 = null;
								hexMD5 = null;
							}
							finally
							{
								if (CS$<>8__locals2.md5 != null)
								{
									((IDisposable)CS$<>8__locals2.md5).Dispose();
								}
							}
							CS$<>8__locals2 = null;
						}
						checksum = null;
					}
				}
				List<SoftwareFileChecksum>.Enumerator enumerator = default(List<SoftwareFileChecksum>.Enumerator);
				flag2 = ret;
			}
			return flag2;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003370 File Offset: 0x00001570
		private async Task<FileUrlResult> GetFileUrlAsync(string packageId, string filename, CancellationToken cancellationToken)
		{
			FileUrlResult ret = new FileUrlResult();
			try
			{
				string baseUrl = "https://api.swrepository.com";
				bool flag = this.SoftwareRepositoryAlternativeBaseUrl != null;
				if (flag)
				{
					baseUrl = this.SoftwareRepositoryAlternativeBaseUrl;
				}
				Uri uri = new Uri(string.Concat(new string[] { baseUrl, "/rest-api/discovery/1/package/", packageId, "/file/", filename, "/urls" }));
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
				HttpClient httpClient = null;
				bool flag2 = this.SoftwareRepositoryProxy != null;
				if (flag2)
				{
					HttpClientHandler handler = new HttpClientHandler();
					handler.Proxy = this.SoftwareRepositoryProxy;
					handler.UseProxy = true;
					httpClient = new HttpClient(handler);
					handler = null;
				}
				else
				{
					httpClient = new HttpClient();
				}
				string userAgent = "SoftwareRepository";
				bool flag3 = this.SoftwareRepositoryUserAgent != null;
				if (flag3)
				{
					userAgent = this.SoftwareRepositoryUserAgent;
				}
				httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd(userAgent);
				bool flag4 = this.SoftwareRepositoryAuthenticationToken != null;
				if (flag4)
				{
					httpClient.DefaultRequestHeaders.Add("X-Authentication", this.SoftwareRepositoryAuthenticationToken);
					httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.SoftwareRepositoryAuthenticationToken);
				}
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uri, cancellationToken);
				HttpResponseMessage responseMsg = httpResponseMessage;
				httpResponseMessage = null;
				if (responseMsg.StatusCode == HttpStatusCode.OK)
				{
					HttpContent result = responseMsg.Content;
					string text = await result.ReadAsStringAsync();
					string jsonString = text;
					text = null;
					DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(FileUrlResult));
					MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
					ret = (FileUrlResult)ser.ReadObject(ms);
					result = null;
					jsonString = null;
					ser = null;
					ms = null;
				}
				ret.StatusCode = responseMsg.StatusCode;
				httpClient.Dispose();
				baseUrl = null;
				uri = null;
				httpClient = null;
				userAgent = null;
				responseMsg = null;
			}
			catch (OperationCanceledException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new DownloadException(999, "Cannot get file url.", ex);
			}
			return ret;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000033CC File Offset: 0x000015CC
		private async Task<string> SelectBestUrlAsync(DownloadChunk testChunk, FileUrlResult fileUrlResult, Streamer streamer, CancellationToken cancellationToken, Downloader.ProgressWrapper progress, ChunkManager chunkManager)
		{
			Downloader.<>c__DisplayClass59_0 CS$<>8__locals1 = new Downloader.<>c__DisplayClass59_0();
			CS$<>8__locals1.progress = progress;
			CS$<>8__locals1.chunkManager = chunkManager;
			CS$<>8__locals1.urlSelectionResult = new UrlSelectionResult();
			CS$<>8__locals1.urlSelectionResult.TestBytes = testChunk.Bytes;
			List<string> fileUrls = fileUrlResult.GetFileUrls();
			bool flag = fileUrls.Count == 1;
			string text;
			if (flag)
			{
				CS$<>8__locals1.urlSelectionResult.UrlResults.Add(new UrlResult
				{
					FileUrl = fileUrls.First<string>(),
					IsSelected = true
				});
				bool flag2 = this.OnUrlSelection != null;
				if (flag2)
				{
					this.OnUrlSelection(CS$<>8__locals1.urlSelectionResult);
				}
				text = fileUrls.First<string>();
			}
			else
			{
				CS$<>8__locals1.urlSelectionResult.UrlResults.AddRange(from x in fileUrlResult.GetFileUrls()
					select new UrlResult
					{
						FileUrl = x
					});
				CS$<>8__locals1.ret = null;
				CS$<>8__locals1.exception = null;
				Task<int> winner = null;
				int statusCode = -1;
				List<DownloadChunk> chunks = new List<DownloadChunk>();
				List<Task<int>> tasks = new List<Task<int>>();
				CS$<>8__locals1.currentDownloaded = CS$<>8__locals1.chunkManager.ProgressBytes;
				CS$<>8__locals1.bestChunk = 0L;
				DownloadChunk chunk;
				DownloadProgressEventHandler progressHandler = delegate(object sender, EventArgs args)
				{
					DownloadChunk chunk = sender as DownloadChunk;
					CS$<>8__locals1.urlSelectionResult.UrlResults.Where((UrlResult x) => x.FileUrl.Equals(chunk.Url, StringComparison.OrdinalIgnoreCase)).ToList<UrlResult>().ForEach(delegate(UrlResult x)
					{
						x.TestSpeed = chunk.DownloadSpeed;
						x.BytesRead = chunk.BytesRead;
					});
					bool flag3 = chunk.BytesRead > CS$<>8__locals1.bestChunk;
					if (flag3)
					{
						CS$<>8__locals1.bestChunk = chunk.BytesRead;
						CS$<>8__locals1.progress.Report(new DownloadProgressInfo(CS$<>8__locals1.currentDownloaded + CS$<>8__locals1.bestChunk, CS$<>8__locals1.chunkManager.FileSize, CS$<>8__locals1.chunkManager.FileName));
					}
				};
				Downloader.<>c__DisplayClass59_2 CS$<>8__locals2 = new Downloader.<>c__DisplayClass59_2();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				CS$<>8__locals2.cts = new CancellationTokenSource();
				try
				{
					using (cancellationToken.Register(delegate
					{
						CS$<>8__locals2.cts.Cancel();
					}))
					{
						foreach (string fileUrl in fileUrls)
						{
							chunk = testChunk.Clone();
							chunk.Url = fileUrl;
							chunk.OutStream = new MemoryStream();
							chunk.AllowSeek = false;
							chunk.DownloadProgress += progressHandler;
							chunks.Add(chunk);
							tasks.Add(chunk.Download());
							chunk = null;
							fileUrl = null;
						}
						List<string>.Enumerator enumerator = default(List<string>.Enumerator);
						while (statusCode == -1 && tasks.Count > 0)
						{
							try
							{
								Task<Task<int>> anyTask = Task.WhenAny<int>(tasks);
								Task<int> task = await anyTask;
								winner = task;
								task = null;
								int num = await winner;
								statusCode = num;
								anyTask = null;
							}
							catch (OperationCanceledException ex)
							{
								CS$<>8__locals2.CS$<>8__locals1.exception = ex;
							}
							catch (Exception ex2)
							{
								if (!(CS$<>8__locals2.CS$<>8__locals1.exception is OperationCanceledException))
								{
									CS$<>8__locals2.CS$<>8__locals1.exception = ex2;
								}
							}
							finally
							{
								if (statusCode == -1)
								{
									Downloader.<>c__DisplayClass59_3 CS$<>8__locals3 = new Downloader.<>c__DisplayClass59_3();
									CS$<>8__locals3.CS$<>8__locals2 = CS$<>8__locals2;
									int index = tasks.IndexOf(winner);
									tasks.Remove(winner);
									fileUrls.RemoveAt(index);
									CS$<>8__locals3.chunk = chunks[index];
									CS$<>8__locals3.walkException = null;
									CS$<>8__locals3.walkException = (Exception e) => (e.InnerException == null) ? string.Format(CultureInfo.InvariantCulture, "{0}: {1}", new object[]
									{
										e.GetType().Name,
										e.Message
									}) : string.Format(CultureInfo.InvariantCulture, "{0}: {1} ({2})", new object[]
									{
										e.GetType().Name,
										e.Message,
										CS$<>8__locals3.walkException(e.InnerException)
									});
									CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.urlSelectionResult.UrlResults.Where((UrlResult x) => x.FileUrl.Equals(CS$<>8__locals3.chunk.Url, StringComparison.OrdinalIgnoreCase)).ToList<UrlResult>().ForEach(delegate(UrlResult x)
									{
										x.Error = CS$<>8__locals3.walkException(CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.exception);
									});
									CS$<>8__locals3.chunk.OutStream.Dispose();
									CS$<>8__locals3.chunk.DownloadProgress -= progressHandler;
									chunks.RemoveAt(index);
									CS$<>8__locals3 = null;
								}
							}
						}
					}
					CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
					if (!CS$<>8__locals2.cts.IsCancellationRequested)
					{
						CS$<>8__locals2.cts.Cancel();
					}
				}
				finally
				{
					if (CS$<>8__locals2.cts != null)
					{
						((IDisposable)CS$<>8__locals2.cts).Dispose();
					}
				}
				CS$<>8__locals2 = null;
				if (statusCode != -1)
				{
					int winnerIndex = tasks.IndexOf(winner);
					CS$<>8__locals1.ret = fileUrls[winnerIndex];
					CS$<>8__locals1.urlSelectionResult.UrlResults.Where((UrlResult x) => x.FileUrl.Equals(CS$<>8__locals1.ret, StringComparison.OrdinalIgnoreCase)).ToList<UrlResult>().ForEach(delegate(UrlResult x)
					{
						x.IsSelected = true;
					});
					DownloadChunk chunk2 = chunks[winnerIndex];
					chunk2.OutStream.Seek(0L, SeekOrigin.Begin);
					streamer.GetStream().Seek(chunk2.BytesFrom, SeekOrigin.Begin);
					await chunk2.OutStream.CopyToAsync(streamer.GetStream());
					await streamer.GetStream().FlushAsync();
					CS$<>8__locals1.chunkManager.MarkDownloaded(chunk2);
					CS$<>8__locals1.chunkManager.SaveMetadata(streamer, false);
					chunk2 = null;
				}
				foreach (DownloadChunk chunk3 in chunks)
				{
					chunk3.OutStream.Dispose();
					chunk3.DownloadProgress -= progressHandler;
					chunk3 = null;
				}
				List<DownloadChunk>.Enumerator enumerator2 = default(List<DownloadChunk>.Enumerator);
				if (statusCode == -1 && CS$<>8__locals1.exception != null)
				{
					throw CS$<>8__locals1.exception;
				}
				if (this.OnUrlSelection != null)
				{
					this.OnUrlSelection(CS$<>8__locals1.urlSelectionResult);
				}
				text = CS$<>8__locals1.ret;
			}
			return text;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003440 File Offset: 0x00001640
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "fileUrlResult")]
		private async Task<int> DownloadAsync(ChunkManager chunkManager, FileUrlResult fileUrlResult, Streamer streamer, Downloader.ProgressWrapper progress)
		{
			int maxConnections = this.MaxParallelConnections;
			DownloadProgressEventHandler progressHandler = delegate(object sender, EventArgs args)
			{
				progress.Report(new DownloadProgressInfo(chunkManager.ProgressBytes, chunkManager.FileSize, chunkManager.FileName));
			};
			List<DownloadChunk> chunks = new List<DownloadChunk>();
			List<Task<int>> tasks = new List<Task<int>>();
			int num;
			for (int i = 0; i < maxConnections; i = num + 1)
			{
				DownloadChunk chunk = chunkManager.GetNextChunk();
				bool flag = chunk == null;
				if (flag)
				{
					break;
				}
				chunk.DownloadProgress += progressHandler;
				chunks.Add(chunk);
				tasks.Add(chunk.Download());
				chunk = null;
				num = i;
			}
			int ret = 200;
			while (tasks.Count > 0)
			{
				Task<int> task = await Task.WhenAny<int>(tasks);
				Task<int> completedChunkTask = task;
				task = null;
				int taskIndex = tasks.IndexOf(completedChunkTask);
				DownloadChunk chunk2 = chunks[taskIndex];
				chunk2.DownloadProgress -= progressHandler;
				int num2 = await completedChunkTask;
				int status = num2;
				if (status != 200 && status != 206)
				{
					return status;
				}
				chunkManager.MarkDownloaded(chunk2);
				chunkManager.SaveMetadata(streamer, false);
				chunk2 = chunkManager.GetNextChunk();
				if (chunk2 == null)
				{
					tasks.RemoveAt(taskIndex);
					chunks.RemoveAt(taskIndex);
				}
				else
				{
					chunk2.OutStream = streamer.GetStream();
					chunk2.DownloadProgress += progressHandler;
					chunks[taskIndex] = chunk2;
					tasks[taskIndex] = chunk2.Download();
				}
				completedChunkTask = null;
				chunk2 = null;
			}
			streamer.ClearMetadata();
			return ret;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000034A4 File Offset: 0x000016A4
		private void ReportCompleted(Downloader.ProgressWrapper progressWrapper, string packageId, string fileName, long fileSize)
		{
			progressWrapper.Report(new DownloadProgressInfo(fileSize, fileSize, fileName));
			bool flag = this.DownloadReady != null;
			if (flag)
			{
				this.DownloadReady(this, new DownloadReadyEventArgs(packageId, fileName));
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000034E8 File Offset: 0x000016E8
		private async Task<int> DownloadSharedFileAsync(string packageId, string fileName, string sourceURL, string destination, FileUrlResult fileUrlResult, Downloader.ProgressWrapper progress)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();
			int resultCode = 0;
			long fileSize = fileUrlResult.FileSize;
			FileInfo downloadedFileInfo = null;
			try
			{
				Uri uri = new Uri(sourceURL);
				string source = uri.LocalPath;
				FileInfo sourceFileInfo = new FileInfo(source);
				bool flag = !sourceFileInfo.Exists;
				if (flag)
				{
					resultCode = 404;
				}
				DownloadProgressEventHandler progressHandler = delegate(object sender, EventArgs args)
				{
					progress.Report(new DownloadProgressInfo(0L, fileSize, fileName));
				};
				bool fileIsAlreadyDownloaded = false;
				downloadedFileInfo = new FileInfo(destination);
				bool exists = downloadedFileInfo.Exists;
				if (exists)
				{
					progress.Report(new DownloadProgressInfo(downloadedFileInfo.Length, fileSize, fileName));
					using (FileStream fs = new FileStream(destination, FileMode.Open, FileAccess.Read))
					{
						bool flag2 = downloadedFileInfo.Length == fileSize;
						bool flag3 = flag2;
						if (flag3)
						{
							bool flag4 = await Downloader.FileIntegrityPreservedAsync(fileUrlResult.Checksum, fs);
							flag3 = flag4;
						}
						bool flag5 = flag3;
						if (flag5)
						{
							fileIsAlreadyDownloaded = true;
							this.ReportCompleted(progress, packageId, fileName, fileUrlResult.FileSize);
							resultCode = 304;
						}
					}
					FileStream fs = null;
				}
				if (!fileIsAlreadyDownloaded)
				{
					using (FileStream SourceStream = File.Open(source, FileMode.Open, FileAccess.Read, FileShare.Read))
					{
						using (FileStream DestinationStream = File.Create(destination))
						{
							byte[] buffer = new byte[this.ChunkSize];
							int num = await SourceStream.ReadAsync(buffer, 0, buffer.Length);
							int bytesInBuffer = num;
							long downloadedFileSize = 0L;
							while (bytesInBuffer != 0)
							{
								await DestinationStream.WriteAsync(buffer, 0, bytesInBuffer);
								downloadedFileSize += (long)bytesInBuffer;
								progress.Report(new DownloadProgressInfo(downloadedFileSize, fileSize, fileName));
								int num2 = await SourceStream.ReadAsync(buffer, 0, buffer.Length);
								bytesInBuffer = num2;
							}
							buffer = null;
						}
						FileStream DestinationStream = null;
					}
					FileStream SourceStream = null;
					downloadedFileInfo = new FileInfo(destination);
					progress.Report(new DownloadProgressInfo(downloadedFileInfo.Length, fileSize, fileName));
					if (!downloadedFileInfo.Exists)
					{
						resultCode = 204;
					}
					else if (downloadedFileInfo.Length != fileSize)
					{
						downloadedFileInfo.Delete();
						resultCode = 204;
					}
					else
					{
						resultCode = 200;
					}
				}
				uri = null;
				source = null;
				sourceFileInfo = null;
			}
			catch (Exception)
			{
				resultCode = 500;
			}
			sw.Stop();
			return resultCode;
		}

		// Token: 0x0400002A RID: 42
		private const int Canceled = 206;

		// Token: 0x0400002B RID: 43
		private const int Completed = 200;

		// Token: 0x0400002C RID: 44
		private long DefaultChunkSize = 1048576L;

		// Token: 0x0400002D RID: 45
		private const int DefaultParallelConnections = 4;

		// Token: 0x0400002E RID: 46
		private const string DefaultSoftwareRepositoryBaseUrl = "https://api.swrepository.com";

		// Token: 0x0400002F RID: 47
		private const string DefaultSoftwareRepositoryFileUrl = "/rest-api/discovery";

		// Token: 0x04000030 RID: 48
		private const string DefaultSoftwareRepositoryFileUrlApiVersion = "/1";

		// Token: 0x04000031 RID: 49
		private const string DefaultSoftwareRepositoryUserAgent = "SoftwareRepository";

		// Token: 0x04000032 RID: 50
		private const int DefaultTimeoutInMilliseconds = 10000;

		// Token: 0x0200002A RID: 42
		private class ProgressWrapper
		{
			// Token: 0x06000168 RID: 360 RVA: 0x00004B97 File Offset: 0x00002D97
			internal ProgressWrapper(DownloadProgress<DownloadProgressInfo> listener)
			{
				this.Listener = listener;
			}

			// Token: 0x06000169 RID: 361 RVA: 0x00004BB0 File Offset: 0x00002DB0
			internal void Report(DownloadProgressInfo report)
			{
				bool flag = this.Listener != null;
				if (flag)
				{
					lock (this)
					{
						bool flag3 = this.LargestReported < report.BytesReceived;
						if (flag3)
						{
							this.Listener.Report(report);
							this.LargestReported = report.BytesReceived;
						}
					}
				}
			}

			// Token: 0x040000CB RID: 203
			private long LargestReported = -1L;

			// Token: 0x040000CC RID: 204
			private DownloadProgress<DownloadProgressInfo> Listener;
		}
	}
}
