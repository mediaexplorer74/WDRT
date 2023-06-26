using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

namespace SoftwareRepository.Streaming
{
	// Token: 0x0200000A RID: 10
	internal class ChunkManager
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022E8 File Offset: 0x000004E8
		internal int TotalChunks
		{
			get
			{
				return this.ChunkStates.Length;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002304 File Offset: 0x00000504
		internal bool IsDownloaded
		{
			get
			{
				return this.DownloadedChunks == this.TotalChunks;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002324 File Offset: 0x00000524
		internal long ProgressBytes
		{
			get
			{
				object stateLock = this.StateLock;
				long num3;
				lock (stateLock)
				{
					long num = (long)this.DownloadedChunks * this.ChunkSize;
					bool flag2 = this.FileSize % this.ChunkSize != 0L && this.ChunkStates[this.ChunkStates.Length - 1] == ChunkState.Downlodaded;
					if (flag2)
					{
						num -= this.ChunkSize;
						num += this.FileSize % this.ChunkSize;
					}
					foreach (DownloadChunk downloadChunk in this.DownloadingChunks)
					{
						num += downloadChunk.BytesRead;
					}
					bool flag3 = this.ResumedPartialProgress != null;
					if (flag3)
					{
						foreach (long num2 in this.ResumedPartialProgress.Values)
						{
							num += num2;
						}
					}
					num3 = num;
				}
				return num3;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002464 File Offset: 0x00000664
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000246C File Offset: 0x0000066C
		internal HashSet<DownloadChunk> DownloadingChunks { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002475 File Offset: 0x00000675
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000247D File Offset: 0x0000067D
		internal long ChunkSize { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002486 File Offset: 0x00000686
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000248E File Offset: 0x0000068E
		internal long FileSize { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002497 File Offset: 0x00000697
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000249F File Offset: 0x0000069F
		internal string FileName { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000024A8 File Offset: 0x000006A8
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000024B0 File Offset: 0x000006B0
		internal string FileUrl { get; set; }

		// Token: 0x06000025 RID: 37 RVA: 0x000024B9 File Offset: 0x000006B9
		internal ChunkManager(long chunkSize, long fileSize, string filename, Streamer streamer, SemaphoreSlim syncLock, CancellationToken cancellationToken)
			: this(null, chunkSize, fileSize, filename, streamer, syncLock, cancellationToken)
		{
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024D0 File Offset: 0x000006D0
		internal ChunkManager(DownloadMetadata metadata, long chunkSize, long fileSize, string filename, Streamer streamer, SemaphoreSlim syncLock, CancellationToken cancellationToken)
		{
			long num = fileSize / chunkSize + ((fileSize % chunkSize == 0L) ? 0L : 1L);
			bool flag = metadata == null || (long)metadata.ChunkStates.Length != num;
			if (flag)
			{
				this.ChunkStates = new ChunkState[num];
			}
			else
			{
				this.ChunkStates = metadata.ChunkStates;
				this.ResumedPartialProgress = metadata.PartialProgress;
			}
			this.NextUndownloadedIndex = 0;
			this.DownloadedChunks = 0;
			while (this.NextUndownloadedIndex < this.ChunkStates.Length && this.ChunkStates[this.NextUndownloadedIndex] > ChunkState.Undownloaded)
			{
				bool flag2 = this.ChunkStates[this.NextUndownloadedIndex] == ChunkState.Downlodaded;
				if (flag2)
				{
					this.DownloadedChunks++;
				}
				this.NextUndownloadedIndex++;
			}
			for (long num2 = (long)(this.NextUndownloadedIndex + 1); num2 < (long)this.ChunkStates.Length; num2 += 1L)
			{
				bool flag3 = this.ChunkStates[(int)(checked((IntPtr)num2))] == ChunkState.Downlodaded;
				if (flag3)
				{
					this.DownloadedChunks++;
				}
			}
			this.ChunkSize = chunkSize;
			this.DownloadingChunks = new HashSet<DownloadChunk>();
			this.FileName = filename;
			this.FileSize = fileSize;
			this.ClientCancellationToken = cancellationToken;
			this.SyncLock = syncLock;
			this.Streamer = streamer;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002640 File Offset: 0x00000840
		internal DownloadChunk GetNextChunk()
		{
			object stateLock = this.StateLock;
			DownloadChunk downloadChunk2;
			lock (stateLock)
			{
				bool flag2 = this.ResumedPartialProgress != null && this.ResumedPartialProgress.Count > 0;
				DownloadChunk downloadChunk;
				if (flag2)
				{
					int num = this.ResumedPartialProgress.Keys.First<int>();
					long num2 = this.ResumedPartialProgress[num];
					long num3 = (long)num * this.ChunkSize + num2;
					long num4 = Math.Min(this.ChunkSize - num2, this.FileSize - num3);
					downloadChunk = this.MakeChunk(num, num3, num4);
					this.ResumedPartialProgress.Remove(num);
					bool flag3 = this.ResumedPartialProgress.Count == 0;
					if (flag3)
					{
						this.ResumedPartialProgress = null;
					}
				}
				else
				{
					while (this.NextUndownloadedIndex < this.ChunkStates.Length && this.ChunkStates[this.NextUndownloadedIndex] > ChunkState.Undownloaded)
					{
						this.NextUndownloadedIndex++;
					}
					bool flag4 = this.NextUndownloadedIndex >= this.ChunkStates.Length;
					if (flag4)
					{
						return null;
					}
					long num5 = (long)this.NextUndownloadedIndex * this.ChunkSize;
					long num6 = Math.Min(this.ChunkSize, this.FileSize - num5);
					downloadChunk = this.MakeChunk(this.NextUndownloadedIndex, num5, num6);
					this.NextUndownloadedIndex++;
				}
				this.ChunkStates[downloadChunk.ChunkIndex] = ChunkState.PartiallyDownloaded;
				this.DownloadingChunks.Add(downloadChunk);
				downloadChunk2 = downloadChunk;
			}
			return downloadChunk2;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000027F4 File Offset: 0x000009F4
		internal DownloadChunk GetTestChunk()
		{
			object stateLock = this.StateLock;
			DownloadChunk downloadChunk;
			lock (stateLock)
			{
				while (this.NextUndownloadedIndex < this.ChunkStates.Length && this.ChunkStates[this.NextUndownloadedIndex] > ChunkState.Undownloaded)
				{
					this.NextUndownloadedIndex++;
				}
				bool flag2 = this.NextUndownloadedIndex < this.ChunkStates.Length - 1 || (this.NextUndownloadedIndex == this.ChunkStates.Length - 1 && this.FileSize % this.ChunkSize == 0L);
				if (flag2)
				{
					long num = (long)this.NextUndownloadedIndex * this.ChunkSize;
					downloadChunk = this.MakeChunk(this.NextUndownloadedIndex, num, this.ChunkSize);
				}
				else
				{
					int num2 = -1;
					long num3 = -1L;
					long num4 = 0L;
					bool flag3 = this.ChunkStates[this.ChunkStates.Length - 1] == ChunkState.Undownloaded;
					if (flag3)
					{
						num2 = this.ChunkStates.Length - 1;
						num3 = this.FileSize % this.ChunkSize;
					}
					bool flag4 = this.ResumedPartialProgress != null;
					if (flag4)
					{
						foreach (int num5 in this.ResumedPartialProgress.Keys)
						{
							long num6 = ((num5 == this.ChunkStates.Length - 1) ? (this.FileSize % this.ChunkSize) : this.ChunkSize);
							long num7 = num6 - this.ResumedPartialProgress[num5];
							bool flag5 = num7 > num3;
							if (flag5)
							{
								num2 = num5;
								num3 = num7;
								num4 = this.ResumedPartialProgress[num5];
							}
						}
					}
					bool flag6 = num2 >= 0 && num3 > 0L;
					if (flag6)
					{
						downloadChunk = this.MakeChunk(num2, (long)num2 * this.ChunkSize + num4, num3);
					}
					else
					{
						downloadChunk = null;
					}
				}
			}
			return downloadChunk;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002A14 File Offset: 0x00000C14
		internal void MarkDownloaded(DownloadChunk chunk)
		{
			object stateLock = this.StateLock;
			lock (stateLock)
			{
				bool flag2 = this.ChunkStates[chunk.ChunkIndex] != ChunkState.Downlodaded;
				if (flag2)
				{
					this.DownloadedChunks++;
					this.ChunkStates[chunk.ChunkIndex] = ChunkState.Downlodaded;
				}
				this.DownloadingChunks.Remove(chunk);
				bool flag3 = this.ResumedPartialProgress != null && this.ResumedPartialProgress.ContainsKey(chunk.ChunkIndex);
				if (flag3)
				{
					this.ResumedPartialProgress.Remove(chunk.ChunkIndex);
					bool flag4 = this.ResumedPartialProgress.Count == 0;
					if (flag4)
					{
						this.ResumedPartialProgress = null;
					}
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002AE8 File Offset: 0x00000CE8
		internal DownloadMetadata GetMetadata(bool includeInProgress)
		{
			object stateLock = this.StateLock;
			DownloadMetadata downloadMetadata2;
			lock (stateLock)
			{
				if (includeInProgress)
				{
					DownloadMetadata downloadMetadata = new DownloadMetadata();
					downloadMetadata.ChunkStates = this.ChunkStates.Clone() as ChunkState[];
					downloadMetadata.PartialProgress = this.DownloadingChunks.ToDictionary((DownloadChunk d) => d.ChunkIndex, (DownloadChunk d) => d.BytesRead);
					downloadMetadata2 = downloadMetadata;
				}
				else
				{
					DownloadMetadata downloadMetadata3 = new DownloadMetadata();
					downloadMetadata3.ChunkStates = this.ChunkStates.Select((ChunkState s) => (s == ChunkState.Downlodaded) ? ChunkState.Downlodaded : ChunkState.Undownloaded).ToArray<ChunkState>();
					downloadMetadata3.PartialProgress = null;
					downloadMetadata2 = downloadMetadata3;
				}
			}
			return downloadMetadata2;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002BE4 File Offset: 0x00000DE4
		internal void SaveMetadata(Streamer target, bool includeInProgress = false)
		{
			byte[] array = this.GetMetadata(includeInProgress).Serialize();
			bool flag = array != null;
			if (flag)
			{
				target.SetMetadata(array);
			}
			else
			{
				target.ClearMetadata();
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002C1C File Offset: 0x00000E1C
		private DownloadChunk MakeChunk(int chunkIndex, long startPosition, long downloadLength)
		{
			DownloadChunk downloadChunk = new DownloadChunk(this.FileName, this.FileUrl, startPosition, downloadLength, this.ClientCancellationToken)
			{
				SyncLock = this.SyncLock,
				ChunkIndex = chunkIndex,
				OutStream = this.Streamer.GetStream(),
				FileSize = this.FileSize,
				AllowWindowsAuth = this.AllowWindowsAuth
			};
			bool flag = this.ChunkTimeoutInMilliseconds != null;
			if (flag)
			{
				downloadChunk.TimeoutInMilliseconds = this.ChunkTimeoutInMilliseconds.Value;
			}
			bool flag2 = this.SoftwareRepositoryProxy != null;
			if (flag2)
			{
				downloadChunk.SoftwareRepositoryProxy = this.SoftwareRepositoryProxy;
			}
			return downloadChunk;
		}

		// Token: 0x04000018 RID: 24
		private object StateLock = new object();

		// Token: 0x04000019 RID: 25
		private Streamer Streamer;

		// Token: 0x0400001A RID: 26
		private SemaphoreSlim SyncLock;

		// Token: 0x0400001B RID: 27
		private ChunkState[] ChunkStates;

		// Token: 0x0400001C RID: 28
		private Dictionary<int, long> ResumedPartialProgress;

		// Token: 0x0400001D RID: 29
		private int NextUndownloadedIndex;

		// Token: 0x0400001E RID: 30
		private int DownloadedChunks;

		// Token: 0x04000024 RID: 36
		private CancellationToken ClientCancellationToken;

		// Token: 0x04000025 RID: 37
		internal int? ChunkTimeoutInMilliseconds;

		// Token: 0x04000026 RID: 38
		internal IWebProxy SoftwareRepositoryProxy;

		// Token: 0x04000027 RID: 39
		internal bool AllowWindowsAuth = false;
	}
}
