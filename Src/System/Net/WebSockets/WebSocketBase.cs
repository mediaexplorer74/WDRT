using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	// Token: 0x02000230 RID: 560
	internal abstract class WebSocketBase : WebSocket, IDisposable
	{
		// Token: 0x060014B6 RID: 5302 RVA: 0x0006C8C4 File Offset: 0x0006AAC4
		protected WebSocketBase(Stream innerStream, string subProtocol, TimeSpan keepAliveInterval, WebSocketBuffer internalBuffer)
		{
			WebSocketHelpers.ValidateInnerStream(innerStream);
			WebSocketHelpers.ValidateOptions(subProtocol, internalBuffer.ReceiveBufferSize, internalBuffer.SendBufferSize, keepAliveInterval);
			WebSocketBase.s_LoggingEnabled = Logging.On && Logging.WebSockets.Switch.ShouldTrace(TraceEventType.Critical);
			string text = string.Empty;
			if (WebSocketBase.s_LoggingEnabled)
			{
				text = string.Format(CultureInfo.InvariantCulture, "ReceiveBufferSize: {0}, SendBufferSize: {1},  Protocols: {2}, KeepAliveInterval: {3}, innerStream: {4}, internalBuffer: {5}", new object[]
				{
					internalBuffer.ReceiveBufferSize,
					internalBuffer.SendBufferSize,
					subProtocol,
					keepAliveInterval,
					Logging.GetObjectLogHash(innerStream),
					Logging.GetObjectLogHash(internalBuffer)
				});
				Logging.Enter(Logging.WebSockets, this, "Initialize", text);
			}
			this.m_ThisLock = new object();
			try
			{
				this.m_InnerStream = innerStream;
				this.m_InternalBuffer = internalBuffer;
				if (WebSocketBase.s_LoggingEnabled)
				{
					Logging.Associate(Logging.WebSockets, this, this.m_InnerStream);
					Logging.Associate(Logging.WebSockets, this, this.m_InternalBuffer);
				}
				this.m_CloseOutstandingOperationHelper = new WebSocketBase.OutstandingOperationHelper();
				this.m_CloseOutputOutstandingOperationHelper = new WebSocketBase.OutstandingOperationHelper();
				this.m_ReceiveOutstandingOperationHelper = new WebSocketBase.OutstandingOperationHelper();
				this.m_SendOutstandingOperationHelper = new WebSocketBase.OutstandingOperationHelper();
				this.m_State = WebSocketState.Open;
				this.m_SubProtocol = subProtocol;
				this.m_SendFrameThrottle = new SemaphoreSlim(1, 1);
				this.m_CloseStatus = null;
				this.m_CloseStatusDescription = null;
				this.m_InnerStreamAsWebSocketStream = innerStream as WebSocketBase.IWebSocketStream;
				if (this.m_InnerStreamAsWebSocketStream != null)
				{
					this.m_InnerStreamAsWebSocketStream.SwitchToOpaqueMode(this);
				}
				this.m_KeepAliveTracker = WebSocketBase.KeepAliveTracker.Create(keepAliveInterval);
			}
			finally
			{
				if (WebSocketBase.s_LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "Initialize", text);
				}
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x0006CA84 File Offset: 0x0006AC84
		internal static bool LoggingEnabled
		{
			get
			{
				return WebSocketBase.s_LoggingEnabled;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x0006CA8D File Offset: 0x0006AC8D
		public override WebSocketState State
		{
			get
			{
				return this.m_State;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x0006CA97 File Offset: 0x0006AC97
		public override string SubProtocol
		{
			get
			{
				return this.m_SubProtocol;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x0006CA9F File Offset: 0x0006AC9F
		public override WebSocketCloseStatus? CloseStatus
		{
			get
			{
				return this.m_CloseStatus;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x0006CAA7 File Offset: 0x0006ACA7
		public override string CloseStatusDescription
		{
			get
			{
				return this.m_CloseStatusDescription;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x0006CAAF File Offset: 0x0006ACAF
		internal WebSocketBuffer InternalBuffer
		{
			get
			{
				return this.m_InternalBuffer;
			}
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0006CAB7 File Offset: 0x0006ACB7
		protected void StartKeepAliveTimer()
		{
			this.m_KeepAliveTracker.StartTimer(this);
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060014BE RID: 5310
		internal abstract SafeHandle SessionHandle { get; }

		// Token: 0x060014BF RID: 5311 RVA: 0x0006CAC5 File Offset: 0x0006ACC5
		public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
		{
			WebSocketHelpers.ValidateArraySegment<byte>(buffer, "buffer");
			return this.ReceiveAsyncCore(buffer, cancellationToken);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0006CADC File Offset: 0x0006ACDC
		private async Task<WebSocketReceiveResult> ReceiveAsyncCore(ArraySegment<byte> buffer, CancellationToken cancellationToken)
		{
			if (WebSocketBase.s_LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, this, "ReceiveAsync", string.Empty);
			}
			WebSocketReceiveResult webSocketReceiveResult2;
			try
			{
				this.ThrowIfPendingException();
				this.ThrowIfDisposed();
				WebSocket.ThrowOnInvalidState(this.State, new WebSocketState[]
				{
					WebSocketState.Open,
					WebSocketState.CloseSent
				});
				bool ownsCancellationTokenSource = false;
				CancellationToken linkedCancellationToken = CancellationToken.None;
				try
				{
					ownsCancellationTokenSource = this.m_ReceiveOutstandingOperationHelper.TryStartOperation(cancellationToken, out linkedCancellationToken);
					if (!ownsCancellationTokenSource)
					{
						object thisLock = this.m_ThisLock;
						lock (thisLock)
						{
							if (this.m_CloseAsyncStartedReceive)
							{
								throw new InvalidOperationException(SR.GetString("net_WebSockets_ReceiveAsyncDisallowedAfterCloseAsync", new object[] { "CloseAsync", "CloseOutputAsync" }));
							}
							throw new InvalidOperationException(SR.GetString("net_Websockets_AlreadyOneOutstandingOperation", new object[] { "ReceiveAsync" }));
						}
					}
					this.EnsureReceiveOperation();
					WebSocketReceiveResult webSocketReceiveResult = await this.m_ReceiveOperation.Process(new ArraySegment<byte>?(buffer), linkedCancellationToken).SuppressContextFlow<WebSocketReceiveResult>();
					webSocketReceiveResult2 = webSocketReceiveResult;
					if (WebSocketBase.s_LoggingEnabled && webSocketReceiveResult2.Count > 0)
					{
						Logging.Dump(Logging.WebSockets, this, "ReceiveAsync", buffer.Array, buffer.Offset, webSocketReceiveResult2.Count);
					}
				}
				catch (Exception ex)
				{
					bool isCancellationRequested = linkedCancellationToken.IsCancellationRequested;
					this.Abort();
					this.ThrowIfConvertibleException("ReceiveAsync", ex, cancellationToken, isCancellationRequested);
					throw;
				}
				finally
				{
					this.m_ReceiveOutstandingOperationHelper.CompleteOperation(ownsCancellationTokenSource);
				}
				linkedCancellationToken = default(CancellationToken);
			}
			finally
			{
				if (WebSocketBase.s_LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "ReceiveAsync", string.Empty);
				}
			}
			return webSocketReceiveResult2;
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0006CB30 File Offset: 0x0006AD30
		public override Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			if (messageType != WebSocketMessageType.Binary && messageType != WebSocketMessageType.Text)
			{
				throw new ArgumentException(SR.GetString("net_WebSockets_Argument_InvalidMessageType", new object[]
				{
					messageType,
					"SendAsync",
					WebSocketMessageType.Binary,
					WebSocketMessageType.Text,
					"CloseOutputAsync"
				}), "messageType");
			}
			WebSocketHelpers.ValidateArraySegment<byte>(buffer, "buffer");
			return this.SendAsyncCore(buffer, messageType, endOfMessage, cancellationToken);
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0006CBA0 File Offset: 0x0006ADA0
		private async Task SendAsyncCore(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			string inputParameter = string.Empty;
			if (WebSocketBase.s_LoggingEnabled)
			{
				inputParameter = string.Format(CultureInfo.InvariantCulture, "messageType: {0}, endOfMessage: {1}", new object[] { messageType, endOfMessage });
				Logging.Enter(Logging.WebSockets, this, "SendAsync", inputParameter);
			}
			try
			{
				this.ThrowIfPendingException();
				this.ThrowIfDisposed();
				WebSocket.ThrowOnInvalidState(this.State, new WebSocketState[]
				{
					WebSocketState.Open,
					WebSocketState.CloseReceived
				});
				bool ownsCancellationTokenSource = false;
				CancellationToken linkedCancellationToken = CancellationToken.None;
				try
				{
					while (!(ownsCancellationTokenSource = this.m_SendOutstandingOperationHelper.TryStartOperation(cancellationToken, out linkedCancellationToken)))
					{
						SafeHandle sessionHandle = this.SessionHandle;
						Task keepAliveTask;
						lock (sessionHandle)
						{
							keepAliveTask = this.m_KeepAliveTask;
							if (keepAliveTask == null)
							{
								this.m_SendOutstandingOperationHelper.CompleteOperation(ownsCancellationTokenSource);
								if (ownsCancellationTokenSource = this.m_SendOutstandingOperationHelper.TryStartOperation(cancellationToken, out linkedCancellationToken))
								{
									break;
								}
								throw new InvalidOperationException(SR.GetString("net_Websockets_AlreadyOneOutstandingOperation", new object[] { "SendAsync" }));
							}
						}
						await keepAliveTask.SuppressContextFlow();
						this.ThrowIfPendingException();
						this.m_SendOutstandingOperationHelper.CompleteOperation(ownsCancellationTokenSource);
					}
					if (WebSocketBase.s_LoggingEnabled && buffer.Count > 0)
					{
						Logging.Dump(Logging.WebSockets, this, "SendAsync", buffer.Array, buffer.Offset, buffer.Count);
					}
					int offset = buffer.Offset;
					this.EnsureSendOperation();
					this.m_SendOperation.BufferType = WebSocketBase.GetBufferType(messageType, endOfMessage);
					await this.m_SendOperation.Process(new ArraySegment<byte>?(buffer), linkedCancellationToken).SuppressContextFlow<WebSocketReceiveResult>();
				}
				catch (Exception ex)
				{
					bool isCancellationRequested = linkedCancellationToken.IsCancellationRequested;
					this.Abort();
					this.ThrowIfConvertibleException("SendAsync", ex, cancellationToken, isCancellationRequested);
					throw;
				}
				finally
				{
					this.m_SendOutstandingOperationHelper.CompleteOperation(ownsCancellationTokenSource);
				}
				linkedCancellationToken = default(CancellationToken);
			}
			finally
			{
				if (WebSocketBase.s_LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "SendAsync", inputParameter);
				}
			}
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0006CC04 File Offset: 0x0006AE04
		private async Task SendFrameAsync(IList<ArraySegment<byte>> sendBuffers, CancellationToken cancellationToken)
		{
			bool sendFrameLockTaken = false;
			try
			{
				await this.m_SendFrameThrottle.WaitAsync(cancellationToken).SuppressContextFlow();
				sendFrameLockTaken = true;
				if (sendBuffers.Count > 1 && this.m_InnerStreamAsWebSocketStream != null && this.m_InnerStreamAsWebSocketStream.SupportsMultipleWrite)
				{
					await this.m_InnerStreamAsWebSocketStream.MultipleWriteAsync(sendBuffers, cancellationToken).SuppressContextFlow();
				}
				else
				{
					foreach (ArraySegment<byte> arraySegment in sendBuffers)
					{
						await this.m_InnerStream.WriteAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count, cancellationToken).SuppressContextFlow();
					}
					IEnumerator<ArraySegment<byte>> enumerator = null;
				}
			}
			catch (ObjectDisposedException ex)
			{
				throw new WebSocketException(WebSocketError.ConnectionClosedPrematurely, ex);
			}
			catch (NotSupportedException ex2)
			{
				throw new WebSocketException(WebSocketError.ConnectionClosedPrematurely, ex2);
			}
			finally
			{
				if (sendFrameLockTaken)
				{
					this.m_SendFrameThrottle.Release();
				}
			}
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0006CC58 File Offset: 0x0006AE58
		public override void Abort()
		{
			if (WebSocketBase.s_LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, this, "Abort", string.Empty);
			}
			bool flag = false;
			bool flag2 = false;
			try
			{
				if (!WebSocket.IsStateTerminal(this.State))
				{
					this.TakeLocks(ref flag, ref flag2);
					if (!WebSocket.IsStateTerminal(this.State))
					{
						this.m_State = WebSocketState.Aborted;
						if (this.SessionHandle != null && !this.SessionHandle.IsClosed && !this.SessionHandle.IsInvalid)
						{
							WebSocketProtocolComponent.WebSocketAbortHandle(this.SessionHandle);
						}
						this.m_ReceiveOutstandingOperationHelper.CancelIO();
						this.m_SendOutstandingOperationHelper.CancelIO();
						this.m_CloseOutputOutstandingOperationHelper.CancelIO();
						this.m_CloseOutstandingOperationHelper.CancelIO();
						if (this.m_InnerStreamAsWebSocketStream != null)
						{
							this.m_InnerStreamAsWebSocketStream.Abort();
						}
						this.CleanUp();
					}
				}
			}
			finally
			{
				this.ReleaseLocks(ref flag, ref flag2);
				if (WebSocketBase.s_LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "Abort", string.Empty);
				}
			}
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0006CD6C File Offset: 0x0006AF6C
		public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			WebSocketHelpers.ValidateCloseStatus(closeStatus, statusDescription);
			return this.CloseOutputAsyncCore(closeStatus, statusDescription, cancellationToken);
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0006CD80 File Offset: 0x0006AF80
		private async Task CloseOutputAsyncCore(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			string inputParameter = string.Empty;
			if (WebSocketBase.s_LoggingEnabled)
			{
				inputParameter = string.Format(CultureInfo.InvariantCulture, "closeStatus: {0}, statusDescription: {1}", new object[] { closeStatus, statusDescription });
				Logging.Enter(Logging.WebSockets, this, "CloseOutputAsync", inputParameter);
			}
			try
			{
				this.ThrowIfPendingException();
				if (!WebSocket.IsStateTerminal(this.State))
				{
					this.ThrowIfDisposed();
					bool thisLockTaken = false;
					bool sessionHandleLockTaken = false;
					bool needToCompleteSendOperation = false;
					bool ownsCloseOutputCancellationTokenSource = false;
					bool ownsSendCancellationTokenSource = false;
					CancellationToken linkedCancellationToken = CancellationToken.None;
					try
					{
						this.TakeLocks(ref thisLockTaken, ref sessionHandleLockTaken);
						this.ThrowIfPendingException();
						this.ThrowIfDisposed();
						if (WebSocket.IsStateTerminal(this.State))
						{
							return;
						}
						WebSocket.ThrowOnInvalidState(this.State, new WebSocketState[]
						{
							WebSocketState.Open,
							WebSocketState.CloseReceived
						});
						ownsCloseOutputCancellationTokenSource = this.m_CloseOutputOutstandingOperationHelper.TryStartOperation(cancellationToken, out linkedCancellationToken);
						if (!ownsCloseOutputCancellationTokenSource)
						{
							Task closeOutputTask = this.m_CloseOutputTask;
							if (closeOutputTask != null)
							{
								this.ReleaseLocks(ref thisLockTaken, ref sessionHandleLockTaken);
								await closeOutputTask.SuppressContextFlow();
								this.TakeLocks(ref thisLockTaken, ref sessionHandleLockTaken);
							}
						}
						else
						{
							needToCompleteSendOperation = true;
							while (!(ownsSendCancellationTokenSource = this.m_SendOutstandingOperationHelper.TryStartOperation(cancellationToken, out linkedCancellationToken)))
							{
								if (this.m_KeepAliveTask == null)
								{
									throw new InvalidOperationException(SR.GetString("net_Websockets_AlreadyOneOutstandingOperation", new object[] { "SendAsync" }));
								}
								Task keepAliveTask = this.m_KeepAliveTask;
								this.ReleaseLocks(ref thisLockTaken, ref sessionHandleLockTaken);
								await keepAliveTask.SuppressContextFlow();
								this.TakeLocks(ref thisLockTaken, ref sessionHandleLockTaken);
								this.ThrowIfPendingException();
								this.m_SendOutstandingOperationHelper.CompleteOperation(ownsSendCancellationTokenSource);
							}
							this.EnsureCloseOutputOperation();
							this.m_CloseOutputOperation.CloseStatus = closeStatus;
							this.m_CloseOutputOperation.CloseReason = statusDescription;
							this.m_CloseOutputTask = this.m_CloseOutputOperation.Process(null, linkedCancellationToken);
							this.ReleaseLocks(ref thisLockTaken, ref sessionHandleLockTaken);
							await this.m_CloseOutputTask.SuppressContextFlow();
							this.TakeLocks(ref thisLockTaken, ref sessionHandleLockTaken);
							if (this.OnCloseOutputCompleted())
							{
								bool flag = false;
								try
								{
									flag = await this.StartOnCloseCompleted(thisLockTaken, sessionHandleLockTaken, linkedCancellationToken).SuppressContextFlow<bool>();
								}
								catch (Exception)
								{
									this.ResetFlagsAndTakeLocks(ref thisLockTaken, ref sessionHandleLockTaken);
									throw;
								}
								if (flag)
								{
									this.ResetFlagsAndTakeLocks(ref thisLockTaken, ref sessionHandleLockTaken);
									this.FinishOnCloseCompleted();
								}
							}
						}
					}
					catch (Exception ex)
					{
						bool isCancellationRequested = linkedCancellationToken.IsCancellationRequested;
						this.Abort();
						this.ThrowIfConvertibleException("CloseOutputAsync", ex, cancellationToken, isCancellationRequested);
						throw;
					}
					finally
					{
						this.m_CloseOutputOutstandingOperationHelper.CompleteOperation(ownsCloseOutputCancellationTokenSource);
						if (needToCompleteSendOperation)
						{
							this.m_SendOutstandingOperationHelper.CompleteOperation(ownsSendCancellationTokenSource);
						}
						this.m_CloseOutputTask = null;
						this.ReleaseLocks(ref thisLockTaken, ref sessionHandleLockTaken);
					}
					linkedCancellationToken = default(CancellationToken);
				}
			}
			finally
			{
				if (WebSocketBase.s_LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "CloseOutputAsync", inputParameter);
				}
			}
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0006CDDC File Offset: 0x0006AFDC
		private bool OnCloseOutputCompleted()
		{
			if (WebSocket.IsStateTerminal(this.State))
			{
				return false;
			}
			WebSocketState state = this.State;
			if (state != WebSocketState.Open)
			{
				return state == WebSocketState.CloseReceived;
			}
			this.m_State = WebSocketState.CloseSent;
			return false;
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0006CE18 File Offset: 0x0006B018
		private async Task<bool> StartOnCloseCompleted(bool thisLockTakenSnapshot, bool sessionHandleLockTakenSnapshot, CancellationToken cancellationToken)
		{
			bool flag;
			if (WebSocket.IsStateTerminal(this.m_State))
			{
				flag = false;
			}
			else
			{
				this.m_State = WebSocketState.Closed;
				if (this.m_InnerStreamAsWebSocketStream != null)
				{
					bool flag2 = thisLockTakenSnapshot;
					bool flag3 = sessionHandleLockTakenSnapshot;
					try
					{
						if (this.m_CloseNetworkConnectionTask == null)
						{
							this.m_CloseNetworkConnectionTask = this.m_InnerStreamAsWebSocketStream.CloseNetworkConnectionAsync(cancellationToken);
						}
						if (flag2 && flag3)
						{
							this.ReleaseLocks(ref flag2, ref flag3);
						}
						else if (flag2)
						{
							WebSocketBase.ReleaseLock(this.m_ThisLock, ref flag2);
						}
						await this.m_CloseNetworkConnectionTask.SuppressContextFlow();
					}
					catch (Exception ex)
					{
						if (!this.CanHandleExceptionDuringClose(ex))
						{
							this.ThrowIfConvertibleException("StartOnCloseCompleted", ex, cancellationToken, cancellationToken.IsCancellationRequested);
							throw;
						}
					}
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0006CE73 File Offset: 0x0006B073
		private void FinishOnCloseCompleted()
		{
			this.CleanUp();
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0006CE7B File Offset: 0x0006B07B
		public override Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			WebSocketHelpers.ValidateCloseStatus(closeStatus, statusDescription);
			return this.CloseAsyncCore(closeStatus, statusDescription, cancellationToken);
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0006CE90 File Offset: 0x0006B090
		private async Task CloseAsyncCore(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			string inputParameter = string.Empty;
			if (WebSocketBase.s_LoggingEnabled)
			{
				inputParameter = string.Format(CultureInfo.InvariantCulture, "closeStatus: {0}, statusDescription: {1}", new object[] { closeStatus, statusDescription });
				Logging.Enter(Logging.WebSockets, this, "CloseAsync", inputParameter);
			}
			try
			{
				this.ThrowIfPendingException();
				if (!WebSocket.IsStateTerminal(this.State))
				{
					this.ThrowIfDisposed();
					bool lockTaken = false;
					Monitor.Enter(this.m_ThisLock, ref lockTaken);
					bool ownsCloseCancellationTokenSource = false;
					CancellationToken linkedCancellationToken = CancellationToken.None;
					try
					{
						this.ThrowIfPendingException();
						if (WebSocket.IsStateTerminal(this.State))
						{
							return;
						}
						this.ThrowIfDisposed();
						WebSocket.ThrowOnInvalidState(this.State, new WebSocketState[]
						{
							WebSocketState.Open,
							WebSocketState.CloseReceived,
							WebSocketState.CloseSent
						});
						ownsCloseCancellationTokenSource = this.m_CloseOutstandingOperationHelper.TryStartOperation(cancellationToken, out linkedCancellationToken);
						Task task;
						if (ownsCloseCancellationTokenSource)
						{
							task = this.m_CloseOutputTask;
							if (task == null && this.State != WebSocketState.CloseSent)
							{
								if (this.m_CloseReceivedTaskCompletionSource == null)
								{
									this.m_CloseReceivedTaskCompletionSource = new TaskCompletionSource<object>();
								}
								WebSocketBase.ReleaseLock(this.m_ThisLock, ref lockTaken);
								task = this.CloseOutputAsync(closeStatus, statusDescription, linkedCancellationToken);
							}
						}
						else
						{
							task = this.m_CloseReceivedTaskCompletionSource.Task;
						}
						if (task != null)
						{
							WebSocketBase.ReleaseLock(this.m_ThisLock, ref lockTaken);
							try
							{
								await task.SuppressContextFlow();
							}
							catch (Exception ex)
							{
								Monitor.Enter(this.m_ThisLock, ref lockTaken);
								if (!this.CanHandleExceptionDuringClose(ex))
								{
									this.ThrowIfConvertibleException("CloseOutputAsync", ex, cancellationToken, linkedCancellationToken.IsCancellationRequested);
									throw;
								}
							}
							if (!lockTaken)
							{
								Monitor.Enter(this.m_ThisLock, ref lockTaken);
							}
						}
						if (this.OnCloseOutputCompleted())
						{
							bool flag = false;
							try
							{
								flag = await this.StartOnCloseCompleted(lockTaken, false, linkedCancellationToken).SuppressContextFlow<bool>();
							}
							catch (Exception)
							{
								this.ResetFlagAndTakeLock(this.m_ThisLock, ref lockTaken);
								throw;
							}
							if (flag)
							{
								this.ResetFlagAndTakeLock(this.m_ThisLock, ref lockTaken);
								this.FinishOnCloseCompleted();
							}
						}
						if (WebSocket.IsStateTerminal(this.State))
						{
							return;
						}
						linkedCancellationToken = CancellationToken.None;
						bool flag2 = this.m_ReceiveOutstandingOperationHelper.TryStartOperation(cancellationToken, out linkedCancellationToken);
						if (flag2)
						{
							this.m_CloseAsyncStartedReceive = true;
							ArraySegment<byte> closeMessageBuffer = new ArraySegment<byte>(new byte[256]);
							this.EnsureReceiveOperation();
							Task<WebSocketReceiveResult> task2 = this.m_ReceiveOperation.Process(new ArraySegment<byte>?(closeMessageBuffer), linkedCancellationToken);
							WebSocketBase.ReleaseLock(this.m_ThisLock, ref lockTaken);
							WebSocketReceiveResult receiveResult = null;
							try
							{
								receiveResult = await task2.SuppressContextFlow<WebSocketReceiveResult>();
							}
							catch (Exception ex2)
							{
								Monitor.Enter(this.m_ThisLock, ref lockTaken);
								if (!this.CanHandleExceptionDuringClose(ex2))
								{
									this.ThrowIfConvertibleException("CloseAsync", ex2, cancellationToken, linkedCancellationToken.IsCancellationRequested);
									throw;
								}
							}
							if (receiveResult != null)
							{
								if (WebSocketBase.s_LoggingEnabled && receiveResult.Count > 0)
								{
									Logging.Dump(Logging.WebSockets, this, "ReceiveAsync", closeMessageBuffer.Array, closeMessageBuffer.Offset, receiveResult.Count);
								}
								if (receiveResult.MessageType != WebSocketMessageType.Close)
								{
									throw new WebSocketException(WebSocketError.InvalidMessageType, SR.GetString("net_WebSockets_InvalidMessageType", new object[]
									{
										typeof(WebSocket).Name + ".CloseAsync",
										typeof(WebSocket).Name + ".CloseOutputAsync",
										receiveResult.MessageType
									}));
								}
							}
							closeMessageBuffer = default(ArraySegment<byte>);
							receiveResult = null;
						}
						else
						{
							this.m_ReceiveOutstandingOperationHelper.CompleteOperation(flag2);
							WebSocketBase.ReleaseLock(this.m_ThisLock, ref lockTaken);
							await this.m_CloseReceivedTaskCompletionSource.Task.SuppressContextFlow<object>();
						}
						if (!lockTaken)
						{
							Monitor.Enter(this.m_ThisLock, ref lockTaken);
						}
						if (!WebSocket.IsStateTerminal(this.State))
						{
							bool ownsSendCancellationSource = false;
							try
							{
								ownsSendCancellationSource = this.m_SendOutstandingOperationHelper.TryStartOperation(cancellationToken, out linkedCancellationToken);
								bool flag3 = false;
								try
								{
									flag3 = await this.StartOnCloseCompleted(lockTaken, false, linkedCancellationToken).SuppressContextFlow<bool>();
								}
								catch (Exception)
								{
									this.ResetFlagAndTakeLock(this.m_ThisLock, ref lockTaken);
									throw;
								}
								if (flag3)
								{
									this.ResetFlagAndTakeLock(this.m_ThisLock, ref lockTaken);
									this.FinishOnCloseCompleted();
								}
							}
							finally
							{
								this.m_SendOutstandingOperationHelper.CompleteOperation(ownsSendCancellationSource);
							}
						}
					}
					catch (Exception ex3)
					{
						bool isCancellationRequested = linkedCancellationToken.IsCancellationRequested;
						this.Abort();
						this.ThrowIfConvertibleException("CloseAsync", ex3, cancellationToken, isCancellationRequested);
						throw;
					}
					finally
					{
						this.m_CloseOutstandingOperationHelper.CompleteOperation(ownsCloseCancellationTokenSource);
						WebSocketBase.ReleaseLock(this.m_ThisLock, ref lockTaken);
					}
					linkedCancellationToken = default(CancellationToken);
				}
			}
			finally
			{
				if (WebSocketBase.s_LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "CloseAsync", inputParameter);
				}
			}
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0006CEEC File Offset: 0x0006B0EC
		public override void Dispose()
		{
			if (this.m_IsDisposed)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			try
			{
				this.TakeLocks(ref flag, ref flag2);
				if (!this.m_IsDisposed)
				{
					if (!WebSocket.IsStateTerminal(this.State))
					{
						this.Abort();
					}
					else
					{
						this.CleanUp();
					}
					this.m_IsDisposed = true;
				}
			}
			finally
			{
				this.ReleaseLocks(ref flag, ref flag2);
			}
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0006CF60 File Offset: 0x0006B160
		private void ResetFlagAndTakeLock(object lockObject, ref bool thisLockTaken)
		{
			thisLockTaken = false;
			Monitor.Enter(lockObject, ref thisLockTaken);
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0006CF6C File Offset: 0x0006B16C
		private void ResetFlagsAndTakeLocks(ref bool thisLockTaken, ref bool sessionHandleLockTaken)
		{
			thisLockTaken = false;
			sessionHandleLockTaken = false;
			this.TakeLocks(ref thisLockTaken, ref sessionHandleLockTaken);
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0006CF7C File Offset: 0x0006B17C
		private void TakeLocks(ref bool thisLockTaken, ref bool sessionHandleLockTaken)
		{
			Monitor.Enter(this.SessionHandle, ref sessionHandleLockTaken);
			Monitor.Enter(this.m_ThisLock, ref thisLockTaken);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0006CF98 File Offset: 0x0006B198
		private void ReleaseLocks(ref bool thisLockTaken, ref bool sessionHandleLockTaken)
		{
			if (thisLockTaken | sessionHandleLockTaken)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					if (thisLockTaken)
					{
						Monitor.Exit(this.m_ThisLock);
						thisLockTaken = false;
					}
					if (sessionHandleLockTaken)
					{
						Monitor.Exit(this.SessionHandle);
						sessionHandleLockTaken = false;
					}
				}
			}
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x0006CFE8 File Offset: 0x0006B1E8
		private void EnsureReceiveOperation()
		{
			if (this.m_ReceiveOperation == null)
			{
				object thisLock = this.m_ThisLock;
				lock (thisLock)
				{
					if (this.m_ReceiveOperation == null)
					{
						this.m_ReceiveOperation = new WebSocketBase.WebSocketOperation.ReceiveOperation(this);
					}
				}
			}
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0006D044 File Offset: 0x0006B244
		private void EnsureSendOperation()
		{
			if (this.m_SendOperation == null)
			{
				object thisLock = this.m_ThisLock;
				lock (thisLock)
				{
					if (this.m_SendOperation == null)
					{
						this.m_SendOperation = new WebSocketBase.WebSocketOperation.SendOperation(this);
					}
				}
			}
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0006D0A0 File Offset: 0x0006B2A0
		private void EnsureKeepAliveOperation()
		{
			if (this.m_KeepAliveOperation == null)
			{
				object thisLock = this.m_ThisLock;
				lock (thisLock)
				{
					if (this.m_KeepAliveOperation == null)
					{
						this.m_KeepAliveOperation = new WebSocketBase.WebSocketOperation.SendOperation(this)
						{
							BufferType = (WebSocketProtocolComponent.BufferType)2147483654U
						};
					}
				}
			}
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0006D10C File Offset: 0x0006B30C
		private void EnsureCloseOutputOperation()
		{
			if (this.m_CloseOutputOperation == null)
			{
				object thisLock = this.m_ThisLock;
				lock (thisLock)
				{
					if (this.m_CloseOutputOperation == null)
					{
						this.m_CloseOutputOperation = new WebSocketBase.WebSocketOperation.CloseOutputOperation(this);
					}
				}
			}
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0006D168 File Offset: 0x0006B368
		private static void ReleaseLock(object lockObject, ref bool lockTaken)
		{
			if (lockTaken)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					Monitor.Exit(lockObject);
					lockTaken = false;
				}
			}
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0006D19C File Offset: 0x0006B39C
		private static WebSocketProtocolComponent.BufferType GetBufferType(WebSocketMessageType messageType, bool endOfMessage)
		{
			if (messageType == WebSocketMessageType.Text)
			{
				if (endOfMessage)
				{
					return (WebSocketProtocolComponent.BufferType)2147483648U;
				}
				return (WebSocketProtocolComponent.BufferType)2147483649U;
			}
			else
			{
				if (endOfMessage)
				{
					return (WebSocketProtocolComponent.BufferType)2147483650U;
				}
				return (WebSocketProtocolComponent.BufferType)2147483651U;
			}
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0006D1C0 File Offset: 0x0006B3C0
		private static WebSocketMessageType GetMessageType(WebSocketProtocolComponent.BufferType bufferType)
		{
			switch (bufferType)
			{
			case (WebSocketProtocolComponent.BufferType)2147483648U:
			case (WebSocketProtocolComponent.BufferType)2147483649U:
				return WebSocketMessageType.Text;
			case (WebSocketProtocolComponent.BufferType)2147483650U:
			case (WebSocketProtocolComponent.BufferType)2147483651U:
				return WebSocketMessageType.Binary;
			case (WebSocketProtocolComponent.BufferType)2147483652U:
				return WebSocketMessageType.Close;
			default:
				throw new WebSocketException(WebSocketError.NativeError, SR.GetString("net_WebSockets_InvalidBufferType", new object[]
				{
					bufferType,
					(WebSocketProtocolComponent.BufferType)2147483652U,
					(WebSocketProtocolComponent.BufferType)2147483651U,
					(WebSocketProtocolComponent.BufferType)2147483650U,
					(WebSocketProtocolComponent.BufferType)2147483649U,
					(WebSocketProtocolComponent.BufferType)2147483648U
				}));
			}
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0006D255 File Offset: 0x0006B455
		internal void ValidateNativeBuffers(WebSocketProtocolComponent.Action action, WebSocketProtocolComponent.BufferType bufferType, WebSocketProtocolComponent.Buffer[] dataBuffers, uint dataBufferCount)
		{
			this.m_InternalBuffer.ValidateNativeBuffers(action, bufferType, dataBuffers, dataBufferCount);
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0006D268 File Offset: 0x0006B468
		internal void ThrowIfClosedOrAborted()
		{
			if (this.State == WebSocketState.Closed || this.State == WebSocketState.Aborted)
			{
				throw new WebSocketException(WebSocketError.InvalidState, SR.GetString("net_WebSockets_InvalidState_ClosedOrAborted", new object[]
				{
					base.GetType().FullName,
					this.State
				}));
			}
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0006D2BB File Offset: 0x0006B4BB
		private void ThrowIfAborted(bool aborted, Exception innerException)
		{
			if (aborted)
			{
				throw new WebSocketException(WebSocketError.InvalidState, SR.GetString("net_WebSockets_InvalidState_ClosedOrAborted", new object[]
				{
					base.GetType().FullName,
					WebSocketState.Aborted
				}), innerException);
			}
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0006D2F0 File Offset: 0x0006B4F0
		private bool CanHandleExceptionDuringClose(Exception error)
		{
			return this.State == WebSocketState.Closed && (error is OperationCanceledException || error is WebSocketException || error is SocketException || error is HttpListenerException || error is IOException);
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0006D328 File Offset: 0x0006B528
		private void ThrowIfConvertibleException(string methodName, Exception exception, CancellationToken cancellationToken, bool aborted)
		{
			if (WebSocketBase.s_LoggingEnabled && !string.IsNullOrEmpty(methodName))
			{
				Logging.Exception(Logging.WebSockets, this, methodName, exception);
			}
			OperationCanceledException ex = exception as OperationCanceledException;
			if (ex != null)
			{
				if (cancellationToken.IsCancellationRequested || !aborted)
				{
					return;
				}
				this.ThrowIfAborted(aborted, exception);
			}
			WebSocketException ex2 = exception as WebSocketException;
			if (ex2 != null)
			{
				cancellationToken.ThrowIfCancellationRequested();
				this.ThrowIfAborted(aborted, ex2);
				return;
			}
			SocketException ex3 = exception as SocketException;
			if (ex3 != null)
			{
				ex2 = new WebSocketException(ex3.NativeErrorCode, ex3);
			}
			HttpListenerException ex4 = exception as HttpListenerException;
			if (ex4 != null)
			{
				ex2 = new WebSocketException(ex4.ErrorCode, ex4);
			}
			IOException ex5 = exception as IOException;
			if (ex5 != null)
			{
				ex3 = exception.InnerException as SocketException;
				if (ex3 != null)
				{
					ex2 = new WebSocketException(ex3.NativeErrorCode, ex5);
				}
			}
			if (ex2 != null)
			{
				cancellationToken.ThrowIfCancellationRequested();
				this.ThrowIfAborted(aborted, ex2);
				throw ex2;
			}
			AggregateException ex6 = exception as AggregateException;
			if (ex6 != null)
			{
				ReadOnlyCollection<Exception> innerExceptions = ex6.Flatten().InnerExceptions;
				if (innerExceptions.Count == 0)
				{
					return;
				}
				foreach (Exception ex7 in innerExceptions)
				{
					this.ThrowIfConvertibleException(null, ex7, cancellationToken, aborted);
				}
			}
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0006D468 File Offset: 0x0006B668
		private void CleanUp()
		{
			if (this.m_CleanedUp)
			{
				return;
			}
			this.m_CleanedUp = true;
			if (this.SessionHandle != null)
			{
				this.SessionHandle.Dispose();
			}
			if (this.m_InternalBuffer != null)
			{
				this.m_InternalBuffer.Dispose(this.State);
			}
			if (this.m_ReceiveOutstandingOperationHelper != null)
			{
				this.m_ReceiveOutstandingOperationHelper.Dispose();
			}
			if (this.m_SendOutstandingOperationHelper != null)
			{
				this.m_SendOutstandingOperationHelper.Dispose();
			}
			if (this.m_CloseOutputOutstandingOperationHelper != null)
			{
				this.m_CloseOutputOutstandingOperationHelper.Dispose();
			}
			if (this.m_CloseOutstandingOperationHelper != null)
			{
				this.m_CloseOutstandingOperationHelper.Dispose();
			}
			if (this.m_InnerStream != null)
			{
				try
				{
					this.m_InnerStream.Close();
				}
				catch (ObjectDisposedException)
				{
				}
				catch (IOException)
				{
				}
				catch (SocketException)
				{
				}
				catch (HttpListenerException)
				{
				}
			}
			this.m_KeepAliveTracker.Dispose();
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0006D564 File Offset: 0x0006B764
		private void OnBackgroundTaskException(Exception exception)
		{
			if (Interlocked.CompareExchange<Exception>(ref this.m_PendingException, exception, null) == null)
			{
				if (WebSocketBase.s_LoggingEnabled)
				{
					Logging.Exception(Logging.WebSockets, this, "Fault", exception);
				}
				this.Abort();
			}
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0006D598 File Offset: 0x0006B798
		private void ThrowIfPendingException()
		{
			Exception ex = Interlocked.Exchange<Exception>(ref this.m_PendingException, null);
			if (ex != null)
			{
				throw new WebSocketException(WebSocketError.Faulted, ex);
			}
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0006D5BD File Offset: 0x0006B7BD
		private void ThrowIfDisposed()
		{
			if (this.m_IsDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0006D5DC File Offset: 0x0006B7DC
		private void UpdateReceiveState(int newReceiveState, int expectedReceiveState)
		{
			int num = Interlocked.Exchange(ref this.m_ReceiveState, newReceiveState);
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0006D5FC File Offset: 0x0006B7FC
		private bool StartOnCloseReceived(ref bool thisLockTaken)
		{
			this.ThrowIfDisposed();
			if (WebSocket.IsStateTerminal(this.State) || this.State == WebSocketState.CloseReceived)
			{
				return false;
			}
			Monitor.Enter(this.m_ThisLock, ref thisLockTaken);
			if (WebSocket.IsStateTerminal(this.State) || this.State == WebSocketState.CloseReceived)
			{
				return false;
			}
			if (this.State == WebSocketState.Open)
			{
				this.m_State = WebSocketState.CloseReceived;
				if (this.m_CloseReceivedTaskCompletionSource == null)
				{
					this.m_CloseReceivedTaskCompletionSource = new TaskCompletionSource<object>();
				}
				return false;
			}
			return true;
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0006D678 File Offset: 0x0006B878
		private void FinishOnCloseReceived(WebSocketCloseStatus closeStatus, string closeStatusDescription)
		{
			if (this.m_CloseReceivedTaskCompletionSource != null)
			{
				this.m_CloseReceivedTaskCompletionSource.TrySetResult(null);
			}
			this.m_CloseStatus = new WebSocketCloseStatus?(closeStatus);
			this.m_CloseStatusDescription = closeStatusDescription;
			if (WebSocketBase.s_LoggingEnabled)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "closeStatus: {0}, closeStatusDescription: {1}, m_State: {2}", new object[] { closeStatus, closeStatusDescription, this.m_State });
				Logging.PrintInfo(Logging.WebSockets, this, "FinishOnCloseReceived", text);
			}
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0006D700 File Offset: 0x0006B900
		private static async void OnKeepAlive(object sender)
		{
			WebSocketBase thisPtr = sender as WebSocketBase;
			bool lockTaken = false;
			if (WebSocketBase.s_LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, thisPtr, "OnKeepAlive", string.Empty);
			}
			CancellationToken linkedCancellationToken = CancellationToken.None;
			try
			{
				Monitor.Enter(thisPtr.SessionHandle, ref lockTaken);
				if (!thisPtr.m_IsDisposed && thisPtr.m_State == WebSocketState.Open && thisPtr.m_CloseOutputTask == null)
				{
					if (thisPtr.m_KeepAliveTracker.ShouldSendKeepAlive())
					{
						bool ownsCancellationTokenSource = false;
						try
						{
							ownsCancellationTokenSource = thisPtr.m_SendOutstandingOperationHelper.TryStartOperation(CancellationToken.None, out linkedCancellationToken);
							if (ownsCancellationTokenSource)
							{
								thisPtr.EnsureKeepAliveOperation();
								thisPtr.m_KeepAliveTask = thisPtr.m_KeepAliveOperation.Process(null, linkedCancellationToken);
								WebSocketBase.ReleaseLock(thisPtr.SessionHandle, ref lockTaken);
								await thisPtr.m_KeepAliveTask.SuppressContextFlow();
							}
						}
						finally
						{
							if (!lockTaken)
							{
								Monitor.Enter(thisPtr.SessionHandle, ref lockTaken);
							}
							thisPtr.m_SendOutstandingOperationHelper.CompleteOperation(ownsCancellationTokenSource);
							thisPtr.m_KeepAliveTask = null;
						}
						thisPtr.m_KeepAliveTracker.ResetTimer();
					}
				}
			}
			catch (Exception ex)
			{
				try
				{
					thisPtr.ThrowIfConvertibleException("OnKeepAlive", ex, CancellationToken.None, linkedCancellationToken.IsCancellationRequested);
					throw;
				}
				catch (Exception ex2)
				{
					thisPtr.OnBackgroundTaskException(ex2);
				}
			}
			finally
			{
				WebSocketBase.ReleaseLock(thisPtr.SessionHandle, ref lockTaken);
				if (WebSocketBase.s_LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, thisPtr, "OnKeepAlive", string.Empty);
				}
			}
		}

		// Token: 0x04001640 RID: 5696
		private static volatile bool s_LoggingEnabled;

		// Token: 0x04001641 RID: 5697
		private readonly WebSocketBase.OutstandingOperationHelper m_CloseOutstandingOperationHelper;

		// Token: 0x04001642 RID: 5698
		private readonly WebSocketBase.OutstandingOperationHelper m_CloseOutputOutstandingOperationHelper;

		// Token: 0x04001643 RID: 5699
		private readonly WebSocketBase.OutstandingOperationHelper m_ReceiveOutstandingOperationHelper;

		// Token: 0x04001644 RID: 5700
		private readonly WebSocketBase.OutstandingOperationHelper m_SendOutstandingOperationHelper;

		// Token: 0x04001645 RID: 5701
		private readonly Stream m_InnerStream;

		// Token: 0x04001646 RID: 5702
		private readonly WebSocketBase.IWebSocketStream m_InnerStreamAsWebSocketStream;

		// Token: 0x04001647 RID: 5703
		private readonly string m_SubProtocol;

		// Token: 0x04001648 RID: 5704
		private readonly SemaphoreSlim m_SendFrameThrottle;

		// Token: 0x04001649 RID: 5705
		private readonly object m_ThisLock;

		// Token: 0x0400164A RID: 5706
		private readonly WebSocketBuffer m_InternalBuffer;

		// Token: 0x0400164B RID: 5707
		private readonly WebSocketBase.KeepAliveTracker m_KeepAliveTracker;

		// Token: 0x0400164C RID: 5708
		private volatile bool m_CleanedUp;

		// Token: 0x0400164D RID: 5709
		private volatile TaskCompletionSource<object> m_CloseReceivedTaskCompletionSource;

		// Token: 0x0400164E RID: 5710
		private volatile Task m_CloseOutputTask;

		// Token: 0x0400164F RID: 5711
		private volatile bool m_IsDisposed;

		// Token: 0x04001650 RID: 5712
		private volatile Task m_CloseNetworkConnectionTask;

		// Token: 0x04001651 RID: 5713
		private volatile bool m_CloseAsyncStartedReceive;

		// Token: 0x04001652 RID: 5714
		private volatile WebSocketState m_State;

		// Token: 0x04001653 RID: 5715
		private volatile Task m_KeepAliveTask;

		// Token: 0x04001654 RID: 5716
		private volatile WebSocketBase.WebSocketOperation.ReceiveOperation m_ReceiveOperation;

		// Token: 0x04001655 RID: 5717
		private volatile WebSocketBase.WebSocketOperation.SendOperation m_SendOperation;

		// Token: 0x04001656 RID: 5718
		private volatile WebSocketBase.WebSocketOperation.SendOperation m_KeepAliveOperation;

		// Token: 0x04001657 RID: 5719
		private volatile WebSocketBase.WebSocketOperation.CloseOutputOperation m_CloseOutputOperation;

		// Token: 0x04001658 RID: 5720
		private WebSocketCloseStatus? m_CloseStatus;

		// Token: 0x04001659 RID: 5721
		private string m_CloseStatusDescription;

		// Token: 0x0400165A RID: 5722
		private int m_ReceiveState;

		// Token: 0x0400165B RID: 5723
		private Exception m_PendingException;

		// Token: 0x0200076B RID: 1899
		private abstract class WebSocketOperation
		{
			// Token: 0x17000F22 RID: 3874
			// (get) Token: 0x06004238 RID: 16952 RVA: 0x00112A16 File Offset: 0x00110C16
			// (set) Token: 0x06004239 RID: 16953 RVA: 0x00112A1E File Offset: 0x00110C1E
			protected bool AsyncOperationCompleted { get; set; }

			// Token: 0x0600423A RID: 16954 RVA: 0x00112A27 File Offset: 0x00110C27
			internal WebSocketOperation(WebSocketBase webSocket)
			{
				this.m_WebSocket = webSocket;
				this.AsyncOperationCompleted = false;
			}

			// Token: 0x17000F23 RID: 3875
			// (get) Token: 0x0600423B RID: 16955 RVA: 0x00112A3D File Offset: 0x00110C3D
			// (set) Token: 0x0600423C RID: 16956 RVA: 0x00112A45 File Offset: 0x00110C45
			public WebSocketReceiveResult ReceiveResult { get; protected set; }

			// Token: 0x17000F24 RID: 3876
			// (get) Token: 0x0600423D RID: 16957
			protected abstract int BufferCount { get; }

			// Token: 0x17000F25 RID: 3877
			// (get) Token: 0x0600423E RID: 16958
			protected abstract WebSocketProtocolComponent.ActionQueue ActionQueue { get; }

			// Token: 0x0600423F RID: 16959
			protected abstract void Initialize(ArraySegment<byte>? buffer, CancellationToken cancellationToken);

			// Token: 0x06004240 RID: 16960
			protected abstract bool ShouldContinue(CancellationToken cancellationToken);

			// Token: 0x06004241 RID: 16961
			protected abstract bool ProcessAction_NoAction();

			// Token: 0x06004242 RID: 16962 RVA: 0x00112A4E File Offset: 0x00110C4E
			protected virtual void ProcessAction_IndicateReceiveComplete(ArraySegment<byte>? buffer, WebSocketProtocolComponent.BufferType bufferType, WebSocketProtocolComponent.Action action, WebSocketProtocolComponent.Buffer[] dataBuffers, uint dataBufferCount, IntPtr actionContext)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06004243 RID: 16963
			protected abstract void Cleanup();

			// Token: 0x06004244 RID: 16964 RVA: 0x00112A58 File Offset: 0x00110C58
			internal async Task<WebSocketReceiveResult> Process(ArraySegment<byte>? buffer, CancellationToken cancellationToken)
			{
				bool sessionHandleLockTaken = false;
				this.AsyncOperationCompleted = false;
				this.ReceiveResult = null;
				try
				{
					Monitor.Enter(this.m_WebSocket.SessionHandle, ref sessionHandleLockTaken);
					this.m_WebSocket.ThrowIfPendingException();
					this.Initialize(buffer, cancellationToken);
					while (this.ShouldContinue(cancellationToken))
					{
						bool completed = false;
						while (!completed)
						{
							WebSocketProtocolComponent.Buffer[] array = new WebSocketProtocolComponent.Buffer[this.BufferCount];
							uint bufferCount = (uint)this.BufferCount;
							this.m_WebSocket.ThrowIfDisposed();
							WebSocketProtocolComponent.Action action;
							WebSocketProtocolComponent.BufferType bufferType;
							IntPtr actionContext;
							WebSocketProtocolComponent.WebSocketGetAction(this.m_WebSocket, this.ActionQueue, array, ref bufferCount, out action, out bufferType, out actionContext);
							switch (action)
							{
							case WebSocketProtocolComponent.Action.NoAction:
								if (this.ProcessAction_NoAction())
								{
									bool thisLockTaken = false;
									try
									{
										if (this.m_WebSocket.StartOnCloseReceived(ref thisLockTaken))
										{
											WebSocketBase.ReleaseLock(this.m_WebSocket.SessionHandle, ref sessionHandleLockTaken);
											bool flag = false;
											try
											{
												bool flag2 = await this.m_WebSocket.StartOnCloseCompleted(thisLockTaken, sessionHandleLockTaken, cancellationToken).SuppressContextFlow<bool>();
												flag = flag2;
											}
											catch (Exception)
											{
												this.m_WebSocket.ResetFlagAndTakeLock(this.m_WebSocket.m_ThisLock, ref thisLockTaken);
												throw;
											}
											if (flag)
											{
												this.m_WebSocket.ResetFlagAndTakeLock(this.m_WebSocket.m_ThisLock, ref thisLockTaken);
												this.m_WebSocket.FinishOnCloseCompleted();
											}
										}
										this.m_WebSocket.FinishOnCloseReceived(this.ReceiveResult.CloseStatus.Value, this.ReceiveResult.CloseStatusDescription);
									}
									finally
									{
										if (thisLockTaken)
										{
											WebSocketBase.ReleaseLock(this.m_WebSocket.m_ThisLock, ref thisLockTaken);
										}
									}
								}
								completed = true;
								continue;
							case WebSocketProtocolComponent.Action.SendToNetwork:
							{
								int bytesSent = 0;
								try
								{
									if (this.m_WebSocket.State != WebSocketState.CloseSent || (bufferType != (WebSocketProtocolComponent.BufferType)2147483653U && bufferType != (WebSocketProtocolComponent.BufferType)2147483654U))
									{
										if (bufferCount != 0U)
										{
											List<ArraySegment<byte>> list = new List<ArraySegment<byte>>((int)bufferCount);
											int sendBufferSize = 0;
											ArraySegment<byte> arraySegment = this.m_WebSocket.m_InternalBuffer.ConvertNativeBuffer(action, array[0], bufferType);
											list.Add(arraySegment);
											sendBufferSize += arraySegment.Count;
											if (bufferCount == 2U)
											{
												ArraySegment<byte> arraySegment2 = ((!this.m_WebSocket.m_InternalBuffer.IsPinnedSendPayloadBuffer(array[1], bufferType)) ? this.m_WebSocket.m_InternalBuffer.ConvertNativeBuffer(action, array[1], bufferType) : this.m_WebSocket.m_InternalBuffer.ConvertPinnedSendPayloadFromNative(array[1], bufferType));
												list.Add(arraySegment2);
												sendBufferSize += arraySegment2.Count;
											}
											WebSocketBase.ReleaseLock(this.m_WebSocket.SessionHandle, ref sessionHandleLockTaken);
											WebSocketHelpers.ThrowIfConnectionAborted(this.m_WebSocket.m_InnerStream, false);
											await this.m_WebSocket.SendFrameAsync(list, cancellationToken).SuppressContextFlow();
											Monitor.Enter(this.m_WebSocket.SessionHandle, ref sessionHandleLockTaken);
											this.m_WebSocket.ThrowIfPendingException();
											bytesSent += sendBufferSize;
											this.m_WebSocket.m_KeepAliveTracker.OnDataSent();
										}
									}
									continue;
								}
								finally
								{
									WebSocketProtocolComponent.WebSocketCompleteAction(this.m_WebSocket, actionContext, bytesSent);
								}
								goto IL_693;
							}
							case WebSocketProtocolComponent.Action.IndicateSendComplete:
								break;
							case WebSocketProtocolComponent.Action.ReceiveFromNetwork:
							{
								int count = 0;
								try
								{
									ArraySegment<byte> arraySegment3 = this.m_WebSocket.m_InternalBuffer.ConvertNativeBuffer(action, array[0], bufferType);
									WebSocketBase.ReleaseLock(this.m_WebSocket.SessionHandle, ref sessionHandleLockTaken);
									WebSocketHelpers.ThrowIfConnectionAborted(this.m_WebSocket.m_InnerStream, true);
									try
									{
										count = await this.m_WebSocket.m_InnerStream.ReadAsync(arraySegment3.Array, arraySegment3.Offset, arraySegment3.Count, cancellationToken).SuppressContextFlow<int>();
										this.m_WebSocket.m_KeepAliveTracker.OnDataReceived();
									}
									catch (ObjectDisposedException ex)
									{
										throw new WebSocketException(WebSocketError.ConnectionClosedPrematurely, ex);
									}
									catch (NotSupportedException ex2)
									{
										throw new WebSocketException(WebSocketError.ConnectionClosedPrematurely, ex2);
									}
									Monitor.Enter(this.m_WebSocket.SessionHandle, ref sessionHandleLockTaken);
									this.m_WebSocket.ThrowIfPendingException();
									if (count == 0)
									{
										throw new WebSocketException(WebSocketError.ConnectionClosedPrematurely);
									}
									continue;
								}
								finally
								{
									WebSocketProtocolComponent.WebSocketCompleteAction(this.m_WebSocket, actionContext, count);
								}
								break;
							}
							case WebSocketProtocolComponent.Action.IndicateReceiveComplete:
								this.ProcessAction_IndicateReceiveComplete(buffer, bufferType, action, array, bufferCount, actionContext);
								continue;
							default:
								goto IL_693;
							}
							WebSocketProtocolComponent.WebSocketCompleteAction(this.m_WebSocket, actionContext, 0);
							this.AsyncOperationCompleted = true;
							WebSocketBase.ReleaseLock(this.m_WebSocket.SessionHandle, ref sessionHandleLockTaken);
							await this.m_WebSocket.m_InnerStream.FlushAsync().SuppressContextFlow();
							Monitor.Enter(this.m_WebSocket.SessionHandle, ref sessionHandleLockTaken);
							continue;
							IL_693:
							string text = string.Format(CultureInfo.InvariantCulture, "Invalid action '{0}' returned from WebSocketGetAction.", new object[] { action });
							throw new InvalidOperationException();
						}
						WebSocketBase.ReleaseLock(this.m_WebSocket.SessionHandle, ref sessionHandleLockTaken);
						Monitor.Enter(this.m_WebSocket.SessionHandle, ref sessionHandleLockTaken);
					}
				}
				finally
				{
					this.Cleanup();
					WebSocketBase.ReleaseLock(this.m_WebSocket.SessionHandle, ref sessionHandleLockTaken);
				}
				return this.ReceiveResult;
			}

			// Token: 0x04003249 RID: 12873
			private readonly WebSocketBase m_WebSocket;

			// Token: 0x02000916 RID: 2326
			public class ReceiveOperation : WebSocketBase.WebSocketOperation
			{
				// Token: 0x06004622 RID: 17954 RVA: 0x001249F8 File Offset: 0x00122BF8
				public ReceiveOperation(WebSocketBase webSocket)
					: base(webSocket)
				{
				}

				// Token: 0x17000FD6 RID: 4054
				// (get) Token: 0x06004623 RID: 17955 RVA: 0x00124A01 File Offset: 0x00122C01
				protected override WebSocketProtocolComponent.ActionQueue ActionQueue
				{
					get
					{
						return WebSocketProtocolComponent.ActionQueue.Receive;
					}
				}

				// Token: 0x17000FD7 RID: 4055
				// (get) Token: 0x06004624 RID: 17956 RVA: 0x00124A04 File Offset: 0x00122C04
				protected override int BufferCount
				{
					get
					{
						return 1;
					}
				}

				// Token: 0x06004625 RID: 17957 RVA: 0x00124A08 File Offset: 0x00122C08
				protected override void Initialize(ArraySegment<byte>? buffer, CancellationToken cancellationToken)
				{
					this.m_PongReceived = false;
					this.m_ReceiveCompleted = false;
					this.m_WebSocket.ThrowIfDisposed();
					switch (Interlocked.CompareExchange(ref this.m_WebSocket.m_ReceiveState, 1, 0))
					{
					case 0:
						this.m_ReceiveState = 1;
						return;
					case 1:
						break;
					case 2:
					{
						WebSocketReceiveResult webSocketReceiveResult;
						if (!this.m_WebSocket.m_InternalBuffer.ReceiveFromBufferedPayload(buffer.Value, out webSocketReceiveResult))
						{
							this.m_WebSocket.UpdateReceiveState(0, 2);
						}
						base.ReceiveResult = webSocketReceiveResult;
						this.m_ReceiveCompleted = true;
						break;
					}
					default:
						return;
					}
				}

				// Token: 0x06004626 RID: 17958 RVA: 0x00124A92 File Offset: 0x00122C92
				protected override void Cleanup()
				{
				}

				// Token: 0x06004627 RID: 17959 RVA: 0x00124A94 File Offset: 0x00122C94
				protected override bool ShouldContinue(CancellationToken cancellationToken)
				{
					cancellationToken.ThrowIfCancellationRequested();
					if (this.m_ReceiveCompleted)
					{
						return false;
					}
					this.m_WebSocket.ThrowIfDisposed();
					this.m_WebSocket.ThrowIfPendingException();
					WebSocketProtocolComponent.WebSocketReceive(this.m_WebSocket);
					return true;
				}

				// Token: 0x06004628 RID: 17960 RVA: 0x00124AC9 File Offset: 0x00122CC9
				protected override bool ProcessAction_NoAction()
				{
					if (this.m_PongReceived)
					{
						this.m_ReceiveCompleted = false;
						this.m_PongReceived = false;
						return false;
					}
					this.m_ReceiveCompleted = true;
					return base.ReceiveResult.MessageType == WebSocketMessageType.Close;
				}

				// Token: 0x06004629 RID: 17961 RVA: 0x00124AFC File Offset: 0x00122CFC
				protected override void ProcessAction_IndicateReceiveComplete(ArraySegment<byte>? buffer, WebSocketProtocolComponent.BufferType bufferType, WebSocketProtocolComponent.Action action, WebSocketProtocolComponent.Buffer[] dataBuffers, uint dataBufferCount, IntPtr actionContext)
				{
					int num = 0;
					this.m_PongReceived = false;
					if (bufferType == (WebSocketProtocolComponent.BufferType)2147483653U)
					{
						this.m_PongReceived = true;
						WebSocketProtocolComponent.WebSocketCompleteAction(this.m_WebSocket, actionContext, num);
						return;
					}
					WebSocketReceiveResult webSocketReceiveResult;
					try
					{
						WebSocketMessageType messageType = WebSocketBase.GetMessageType(bufferType);
						int num2 = 0;
						if (bufferType == (WebSocketProtocolComponent.BufferType)2147483652U)
						{
							ArraySegment<byte> arraySegment = WebSocketHelpers.EmptyPayload;
							WebSocketCloseStatus webSocketCloseStatus;
							string text;
							this.m_WebSocket.m_InternalBuffer.ConvertCloseBuffer(action, dataBuffers[0], out webSocketCloseStatus, out text);
							webSocketReceiveResult = new WebSocketReceiveResult(num, messageType, true, new WebSocketCloseStatus?(webSocketCloseStatus), text);
						}
						else
						{
							ArraySegment<byte> arraySegment = this.m_WebSocket.m_InternalBuffer.ConvertNativeBuffer(action, dataBuffers[0], bufferType);
							bool flag = bufferType == (WebSocketProtocolComponent.BufferType)2147483650U || bufferType == (WebSocketProtocolComponent.BufferType)2147483648U || bufferType == (WebSocketProtocolComponent.BufferType)2147483652U;
							if (arraySegment.Count > buffer.Value.Count)
							{
								this.m_WebSocket.m_InternalBuffer.BufferPayload(arraySegment, buffer.Value.Count, messageType, flag);
								num2 = 2;
								flag = false;
							}
							num = Math.Min(arraySegment.Count, buffer.Value.Count);
							if (num > 0)
							{
								Buffer.BlockCopy(arraySegment.Array, arraySegment.Offset, buffer.Value.Array, buffer.Value.Offset, num);
							}
							webSocketReceiveResult = new WebSocketReceiveResult(num, messageType, flag);
						}
						this.m_WebSocket.UpdateReceiveState(num2, this.m_ReceiveState);
					}
					finally
					{
						WebSocketProtocolComponent.WebSocketCompleteAction(this.m_WebSocket, actionContext, num);
					}
					base.ReceiveResult = webSocketReceiveResult;
				}

				// Token: 0x04003D60 RID: 15712
				private int m_ReceiveState;

				// Token: 0x04003D61 RID: 15713
				private bool m_PongReceived;

				// Token: 0x04003D62 RID: 15714
				private bool m_ReceiveCompleted;
			}

			// Token: 0x02000917 RID: 2327
			public class SendOperation : WebSocketBase.WebSocketOperation
			{
				// Token: 0x0600462A RID: 17962 RVA: 0x00124CA0 File Offset: 0x00122EA0
				public SendOperation(WebSocketBase webSocket)
					: base(webSocket)
				{
				}

				// Token: 0x17000FD8 RID: 4056
				// (get) Token: 0x0600462B RID: 17963 RVA: 0x00124CA9 File Offset: 0x00122EA9
				protected override WebSocketProtocolComponent.ActionQueue ActionQueue
				{
					get
					{
						return WebSocketProtocolComponent.ActionQueue.Send;
					}
				}

				// Token: 0x17000FD9 RID: 4057
				// (get) Token: 0x0600462C RID: 17964 RVA: 0x00124CAC File Offset: 0x00122EAC
				protected override int BufferCount
				{
					get
					{
						return 2;
					}
				}

				// Token: 0x0600462D RID: 17965 RVA: 0x00124CB0 File Offset: 0x00122EB0
				protected virtual WebSocketProtocolComponent.Buffer? CreateBuffer(ArraySegment<byte>? buffer)
				{
					if (buffer == null)
					{
						return null;
					}
					WebSocketProtocolComponent.Buffer buffer2 = default(WebSocketProtocolComponent.Buffer);
					this.m_WebSocket.m_InternalBuffer.PinSendBuffer(buffer.Value, out this.m_BufferHasBeenPinned);
					buffer2.Data.BufferData = this.m_WebSocket.m_InternalBuffer.ConvertPinnedSendPayloadToNative(buffer.Value);
					buffer2.Data.BufferLength = (uint)buffer.Value.Count;
					return new WebSocketProtocolComponent.Buffer?(buffer2);
				}

				// Token: 0x0600462E RID: 17966 RVA: 0x00124D39 File Offset: 0x00122F39
				protected override bool ProcessAction_NoAction()
				{
					return false;
				}

				// Token: 0x0600462F RID: 17967 RVA: 0x00124D3C File Offset: 0x00122F3C
				protected override void Cleanup()
				{
					if (this.m_BufferHasBeenPinned)
					{
						this.m_BufferHasBeenPinned = false;
						this.m_WebSocket.m_InternalBuffer.ReleasePinnedSendBuffer();
					}
				}

				// Token: 0x17000FDA RID: 4058
				// (get) Token: 0x06004630 RID: 17968 RVA: 0x00124D5D File Offset: 0x00122F5D
				// (set) Token: 0x06004631 RID: 17969 RVA: 0x00124D65 File Offset: 0x00122F65
				internal WebSocketProtocolComponent.BufferType BufferType { get; set; }

				// Token: 0x06004632 RID: 17970 RVA: 0x00124D70 File Offset: 0x00122F70
				protected override void Initialize(ArraySegment<byte>? buffer, CancellationToken cancellationToken)
				{
					this.m_WebSocket.ThrowIfDisposed();
					this.m_WebSocket.ThrowIfPendingException();
					WebSocketProtocolComponent.Buffer? buffer2 = this.CreateBuffer(buffer);
					if (buffer2 != null)
					{
						WebSocketProtocolComponent.WebSocketSend(this.m_WebSocket, this.BufferType, buffer2.Value);
						return;
					}
					WebSocketProtocolComponent.WebSocketSendWithoutBody(this.m_WebSocket, this.BufferType);
				}

				// Token: 0x06004633 RID: 17971 RVA: 0x00124DCE File Offset: 0x00122FCE
				protected override bool ShouldContinue(CancellationToken cancellationToken)
				{
					if (base.AsyncOperationCompleted)
					{
						return false;
					}
					cancellationToken.ThrowIfCancellationRequested();
					return true;
				}

				// Token: 0x04003D63 RID: 15715
				protected bool m_BufferHasBeenPinned;
			}

			// Token: 0x02000918 RID: 2328
			public class CloseOutputOperation : WebSocketBase.WebSocketOperation.SendOperation
			{
				// Token: 0x06004634 RID: 17972 RVA: 0x00124DE2 File Offset: 0x00122FE2
				public CloseOutputOperation(WebSocketBase webSocket)
					: base(webSocket)
				{
					base.BufferType = (WebSocketProtocolComponent.BufferType)2147483652U;
				}

				// Token: 0x17000FDB RID: 4059
				// (get) Token: 0x06004635 RID: 17973 RVA: 0x00124DF6 File Offset: 0x00122FF6
				// (set) Token: 0x06004636 RID: 17974 RVA: 0x00124DFE File Offset: 0x00122FFE
				internal WebSocketCloseStatus CloseStatus { get; set; }

				// Token: 0x17000FDC RID: 4060
				// (get) Token: 0x06004637 RID: 17975 RVA: 0x00124E07 File Offset: 0x00123007
				// (set) Token: 0x06004638 RID: 17976 RVA: 0x00124E0F File Offset: 0x0012300F
				internal string CloseReason { get; set; }

				// Token: 0x06004639 RID: 17977 RVA: 0x00124E18 File Offset: 0x00123018
				protected override WebSocketProtocolComponent.Buffer? CreateBuffer(ArraySegment<byte>? buffer)
				{
					this.m_WebSocket.ThrowIfDisposed();
					this.m_WebSocket.ThrowIfPendingException();
					if (this.CloseStatus == WebSocketCloseStatus.Empty)
					{
						return null;
					}
					WebSocketProtocolComponent.Buffer buffer2 = default(WebSocketProtocolComponent.Buffer);
					if (this.CloseReason != null)
					{
						byte[] bytes = Encoding.UTF8.GetBytes(this.CloseReason);
						ArraySegment<byte> arraySegment = new ArraySegment<byte>(bytes, 0, Math.Min(123, bytes.Length));
						this.m_WebSocket.m_InternalBuffer.PinSendBuffer(arraySegment, out this.m_BufferHasBeenPinned);
						buffer2.CloseStatus.ReasonData = this.m_WebSocket.m_InternalBuffer.ConvertPinnedSendPayloadToNative(arraySegment);
						buffer2.CloseStatus.ReasonLength = (uint)arraySegment.Count;
					}
					buffer2.CloseStatus.CloseStatus = (ushort)this.CloseStatus;
					return new WebSocketProtocolComponent.Buffer?(buffer2);
				}
			}
		}

		// Token: 0x0200076C RID: 1900
		private abstract class KeepAliveTracker : IDisposable
		{
			// Token: 0x06004245 RID: 16965
			public abstract void OnDataReceived();

			// Token: 0x06004246 RID: 16966
			public abstract void OnDataSent();

			// Token: 0x06004247 RID: 16967
			public abstract void Dispose();

			// Token: 0x06004248 RID: 16968
			public abstract void StartTimer(WebSocketBase webSocket);

			// Token: 0x06004249 RID: 16969
			public abstract void ResetTimer();

			// Token: 0x0600424A RID: 16970
			public abstract bool ShouldSendKeepAlive();

			// Token: 0x0600424B RID: 16971 RVA: 0x00112AAB File Offset: 0x00110CAB
			public static WebSocketBase.KeepAliveTracker Create(TimeSpan keepAliveInterval)
			{
				if ((int)keepAliveInterval.TotalMilliseconds > 0)
				{
					return new WebSocketBase.KeepAliveTracker.DefaultKeepAliveTracker(keepAliveInterval);
				}
				return new WebSocketBase.KeepAliveTracker.DisabledKeepAliveTracker();
			}

			// Token: 0x0200091A RID: 2330
			private class DisabledKeepAliveTracker : WebSocketBase.KeepAliveTracker
			{
				// Token: 0x0600463C RID: 17980 RVA: 0x00125722 File Offset: 0x00123922
				public override void OnDataReceived()
				{
				}

				// Token: 0x0600463D RID: 17981 RVA: 0x00125724 File Offset: 0x00123924
				public override void OnDataSent()
				{
				}

				// Token: 0x0600463E RID: 17982 RVA: 0x00125726 File Offset: 0x00123926
				public override void ResetTimer()
				{
				}

				// Token: 0x0600463F RID: 17983 RVA: 0x00125728 File Offset: 0x00123928
				public override void StartTimer(WebSocketBase webSocket)
				{
				}

				// Token: 0x06004640 RID: 17984 RVA: 0x0012572A File Offset: 0x0012392A
				public override bool ShouldSendKeepAlive()
				{
					return false;
				}

				// Token: 0x06004641 RID: 17985 RVA: 0x0012572D File Offset: 0x0012392D
				public override void Dispose()
				{
				}
			}

			// Token: 0x0200091B RID: 2331
			private class DefaultKeepAliveTracker : WebSocketBase.KeepAliveTracker
			{
				// Token: 0x06004643 RID: 17987 RVA: 0x00125737 File Offset: 0x00123937
				public DefaultKeepAliveTracker(TimeSpan keepAliveInterval)
				{
					this.m_KeepAliveInterval = keepAliveInterval;
					this.m_LastSendActivity = new Stopwatch();
					this.m_LastReceiveActivity = new Stopwatch();
				}

				// Token: 0x06004644 RID: 17988 RVA: 0x0012575C File Offset: 0x0012395C
				public override void OnDataReceived()
				{
					this.m_LastReceiveActivity.Restart();
				}

				// Token: 0x06004645 RID: 17989 RVA: 0x00125769 File Offset: 0x00123969
				public override void OnDataSent()
				{
					this.m_LastSendActivity.Restart();
				}

				// Token: 0x06004646 RID: 17990 RVA: 0x00125778 File Offset: 0x00123978
				public override void ResetTimer()
				{
					this.ResetTimer((int)this.m_KeepAliveInterval.TotalMilliseconds);
				}

				// Token: 0x06004647 RID: 17991 RVA: 0x0012579C File Offset: 0x0012399C
				public override void StartTimer(WebSocketBase webSocket)
				{
					int num = (int)this.m_KeepAliveInterval.TotalMilliseconds;
					if (ExecutionContext.IsFlowSuppressed())
					{
						this.m_KeepAliveTimer = new Timer(WebSocketBase.KeepAliveTracker.DefaultKeepAliveTracker.s_KeepAliveTimerElapsedCallback, webSocket, -1, -1);
						this.m_KeepAliveTimer.Change(num, -1);
						return;
					}
					using (ExecutionContext.SuppressFlow())
					{
						this.m_KeepAliveTimer = new Timer(WebSocketBase.KeepAliveTracker.DefaultKeepAliveTracker.s_KeepAliveTimerElapsedCallback, webSocket, -1, -1);
						this.m_KeepAliveTimer.Change(num, -1);
					}
				}

				// Token: 0x06004648 RID: 17992 RVA: 0x0012582C File Offset: 0x00123A2C
				public override bool ShouldSendKeepAlive()
				{
					TimeSpan idleTime = this.GetIdleTime();
					if (idleTime >= this.m_KeepAliveInterval)
					{
						return true;
					}
					this.ResetTimer((int)(this.m_KeepAliveInterval - idleTime).TotalMilliseconds);
					return false;
				}

				// Token: 0x06004649 RID: 17993 RVA: 0x0012586C File Offset: 0x00123A6C
				public override void Dispose()
				{
					this.m_KeepAliveTimer.Dispose();
				}

				// Token: 0x0600464A RID: 17994 RVA: 0x00125879 File Offset: 0x00123A79
				private void ResetTimer(int dueInMilliseconds)
				{
					this.m_KeepAliveTimer.Change(dueInMilliseconds, -1);
				}

				// Token: 0x0600464B RID: 17995 RVA: 0x0012588C File Offset: 0x00123A8C
				private TimeSpan GetIdleTime()
				{
					TimeSpan timeElapsed = this.GetTimeElapsed(this.m_LastSendActivity);
					TimeSpan timeElapsed2 = this.GetTimeElapsed(this.m_LastReceiveActivity);
					if (timeElapsed2 < timeElapsed)
					{
						return timeElapsed2;
					}
					return timeElapsed;
				}

				// Token: 0x0600464C RID: 17996 RVA: 0x001258BF File Offset: 0x00123ABF
				private TimeSpan GetTimeElapsed(Stopwatch watch)
				{
					if (watch.IsRunning)
					{
						return watch.Elapsed;
					}
					return this.m_KeepAliveInterval;
				}

				// Token: 0x04003D76 RID: 15734
				private static readonly TimerCallback s_KeepAliveTimerElapsedCallback = new TimerCallback(WebSocketBase.OnKeepAlive);

				// Token: 0x04003D77 RID: 15735
				private readonly TimeSpan m_KeepAliveInterval;

				// Token: 0x04003D78 RID: 15736
				private readonly Stopwatch m_LastSendActivity;

				// Token: 0x04003D79 RID: 15737
				private readonly Stopwatch m_LastReceiveActivity;

				// Token: 0x04003D7A RID: 15738
				private Timer m_KeepAliveTimer;
			}
		}

		// Token: 0x0200076D RID: 1901
		private class OutstandingOperationHelper : IDisposable
		{
			// Token: 0x0600424D RID: 16973 RVA: 0x00112ACC File Offset: 0x00110CCC
			public bool TryStartOperation(CancellationToken userCancellationToken, out CancellationToken linkedCancellationToken)
			{
				linkedCancellationToken = CancellationToken.None;
				this.ThrowIfDisposed();
				object thisLock = this.m_ThisLock;
				bool flag2;
				lock (thisLock)
				{
					int num = this.m_OperationsOutstanding + 1;
					this.m_OperationsOutstanding = num;
					int num2 = num;
					if (num2 == 1)
					{
						linkedCancellationToken = this.CreateLinkedCancellationToken(userCancellationToken);
						flag2 = true;
					}
					else
					{
						flag2 = false;
					}
				}
				return flag2;
			}

			// Token: 0x0600424E RID: 16974 RVA: 0x00112B48 File Offset: 0x00110D48
			public void CompleteOperation(bool ownsCancellationTokenSource)
			{
				if (this.m_IsDisposed)
				{
					return;
				}
				CancellationTokenSource cancellationTokenSource = null;
				object thisLock = this.m_ThisLock;
				lock (thisLock)
				{
					this.m_OperationsOutstanding--;
					if (ownsCancellationTokenSource)
					{
						cancellationTokenSource = this.m_CancellationTokenSource;
						this.m_CancellationTokenSource = null;
					}
				}
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Dispose();
				}
			}

			// Token: 0x0600424F RID: 16975 RVA: 0x00112BC0 File Offset: 0x00110DC0
			private CancellationToken CreateLinkedCancellationToken(CancellationToken cancellationToken)
			{
				CancellationTokenSource cancellationTokenSource;
				if (cancellationToken == CancellationToken.None)
				{
					cancellationTokenSource = new CancellationTokenSource();
				}
				else
				{
					cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, new CancellationTokenSource().Token);
				}
				this.m_CancellationTokenSource = cancellationTokenSource;
				return cancellationTokenSource.Token;
			}

			// Token: 0x06004250 RID: 16976 RVA: 0x00112C04 File Offset: 0x00110E04
			public void CancelIO()
			{
				CancellationTokenSource cancellationTokenSource = null;
				object thisLock = this.m_ThisLock;
				lock (thisLock)
				{
					if (this.m_OperationsOutstanding == 0)
					{
						return;
					}
					cancellationTokenSource = this.m_CancellationTokenSource;
				}
				if (cancellationTokenSource != null)
				{
					try
					{
						cancellationTokenSource.Cancel();
					}
					catch (ObjectDisposedException)
					{
					}
				}
			}

			// Token: 0x06004251 RID: 16977 RVA: 0x00112C70 File Offset: 0x00110E70
			public void Dispose()
			{
				if (this.m_IsDisposed)
				{
					return;
				}
				CancellationTokenSource cancellationTokenSource = null;
				object thisLock = this.m_ThisLock;
				lock (thisLock)
				{
					if (this.m_IsDisposed)
					{
						return;
					}
					this.m_IsDisposed = true;
					cancellationTokenSource = this.m_CancellationTokenSource;
					this.m_CancellationTokenSource = null;
				}
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Dispose();
				}
			}

			// Token: 0x06004252 RID: 16978 RVA: 0x00112CE8 File Offset: 0x00110EE8
			private void ThrowIfDisposed()
			{
				if (this.m_IsDisposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
			}

			// Token: 0x0400324B RID: 12875
			private volatile int m_OperationsOutstanding;

			// Token: 0x0400324C RID: 12876
			private volatile CancellationTokenSource m_CancellationTokenSource;

			// Token: 0x0400324D RID: 12877
			private volatile bool m_IsDisposed;

			// Token: 0x0400324E RID: 12878
			private readonly object m_ThisLock = new object();
		}

		// Token: 0x0200076E RID: 1902
		internal interface IWebSocketStream
		{
			// Token: 0x06004254 RID: 16980
			void SwitchToOpaqueMode(WebSocketBase webSocket);

			// Token: 0x06004255 RID: 16981
			void Abort();

			// Token: 0x17000F26 RID: 3878
			// (get) Token: 0x06004256 RID: 16982
			bool SupportsMultipleWrite { get; }

			// Token: 0x06004257 RID: 16983
			Task MultipleWriteAsync(IList<ArraySegment<byte>> buffers, CancellationToken cancellationToken);

			// Token: 0x06004258 RID: 16984
			Task CloseNetworkConnectionAsync(CancellationToken cancellationToken);
		}

		// Token: 0x0200076F RID: 1903
		private static class ReceiveState
		{
			// Token: 0x0400324F RID: 12879
			internal const int SendOperation = -1;

			// Token: 0x04003250 RID: 12880
			internal const int Idle = 0;

			// Token: 0x04003251 RID: 12881
			internal const int Application = 1;

			// Token: 0x04003252 RID: 12882
			internal const int PayloadAvailable = 2;
		}

		// Token: 0x02000770 RID: 1904
		internal static class Methods
		{
			// Token: 0x04003253 RID: 12883
			internal const string ReceiveAsync = "ReceiveAsync";

			// Token: 0x04003254 RID: 12884
			internal const string SendAsync = "SendAsync";

			// Token: 0x04003255 RID: 12885
			internal const string CloseAsync = "CloseAsync";

			// Token: 0x04003256 RID: 12886
			internal const string CloseOutputAsync = "CloseOutputAsync";

			// Token: 0x04003257 RID: 12887
			internal const string Abort = "Abort";

			// Token: 0x04003258 RID: 12888
			internal const string Initialize = "Initialize";

			// Token: 0x04003259 RID: 12889
			internal const string Fault = "Fault";

			// Token: 0x0400325A RID: 12890
			internal const string StartOnCloseCompleted = "StartOnCloseCompleted";

			// Token: 0x0400325B RID: 12891
			internal const string FinishOnCloseReceived = "FinishOnCloseReceived";

			// Token: 0x0400325C RID: 12892
			internal const string OnKeepAlive = "OnKeepAlive";
		}
	}
}
