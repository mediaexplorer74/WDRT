using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020001BE RID: 446
	internal class HttpRequestStream : Stream
	{
		// Token: 0x0600116A RID: 4458 RVA: 0x0005EAE7 File Offset: 0x0005CCE7
		internal HttpRequestStream(HttpListenerContext httpContext)
		{
			this.m_HttpContext = httpContext;
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x0005EAF6 File Offset: 0x0005CCF6
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x0005EAF9 File Offset: 0x0005CCF9
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x0005EAFC File Offset: 0x0005CCFC
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x0005EAFF File Offset: 0x0005CCFF
		internal bool Closed
		{
			get
			{
				return this.m_Closed;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x0005EB07 File Offset: 0x0005CD07
		internal bool BufferedDataChunksAvailable
		{
			get
			{
				return this.m_DataChunkIndex > -1;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x0005EB12 File Offset: 0x0005CD12
		internal HttpListenerContext InternalHttpContext
		{
			get
			{
				return this.m_HttpContext;
			}
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0005EB1A File Offset: 0x0005CD1A
		public override void Flush()
		{
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0005EB1C File Offset: 0x0005CD1C
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x0005EB23 File Offset: 0x0005CD23
		public override long Length
		{
			get
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x0005EB34 File Offset: 0x0005CD34
		// (set) Token: 0x06001175 RID: 4469 RVA: 0x0005EB45 File Offset: 0x0005CD45
		public override long Position
		{
			get
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
			set
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x0005EB56 File Offset: 0x0005CD56
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0005EB67 File Offset: 0x0005CD67
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0005EB78 File Offset: 0x0005CD78
		public unsafe override int Read([In] [Out] byte[] buffer, int offset, int size)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "Read", "");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (size == 0 || this.m_Closed)
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "Read", "dataRead:0");
				}
				return 0;
			}
			uint num = 0U;
			if (this.m_DataChunkIndex != -1)
			{
				num = UnsafeNclNativeMethods.HttpApi.GetChunks(this.m_HttpContext.Request.RequestBuffer, this.m_HttpContext.Request.OriginalBlobAddress, ref this.m_DataChunkIndex, ref this.m_DataChunkOffset, buffer, offset, size);
			}
			if (this.m_DataChunkIndex == -1 && (ulong)num < (ulong)((long)size))
			{
				uint num2 = 0U;
				offset += (int)num;
				size -= (int)num;
				if (size > 131072)
				{
					size = 131072;
				}
				uint num4;
				fixed (byte[] array = buffer)
				{
					byte* ptr;
					if (buffer == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					uint num3 = 0U;
					if (!this.m_InOpaqueMode)
					{
						num3 = 1U;
					}
					num4 = UnsafeNclNativeMethods.HttpApi.HttpReceiveRequestEntityBody(this.m_HttpContext.RequestQueueHandle, this.m_HttpContext.RequestId, num3, (void*)(ptr + offset), (uint)size, out num2, null);
					num += num2;
				}
				if (num4 != 0U && num4 != 38U)
				{
					Exception ex = new HttpListenerException((int)num4);
					if (Logging.On)
					{
						Logging.Exception(Logging.HttpListener, this, "Read", ex);
					}
					throw ex;
				}
				this.UpdateAfterRead(num4, num);
			}
			if (Logging.On)
			{
				Logging.Dump(Logging.HttpListener, this, "Read", buffer, offset, (int)num);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "Read", "dataRead:" + num.ToString());
			}
			return (int)num;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0005ED43 File Offset: 0x0005CF43
		private void UpdateAfterRead(uint statusCode, uint dataRead)
		{
			if (statusCode == 38U || dataRead == 0U)
			{
				this.Close();
			}
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x0005ED54 File Offset: 0x0005CF54
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public unsafe override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "BeginRead", "");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			if (size == 0 || this.m_Closed)
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "BeginRead", "");
				}
				HttpRequestStream.HttpRequestStreamAsyncResult httpRequestStreamAsyncResult = new HttpRequestStream.HttpRequestStreamAsyncResult(this, state, callback);
				httpRequestStreamAsyncResult.InvokeCallback(0U);
				return httpRequestStreamAsyncResult;
			}
			HttpRequestStream.HttpRequestStreamAsyncResult httpRequestStreamAsyncResult2 = null;
			uint num = 0U;
			if (this.m_DataChunkIndex != -1)
			{
				num = UnsafeNclNativeMethods.HttpApi.GetChunks(this.m_HttpContext.Request.RequestBuffer, this.m_HttpContext.Request.OriginalBlobAddress, ref this.m_DataChunkIndex, ref this.m_DataChunkOffset, buffer, offset, size);
				if (this.m_DataChunkIndex != -1 && (ulong)num == (ulong)((long)size))
				{
					httpRequestStreamAsyncResult2 = new HttpRequestStream.HttpRequestStreamAsyncResult(this, state, callback, buffer, offset, (uint)size, 0U);
					httpRequestStreamAsyncResult2.InvokeCallback(num);
				}
			}
			if (this.m_DataChunkIndex == -1 && (ulong)num < (ulong)((long)size))
			{
				uint num2 = 0U;
				offset += (int)num;
				size -= (int)num;
				if (size > 131072)
				{
					size = 131072;
				}
				httpRequestStreamAsyncResult2 = new HttpRequestStream.HttpRequestStreamAsyncResult(this, state, callback, buffer, offset, (uint)size, num);
				uint num4;
				try
				{
					try
					{
						fixed (byte[] array = buffer)
						{
							if (buffer == null || array.Length == 0)
							{
								byte* ptr = null;
							}
							else
							{
								byte* ptr = &array[0];
							}
							this.m_HttpContext.EnsureBoundHandle();
							uint num3 = 0U;
							if (!this.m_InOpaqueMode)
							{
								num3 = 1U;
							}
							num2 = UnsafeNclNativeMethods.HttpApi.HttpReceiveRequestEntityBody(this.m_HttpContext.RequestQueueHandle, this.m_HttpContext.RequestId, num3, httpRequestStreamAsyncResult2.m_pPinnedBuffer, (uint)size, out num4, httpRequestStreamAsyncResult2.m_pOverlapped);
						}
					}
					finally
					{
						byte[] array = null;
					}
				}
				catch (Exception ex)
				{
					if (Logging.On)
					{
						Logging.Exception(Logging.HttpListener, this, "BeginRead", ex);
					}
					httpRequestStreamAsyncResult2.InternalCleanup();
					throw;
				}
				if (num2 != 0U && num2 != 997U)
				{
					httpRequestStreamAsyncResult2.InternalCleanup();
					if (num2 != 38U)
					{
						Exception ex2 = new HttpListenerException((int)num2);
						if (Logging.On)
						{
							Logging.Exception(Logging.HttpListener, this, "BeginRead", ex2);
						}
						httpRequestStreamAsyncResult2.InternalCleanup();
						throw ex2;
					}
					httpRequestStreamAsyncResult2 = new HttpRequestStream.HttpRequestStreamAsyncResult(this, state, callback, num);
					httpRequestStreamAsyncResult2.InvokeCallback(0U);
				}
				else if (num2 == 0U && HttpListener.SkipIOCPCallbackOnSuccess)
				{
					httpRequestStreamAsyncResult2.IOCompleted(num2, num4);
				}
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "BeginRead", "");
			}
			return httpRequestStreamAsyncResult2;
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0005EFD8 File Offset: 0x0005D1D8
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "EndRead", "");
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			HttpRequestStream.HttpRequestStreamAsyncResult httpRequestStreamAsyncResult = asyncResult as HttpRequestStream.HttpRequestStreamAsyncResult;
			if (httpRequestStreamAsyncResult == null || httpRequestStreamAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			if (httpRequestStreamAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndRead" }));
			}
			httpRequestStreamAsyncResult.EndCalled = true;
			object obj = httpRequestStreamAsyncResult.InternalWaitForCompletion();
			Exception ex = obj as Exception;
			if (ex != null)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.HttpListener, this, "EndRead", ex);
				}
				throw ex;
			}
			uint num = (uint)obj;
			this.UpdateAfterRead((uint)httpRequestStreamAsyncResult.ErrorCode, num);
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "EndRead", "");
			}
			return (int)(num + httpRequestStreamAsyncResult.m_dataAlreadyRead);
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x0005F0C7 File Offset: 0x0005D2C7
		public override void Write(byte[] buffer, int offset, int size)
		{
			throw new InvalidOperationException(SR.GetString("net_readonlystream"));
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0005F0D8 File Offset: 0x0005D2D8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			throw new InvalidOperationException(SR.GetString("net_readonlystream"));
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0005F0E9 File Offset: 0x0005D2E9
		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new InvalidOperationException(SR.GetString("net_readonlystream"));
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0005F0FC File Offset: 0x0005D2FC
		protected override void Dispose(bool disposing)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "Dispose", "");
			}
			try
			{
				this.m_Closed = true;
			}
			finally
			{
				base.Dispose(disposing);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "Dispose", "");
			}
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0005F164 File Offset: 0x0005D364
		internal void SwitchToOpaqueMode()
		{
			this.m_InOpaqueMode = true;
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0005F16D File Offset: 0x0005D36D
		internal uint GetChunks(byte[] buffer, int offset, int size)
		{
			return UnsafeNclNativeMethods.HttpApi.GetChunks(this.m_HttpContext.Request.RequestBuffer, this.m_HttpContext.Request.OriginalBlobAddress, ref this.m_DataChunkIndex, ref this.m_DataChunkOffset, buffer, offset, size);
		}

		// Token: 0x04001446 RID: 5190
		private HttpListenerContext m_HttpContext;

		// Token: 0x04001447 RID: 5191
		private uint m_DataChunkOffset;

		// Token: 0x04001448 RID: 5192
		private int m_DataChunkIndex;

		// Token: 0x04001449 RID: 5193
		private bool m_Closed;

		// Token: 0x0400144A RID: 5194
		internal const int MaxReadSize = 131072;

		// Token: 0x0400144B RID: 5195
		private bool m_InOpaqueMode;

		// Token: 0x02000750 RID: 1872
		private class HttpRequestStreamAsyncResult : LazyAsyncResult
		{
			// Token: 0x060041D0 RID: 16848 RVA: 0x00111568 File Offset: 0x0010F768
			internal HttpRequestStreamAsyncResult(object asyncObject, object userState, AsyncCallback callback)
				: base(asyncObject, userState, callback)
			{
			}

			// Token: 0x060041D1 RID: 16849 RVA: 0x00111573 File Offset: 0x0010F773
			internal HttpRequestStreamAsyncResult(object asyncObject, object userState, AsyncCallback callback, uint dataAlreadyRead)
				: base(asyncObject, userState, callback)
			{
				this.m_dataAlreadyRead = dataAlreadyRead;
			}

			// Token: 0x060041D2 RID: 16850 RVA: 0x00111588 File Offset: 0x0010F788
			internal unsafe HttpRequestStreamAsyncResult(object asyncObject, object userState, AsyncCallback callback, byte[] buffer, int offset, uint size, uint dataAlreadyRead)
				: base(asyncObject, userState, callback)
			{
				this.m_dataAlreadyRead = dataAlreadyRead;
				this.m_pOverlapped = new Overlapped
				{
					AsyncResult = this
				}.Pack(HttpRequestStream.HttpRequestStreamAsyncResult.s_IOCallback, buffer);
				this.m_pPinnedBuffer = (void*)Marshal.UnsafeAddrOfPinnedArrayElement(buffer, offset);
			}

			// Token: 0x060041D3 RID: 16851 RVA: 0x001115DA File Offset: 0x0010F7DA
			internal void IOCompleted(uint errorCode, uint numBytes)
			{
				HttpRequestStream.HttpRequestStreamAsyncResult.IOCompleted(this, errorCode, numBytes);
			}

			// Token: 0x060041D4 RID: 16852 RVA: 0x001115E4 File Offset: 0x0010F7E4
			private static void IOCompleted(HttpRequestStream.HttpRequestStreamAsyncResult asyncResult, uint errorCode, uint numBytes)
			{
				object obj = null;
				try
				{
					if (errorCode != 0U && errorCode != 38U)
					{
						asyncResult.ErrorCode = (int)errorCode;
						obj = new HttpListenerException((int)errorCode);
					}
					else
					{
						obj = numBytes;
						if (Logging.On)
						{
							Logging.Dump(Logging.HttpListener, asyncResult, "Callback", (IntPtr)asyncResult.m_pPinnedBuffer, (int)numBytes);
						}
					}
				}
				catch (Exception ex)
				{
					obj = ex;
				}
				asyncResult.InvokeCallback(obj);
			}

			// Token: 0x060041D5 RID: 16853 RVA: 0x00111654 File Offset: 0x0010F854
			private unsafe static void Callback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
			{
				Overlapped overlapped = Overlapped.Unpack(nativeOverlapped);
				HttpRequestStream.HttpRequestStreamAsyncResult httpRequestStreamAsyncResult = overlapped.AsyncResult as HttpRequestStream.HttpRequestStreamAsyncResult;
				HttpRequestStream.HttpRequestStreamAsyncResult.IOCompleted(httpRequestStreamAsyncResult, errorCode, numBytes);
			}

			// Token: 0x060041D6 RID: 16854 RVA: 0x0011167C File Offset: 0x0010F87C
			protected override void Cleanup()
			{
				base.Cleanup();
				if (this.m_pOverlapped != null)
				{
					Overlapped.Free(this.m_pOverlapped);
				}
			}

			// Token: 0x040031E6 RID: 12774
			internal unsafe NativeOverlapped* m_pOverlapped;

			// Token: 0x040031E7 RID: 12775
			internal unsafe void* m_pPinnedBuffer;

			// Token: 0x040031E8 RID: 12776
			internal uint m_dataAlreadyRead;

			// Token: 0x040031E9 RID: 12777
			private static readonly IOCompletionCallback s_IOCallback = new IOCompletionCallback(HttpRequestStream.HttpRequestStreamAsyncResult.Callback);
		}
	}
}
