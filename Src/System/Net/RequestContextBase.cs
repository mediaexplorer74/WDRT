using System;

namespace System.Net
{
	// Token: 0x020000F4 RID: 244
	internal abstract class RequestContextBase : IDisposable
	{
		// Token: 0x0600087A RID: 2170 RVA: 0x0002F506 File Offset: 0x0002D706
		protected unsafe void BaseConstruction(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* requestBlob)
		{
			if (requestBlob == null)
			{
				GC.SuppressFinalize(this);
				return;
			}
			this.m_MemoryBlob = requestBlob;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0002F51B File Offset: 0x0002D71B
		internal void ReleasePins()
		{
			this.m_OriginalBlobAddress = this.m_MemoryBlob;
			this.UnsetBlob();
			this.OnReleasePins();
		}

		// Token: 0x0600087C RID: 2172
		protected abstract void OnReleasePins();

		// Token: 0x0600087D RID: 2173 RVA: 0x0002F535 File Offset: 0x0002D735
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0002F53D File Offset: 0x0002D73D
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0002F546 File Offset: 0x0002D746
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0002F548 File Offset: 0x0002D748
		~RequestContextBase()
		{
			this.Dispose(false);
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0002F578 File Offset: 0x0002D778
		internal unsafe UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* RequestBlob
		{
			get
			{
				return this.m_MemoryBlob;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x0002F580 File Offset: 0x0002D780
		internal byte[] RequestBuffer
		{
			get
			{
				return this.m_BackingBuffer;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0002F588 File Offset: 0x0002D788
		internal uint Size
		{
			get
			{
				return (uint)this.m_BackingBuffer.Length;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0002F594 File Offset: 0x0002D794
		internal unsafe IntPtr OriginalBlobAddress
		{
			get
			{
				UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* memoryBlob = this.m_MemoryBlob;
				return (IntPtr)((void*)((memoryBlob == null) ? this.m_OriginalBlobAddress : memoryBlob));
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0002F5BB File Offset: 0x0002D7BB
		protected unsafe void SetBlob(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* requestBlob)
		{
			if (requestBlob == null)
			{
				this.UnsetBlob();
				return;
			}
			if (this.m_MemoryBlob == null)
			{
				GC.ReRegisterForFinalize(this);
			}
			this.m_MemoryBlob = requestBlob;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0002F5E0 File Offset: 0x0002D7E0
		protected void UnsetBlob()
		{
			if (this.m_MemoryBlob != null)
			{
				GC.SuppressFinalize(this);
			}
			this.m_MemoryBlob = null;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0002F5FA File Offset: 0x0002D7FA
		protected void SetBuffer(int size)
		{
			this.m_BackingBuffer = ((size == 0) ? null : new byte[size]);
		}

		// Token: 0x04000DD3 RID: 3539
		private unsafe UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* m_MemoryBlob;

		// Token: 0x04000DD4 RID: 3540
		private unsafe UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* m_OriginalBlobAddress;

		// Token: 0x04000DD5 RID: 3541
		private byte[] m_BackingBuffer;
	}
}
