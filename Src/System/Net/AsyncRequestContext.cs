using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net
{
	// Token: 0x020000F5 RID: 245
	internal class AsyncRequestContext : RequestContextBase
	{
		// Token: 0x06000889 RID: 2185 RVA: 0x0002F616 File Offset: 0x0002D816
		internal AsyncRequestContext(ListenerAsyncResult result)
		{
			this.m_Result = result;
			base.BaseConstruction(this.Allocate(0U));
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0002F634 File Offset: 0x0002D834
		private unsafe UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST* Allocate(uint size)
		{
			uint num = ((size != 0U) ? size : ((base.RequestBuffer == null) ? 4096U : base.Size));
			if (this.m_NativeOverlapped != null && (ulong)num != (ulong)((long)base.RequestBuffer.Length))
			{
				NativeOverlapped* nativeOverlapped = this.m_NativeOverlapped;
				this.m_NativeOverlapped = null;
				Overlapped.Free(nativeOverlapped);
			}
			if (this.m_NativeOverlapped == null)
			{
				base.SetBuffer(checked((int)num));
				this.m_NativeOverlapped = new Overlapped
				{
					AsyncResult = this.m_Result
				}.Pack(ListenerAsyncResult.IOCallback, base.RequestBuffer);
				return (UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST*)(void*)Marshal.UnsafeAddrOfPinnedArrayElement(base.RequestBuffer, 0);
			}
			return base.RequestBlob;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0002F6DC File Offset: 0x0002D8DC
		internal unsafe void Reset(ulong requestId, uint size)
		{
			base.SetBlob(this.Allocate(size));
			base.RequestBlob->RequestId = requestId;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0002F6F8 File Offset: 0x0002D8F8
		protected unsafe override void OnReleasePins()
		{
			if (this.m_NativeOverlapped != null)
			{
				NativeOverlapped* nativeOverlapped = this.m_NativeOverlapped;
				this.m_NativeOverlapped = null;
				Overlapped.Free(nativeOverlapped);
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0002F724 File Offset: 0x0002D924
		protected override void Dispose(bool disposing)
		{
			if (this.m_NativeOverlapped != null && (!NclUtilities.HasShutdownStarted || disposing))
			{
				Overlapped.Free(this.m_NativeOverlapped);
			}
			base.Dispose(disposing);
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x0002F74E File Offset: 0x0002D94E
		internal unsafe NativeOverlapped* NativeOverlapped
		{
			get
			{
				return this.m_NativeOverlapped;
			}
		}

		// Token: 0x04000DD6 RID: 3542
		private unsafe NativeOverlapped* m_NativeOverlapped;

		// Token: 0x04000DD7 RID: 3543
		private ListenerAsyncResult m_Result;
	}
}
