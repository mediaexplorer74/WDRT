using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace SoftwareRepository.Streaming
{
	// Token: 0x02000016 RID: 22
	internal class DownloadChunk
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00003A2C File Offset: 0x00001C2C
		internal DownloadChunk(string filename, string url, long bytesFrom, long byteCount, CancellationToken cancellationToken)
		{
			this.FileName = filename;
			this.Url = url;
			this.BytesFrom = bytesFrom;
			this.Bytes = byteCount;
			this.CancellationToken = cancellationToken;
			this.TimeoutInMilliseconds = 10000;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003A81 File Offset: 0x00001C81
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003A89 File Offset: 0x00001C89
		internal string FileName { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003A92 File Offset: 0x00001C92
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00003A9A File Offset: 0x00001C9A
		internal string Url { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003AA3 File Offset: 0x00001CA3
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00003AAB File Offset: 0x00001CAB
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Unused for now but might prove useful later.")]
		internal long FileSize { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003AB4 File Offset: 0x00001CB4
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00003ABC File Offset: 0x00001CBC
		internal IWebProxy SoftwareRepositoryProxy { get; set; }

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000090 RID: 144 RVA: 0x00003AC8 File Offset: 0x00001CC8
		// (remove) Token: 0x06000091 RID: 145 RVA: 0x00003B00 File Offset: 0x00001D00
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event DownloadProgressEventHandler DownloadProgress;

		// Token: 0x06000092 RID: 146 RVA: 0x00003B38 File Offset: 0x00001D38
		private void OnDownloadProgress(EventArgs e)
		{
			bool flag = this.DownloadProgress != null;
			if (flag)
			{
				this.DownloadProgress(this, e);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003B64 File Offset: 0x00001D64
		internal DownloadChunk Clone()
		{
			return new DownloadChunk(this.FileName, this.Url, this.BytesFrom, this.Bytes, this.CancellationToken)
			{
				FileSize = this.FileSize,
				OutStream = this.OutStream,
				SoftwareRepositoryProxy = this.SoftwareRepositoryProxy,
				TimeoutInMilliseconds = this.TimeoutInMilliseconds,
				SyncLock = this.SyncLock,
				ChunkIndex = this.ChunkIndex,
				AllowSeek = this.AllowSeek,
				AllowWindowsAuth = this.AllowWindowsAuth
			};
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003BFC File Offset: 0x00001DFC
		internal async Task<int> Download()
		{
			bool flag = this.Bytes == 0L;
			int num;
			if (flag)
			{
				num = 200;
			}
			else
			{
				int ret = -1;
				DateTime downloadStartTime = DateTime.UtcNow;
				HttpClientHandler handler = new HttpClientHandler
				{
					UseDefaultCredentials = this.AllowWindowsAuth
				};
				bool flag2 = this.SoftwareRepositoryProxy != null;
				if (flag2)
				{
					handler.Proxy = this.SoftwareRepositoryProxy;
					handler.UseProxy = true;
				}
				HttpClient httpClient = new HttpClient(handler);
				HttpRequestMessage contentRequest = new HttpRequestMessage(HttpMethod.Get, new Uri(this.Url));
				long rangeStart = this.BytesFrom + this.BytesRead;
				long rangeEnd = this.BytesFrom + this.Bytes - 1L;
				contentRequest.Headers.Range = new RangeHeaderValue(new long?(rangeStart), new long?(rangeEnd));
				HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(contentRequest, HttpCompletionOption.ResponseHeadersRead, this.CancellationToken);
				HttpResponseMessage response = httpResponseMessage;
				httpResponseMessage = null;
				HttpStatusCode httpStatusCode = response.StatusCode;
				if (httpStatusCode != HttpStatusCode.OK && httpStatusCode != HttpStatusCode.PartialContent)
				{
					httpClient.Dispose();
					int num2 = (int)httpStatusCode;
					string text = "HTTP Response status code: ";
					int num3 = (int)httpStatusCode;
					throw new DownloadException(num2, text + num3.ToString());
				}
				HttpContentHeaders headers = response.Content.Headers;
				if (headers.ContentLength == null)
				{
					Diagnostics.Log(LogLevel.Warning, "Missing Content-Length header", new object[0]);
				}
				else if (headers.ContentLength.Value != rangeEnd - rangeStart + 1L)
				{
					throw new DownloadException(0, "Content-Length does not match request range");
				}
				if (headers.ContentRange == null)
				{
					Diagnostics.Log(LogLevel.Warning, "Missing Content-Range header", new object[0]);
				}
				else
				{
					bool flag3;
					if (!(headers.ContentRange.Unit.ToLowerInvariant() != "bytes"))
					{
						long? num4 = headers.ContentRange.From;
						long num5 = rangeStart;
						if ((num4.GetValueOrDefault() == num5) & (num4 != null))
						{
							num4 = headers.ContentRange.To;
							num5 = rangeEnd;
							flag3 = !((num4.GetValueOrDefault() == num5) & (num4 != null));
							goto IL_35A;
						}
					}
					flag3 = true;
					IL_35A:
					if (flag3)
					{
						throw new DownloadException(0, "Content-Range does not match request range");
					}
				}
				Stream stream2 = await response.Content.ReadAsStreamAsync();
				Stream stream = stream2;
				stream2 = null;
				try
				{
					byte[] buffer = new byte[4096];
					int bytesRead = 0;
					do
					{
						int num6 = await DownloadChunk.WithTimeout<int>(stream.ReadAsync(buffer, 0, buffer.Length, this.CancellationToken), this.TimeoutInMilliseconds);
						if ((bytesRead = num6) == 0)
						{
							goto Block_16;
						}
						this.CancellationToken.ThrowIfCancellationRequested();
						bytesRead = (int)Math.Min(this.Bytes - this.BytesRead, (long)bytesRead);
						if (this.SyncLock != null)
						{
							await this.SyncLock.WaitAsync();
						}
						try
						{
							if (this.AllowSeek)
							{
								this.OutStream.Seek(this.BytesFrom + this.BytesRead, SeekOrigin.Begin);
							}
							await this.OutStream.WriteAsync(buffer, 0, bytesRead, this.CancellationToken);
							this.BytesRead += (long)bytesRead;
							if (this.BytesRead >= this.Bytes)
							{
								await this.OutStream.FlushAsync();
							}
						}
						catch (OperationCanceledException e)
						{
							httpClient.Dispose();
							throw e;
						}
						catch (Exception)
						{
							httpClient.Dispose();
							throw new DownloadException(508, "Download interrupted because disk full, out of memory or other local storage reason prevents storing file locally.");
						}
						finally
						{
							if (this.SyncLock != null)
							{
								this.SyncLock.Release();
							}
						}
						this.DownloadSpeed = (double)this.BytesRead / (DateTime.UtcNow - downloadStartTime).TotalSeconds;
						this.OnDownloadProgress(null);
					}
					while (this.BytesRead < this.Bytes);
					httpClient.Dispose();
					return (int)httpStatusCode;
					Block_16:
					buffer = null;
				}
				finally
				{
					if (stream != null)
					{
						((IDisposable)stream).Dispose();
					}
				}
				stream = null;
				headers = null;
				num = ret;
			}
			return num;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003C40 File Offset: 0x00001E40
		[DebuggerStepThrough]
		private static Task<T> WithTimeout<T>(Task<T> task, int time)
		{
			DownloadChunk.<WithTimeout>d__36<T> <WithTimeout>d__ = new DownloadChunk.<WithTimeout>d__36<T>();
			<WithTimeout>d__.<>t__builder = AsyncTaskMethodBuilder<T>.Create();
			<WithTimeout>d__.task = task;
			<WithTimeout>d__.time = time;
			<WithTimeout>d__.<>1__state = -1;
			<WithTimeout>d__.<>t__builder.Start<DownloadChunk.<WithTimeout>d__36<T>>(ref <WithTimeout>d__);
			return <WithTimeout>d__.<>t__builder.Task;
		}

		// Token: 0x0400004F RID: 79
		private const int DefaultTimeoutInMilliseconds = 10000;

		// Token: 0x04000050 RID: 80
		internal const int BufferSize = 4096;

		// Token: 0x04000053 RID: 83
		internal long BytesFrom;

		// Token: 0x04000054 RID: 84
		internal long Bytes;

		// Token: 0x04000055 RID: 85
		internal long BytesRead;

		// Token: 0x04000056 RID: 86
		internal double DownloadSpeed;

		// Token: 0x04000057 RID: 87
		internal CancellationToken CancellationToken;

		// Token: 0x04000059 RID: 89
		internal Stream OutStream;

		// Token: 0x0400005B RID: 91
		internal int TimeoutInMilliseconds;

		// Token: 0x0400005C RID: 92
		internal SemaphoreSlim SyncLock;

		// Token: 0x0400005D RID: 93
		internal int ChunkIndex;

		// Token: 0x0400005E RID: 94
		internal bool AllowSeek = true;

		// Token: 0x0400005F RID: 95
		internal bool AllowWindowsAuth = false;
	}
}
