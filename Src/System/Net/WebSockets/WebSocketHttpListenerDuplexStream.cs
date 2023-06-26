using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	// Token: 0x02000238 RID: 568
	internal class WebSocketHttpListenerDuplexStream : Stream, WebSocketBase.IWebSocketStream
	{
		// Token: 0x06001553 RID: 5459 RVA: 0x0006F1EB File Offset: 0x0006D3EB
		public WebSocketHttpListenerDuplexStream(HttpRequestStream inputStream, HttpResponseStream outputStream, HttpListenerContext context)
		{
			this.m_InputStream = inputStream;
			this.m_OutputStream = outputStream;
			this.m_Context = context;
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Associate(Logging.WebSockets, inputStream, this);
				Logging.Associate(Logging.WebSockets, outputStream, this);
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x0006F227 File Offset: 0x0006D427
		public override bool CanRead
		{
			get
			{
				return this.m_InputStream.CanRead;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x0006F234 File Offset: 0x0006D434
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x0006F237 File Offset: 0x0006D437
		public override bool CanTimeout
		{
			get
			{
				return this.m_InputStream.CanTimeout && this.m_OutputStream.CanTimeout;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x0006F253 File Offset: 0x0006D453
		public override bool CanWrite
		{
			get
			{
				return this.m_OutputStream.CanWrite;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x0006F260 File Offset: 0x0006D460
		public override long Length
		{
			get
			{
				throw new NotSupportedException(SR.GetString("net_noseek"));
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x0006F271 File Offset: 0x0006D471
		// (set) Token: 0x0600155A RID: 5466 RVA: 0x0006F282 File Offset: 0x0006D482
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

		// Token: 0x0600155B RID: 5467 RVA: 0x0006F293 File Offset: 0x0006D493
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.m_InputStream.Read(buffer, offset, count);
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0006F2A3 File Offset: 0x0006D4A3
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			WebSocketHelpers.ValidateBuffer(buffer, offset, count);
			return this.ReadAsyncCore(buffer, offset, count, cancellationToken);
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0006F2B8 File Offset: 0x0006D4B8
		private async Task<int> ReadAsyncCore(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, this, "ReadAsyncCore", WebSocketHelpers.GetTraceMsgForParameters(offset, count, cancellationToken));
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			int bytesRead = 0;
			try
			{
				if (cancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration = cancellationToken.Register(WebSocketHttpListenerDuplexStream.s_OnCancel, this, false);
				}
				if (!this.m_InOpaqueMode)
				{
					int num = await this.m_InputStream.ReadAsync(buffer, offset, count, cancellationToken).SuppressContextFlow<int>();
					bytesRead = num;
				}
				else
				{
					this.m_ReadTaskCompletionSource = new TaskCompletionSource<int>();
					this.m_ReadEventArgs.SetBuffer(buffer, offset, count);
					if (!this.ReadAsyncFast(this.m_ReadEventArgs))
					{
						if (this.m_ReadEventArgs.Exception != null)
						{
							throw this.m_ReadEventArgs.Exception;
						}
						bytesRead = this.m_ReadEventArgs.BytesTransferred;
					}
					else
					{
						bytesRead = await this.m_ReadTaskCompletionSource.Task.SuppressContextFlow<int>();
					}
				}
			}
			catch (Exception ex)
			{
				if (WebSocketHttpListenerDuplexStream.s_CanHandleException(ex))
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
				throw;
			}
			finally
			{
				cancellationTokenRegistration.Dispose();
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "ReadAsyncCore", bytesRead);
				}
			}
			return bytesRead;
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0006F31C File Offset: 0x0006D51C
		private unsafe bool ReadAsyncFast(WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs eventArgs)
		{
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, this, "ReadAsyncFast", string.Empty);
			}
			eventArgs.StartOperationCommon(this);
			eventArgs.StartOperationReceive();
			bool flag = false;
			try
			{
				if (eventArgs.Count == 0 || this.m_InputStream.Closed)
				{
					eventArgs.FinishOperationSuccess(0, true);
					return false;
				}
				uint num = 0U;
				int num2 = eventArgs.Offset;
				int num3 = eventArgs.Count;
				if (this.m_InputStream.BufferedDataChunksAvailable)
				{
					num = this.m_InputStream.GetChunks(eventArgs.Buffer, eventArgs.Offset, eventArgs.Count);
					if (this.m_InputStream.BufferedDataChunksAvailable && (ulong)num == (ulong)((long)eventArgs.Count))
					{
						eventArgs.FinishOperationSuccess(eventArgs.Count, true);
						return false;
					}
				}
				if (num != 0U)
				{
					num2 += (int)num;
					num3 -= (int)num;
					if (num3 > 131072)
					{
						num3 = 131072;
					}
					eventArgs.SetBuffer(eventArgs.Buffer, num2, num3);
				}
				else if (num3 > 131072)
				{
					num3 = 131072;
					eventArgs.SetBuffer(eventArgs.Buffer, num2, num3);
				}
				this.m_InputStream.InternalHttpContext.EnsureBoundHandle();
				uint num4 = 0U;
				uint num5 = 0U;
				uint num6 = UnsafeNclNativeMethods.HttpApi.HttpReceiveRequestEntityBody2(this.m_InputStream.InternalHttpContext.RequestQueueHandle, this.m_InputStream.InternalHttpContext.RequestId, num4, (void*)this.m_WebSocket.InternalBuffer.ToIntPtr(eventArgs.Offset), (uint)eventArgs.Count, out num5, eventArgs.NativeOverlapped);
				if (num6 != 0U && num6 != 997U && num6 != 38U)
				{
					throw new HttpListenerException((int)num6);
				}
				if (num6 == 0U && HttpListener.SkipIOCPCallbackOnSuccess)
				{
					eventArgs.FinishOperationSuccess((int)num5, true);
					flag = false;
				}
				else if (num6 == 38U)
				{
					eventArgs.FinishOperationSuccess(0, true);
					flag = false;
				}
				else
				{
					flag = true;
				}
			}
			catch (Exception ex)
			{
				this.m_ReadEventArgs.FinishOperationFailure(ex, true);
				this.m_OutputStream.SetClosedFlag();
				this.m_OutputStream.InternalHttpContext.Abort();
				throw;
			}
			finally
			{
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "ReadAsyncFast", flag);
				}
			}
			return flag;
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x0006F560 File Offset: 0x0006D760
		public override int ReadByte()
		{
			return this.m_InputStream.ReadByte();
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x0006F56D File Offset: 0x0006D76D
		public bool SupportsMultipleWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x0006F570 File Offset: 0x0006D770
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.m_InputStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0006F584 File Offset: 0x0006D784
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.m_InputStream.EndRead(asyncResult);
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0006F594 File Offset: 0x0006D794
		public Task MultipleWriteAsync(IList<ArraySegment<byte>> sendBuffers, CancellationToken cancellationToken)
		{
			if (sendBuffers.Count == 1)
			{
				ArraySegment<byte> arraySegment = sendBuffers[0];
				return this.WriteAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count, cancellationToken);
			}
			return this.MultipleWriteAsyncCore(sendBuffers, cancellationToken);
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0006F5D8 File Offset: 0x0006D7D8
		private async Task MultipleWriteAsyncCore(IList<ArraySegment<byte>> sendBuffers, CancellationToken cancellationToken)
		{
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, this, "MultipleWriteAsyncCore", string.Empty);
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			try
			{
				if (cancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration = cancellationToken.Register(WebSocketHttpListenerDuplexStream.s_OnCancel, this, false);
				}
				this.m_WriteTaskCompletionSource = new TaskCompletionSource<object>();
				this.m_WriteEventArgs.SetBuffer(null, 0, 0);
				this.m_WriteEventArgs.BufferList = sendBuffers;
				if (this.WriteAsyncFast(this.m_WriteEventArgs))
				{
					await this.m_WriteTaskCompletionSource.Task.SuppressContextFlow<object>();
				}
			}
			catch (Exception ex)
			{
				if (WebSocketHttpListenerDuplexStream.s_CanHandleException(ex))
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
				throw;
			}
			finally
			{
				cancellationTokenRegistration.Dispose();
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "MultipleWriteAsyncCore", string.Empty);
				}
			}
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x0006F62B File Offset: 0x0006D82B
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.m_OutputStream.Write(buffer, offset, count);
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0006F63B File Offset: 0x0006D83B
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			WebSocketHelpers.ValidateBuffer(buffer, offset, count);
			return this.WriteAsyncCore(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0006F650 File Offset: 0x0006D850
		private async Task WriteAsyncCore(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, this, "WriteAsyncCore", WebSocketHelpers.GetTraceMsgForParameters(offset, count, cancellationToken));
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			try
			{
				if (cancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration = cancellationToken.Register(WebSocketHttpListenerDuplexStream.s_OnCancel, this, false);
				}
				if (!this.m_InOpaqueMode)
				{
					await this.m_OutputStream.WriteAsync(buffer, offset, count, cancellationToken).SuppressContextFlow();
				}
				else
				{
					this.m_WriteTaskCompletionSource = new TaskCompletionSource<object>();
					this.m_WriteEventArgs.BufferList = null;
					this.m_WriteEventArgs.SetBuffer(buffer, offset, count);
					if (this.WriteAsyncFast(this.m_WriteEventArgs))
					{
						await this.m_WriteTaskCompletionSource.Task.SuppressContextFlow<object>();
					}
				}
			}
			catch (Exception ex)
			{
				if (WebSocketHttpListenerDuplexStream.s_CanHandleException(ex))
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
				throw;
			}
			finally
			{
				cancellationTokenRegistration.Dispose();
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "WriteAsyncCore", string.Empty);
				}
			}
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0006F6B4 File Offset: 0x0006D8B4
		private bool WriteAsyncFast(WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs eventArgs)
		{
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, this, "WriteAsyncFast", string.Empty);
			}
			UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS http_FLAGS = UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.NONE;
			eventArgs.StartOperationCommon(this);
			eventArgs.StartOperationSend();
			bool flag = false;
			try
			{
				if (this.m_OutputStream.Closed || (eventArgs.Buffer != null && eventArgs.Count == 0))
				{
					eventArgs.FinishOperationSuccess(eventArgs.Count, true);
					return false;
				}
				if (eventArgs.ShouldCloseOutput)
				{
					http_FLAGS |= UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_RECEIVE_REQUEST_FLAG_COPY_BODY;
				}
				else
				{
					http_FLAGS |= UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_SEND_RESPONSE_FLAG_MORE_DATA;
					http_FLAGS |= UnsafeNclNativeMethods.HttpApi.HTTP_FLAGS.HTTP_SEND_RESPONSE_FLAG_BUFFER_DATA;
				}
				this.m_OutputStream.InternalHttpContext.EnsureBoundHandle();
				uint num2;
				uint num = UnsafeNclNativeMethods.HttpApi.HttpSendResponseEntityBody2(this.m_OutputStream.InternalHttpContext.RequestQueueHandle, this.m_OutputStream.InternalHttpContext.RequestId, (uint)http_FLAGS, eventArgs.EntityChunkCount, eventArgs.EntityChunks, out num2, SafeLocalFree.Zero, 0U, eventArgs.NativeOverlapped, IntPtr.Zero);
				if (num != 0U && num != 997U)
				{
					throw new HttpListenerException((int)num);
				}
				if (num == 0U && HttpListener.SkipIOCPCallbackOnSuccess)
				{
					eventArgs.FinishOperationSuccess((int)num2, true);
					flag = false;
				}
				else
				{
					flag = true;
				}
			}
			catch (Exception ex)
			{
				this.m_WriteEventArgs.FinishOperationFailure(ex, true);
				this.m_OutputStream.SetClosedFlag();
				this.m_OutputStream.InternalHttpContext.Abort();
				throw;
			}
			finally
			{
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "WriteAsyncFast", flag);
				}
			}
			return flag;
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0006F824 File Offset: 0x0006DA24
		public override void WriteByte(byte value)
		{
			this.m_OutputStream.WriteByte(value);
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0006F832 File Offset: 0x0006DA32
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.m_OutputStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x0006F846 File Offset: 0x0006DA46
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.m_OutputStream.EndWrite(asyncResult);
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0006F854 File Offset: 0x0006DA54
		public override void Flush()
		{
			this.m_OutputStream.Flush();
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0006F861 File Offset: 0x0006DA61
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this.m_OutputStream.FlushAsync(cancellationToken);
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0006F86F File Offset: 0x0006DA6F
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0006F880 File Offset: 0x0006DA80
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_noseek"));
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0006F894 File Offset: 0x0006DA94
		public async Task CloseNetworkConnectionAsync(CancellationToken cancellationToken)
		{
			await Task.Yield();
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, this, "CloseNetworkConnectionAsync", string.Empty);
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			try
			{
				if (cancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration = cancellationToken.Register(WebSocketHttpListenerDuplexStream.s_OnCancel, this, false);
				}
				this.m_WriteTaskCompletionSource = new TaskCompletionSource<object>();
				this.m_WriteEventArgs.SetShouldCloseOutput();
				if (this.WriteAsyncFast(this.m_WriteEventArgs))
				{
					await this.m_WriteTaskCompletionSource.Task.SuppressContextFlow<object>();
				}
			}
			catch (Exception ex)
			{
				if (!WebSocketHttpListenerDuplexStream.s_CanHandleException(ex))
				{
					throw;
				}
				cancellationToken.ThrowIfCancellationRequested();
			}
			finally
			{
				cancellationTokenRegistration.Dispose();
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "CloseNetworkConnectionAsync", string.Empty);
				}
			}
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0006F8E0 File Offset: 0x0006DAE0
		protected override void Dispose(bool disposing)
		{
			if (disposing && Interlocked.Exchange(ref this.m_CleanedUp, 1) == 0)
			{
				if (this.m_ReadTaskCompletionSource != null)
				{
					this.m_ReadTaskCompletionSource.TrySetCanceled();
				}
				if (this.m_WriteTaskCompletionSource != null)
				{
					this.m_WriteTaskCompletionSource.TrySetCanceled();
				}
				if (this.m_ReadEventArgs != null)
				{
					this.m_ReadEventArgs.Dispose();
				}
				if (this.m_WriteEventArgs != null)
				{
					this.m_WriteEventArgs.Dispose();
				}
				try
				{
					this.m_InputStream.Close();
				}
				finally
				{
					this.m_OutputStream.Close();
				}
			}
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0006F978 File Offset: 0x0006DB78
		public void Abort()
		{
			WebSocketHttpListenerDuplexStream.OnCancel(this);
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0006F980 File Offset: 0x0006DB80
		private static bool CanHandleException(Exception error)
		{
			return error is HttpListenerException || error is ObjectDisposedException || error is IOException;
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0006F9A0 File Offset: 0x0006DBA0
		private static void OnCancel(object state)
		{
			WebSocketHttpListenerDuplexStream webSocketHttpListenerDuplexStream = state as WebSocketHttpListenerDuplexStream;
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, state, "OnCancel", string.Empty);
			}
			try
			{
				webSocketHttpListenerDuplexStream.m_OutputStream.SetClosedFlag();
				webSocketHttpListenerDuplexStream.m_Context.Abort();
			}
			catch
			{
			}
			TaskCompletionSource<int> readTaskCompletionSource = webSocketHttpListenerDuplexStream.m_ReadTaskCompletionSource;
			if (readTaskCompletionSource != null)
			{
				readTaskCompletionSource.TrySetCanceled();
			}
			TaskCompletionSource<object> writeTaskCompletionSource = webSocketHttpListenerDuplexStream.m_WriteTaskCompletionSource;
			if (writeTaskCompletionSource != null)
			{
				writeTaskCompletionSource.TrySetCanceled();
			}
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Exit(Logging.WebSockets, state, "OnCancel", string.Empty);
			}
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0006FA3C File Offset: 0x0006DC3C
		public void SwitchToOpaqueMode(WebSocketBase webSocket)
		{
			if (this.m_InOpaqueMode)
			{
				throw new InvalidOperationException();
			}
			this.m_WebSocket = webSocket;
			this.m_InOpaqueMode = true;
			this.m_ReadEventArgs = new WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs(webSocket, this);
			this.m_ReadEventArgs.Completed += WebSocketHttpListenerDuplexStream.s_OnReadCompleted;
			this.m_WriteEventArgs = new WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs(webSocket, this);
			this.m_WriteEventArgs.Completed += WebSocketHttpListenerDuplexStream.s_OnWriteCompleted;
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Associate(Logging.WebSockets, this, webSocket);
			}
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0006FAB4 File Offset: 0x0006DCB4
		private static void OnWriteCompleted(object sender, WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs eventArgs)
		{
			WebSocketHttpListenerDuplexStream currentStream = eventArgs.CurrentStream;
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, currentStream, "OnWriteCompleted", string.Empty);
			}
			if (eventArgs.Exception != null)
			{
				currentStream.m_WriteTaskCompletionSource.TrySetException(eventArgs.Exception);
			}
			else
			{
				currentStream.m_WriteTaskCompletionSource.TrySetResult(null);
			}
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Exit(Logging.WebSockets, currentStream, "OnWriteCompleted", string.Empty);
			}
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0006FB2C File Offset: 0x0006DD2C
		private static void OnReadCompleted(object sender, WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs eventArgs)
		{
			WebSocketHttpListenerDuplexStream currentStream = eventArgs.CurrentStream;
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, currentStream, "OnReadCompleted", string.Empty);
			}
			if (eventArgs.Exception != null)
			{
				currentStream.m_ReadTaskCompletionSource.TrySetException(eventArgs.Exception);
			}
			else
			{
				currentStream.m_ReadTaskCompletionSource.TrySetResult(eventArgs.BytesTransferred);
			}
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Exit(Logging.WebSockets, currentStream, "OnReadCompleted", string.Empty);
			}
		}

		// Token: 0x040016A3 RID: 5795
		private static readonly EventHandler<WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs> s_OnReadCompleted = new EventHandler<WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs>(WebSocketHttpListenerDuplexStream.OnReadCompleted);

		// Token: 0x040016A4 RID: 5796
		private static readonly EventHandler<WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs> s_OnWriteCompleted = new EventHandler<WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs>(WebSocketHttpListenerDuplexStream.OnWriteCompleted);

		// Token: 0x040016A5 RID: 5797
		private static readonly Func<Exception, bool> s_CanHandleException = new Func<Exception, bool>(WebSocketHttpListenerDuplexStream.CanHandleException);

		// Token: 0x040016A6 RID: 5798
		private static readonly Action<object> s_OnCancel = new Action<object>(WebSocketHttpListenerDuplexStream.OnCancel);

		// Token: 0x040016A7 RID: 5799
		private readonly HttpRequestStream m_InputStream;

		// Token: 0x040016A8 RID: 5800
		private readonly HttpResponseStream m_OutputStream;

		// Token: 0x040016A9 RID: 5801
		private HttpListenerContext m_Context;

		// Token: 0x040016AA RID: 5802
		private bool m_InOpaqueMode;

		// Token: 0x040016AB RID: 5803
		private WebSocketBase m_WebSocket;

		// Token: 0x040016AC RID: 5804
		private WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs m_WriteEventArgs;

		// Token: 0x040016AD RID: 5805
		private WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs m_ReadEventArgs;

		// Token: 0x040016AE RID: 5806
		private TaskCompletionSource<object> m_WriteTaskCompletionSource;

		// Token: 0x040016AF RID: 5807
		private TaskCompletionSource<int> m_ReadTaskCompletionSource;

		// Token: 0x040016B0 RID: 5808
		private int m_CleanedUp;

		// Token: 0x02000781 RID: 1921
		internal class HttpListenerAsyncEventArgs : EventArgs, IDisposable
		{
			// Token: 0x14000072 RID: 114
			// (add) Token: 0x06004288 RID: 17032 RVA: 0x00116280 File Offset: 0x00114480
			// (remove) Token: 0x06004289 RID: 17033 RVA: 0x001162B8 File Offset: 0x001144B8
			private event EventHandler<WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs> m_Completed;

			// Token: 0x0600428A RID: 17034 RVA: 0x001162ED File Offset: 0x001144ED
			public HttpListenerAsyncEventArgs(WebSocketBase webSocket, WebSocketHttpListenerDuplexStream stream)
			{
				this.m_WebSocket = webSocket;
				this.m_CurrentStream = stream;
				this.m_AllocateOverlappedOnDemand = LocalAppContextSwitches.AllocateOverlappedOnDemand;
				if (!this.m_AllocateOverlappedOnDemand)
				{
					this.InitializeOverlapped();
				}
			}

			// Token: 0x17000F2C RID: 3884
			// (get) Token: 0x0600428B RID: 17035 RVA: 0x0011631C File Offset: 0x0011451C
			public int BytesTransferred
			{
				get
				{
					return this.m_BytesTransferred;
				}
			}

			// Token: 0x17000F2D RID: 3885
			// (get) Token: 0x0600428C RID: 17036 RVA: 0x00116324 File Offset: 0x00114524
			public byte[] Buffer
			{
				get
				{
					return this.m_Buffer;
				}
			}

			// Token: 0x17000F2E RID: 3886
			// (get) Token: 0x0600428D RID: 17037 RVA: 0x0011632C File Offset: 0x0011452C
			// (set) Token: 0x0600428E RID: 17038 RVA: 0x00116334 File Offset: 0x00114534
			public IList<ArraySegment<byte>> BufferList
			{
				get
				{
					return this.m_BufferList;
				}
				set
				{
					this.m_BufferList = value;
				}
			}

			// Token: 0x17000F2F RID: 3887
			// (get) Token: 0x0600428F RID: 17039 RVA: 0x0011633D File Offset: 0x0011453D
			public bool ShouldCloseOutput
			{
				get
				{
					return this.m_ShouldCloseOutput;
				}
			}

			// Token: 0x17000F30 RID: 3888
			// (get) Token: 0x06004290 RID: 17040 RVA: 0x00116345 File Offset: 0x00114545
			public int Offset
			{
				get
				{
					return this.m_Offset;
				}
			}

			// Token: 0x17000F31 RID: 3889
			// (get) Token: 0x06004291 RID: 17041 RVA: 0x0011634D File Offset: 0x0011454D
			public int Count
			{
				get
				{
					return this.m_Count;
				}
			}

			// Token: 0x17000F32 RID: 3890
			// (get) Token: 0x06004292 RID: 17042 RVA: 0x00116355 File Offset: 0x00114555
			public Exception Exception
			{
				get
				{
					return this.m_Exception;
				}
			}

			// Token: 0x17000F33 RID: 3891
			// (get) Token: 0x06004293 RID: 17043 RVA: 0x0011635D File Offset: 0x0011455D
			public ushort EntityChunkCount
			{
				get
				{
					if (this.m_DataChunks == null)
					{
						return 0;
					}
					return this.m_DataChunkCount;
				}
			}

			// Token: 0x17000F34 RID: 3892
			// (get) Token: 0x06004294 RID: 17044 RVA: 0x0011636F File Offset: 0x0011456F
			public SafeNativeOverlapped NativeOverlapped
			{
				get
				{
					return this.m_PtrNativeOverlapped;
				}
			}

			// Token: 0x17000F35 RID: 3893
			// (get) Token: 0x06004295 RID: 17045 RVA: 0x00116377 File Offset: 0x00114577
			public IntPtr EntityChunks
			{
				get
				{
					if (this.m_DataChunks == null)
					{
						return IntPtr.Zero;
					}
					return Marshal.UnsafeAddrOfPinnedArrayElement(this.m_DataChunks, 0);
				}
			}

			// Token: 0x17000F36 RID: 3894
			// (get) Token: 0x06004296 RID: 17046 RVA: 0x00116393 File Offset: 0x00114593
			public WebSocketHttpListenerDuplexStream CurrentStream
			{
				get
				{
					return this.m_CurrentStream;
				}
			}

			// Token: 0x14000073 RID: 115
			// (add) Token: 0x06004297 RID: 17047 RVA: 0x0011639B File Offset: 0x0011459B
			// (remove) Token: 0x06004298 RID: 17048 RVA: 0x001163A4 File Offset: 0x001145A4
			public event EventHandler<WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs> Completed
			{
				add
				{
					this.m_Completed += value;
				}
				remove
				{
					this.m_Completed -= value;
				}
			}

			// Token: 0x06004299 RID: 17049 RVA: 0x001163B0 File Offset: 0x001145B0
			protected virtual void OnCompleted(WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs e)
			{
				EventHandler<WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs> completed = this.m_Completed;
				if (completed != null)
				{
					completed(e.m_CurrentStream, e);
				}
			}

			// Token: 0x0600429A RID: 17050 RVA: 0x001163D4 File Offset: 0x001145D4
			public void SetShouldCloseOutput()
			{
				this.m_BufferList = null;
				this.m_Buffer = null;
				this.m_ShouldCloseOutput = true;
			}

			// Token: 0x0600429B RID: 17051 RVA: 0x001163EB File Offset: 0x001145EB
			public void Dispose()
			{
				this.m_DisposeCalled = true;
				if (Interlocked.CompareExchange(ref this.m_Operating, 2, 0) != 0)
				{
					return;
				}
				if (!this.m_AllocateOverlappedOnDemand)
				{
					this.FreeOverlapped(false);
				}
				GC.SuppressFinalize(this);
			}

			// Token: 0x0600429C RID: 17052 RVA: 0x0011641C File Offset: 0x0011461C
			~HttpListenerAsyncEventArgs()
			{
				if (!this.m_AllocateOverlappedOnDemand)
				{
					this.FreeOverlapped(true);
				}
			}

			// Token: 0x0600429D RID: 17053 RVA: 0x00116454 File Offset: 0x00114654
			private void InitializeOverlapped()
			{
				this.m_Overlapped = new Overlapped();
				this.m_PtrNativeOverlapped = new SafeNativeOverlapped(this.m_Overlapped.UnsafePack(new IOCompletionCallback(this.CompletionPortCallback), null));
			}

			// Token: 0x0600429E RID: 17054 RVA: 0x00116484 File Offset: 0x00114684
			private void FreeOverlapped(bool checkForShutdown)
			{
				if (!checkForShutdown || !NclUtilities.HasShutdownStarted)
				{
					if (this.m_PtrNativeOverlapped != null && !this.m_PtrNativeOverlapped.IsInvalid)
					{
						this.m_PtrNativeOverlapped.Dispose();
					}
					if (this.m_DataChunksGCHandle.IsAllocated)
					{
						this.m_DataChunksGCHandle.Free();
						if (this.m_AllocateOverlappedOnDemand)
						{
							this.m_DataChunks = null;
						}
					}
				}
			}

			// Token: 0x0600429F RID: 17055 RVA: 0x001164E4 File Offset: 0x001146E4
			internal void StartOperationCommon(WebSocketHttpListenerDuplexStream currentStream)
			{
				if (Interlocked.CompareExchange(ref this.m_Operating, 1, 0) == 0)
				{
					if (this.m_AllocateOverlappedOnDemand)
					{
						this.InitializeOverlapped();
					}
					else
					{
						this.NativeOverlapped.ReinitializeNativeOverlapped();
					}
					this.m_Exception = null;
					this.m_BytesTransferred = 0;
					return;
				}
				if (this.m_DisposeCalled)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				throw new InvalidOperationException();
			}

			// Token: 0x060042A0 RID: 17056 RVA: 0x00116548 File Offset: 0x00114748
			internal void StartOperationReceive()
			{
				this.m_CompletedOperation = WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs.HttpListenerAsyncOperation.Receive;
			}

			// Token: 0x060042A1 RID: 17057 RVA: 0x00116551 File Offset: 0x00114751
			internal void StartOperationSend()
			{
				this.UpdateDataChunk();
				this.m_CompletedOperation = WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs.HttpListenerAsyncOperation.Send;
			}

			// Token: 0x060042A2 RID: 17058 RVA: 0x00116560 File Offset: 0x00114760
			public void SetBuffer(byte[] buffer, int offset, int count)
			{
				this.m_Buffer = buffer;
				this.m_Offset = offset;
				this.m_Count = count;
			}

			// Token: 0x060042A3 RID: 17059 RVA: 0x00116578 File Offset: 0x00114778
			private void UpdateDataChunk()
			{
				if (this.m_DataChunks == null)
				{
					this.m_DataChunks = new UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK[2];
					this.m_DataChunksGCHandle = GCHandle.Alloc(this.m_DataChunks, GCHandleType.Pinned);
					this.m_DataChunks[0] = default(UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK);
					this.m_DataChunks[0].DataChunkType = UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK_TYPE.HttpDataChunkFromMemory;
					this.m_DataChunks[1] = default(UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK);
					this.m_DataChunks[1].DataChunkType = UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK_TYPE.HttpDataChunkFromMemory;
				}
				if (this.m_Buffer != null)
				{
					this.UpdateDataChunk(0, this.m_Buffer, this.m_Offset, this.m_Count);
					this.UpdateDataChunk(1, null, 0, 0);
					this.m_DataChunkCount = 1;
					return;
				}
				if (this.m_BufferList != null)
				{
					this.UpdateDataChunk(0, this.m_BufferList[0].Array, this.m_BufferList[0].Offset, this.m_BufferList[0].Count);
					this.UpdateDataChunk(1, this.m_BufferList[1].Array, this.m_BufferList[1].Offset, this.m_BufferList[1].Count);
					this.m_DataChunkCount = 2;
					return;
				}
				this.m_DataChunks = null;
			}

			// Token: 0x060042A4 RID: 17060 RVA: 0x001166C8 File Offset: 0x001148C8
			private unsafe void UpdateDataChunk(int index, byte[] buffer, int offset, int count)
			{
				if (buffer == null)
				{
					this.m_DataChunks[index].pBuffer = null;
					this.m_DataChunks[index].BufferLength = 0U;
					return;
				}
				if (this.m_WebSocket.InternalBuffer.IsInternalBuffer(buffer, offset, count))
				{
					this.m_DataChunks[index].pBuffer = (byte*)(void*)this.m_WebSocket.InternalBuffer.ToIntPtr(offset);
				}
				else
				{
					this.m_DataChunks[index].pBuffer = (byte*)(void*)this.m_WebSocket.InternalBuffer.ConvertPinnedSendPayloadToNative(buffer, offset, count);
				}
				this.m_DataChunks[index].BufferLength = (uint)count;
			}

			// Token: 0x060042A5 RID: 17061 RVA: 0x0011677A File Offset: 0x0011497A
			internal void Complete()
			{
				if (this.m_AllocateOverlappedOnDemand)
				{
					this.FreeOverlapped(false);
					Interlocked.Exchange(ref this.m_Operating, 0);
				}
				else
				{
					this.m_Operating = 0;
				}
				if (this.m_DisposeCalled)
				{
					this.Dispose();
				}
			}

			// Token: 0x060042A6 RID: 17062 RVA: 0x001167AF File Offset: 0x001149AF
			private void SetResults(Exception exception, int bytesTransferred)
			{
				this.m_Exception = exception;
				this.m_BytesTransferred = bytesTransferred;
			}

			// Token: 0x060042A7 RID: 17063 RVA: 0x001167C0 File Offset: 0x001149C0
			internal void FinishOperationFailure(Exception exception, bool syncCompletion)
			{
				this.SetResults(exception, 0);
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.PrintError(Logging.WebSockets, this.m_CurrentStream, (this.m_CompletedOperation == WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs.HttpListenerAsyncOperation.Receive) ? "ReadAsyncFast" : "WriteAsyncFast", exception.ToString());
				}
				this.Complete();
				this.OnCompleted(this);
			}

			// Token: 0x060042A8 RID: 17064 RVA: 0x00116814 File Offset: 0x00114A14
			internal void FinishOperationSuccess(int bytesTransferred, bool syncCompletion)
			{
				this.SetResults(null, bytesTransferred);
				if (WebSocketBase.LoggingEnabled)
				{
					if (this.m_Buffer != null)
					{
						Logging.Dump(Logging.WebSockets, this.m_CurrentStream, (this.m_CompletedOperation == WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs.HttpListenerAsyncOperation.Receive) ? "ReadAsyncFast" : "WriteAsyncFast", this.m_Buffer, this.m_Offset, bytesTransferred);
					}
					else
					{
						if (this.m_BufferList != null)
						{
							using (IEnumerator<ArraySegment<byte>> enumerator = this.BufferList.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									ArraySegment<byte> arraySegment = enumerator.Current;
									Logging.Dump(Logging.WebSockets, this, "WriteAsyncFast", arraySegment.Array, arraySegment.Offset, arraySegment.Count);
								}
								goto IL_EA;
							}
						}
						Logging.PrintLine(Logging.WebSockets, TraceEventType.Verbose, 0, string.Format(CultureInfo.InvariantCulture, "Output channel closed for {0}#{1}", new object[]
						{
							this.m_CurrentStream.GetType().Name,
							ValidationHelper.HashString(this.m_CurrentStream)
						}));
					}
				}
				IL_EA:
				if (this.m_ShouldCloseOutput)
				{
					this.m_CurrentStream.m_OutputStream.SetClosedFlag();
				}
				this.Complete();
				this.OnCompleted(this);
			}

			// Token: 0x060042A9 RID: 17065 RVA: 0x00116940 File Offset: 0x00114B40
			private unsafe void CompletionPortCallback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
			{
				if (errorCode == 0U || errorCode == 38U)
				{
					this.FinishOperationSuccess((int)numBytes, false);
					return;
				}
				this.FinishOperationFailure(new HttpListenerException((int)errorCode), false);
			}

			// Token: 0x040032F3 RID: 13043
			private const int Free = 0;

			// Token: 0x040032F4 RID: 13044
			private const int InProgress = 1;

			// Token: 0x040032F5 RID: 13045
			private const int Disposed = 2;

			// Token: 0x040032F6 RID: 13046
			private int m_Operating;

			// Token: 0x040032F7 RID: 13047
			private bool m_DisposeCalled;

			// Token: 0x040032F8 RID: 13048
			private SafeNativeOverlapped m_PtrNativeOverlapped;

			// Token: 0x040032F9 RID: 13049
			private Overlapped m_Overlapped;

			// Token: 0x040032FB RID: 13051
			private byte[] m_Buffer;

			// Token: 0x040032FC RID: 13052
			private IList<ArraySegment<byte>> m_BufferList;

			// Token: 0x040032FD RID: 13053
			private int m_Count;

			// Token: 0x040032FE RID: 13054
			private int m_Offset;

			// Token: 0x040032FF RID: 13055
			private int m_BytesTransferred;

			// Token: 0x04003300 RID: 13056
			private WebSocketHttpListenerDuplexStream.HttpListenerAsyncEventArgs.HttpListenerAsyncOperation m_CompletedOperation;

			// Token: 0x04003301 RID: 13057
			private UnsafeNclNativeMethods.HttpApi.HTTP_DATA_CHUNK[] m_DataChunks;

			// Token: 0x04003302 RID: 13058
			private GCHandle m_DataChunksGCHandle;

			// Token: 0x04003303 RID: 13059
			private ushort m_DataChunkCount;

			// Token: 0x04003304 RID: 13060
			private Exception m_Exception;

			// Token: 0x04003305 RID: 13061
			private bool m_ShouldCloseOutput;

			// Token: 0x04003306 RID: 13062
			private readonly WebSocketBase m_WebSocket;

			// Token: 0x04003307 RID: 13063
			private readonly WebSocketHttpListenerDuplexStream m_CurrentStream;

			// Token: 0x04003308 RID: 13064
			private readonly bool m_AllocateOverlappedOnDemand;

			// Token: 0x0200091D RID: 2333
			public enum HttpListenerAsyncOperation
			{
				// Token: 0x04003D83 RID: 15747
				None,
				// Token: 0x04003D84 RID: 15748
				Receive,
				// Token: 0x04003D85 RID: 15749
				Send
			}
		}

		// Token: 0x02000782 RID: 1922
		private static class Methods
		{
			// Token: 0x04003309 RID: 13065
			public const string CloseNetworkConnectionAsync = "CloseNetworkConnectionAsync";

			// Token: 0x0400330A RID: 13066
			public const string OnCancel = "OnCancel";

			// Token: 0x0400330B RID: 13067
			public const string OnReadCompleted = "OnReadCompleted";

			// Token: 0x0400330C RID: 13068
			public const string OnWriteCompleted = "OnWriteCompleted";

			// Token: 0x0400330D RID: 13069
			public const string ReadAsyncFast = "ReadAsyncFast";

			// Token: 0x0400330E RID: 13070
			public const string ReadAsyncCore = "ReadAsyncCore";

			// Token: 0x0400330F RID: 13071
			public const string WriteAsyncFast = "WriteAsyncFast";

			// Token: 0x04003310 RID: 13072
			public const string WriteAsyncCore = "WriteAsyncCore";

			// Token: 0x04003311 RID: 13073
			public const string MultipleWriteAsyncCore = "MultipleWriteAsyncCore";
		}
	}
}
