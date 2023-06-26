using System;
using System.Diagnostics.Tracing;
using System.Security;

namespace System.Threading
{
	// Token: 0x0200050D RID: 1293
	[EventSource(Name = "Microsoft-DotNETRuntime-PinnableBufferCache")]
	internal sealed class PinnableBufferCacheEventSource : EventSource
	{
		// Token: 0x06003CF1 RID: 15601 RVA: 0x000E73CC File Offset: 0x000E55CC
		[Event(1, Level = EventLevel.Verbose)]
		public void DebugMessage(string message)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(1, message);
			}
		}

		// Token: 0x06003CF2 RID: 15602 RVA: 0x000E73DE File Offset: 0x000E55DE
		[Event(2, Level = EventLevel.Verbose)]
		public void DebugMessage1(string message, long value)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(2, message, value);
			}
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x000E73F1 File Offset: 0x000E55F1
		[Event(3, Level = EventLevel.Verbose)]
		public void DebugMessage2(string message, long value1, long value2)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(3, new object[] { message, value1, value2 });
			}
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x000E741E File Offset: 0x000E561E
		[Event(18, Level = EventLevel.Verbose)]
		public void DebugMessage3(string message, long value1, long value2, long value3)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(18, new object[] { message, value1, value2, value3 });
			}
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x000E7456 File Offset: 0x000E5656
		[Event(4)]
		public void Create(string cacheName)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(4, cacheName);
			}
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x000E7468 File Offset: 0x000E5668
		[Event(5, Level = EventLevel.Verbose)]
		public void AllocateBuffer(string cacheName, ulong objectId, int objectHash, int objectGen, int freeCountAfter)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(5, new object[] { cacheName, objectId, objectHash, objectGen, freeCountAfter });
			}
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x000E74B4 File Offset: 0x000E56B4
		[Event(6)]
		public void AllocateBufferFromNotGen2(string cacheName, int notGen2CountAfter)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(6, cacheName, notGen2CountAfter);
			}
		}

		// Token: 0x06003CF8 RID: 15608 RVA: 0x000E74C7 File Offset: 0x000E56C7
		[Event(7)]
		public void AllocateBufferCreatingNewBuffers(string cacheName, int totalBuffsBefore, int objectCount)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(7, cacheName, totalBuffsBefore, objectCount);
			}
		}

		// Token: 0x06003CF9 RID: 15609 RVA: 0x000E74DB File Offset: 0x000E56DB
		[Event(8)]
		public void AllocateBufferAged(string cacheName, int agedCount)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(8, cacheName, agedCount);
			}
		}

		// Token: 0x06003CFA RID: 15610 RVA: 0x000E74EE File Offset: 0x000E56EE
		[Event(9)]
		public void AllocateBufferFreeListEmpty(string cacheName, int notGen2CountBefore)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(9, cacheName, notGen2CountBefore);
			}
		}

		// Token: 0x06003CFB RID: 15611 RVA: 0x000E7502 File Offset: 0x000E5702
		[Event(10, Level = EventLevel.Verbose)]
		public void FreeBuffer(string cacheName, ulong objectId, int objectHash, int freeCountBefore)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(10, new object[] { cacheName, objectId, objectHash, freeCountBefore });
			}
		}

		// Token: 0x06003CFC RID: 15612 RVA: 0x000E753A File Offset: 0x000E573A
		[Event(11)]
		public void FreeBufferStillTooYoung(string cacheName, int notGen2CountBefore)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(11, cacheName, notGen2CountBefore);
			}
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x000E754E File Offset: 0x000E574E
		[Event(13)]
		public void TrimCheck(string cacheName, int totalBuffs, bool neededMoreThanFreeList, int deltaMSec)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(13, new object[] { cacheName, totalBuffs, neededMoreThanFreeList, deltaMSec });
			}
		}

		// Token: 0x06003CFE RID: 15614 RVA: 0x000E7586 File Offset: 0x000E5786
		[Event(14)]
		public void TrimFree(string cacheName, int totalBuffs, int freeListCount, int toBeFreed)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(14, new object[] { cacheName, totalBuffs, freeListCount, toBeFreed });
			}
		}

		// Token: 0x06003CFF RID: 15615 RVA: 0x000E75BE File Offset: 0x000E57BE
		[Event(15)]
		public void TrimExperiment(string cacheName, int totalBuffs, int freeListCount, int numTrimTrial)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(15, new object[] { cacheName, totalBuffs, freeListCount, numTrimTrial });
			}
		}

		// Token: 0x06003D00 RID: 15616 RVA: 0x000E75F6 File Offset: 0x000E57F6
		[Event(16)]
		public void TrimFreeSizeOK(string cacheName, int totalBuffs, int freeListCount)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(16, cacheName, totalBuffs, freeListCount);
			}
		}

		// Token: 0x06003D01 RID: 15617 RVA: 0x000E760B File Offset: 0x000E580B
		[Event(17)]
		public void TrimFlush(string cacheName, int totalBuffs, int freeListCount, int notGen2CountBefore)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(17, new object[] { cacheName, totalBuffs, freeListCount, notGen2CountBefore });
			}
		}

		// Token: 0x06003D02 RID: 15618 RVA: 0x000E7643 File Offset: 0x000E5843
		[Event(20)]
		public void AgePendingBuffersResults(string cacheName, int promotedToFreeListCount, int heldBackCount)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(20, cacheName, promotedToFreeListCount, heldBackCount);
			}
		}

		// Token: 0x06003D03 RID: 15619 RVA: 0x000E7658 File Offset: 0x000E5858
		[Event(21)]
		public void WalkFreeListResult(string cacheName, int freeListCount, int gen0BuffersInFreeList)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(21, cacheName, freeListCount, gen0BuffersInFreeList);
			}
		}

		// Token: 0x06003D04 RID: 15620 RVA: 0x000E766D File Offset: 0x000E586D
		[Event(22)]
		public void FreeBufferNull(string cacheName, int freeCountBefore)
		{
			if (base.IsEnabled())
			{
				base.WriteEvent(22, cacheName, freeCountBefore);
			}
		}

		// Token: 0x06003D05 RID: 15621 RVA: 0x000E7684 File Offset: 0x000E5884
		internal static ulong AddressOf(object obj)
		{
			byte[] array = obj as byte[];
			if (array != null)
			{
				return (ulong)PinnableBufferCacheEventSource.AddressOfByteArray(array);
			}
			return 0UL;
		}

		// Token: 0x06003D06 RID: 15622 RVA: 0x000E76A4 File Offset: 0x000E58A4
		[SecuritySafeCritical]
		internal unsafe static long AddressOfByteArray(byte[] array)
		{
			if (array == null)
			{
				return 0L;
			}
			byte* ptr;
			if (array == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return ptr - 2 * sizeof(void*);
		}

		// Token: 0x040019E1 RID: 6625
		public static readonly PinnableBufferCacheEventSource Log = new PinnableBufferCacheEventSource();
	}
}
