using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020000F6 RID: 246
	internal class SyncRequestContext : RequestContextBase
	{
		// Token: 0x0600088F RID: 2191 RVA: 0x0002F756 File Offset: 0x0002D956
		internal SyncRequestContext(int size)
		{
			base.BaseConstruction(this.Allocate(size));
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0002F76C File Offset: 0x0002D96C
		private unsafe UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* Allocate(int size)
		{
			if (this.m_PinnedHandle.IsAllocated)
			{
				if (base.RequestBuffer.Length == size)
				{
					return base.RequestBlob;
				}
				this.m_PinnedHandle.Free();
			}
			base.SetBuffer(size);
			if (base.RequestBuffer == null)
			{
				return null;
			}
			this.m_PinnedHandle = GCHandle.Alloc(base.RequestBuffer, GCHandleType.Pinned);
			return (UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST*)(void*)Marshal.UnsafeAddrOfPinnedArrayElement(base.RequestBuffer, 0);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0002F7D8 File Offset: 0x0002D9D8
		internal void Reset(int size)
		{
			base.SetBlob(this.Allocate(size));
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0002F7E7 File Offset: 0x0002D9E7
		protected override void OnReleasePins()
		{
			if (this.m_PinnedHandle.IsAllocated)
			{
				this.m_PinnedHandle.Free();
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0002F801 File Offset: 0x0002DA01
		protected override void Dispose(bool disposing)
		{
			if (this.m_PinnedHandle.IsAllocated && (!NclUtilities.HasShutdownStarted || disposing))
			{
				this.m_PinnedHandle.Free();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000DD8 RID: 3544
		private GCHandle m_PinnedHandle;
	}
}
