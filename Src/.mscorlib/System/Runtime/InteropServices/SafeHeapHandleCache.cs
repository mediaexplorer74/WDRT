using System;
using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000957 RID: 2391
	internal sealed class SafeHeapHandleCache : IDisposable
	{
		// Token: 0x06006201 RID: 25089 RVA: 0x001505C3 File Offset: 0x0014E7C3
		[SecuritySafeCritical]
		public SafeHeapHandleCache(ulong minSize = 64UL, ulong maxSize = 2048UL, int maxHandles = 0)
		{
			this._minSize = minSize;
			this._maxSize = maxSize;
			this._handleCache = new SafeHeapHandle[(maxHandles > 0) ? maxHandles : (Environment.ProcessorCount * 4)];
		}

		// Token: 0x06006202 RID: 25090 RVA: 0x001505F4 File Offset: 0x0014E7F4
		[SecurityCritical]
		public SafeHeapHandle Acquire(ulong minSize = 0UL)
		{
			if (minSize < this._minSize)
			{
				minSize = this._minSize;
			}
			SafeHeapHandle safeHeapHandle = null;
			for (int i = 0; i < this._handleCache.Length; i++)
			{
				safeHeapHandle = Interlocked.Exchange<SafeHeapHandle>(ref this._handleCache[i], null);
				if (safeHeapHandle != null)
				{
					break;
				}
			}
			if (safeHeapHandle != null)
			{
				if (safeHeapHandle.ByteLength < minSize)
				{
					safeHeapHandle.Resize(minSize);
				}
			}
			else
			{
				safeHeapHandle = new SafeHeapHandle(minSize);
			}
			return safeHeapHandle;
		}

		// Token: 0x06006203 RID: 25091 RVA: 0x0015065C File Offset: 0x0014E85C
		[SecurityCritical]
		public void Release(SafeHeapHandle handle)
		{
			if (handle.ByteLength <= this._maxSize)
			{
				for (int i = 0; i < this._handleCache.Length; i++)
				{
					handle = Interlocked.Exchange<SafeHeapHandle>(ref this._handleCache[i], handle);
					if (handle == null)
					{
						return;
					}
				}
			}
			handle.Dispose();
		}

		// Token: 0x06006204 RID: 25092 RVA: 0x001506A8 File Offset: 0x0014E8A8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06006205 RID: 25093 RVA: 0x001506B8 File Offset: 0x0014E8B8
		[SecuritySafeCritical]
		private void Dispose(bool disposing)
		{
			if (this._handleCache != null)
			{
				for (int i = 0; i < this._handleCache.Length; i++)
				{
					SafeHeapHandle safeHeapHandle = this._handleCache[i];
					this._handleCache[i] = null;
					if (safeHeapHandle != null && disposing)
					{
						safeHeapHandle.Dispose();
					}
				}
			}
		}

		// Token: 0x06006206 RID: 25094 RVA: 0x00150700 File Offset: 0x0014E900
		~SafeHeapHandleCache()
		{
			this.Dispose(false);
		}

		// Token: 0x04002B8B RID: 11147
		private readonly ulong _minSize;

		// Token: 0x04002B8C RID: 11148
		private readonly ulong _maxSize;

		// Token: 0x04002B8D RID: 11149
		[SecurityCritical]
		internal readonly SafeHeapHandle[] _handleCache;
	}
}
