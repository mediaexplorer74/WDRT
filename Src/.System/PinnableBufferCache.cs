using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System
{
	// Token: 0x02000060 RID: 96
	internal sealed class PinnableBufferCache
	{
		// Token: 0x06000420 RID: 1056 RVA: 0x0001DA50 File Offset: 0x0001BC50
		public PinnableBufferCache(string cacheName, int numberOfElements)
			: this(cacheName, () => new byte[numberOfElements])
		{
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001DA7D File Offset: 0x0001BC7D
		public byte[] AllocateBuffer()
		{
			return (byte[])this.Allocate();
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0001DA8A File Offset: 0x0001BC8A
		public void FreeBuffer(byte[] buffer)
		{
			this.Free(buffer);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001DA94 File Offset: 0x0001BC94
		[SecuritySafeCritical]
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		internal PinnableBufferCache(string cacheName, Func<object> factory)
		{
			this.m_NotGen2 = new List<object>(16);
			this.m_factory = factory;
			string text = "PinnableBufferCache_" + cacheName + "_Disabled";
			try
			{
				string environmentVariable = Environment.GetEnvironmentVariable(text);
				if (environmentVariable != null)
				{
					System.PinnableBufferCacheEventSource.Log.DebugMessage("Creating " + cacheName + " PinnableBufferCacheDisabled=" + environmentVariable);
					int num = environmentVariable.IndexOf(cacheName, StringComparison.OrdinalIgnoreCase);
					if (0 <= num)
					{
						System.PinnableBufferCacheEventSource.Log.DebugMessage("Disabling " + cacheName);
						return;
					}
				}
			}
			catch
			{
			}
			string text2 = "PinnableBufferCache_" + cacheName + "_MinCount";
			try
			{
				string environmentVariable2 = Environment.GetEnvironmentVariable(text2);
				if (environmentVariable2 != null && int.TryParse(environmentVariable2, out this.m_minBufferCount))
				{
					this.CreateNewBuffers();
				}
			}
			catch
			{
			}
			System.PinnableBufferCacheEventSource.Log.Create(cacheName);
			this.m_CacheName = cacheName;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001DB8C File Offset: 0x0001BD8C
		[SecuritySafeCritical]
		internal object Allocate()
		{
			if (this.m_CacheName == null)
			{
				return this.m_factory();
			}
			object obj;
			if (!this.m_FreeList.TryPop(out obj))
			{
				this.Restock(out obj);
			}
			if (System.PinnableBufferCacheEventSource.Log.IsEnabled())
			{
				int num = Interlocked.Increment(ref this.m_numAllocCalls);
				if (num >= 1024)
				{
					lock (this)
					{
						int num2 = Interlocked.Exchange(ref this.m_numAllocCalls, 0);
						if (num2 >= 1024)
						{
							int num3 = 0;
							foreach (object obj2 in this.m_FreeList)
							{
								if (GC.GetGeneration(obj2) < GC.MaxGeneration)
								{
									num3++;
								}
							}
							System.PinnableBufferCacheEventSource.Log.WalkFreeListResult(this.m_CacheName, this.m_FreeList.Count, num3);
						}
					}
				}
				System.PinnableBufferCacheEventSource.Log.AllocateBuffer(this.m_CacheName, System.PinnableBufferCacheEventSource.AddressOf(obj), obj.GetHashCode(), GC.GetGeneration(obj), this.m_FreeList.Count);
			}
			return obj;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001DCC8 File Offset: 0x0001BEC8
		[SecuritySafeCritical]
		internal void Free(object buffer)
		{
			if (this.m_CacheName == null)
			{
				return;
			}
			if (System.PinnableBufferCacheEventSource.Log.IsEnabled())
			{
				System.PinnableBufferCacheEventSource.Log.FreeBuffer(this.m_CacheName, System.PinnableBufferCacheEventSource.AddressOf(buffer), buffer.GetHashCode(), this.m_FreeList.Count);
			}
			if (buffer == null)
			{
				if (System.PinnableBufferCacheEventSource.Log.IsEnabled())
				{
					System.PinnableBufferCacheEventSource.Log.FreeBufferNull(this.m_CacheName, this.m_FreeList.Count);
				}
				return;
			}
			if (this.m_gen1CountAtLastRestock + 3 > GC.CollectionCount(GC.MaxGeneration - 1))
			{
				lock (this)
				{
					if (GC.GetGeneration(buffer) < GC.MaxGeneration)
					{
						this.m_moreThanFreeListNeeded = true;
						System.PinnableBufferCacheEventSource.Log.FreeBufferStillTooYoung(this.m_CacheName, this.m_NotGen2.Count);
						this.m_NotGen2.Add(buffer);
						this.m_gen1CountAtLastRestock = GC.CollectionCount(GC.MaxGeneration - 1);
						return;
					}
				}
			}
			this.m_FreeList.Push(buffer);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001DDD8 File Offset: 0x0001BFD8
		[SecuritySafeCritical]
		private void Restock(out object returnBuffer)
		{
			lock (this)
			{
				if (!this.m_FreeList.TryPop(out returnBuffer))
				{
					if (this.m_restockSize == 0)
					{
						System.Gen2GcCallback.Register(new Func<object, bool>(System.PinnableBufferCache.Gen2GcCallbackFunc), this);
					}
					this.m_moreThanFreeListNeeded = true;
					System.PinnableBufferCacheEventSource.Log.AllocateBufferFreeListEmpty(this.m_CacheName, this.m_NotGen2.Count);
					if (this.m_NotGen2.Count == 0)
					{
						this.CreateNewBuffers();
					}
					int num = this.m_NotGen2.Count - 1;
					if (GC.GetGeneration(this.m_NotGen2[num]) < GC.MaxGeneration && GC.GetGeneration(this.m_NotGen2[0]) == GC.MaxGeneration)
					{
						num = 0;
					}
					returnBuffer = this.m_NotGen2[num];
					this.m_NotGen2.RemoveAt(num);
					if (System.PinnableBufferCacheEventSource.Log.IsEnabled() && GC.GetGeneration(returnBuffer) < GC.MaxGeneration)
					{
						System.PinnableBufferCacheEventSource.Log.AllocateBufferFromNotGen2(this.m_CacheName, this.m_NotGen2.Count);
					}
					if (!this.AgePendingBuffers() && this.m_NotGen2.Count == this.m_restockSize / 2)
					{
						System.PinnableBufferCacheEventSource.Log.DebugMessage("Proactively adding more buffers to aging pool");
						this.CreateNewBuffers();
					}
				}
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001DF3C File Offset: 0x0001C13C
		[SecuritySafeCritical]
		private bool AgePendingBuffers()
		{
			if (this.m_gen1CountAtLastRestock < GC.CollectionCount(GC.MaxGeneration - 1))
			{
				int num = 0;
				List<object> list = new List<object>();
				System.PinnableBufferCacheEventSource.Log.AllocateBufferAged(this.m_CacheName, this.m_NotGen2.Count);
				for (int i = 0; i < this.m_NotGen2.Count; i++)
				{
					object obj = this.m_NotGen2[i];
					if (GC.GetGeneration(obj) >= GC.MaxGeneration)
					{
						this.m_FreeList.Push(obj);
						num++;
					}
					else
					{
						list.Add(obj);
					}
				}
				System.PinnableBufferCacheEventSource.Log.AgePendingBuffersResults(this.m_CacheName, num, list.Count);
				this.m_NotGen2 = list;
				return true;
			}
			return false;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001DFF0 File Offset: 0x0001C1F0
		private void CreateNewBuffers()
		{
			if (this.m_restockSize == 0)
			{
				this.m_restockSize = 4;
			}
			else if (this.m_restockSize < 16)
			{
				this.m_restockSize = 16;
			}
			else if (this.m_restockSize < 256)
			{
				this.m_restockSize *= 2;
			}
			else if (this.m_restockSize < 4096)
			{
				this.m_restockSize = this.m_restockSize * 3 / 2;
			}
			else
			{
				this.m_restockSize = 4096;
			}
			if (this.m_minBufferCount > this.m_buffersUnderManagement)
			{
				this.m_restockSize = Math.Max(this.m_restockSize, this.m_minBufferCount - this.m_buffersUnderManagement);
			}
			System.PinnableBufferCacheEventSource.Log.AllocateBufferCreatingNewBuffers(this.m_CacheName, this.m_buffersUnderManagement, this.m_restockSize);
			for (int i = 0; i < this.m_restockSize; i++)
			{
				object obj = this.m_factory();
				object obj2 = new object();
				this.m_NotGen2.Add(obj);
			}
			this.m_buffersUnderManagement += this.m_restockSize;
			this.m_gen1CountAtLastRestock = GC.CollectionCount(GC.MaxGeneration - 1);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001E105 File Offset: 0x0001C305
		[SecuritySafeCritical]
		private static bool Gen2GcCallbackFunc(object targetObj)
		{
			return ((System.PinnableBufferCache)targetObj).TrimFreeListIfNeeded();
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001E114 File Offset: 0x0001C314
		[SecuritySafeCritical]
		private bool TrimFreeListIfNeeded()
		{
			int tickCount = Environment.TickCount;
			int num = tickCount - this.m_msecNoUseBeyondFreeListSinceThisTime;
			System.PinnableBufferCacheEventSource.Log.TrimCheck(this.m_CacheName, this.m_buffersUnderManagement, this.m_moreThanFreeListNeeded, num);
			if (this.m_moreThanFreeListNeeded)
			{
				this.m_moreThanFreeListNeeded = false;
				this.m_trimmingExperimentInProgress = false;
				this.m_msecNoUseBeyondFreeListSinceThisTime = tickCount;
				return true;
			}
			if (0 <= num && num < 10000)
			{
				return true;
			}
			lock (this)
			{
				if (this.m_moreThanFreeListNeeded)
				{
					this.m_moreThanFreeListNeeded = false;
					this.m_trimmingExperimentInProgress = false;
					this.m_msecNoUseBeyondFreeListSinceThisTime = tickCount;
					return true;
				}
				int count = this.m_FreeList.Count;
				if (this.m_NotGen2.Count > 0)
				{
					if (!this.m_trimmingExperimentInProgress)
					{
						System.PinnableBufferCacheEventSource.Log.TrimFlush(this.m_CacheName, this.m_buffersUnderManagement, count, this.m_NotGen2.Count);
						this.AgePendingBuffers();
						this.m_trimmingExperimentInProgress = true;
						return true;
					}
					System.PinnableBufferCacheEventSource.Log.TrimFree(this.m_CacheName, this.m_buffersUnderManagement, count, this.m_NotGen2.Count);
					this.m_buffersUnderManagement -= this.m_NotGen2.Count;
					int num2 = this.m_buffersUnderManagement / 4;
					if (num2 < this.m_restockSize)
					{
						this.m_restockSize = Math.Max(num2, 16);
					}
					this.m_NotGen2.Clear();
					this.m_trimmingExperimentInProgress = false;
					return true;
				}
				else
				{
					int num3 = count / 4 + 1;
					if (count * 15 <= this.m_buffersUnderManagement || this.m_buffersUnderManagement - num3 <= this.m_minBufferCount)
					{
						System.PinnableBufferCacheEventSource.Log.TrimFreeSizeOK(this.m_CacheName, this.m_buffersUnderManagement, count);
						return true;
					}
					System.PinnableBufferCacheEventSource.Log.TrimExperiment(this.m_CacheName, this.m_buffersUnderManagement, count, num3);
					for (int i = 0; i < num3; i++)
					{
						object obj;
						if (this.m_FreeList.TryPop(out obj))
						{
							this.m_NotGen2.Add(obj);
						}
					}
					this.m_msecNoUseBeyondFreeListSinceThisTime = tickCount;
					this.m_trimmingExperimentInProgress = true;
				}
			}
			return true;
		}

		// Token: 0x0400051B RID: 1307
		private const int DefaultNumberOfBuffers = 16;

		// Token: 0x0400051C RID: 1308
		private string m_CacheName;

		// Token: 0x0400051D RID: 1309
		private Func<object> m_factory;

		// Token: 0x0400051E RID: 1310
		private ConcurrentStack<object> m_FreeList = new ConcurrentStack<object>();

		// Token: 0x0400051F RID: 1311
		private List<object> m_NotGen2;

		// Token: 0x04000520 RID: 1312
		private int m_gen1CountAtLastRestock;

		// Token: 0x04000521 RID: 1313
		private int m_msecNoUseBeyondFreeListSinceThisTime;

		// Token: 0x04000522 RID: 1314
		private bool m_moreThanFreeListNeeded;

		// Token: 0x04000523 RID: 1315
		private int m_buffersUnderManagement;

		// Token: 0x04000524 RID: 1316
		private int m_restockSize;

		// Token: 0x04000525 RID: 1317
		private bool m_trimmingExperimentInProgress;

		// Token: 0x04000526 RID: 1318
		private int m_minBufferCount;

		// Token: 0x04000527 RID: 1319
		private int m_numAllocCalls;
	}
}
