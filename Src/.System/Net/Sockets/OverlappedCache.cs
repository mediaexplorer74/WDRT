using System;
using System.Threading;

namespace System.Net.Sockets
{
	// Token: 0x0200038D RID: 909
	internal class OverlappedCache
	{
		// Token: 0x0600221C RID: 8732 RVA: 0x000A350A File Offset: 0x000A170A
		internal OverlappedCache(Overlapped overlapped, object[] pinnedObjectsArray, IOCompletionCallback callback)
		{
			this.m_Overlapped = overlapped;
			this.m_PinnedObjects = pinnedObjectsArray;
			this.m_PinnedObjectsArray = pinnedObjectsArray;
			this.m_NativeOverlapped = new SafeNativeOverlapped(overlapped.UnsafePack(callback, pinnedObjectsArray));
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x000A353A File Offset: 0x000A173A
		internal OverlappedCache(Overlapped overlapped, object pinnedObjects, IOCompletionCallback callback, bool alreadyTriedCast)
		{
			this.m_Overlapped = overlapped;
			this.m_PinnedObjects = pinnedObjects;
			this.m_PinnedObjectsArray = (alreadyTriedCast ? null : NclConstants.EmptyObjectArray);
			this.m_NativeOverlapped = new SafeNativeOverlapped(overlapped.UnsafePack(callback, pinnedObjects));
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x000A3575 File Offset: 0x000A1775
		internal Overlapped Overlapped
		{
			get
			{
				return this.m_Overlapped;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x0600221F RID: 8735 RVA: 0x000A357D File Offset: 0x000A177D
		internal SafeNativeOverlapped NativeOverlapped
		{
			get
			{
				return this.m_NativeOverlapped;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06002220 RID: 8736 RVA: 0x000A3585 File Offset: 0x000A1785
		internal object PinnedObjects
		{
			get
			{
				return this.m_PinnedObjects;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06002221 RID: 8737 RVA: 0x000A3590 File Offset: 0x000A1790
		internal object[] PinnedObjectsArray
		{
			get
			{
				object[] array = this.m_PinnedObjectsArray;
				if (array != null && array.Length == 0)
				{
					array = this.m_PinnedObjects as object[];
					if (array != null && array.Length == 0)
					{
						this.m_PinnedObjectsArray = null;
					}
					else
					{
						this.m_PinnedObjectsArray = array;
					}
				}
				return this.m_PinnedObjectsArray;
			}
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x000A35D4 File Offset: 0x000A17D4
		internal void Free()
		{
			this.InternalFree();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x000A35E2 File Offset: 0x000A17E2
		private void InternalFree()
		{
			this.m_Overlapped = null;
			this.m_PinnedObjects = null;
			if (this.m_NativeOverlapped != null)
			{
				if (!this.m_NativeOverlapped.IsInvalid)
				{
					this.m_NativeOverlapped.Dispose();
				}
				this.m_NativeOverlapped = null;
			}
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x000A361C File Offset: 0x000A181C
		internal static void InterlockedFree(ref OverlappedCache overlappedCache)
		{
			OverlappedCache overlappedCache2 = ((overlappedCache == null) ? null : Interlocked.Exchange<OverlappedCache>(ref overlappedCache, null));
			if (overlappedCache2 != null)
			{
				overlappedCache2.Free();
			}
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x000A3644 File Offset: 0x000A1844
		~OverlappedCache()
		{
			if (!NclUtilities.HasShutdownStarted)
			{
				this.InternalFree();
			}
		}

		// Token: 0x04001F50 RID: 8016
		internal Overlapped m_Overlapped;

		// Token: 0x04001F51 RID: 8017
		internal SafeNativeOverlapped m_NativeOverlapped;

		// Token: 0x04001F52 RID: 8018
		internal object m_PinnedObjects;

		// Token: 0x04001F53 RID: 8019
		internal object[] m_PinnedObjectsArray;
	}
}
