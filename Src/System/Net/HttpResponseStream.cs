using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020001BF RID: 447
	internal class HttpResponseStream : Stream
	{
		// Token: 0x06001182 RID: 4482 RVA: 0x0005F1A3 File Offset: 0x0005D3A3
		internal HttpResponseStream(HttpListenerContext httpContext)
		{
			this.m_HttpContext = httpContext;
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0005F1C4 File Offset: 0x0005D3C4
		internal UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS ComputeLeftToWrite()
		{
			UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS http_FLAGS = UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.NONE;
			if (!this.m_HttpContext.Response.ComputedHeaders)
			{
				http_FLAGS = this.m_HttpContext.Response.ComputeHeaders();
			}
			if (this.m_LeftToWrite == -9223372036854775808L)
			{
				UnsafeNclNativeMethods.HttpApi.HTTP_VERB knownMethod = this.m_HttpContext.GetKnownMethod();
				this.m_LeftToWrite = ((knownMethod != UnsafeNclNativeMethods.HttpApi.HTTP_VERB.HttpVerbHEAD) ? this.m_HttpContext.Response.ContentLength64 : 0L);
			}
			return http_FLAGS;
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x0005F232 File Offset: 0x0005D432
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x0005F235 File Offset: 0x0005D435
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x0005F238 File Offset: 0x0005D438
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x0005F23B File Offset: 0x0005D43B
		internal bool Closed
		{
			get
			{
				return this.m_Closed;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x0005F243 File Offset: 0x0005D443
		internal HttpListenerContext InternalHttpContext
		{
			get
			{
				return this.m_HttpContext;
			}
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0005F24B File Offset: 0x0005D44B
		internal void SetClosedFlag()
		{
			this.m_Closed = true;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0005F254 File Offset: 0x0005D454
		public override void Flush()
		{
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0005F256 File Offset: 0x0005D456
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x0005F25D File Offset: 0x0005D45D
		public override long Length
		{
			get
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x0005F26E File Offset: 0x0005D46E
		// (set) Token: 0x0600118E RID: 4494 RVA: 0x0005F27F File Offset: 0x0005D47F
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

		// Token: 0x0600118F RID: 4495 RVA: 0x0005F290 File Offset: 0x0005D490
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0005F2A1 File Offset: 0x0005D4A1
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0005F2B2 File Offset: 0x0005D4B2
		public override int Read([In] [Out] byte[] buffer, int offset, int size)
		{
			throw new InvalidOperationException(SR.GetString("net_writeonlystream"));
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x0005F2C3 File Offset: 0x0005D4C3
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			throw new InvalidOperationException(SR.GetString("net_writeonlystream"));
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0005F2D4 File Offset: 0x0005D4D4
		public override int EndRead(IAsyncResult asyncResult)
		{
			throw new InvalidOperationException(SR.GetString("net_writeonlystream"));
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0005F2E8 File Offset: 0x0005D4E8
		public unsafe override void Write(byte[] buffer, int offset, int size)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "Write", "");
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
			UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS http_FLAGS = this.ComputeLeftToWrite();
			if (this.m_Closed || (size == 0 && this.m_LeftToWrite != 0L))
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "Write", "");
				}
				return;
			}
			if (this.m_LeftToWrite >= 0L && (long)size > this.m_LeftToWrite)
			{
				throw new ProtocolViolationException(SR.GetString("net_entitytoobig"));
			}
			uint num = (uint)size;
			SafeLocalFree safeLocalFree = null;
			IntPtr intPtr = IntPtr.Zero;
			bool sentHeaders = this.m_HttpContext.Response.SentHeaders;
			uint num2;
			try
			{
				if (size == 0)
				{
					num2 = this.m_HttpContext.Response.SendHeaders(null, null, http_FLAGS, false);
				}
				else
				{
					try
					{
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
							byte* ptr2 = ptr;
							if (this.m_HttpContext.Response.BoundaryType == BoundaryType.Chunked)
							{
								string text = size.ToString("x", CultureInfo.InvariantCulture);
								num += (uint)(text.Length + 4);
								safeLocalFree = SafeLocalFree.LocalAlloc((int)num);
								intPtr = safeLocalFree.DangerousGetHandle();
								for (int i = 0; i < text.Length; i++)
								{
									Marshal.WriteByte(intPtr, i, (byte)text[i]);
								}
								Marshal.WriteInt16(intPtr, text.Length, 2573);
								Marshal.Copy(buffer, offset, IntPtrHelper.Add(intPtr, text.Length + 2), size);
								Marshal.WriteInt16(intPtr, (int)(num - 2U), 2573);
								ptr2 = (byte*)(void*)intPtr;
								offset = 0;
							}
							UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK http_DATA_CHUNK = default(UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK);
							http_DATA_CHUNK.DataChunkType = UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK_TYPE.HttpDataChunkFromMemory;
							http_DATA_CHUNK.pBuffer = ptr2 + offset;
							http_DATA_CHUNK.BufferLength = num;
							http_FLAGS |= ((this.m_LeftToWrite == (long)size) ? UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.NONE : UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_SEND_RESPONSE_FLAG_MORE_DATA);
							if (!sentHeaders)
							{
								num2 = this.m_HttpContext.Response.SendHeaders(&http_DATA_CHUNK, null, http_FLAGS, false);
							}
							else
							{
								num2 = UnsafeNclNativeMethods.HttpApi.HttpSendResponseEntityBody(this.m_HttpContext.RequestQueueHandle, this.m_HttpContext.RequestId, (uint)http_FLAGS, 1, &http_DATA_CHUNK, null, SafeLocalFree.Zero, 0U, null, null);
								if (this.m_HttpContext.Listener.IgnoreWriteExceptions)
								{
									num2 = 0U;
								}
							}
						}
					}
					finally
					{
						byte[] array = null;
					}
				}
			}
			finally
			{
				if (safeLocalFree != null)
				{
					safeLocalFree.Close();
				}
			}
			if (num2 != 0U && num2 != 38U)
			{
				Exception ex = new HttpListenerException((int)num2);
				if (Logging.On)
				{
					Logging.Exception(Logging.HttpListener, this, "Write", ex);
				}
				this.m_Closed = true;
				this.m_HttpContext.Abort();
				throw ex;
			}
			this.UpdateAfterWrite(num);
			if (Logging.On)
			{
				Logging.Dump(Logging.HttpListener, this, "Write", buffer, offset, (int)num);
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "Write", "");
			}
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0005F608 File Offset: 0x0005D808
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public unsafe override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
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
			UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS http_FLAGS = this.ComputeLeftToWrite();
			if (this.m_Closed || (size == 0 && this.m_LeftToWrite != 0L))
			{
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "BeginWrite", "");
				}
				HttpResponseStreamAsyncResult httpResponseStreamAsyncResult = new HttpResponseStreamAsyncResult(this, state, callback);
				httpResponseStreamAsyncResult.InvokeCallback(0U);
				return httpResponseStreamAsyncResult;
			}
			if (this.m_LeftToWrite >= 0L && (long)size > this.m_LeftToWrite)
			{
				throw new ProtocolViolationException(SR.GetString("net_entitytoobig"));
			}
			uint num = 0U;
			http_FLAGS |= ((this.m_LeftToWrite == (long)size) ? UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.NONE : UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_SEND_RESPONSE_FLAG_MORE_DATA);
			bool sentHeaders = this.m_HttpContext.Response.SentHeaders;
			HttpResponseStreamAsyncResult httpResponseStreamAsyncResult2 = new HttpResponseStreamAsyncResult(this, state, callback, buffer, offset, size, this.m_HttpContext.Response.BoundaryType == BoundaryType.Chunked, sentHeaders);
			this.UpdateAfterWrite((uint)((this.m_HttpContext.Response.BoundaryType == BoundaryType.Chunked) ? 0 : size));
			uint num2;
			try
			{
				if (!sentHeaders)
				{
					num2 = this.m_HttpContext.Response.SendHeaders(null, httpResponseStreamAsyncResult2, http_FLAGS, false);
				}
				else
				{
					this.m_HttpContext.EnsureBoundHandle();
					num2 = UnsafeNclNativeMethods.HttpApi.HttpSendResponseEntityBody(this.m_HttpContext.RequestQueueHandle, this.m_HttpContext.RequestId, (uint)http_FLAGS, httpResponseStreamAsyncResult2.dataChunkCount, httpResponseStreamAsyncResult2.pDataChunks, &num, SafeLocalFree.Zero, 0U, httpResponseStreamAsyncResult2.m_pOverlapped, null);
				}
			}
			catch (Exception ex)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.HttpListener, this, "BeginWrite", ex);
				}
				httpResponseStreamAsyncResult2.InternalCleanup();
				this.m_Closed = true;
				this.m_HttpContext.Abort();
				throw;
			}
			if (num2 != 0U && num2 != 997U)
			{
				httpResponseStreamAsyncResult2.InternalCleanup();
				if (!this.m_HttpContext.Listener.IgnoreWriteExceptions || !sentHeaders)
				{
					Exception ex2 = new HttpListenerException((int)num2);
					if (Logging.On)
					{
						Logging.Exception(Logging.HttpListener, this, "BeginWrite", ex2);
					}
					this.m_Closed = true;
					this.m_HttpContext.Abort();
					throw ex2;
				}
			}
			if (num2 == 0U && HttpListener.SkipIOCPCallbackOnSuccess)
			{
				httpResponseStreamAsyncResult2.IOCompleted(num2, num);
			}
			if ((http_FLAGS & UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_SEND_RESPONSE_FLAG_MORE_DATA) == UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.NONE)
			{
				this.m_LastWrite = httpResponseStreamAsyncResult2;
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "BeginWrite", "");
			}
			return httpResponseStreamAsyncResult2;
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0005F870 File Offset: 0x0005DA70
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "EndWrite", "");
			}
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			HttpResponseStreamAsyncResult httpResponseStreamAsyncResult = asyncResult as HttpResponseStreamAsyncResult;
			if (httpResponseStreamAsyncResult == null || httpResponseStreamAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			if (httpResponseStreamAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[] { "EndWrite" }));
			}
			httpResponseStreamAsyncResult.EndCalled = true;
			object obj = httpResponseStreamAsyncResult.InternalWaitForCompletion();
			Exception ex = obj as Exception;
			if (ex != null)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.HttpListener, this, "EndWrite", ex);
				}
				this.m_Closed = true;
				this.m_HttpContext.Abort();
				throw ex;
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "EndWrite", "");
			}
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0005F955 File Offset: 0x0005DB55
		private void UpdateAfterWrite(uint dataWritten)
		{
			if (!this.m_InOpaqueMode)
			{
				if (this.m_LeftToWrite > 0L)
				{
					this.m_LeftToWrite -= (long)((ulong)dataWritten);
				}
				if (this.m_LeftToWrite == 0L)
				{
					this.m_Closed = true;
				}
			}
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0005F988 File Offset: 0x0005DB88
		protected unsafe override void Dispose(bool disposing)
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "Close", "");
			}
			try
			{
				if (disposing)
				{
					if (this.m_Closed)
					{
						if (Logging.On)
						{
							Logging.Exit(Logging.HttpListener, this, "Close", "");
						}
						return;
					}
					this.m_Closed = true;
					UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS http_FLAGS = this.ComputeLeftToWrite();
					if (this.m_LeftToWrite > 0L && !this.m_InOpaqueMode)
					{
						throw new InvalidOperationException(SR.GetString("net_io_notenoughbyteswritten"));
					}
					bool sentHeaders = this.m_HttpContext.Response.SentHeaders;
					if (sentHeaders && this.m_LeftToWrite == 0L)
					{
						if (Logging.On)
						{
							Logging.Exit(Logging.HttpListener, this, "Close", "");
						}
						return;
					}
					uint num = 0U;
					if ((this.m_HttpContext.Response.BoundaryType == BoundaryType.Chunked || this.m_HttpContext.Response.BoundaryType == BoundaryType.None) && string.Compare(this.m_HttpContext.Request.HttpMethod, "HEAD", StringComparison.OrdinalIgnoreCase) != 0)
					{
						if (this.m_HttpContext.Response.BoundaryType == BoundaryType.None)
						{
							http_FLAGS |= UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_RECEIVE_REQUEST_FLAG_COPY_BODY;
						}
						try
						{
							byte[] array;
							void* ptr;
							if ((array = NclConstants.ChunkTerminator) == null || array.Length == 0)
							{
								ptr = null;
							}
							else
							{
								ptr = (void*)(&array[0]);
							}
							UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK* ptr2 = null;
							if (this.m_HttpContext.Response.BoundaryType == BoundaryType.Chunked)
							{
								UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK http_DATA_CHUNK = default(UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK);
								http_DATA_CHUNK.DataChunkType = UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK_TYPE.HttpDataChunkFromMemory;
								http_DATA_CHUNK.pBuffer = (byte*)ptr;
								http_DATA_CHUNK.BufferLength = (uint)NclConstants.ChunkTerminator.Length;
								ptr2 = &http_DATA_CHUNK;
							}
							if (!sentHeaders)
							{
								num = this.m_HttpContext.Response.SendHeaders(ptr2, null, http_FLAGS, false);
								goto IL_200;
							}
							num = UnsafeNclNativeMethods.HttpApi.HttpSendResponseEntityBody(this.m_HttpContext.RequestQueueHandle, this.m_HttpContext.RequestId, (uint)http_FLAGS, (ptr2 != null) ? 1 : 0, ptr2, null, SafeLocalFree.Zero, 0U, null, null);
							if (this.m_HttpContext.Listener.IgnoreWriteExceptions)
							{
								num = 0U;
							}
							goto IL_200;
						}
						finally
						{
							byte[] array = null;
						}
					}
					if (!sentHeaders)
					{
						num = this.m_HttpContext.Response.SendHeaders(null, null, http_FLAGS, false);
					}
					IL_200:
					if (num != 0U && num != 38U)
					{
						Exception ex = new HttpListenerException((int)num);
						if (Logging.On)
						{
							Logging.Exception(Logging.HttpListener, this, "Close", ex);
						}
						this.m_HttpContext.Abort();
						throw ex;
					}
					this.m_LeftToWrite = 0L;
				}
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

		// Token: 0x06001199 RID: 4505 RVA: 0x0005FC30 File Offset: 0x0005DE30
		internal void SwitchToOpaqueMode()
		{
			this.m_InOpaqueMode = true;
			this.m_LeftToWrite = long.MaxValue;
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0005FC48 File Offset: 0x0005DE48
		internal void CancelLastWrite(CriticalHandle requestQueueHandle)
		{
			HttpResponseStreamAsyncResult lastWrite = this.m_LastWrite;
			if (lastWrite != null && !lastWrite.IsCompleted)
			{
				UnsafeNclNativeMethods.CancelIoEx(requestQueueHandle, lastWrite.m_pOverlapped);
			}
		}

		// Token: 0x0400144C RID: 5196
		private HttpListenerContext m_HttpContext;

		// Token: 0x0400144D RID: 5197
		private long m_LeftToWrite = long.MinValue;

		// Token: 0x0400144E RID: 5198
		private bool m_Closed;

		// Token: 0x0400144F RID: 5199
		private bool m_InOpaqueMode;

		// Token: 0x04001450 RID: 5200
		private HttpResponseStreamAsyncResult m_LastWrite;
	}
}
