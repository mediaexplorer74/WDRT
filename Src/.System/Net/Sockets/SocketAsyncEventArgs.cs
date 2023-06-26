using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Net.Sockets
{
	/// <summary>Represents an asynchronous socket operation.</summary>
	// Token: 0x0200037C RID: 892
	public class SocketAsyncEventArgs : EventArgs, IDisposable
	{
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x0600212B RID: 8491 RVA: 0x0009EEE0 File Offset: 0x0009D0E0
		// (remove) Token: 0x0600212C RID: 8492 RVA: 0x0009EF18 File Offset: 0x0009D118
		private event EventHandler<SocketAsyncEventArgs> m_Completed;

		/// <summary>Gets or sets the protocol to use to download the socket client access policy file.</summary>
		/// <returns>The protocol to use to download the socket client access policy file.</returns>
		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x0600212D RID: 8493 RVA: 0x0009EF4D File Offset: 0x0009D14D
		// (set) Token: 0x0600212E RID: 8494 RVA: 0x0009EF55 File Offset: 0x0009D155
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public SocketClientAccessPolicyProtocol SocketClientAccessPolicyProtocol { get; set; }

		/// <summary>Creates an empty <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> instance.</summary>
		/// <exception cref="T:System.NotSupportedException">The platform is not supported.</exception>
		// Token: 0x0600212F RID: 8495 RVA: 0x0009EF5E File Offset: 0x0009D15E
		public SocketAsyncEventArgs()
		{
			this.m_ExecutionCallback = new ContextCallback(this.ExecutionCallback);
			this.m_SendPacketsSendSize = 0;
		}

		/// <summary>Gets or sets the socket to use or the socket created for accepting a connection with an asynchronous socket method.</summary>
		/// <returns>The <see cref="T:System.Net.Sockets.Socket" /> to use or the socket created for accepting a connection with an asynchronous socket method.</returns>
		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06002130 RID: 8496 RVA: 0x0009EF7F File Offset: 0x0009D17F
		// (set) Token: 0x06002131 RID: 8497 RVA: 0x0009EF87 File Offset: 0x0009D187
		public Socket AcceptSocket
		{
			get
			{
				return this.m_AcceptSocket;
			}
			set
			{
				this.m_AcceptSocket = value;
			}
		}

		/// <summary>The created and connected <see cref="T:System.Net.Sockets.Socket" /> object after successful completion of the <see cref="Overload:System.Net.Sockets.Socket.ConnectAsync" /> method.</summary>
		/// <returns>The connected <see cref="T:System.Net.Sockets.Socket" /> object.</returns>
		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002132 RID: 8498 RVA: 0x0009EF90 File Offset: 0x0009D190
		public Socket ConnectSocket
		{
			get
			{
				return this.m_ConnectSocket;
			}
		}

		/// <summary>Gets the data buffer to use with an asynchronous socket method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that represents the data buffer to use with an asynchronous socket method.</returns>
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002133 RID: 8499 RVA: 0x0009EF98 File Offset: 0x0009D198
		public byte[] Buffer
		{
			get
			{
				return this.m_Buffer;
			}
		}

		/// <summary>Gets the offset, in bytes, into the data buffer referenced by the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the offset, in bytes, into the data buffer referenced by the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property.</returns>
		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002134 RID: 8500 RVA: 0x0009EFA0 File Offset: 0x0009D1A0
		public int Offset
		{
			get
			{
				return this.m_Offset;
			}
		}

		/// <summary>Gets the maximum amount of data, in bytes, to send or receive in an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the maximum amount of data, in bytes, to send or receive.</returns>
		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06002135 RID: 8501 RVA: 0x0009EFA8 File Offset: 0x0009D1A8
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		/// <summary>Gets or sets an array of data buffers to use with an asynchronous socket method.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> that represents an array of data buffers to use with an asynchronous socket method.</returns>
		/// <exception cref="T:System.ArgumentException">There are ambiguous buffers specified on a set operation. This exception occurs if the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property has been set to a non-null value and an attempt was made to set the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> property to a non-null value.</exception>
		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06002136 RID: 8502 RVA: 0x0009EFB0 File Offset: 0x0009D1B0
		// (set) Token: 0x06002137 RID: 8503 RVA: 0x0009EFB8 File Offset: 0x0009D1B8
		public IList<ArraySegment<byte>> BufferList
		{
			get
			{
				return this.m_BufferList;
			}
			set
			{
				this.StartConfiguring();
				try
				{
					if (value != null && this.m_Buffer != null)
					{
						throw new ArgumentException(SR.GetString("net_ambiguousbuffers", new object[] { "Buffer" }));
					}
					this.m_BufferList = value;
					this.m_BufferListChanged = true;
					this.CheckPinMultipleBuffers();
				}
				finally
				{
					this.Complete();
				}
			}
		}

		/// <summary>Gets the number of bytes transferred in the socket operation.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the number of bytes transferred in the socket operation.</returns>
		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06002138 RID: 8504 RVA: 0x0009F024 File Offset: 0x0009D224
		public int BytesTransferred
		{
			get
			{
				return this.m_BytesTransferred;
			}
		}

		/// <summary>The event used to complete an asynchronous operation.</summary>
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06002139 RID: 8505 RVA: 0x0009F02C File Offset: 0x0009D22C
		// (remove) Token: 0x0600213A RID: 8506 RVA: 0x0009F03C File Offset: 0x0009D23C
		public event EventHandler<SocketAsyncEventArgs> Completed
		{
			add
			{
				this.m_Completed += value;
				this.m_CompletedChanged = true;
			}
			remove
			{
				this.m_Completed -= value;
				this.m_CompletedChanged = true;
			}
		}

		/// <summary>Represents a method that is called when an asynchronous operation completes.</summary>
		/// <param name="e">The event that is signaled.</param>
		// Token: 0x0600213B RID: 8507 RVA: 0x0009F04C File Offset: 0x0009D24C
		protected virtual void OnCompleted(SocketAsyncEventArgs e)
		{
			EventHandler<SocketAsyncEventArgs> completed = this.m_Completed;
			if (completed != null)
			{
				completed(e.m_CurrentSocket, e);
			}
		}

		/// <summary>Gets or sets a value that specifies if socket can be reused after a disconnect operation.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> that specifies if socket can be reused after a disconnect operation.</returns>
		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x0600213C RID: 8508 RVA: 0x0009F070 File Offset: 0x0009D270
		// (set) Token: 0x0600213D RID: 8509 RVA: 0x0009F078 File Offset: 0x0009D278
		public bool DisconnectReuseSocket
		{
			get
			{
				return this.m_DisconnectReuseSocket;
			}
			set
			{
				this.m_DisconnectReuseSocket = value;
			}
		}

		/// <summary>Gets the type of socket operation most recently performed with this context object.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketAsyncOperation" /> instance that indicates the type of socket operation most recently performed with this context object.</returns>
		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x0009F081 File Offset: 0x0009D281
		public SocketAsyncOperation LastOperation
		{
			get
			{
				return this.m_CompletedOperation;
			}
		}

		/// <summary>Gets the IP address and interface of a received packet.</summary>
		/// <returns>An <see cref="T:System.Net.Sockets.IPPacketInformation" /> instance that contains the destination IP address and interface of a received packet.</returns>
		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x0600213F RID: 8511 RVA: 0x0009F089 File Offset: 0x0009D289
		public IPPacketInformation ReceiveMessageFromPacketInfo
		{
			get
			{
				return this.m_ReceiveMessageFromPacketInfo;
			}
		}

		/// <summary>Gets or sets the remote IP endpoint for an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Net.EndPoint" /> that represents the remote IP endpoint for an asynchronous operation.</returns>
		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x0009F091 File Offset: 0x0009D291
		// (set) Token: 0x06002141 RID: 8513 RVA: 0x0009F099 File Offset: 0x0009D299
		public EndPoint RemoteEndPoint
		{
			get
			{
				return this.m_RemoteEndPoint;
			}
			set
			{
				this.m_RemoteEndPoint = value;
			}
		}

		/// <summary>Gets or sets an array of buffers to be sent for an asynchronous operation used by the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</summary>
		/// <returns>An array of <see cref="T:System.Net.Sockets.SendPacketsElement" /> objects that represent an array of buffers to be sent.</returns>
		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x0009F0A2 File Offset: 0x0009D2A2
		// (set) Token: 0x06002143 RID: 8515 RVA: 0x0009F0AC File Offset: 0x0009D2AC
		public SendPacketsElement[] SendPacketsElements
		{
			get
			{
				return this.m_SendPacketsElements;
			}
			set
			{
				this.StartConfiguring();
				try
				{
					this.m_SendPacketsElements = value;
					this.m_SendPacketsElementsInternal = null;
				}
				finally
				{
					this.Complete();
				}
			}
		}

		/// <summary>Gets or sets a bitwise combination of <see cref="T:System.Net.Sockets.TransmitFileOptions" /> values for an asynchronous operation used by the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.TransmitFileOptions" /> that contains a bitwise combination of values that are used with an asynchronous operation.</returns>
		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06002144 RID: 8516 RVA: 0x0009F0E8 File Offset: 0x0009D2E8
		// (set) Token: 0x06002145 RID: 8517 RVA: 0x0009F0F0 File Offset: 0x0009D2F0
		public TransmitFileOptions SendPacketsFlags
		{
			get
			{
				return this.m_SendPacketsFlags;
			}
			set
			{
				this.m_SendPacketsFlags = value;
			}
		}

		/// <summary>Gets or sets the size, in bytes, of the data block used in the send operation.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the size, in bytes, of the data block used in the send operation.</returns>
		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06002146 RID: 8518 RVA: 0x0009F0F9 File Offset: 0x0009D2F9
		// (set) Token: 0x06002147 RID: 8519 RVA: 0x0009F101 File Offset: 0x0009D301
		public int SendPacketsSendSize
		{
			get
			{
				return this.m_SendPacketsSendSize;
			}
			set
			{
				this.m_SendPacketsSendSize = value;
			}
		}

		/// <summary>Gets or sets the result of the asynchronous socket operation.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketError" /> that represents the result of the asynchronous socket operation.</returns>
		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06002148 RID: 8520 RVA: 0x0009F10A File Offset: 0x0009D30A
		// (set) Token: 0x06002149 RID: 8521 RVA: 0x0009F112 File Offset: 0x0009D312
		public SocketError SocketError
		{
			get
			{
				return this.m_SocketError;
			}
			set
			{
				this.m_SocketError = value;
			}
		}

		/// <summary>Gets the exception in the case of a connection failure when a <see cref="T:System.Net.DnsEndPoint" /> was used.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that indicates the cause of the connection error when a <see cref="T:System.Net.DnsEndPoint" /> was specified for the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.RemoteEndPoint" /> property.</returns>
		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x0600214A RID: 8522 RVA: 0x0009F11B File Offset: 0x0009D31B
		public Exception ConnectByNameError
		{
			get
			{
				return this.m_ConnectByNameError;
			}
		}

		/// <summary>Gets the results of an asynchronous socket operation or sets the behavior of an asynchronous operation.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketFlags" /> that represents the results of an asynchronous socket operation.</returns>
		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x0600214B RID: 8523 RVA: 0x0009F123 File Offset: 0x0009D323
		// (set) Token: 0x0600214C RID: 8524 RVA: 0x0009F12B File Offset: 0x0009D32B
		public SocketFlags SocketFlags
		{
			get
			{
				return this.m_SocketFlags;
			}
			set
			{
				this.m_SocketFlags = value;
			}
		}

		/// <summary>Gets or sets a user or application object associated with this asynchronous socket operation.</summary>
		/// <returns>An object that represents the user or application object associated with this asynchronous socket operation.</returns>
		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x0600214D RID: 8525 RVA: 0x0009F134 File Offset: 0x0009D334
		// (set) Token: 0x0600214E RID: 8526 RVA: 0x0009F13C File Offset: 0x0009D33C
		public object UserToken
		{
			get
			{
				return this.m_UserToken;
			}
			set
			{
				this.m_UserToken = value;
			}
		}

		/// <summary>Sets the data buffer to use with an asynchronous socket method.</summary>
		/// <param name="buffer">The data buffer to use with an asynchronous socket method.</param>
		/// <param name="offset">The offset, in bytes, in the data buffer where the operation starts.</param>
		/// <param name="count">The maximum amount of data, in bytes, to send or receive in the buffer.</param>
		/// <exception cref="T:System.ArgumentException">There are ambiguous buffers specified. This exception occurs if the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property is also not null and the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.BufferList" /> property is also not null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An argument was out of range. This exception occurs if the <paramref name="offset" /> parameter is less than zero or greater than the length of the array in the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property. This exception also occurs if the <paramref name="count" /> parameter is less than zero or greater than the length of the array in the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property minus the <paramref name="offset" /> parameter.</exception>
		// Token: 0x0600214F RID: 8527 RVA: 0x0009F145 File Offset: 0x0009D345
		public void SetBuffer(byte[] buffer, int offset, int count)
		{
			this.SetBufferInternal(buffer, offset, count);
		}

		/// <summary>Sets the data buffer to use with an asynchronous socket method.</summary>
		/// <param name="offset">The offset, in bytes, in the data buffer where the operation starts.</param>
		/// <param name="count">The maximum amount of data, in bytes, to send or receive in the buffer.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An argument was out of range. This exception occurs if the <paramref name="offset" /> parameter is less than zero or greater than the length of the array in the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property. This exception also occurs if the <paramref name="count" /> parameter is less than zero or greater than the length of the array in the <see cref="P:System.Net.Sockets.SocketAsyncEventArgs.Buffer" /> property minus the <paramref name="offset" /> parameter.</exception>
		// Token: 0x06002150 RID: 8528 RVA: 0x0009F150 File Offset: 0x0009D350
		public void SetBuffer(int offset, int count)
		{
			this.SetBufferInternal(this.m_Buffer, offset, count);
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x0009F160 File Offset: 0x0009D360
		private void SetBufferInternal(byte[] buffer, int offset, int count)
		{
			this.StartConfiguring();
			try
			{
				if (buffer == null)
				{
					this.m_Buffer = null;
					this.m_Offset = 0;
					this.m_Count = 0;
				}
				else
				{
					if (this.m_BufferList != null)
					{
						throw new ArgumentException(SR.GetString("net_ambiguousbuffers", new object[] { "BufferList" }));
					}
					if (offset < 0 || offset > buffer.Length)
					{
						throw new ArgumentOutOfRangeException("offset");
					}
					if (count < 0 || count > buffer.Length - offset)
					{
						throw new ArgumentOutOfRangeException("count");
					}
					this.m_Buffer = buffer;
					this.m_Offset = offset;
					this.m_Count = count;
				}
				this.CheckPinSingleBuffer(true);
			}
			finally
			{
				this.Complete();
			}
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x0009F214 File Offset: 0x0009D414
		internal void SetResults(SocketError socketError, int bytesTransferred, SocketFlags flags)
		{
			this.m_SocketError = socketError;
			this.m_ConnectByNameError = null;
			this.m_BytesTransferred = bytesTransferred;
			this.m_SocketFlags = flags;
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x0009F234 File Offset: 0x0009D434
		internal void SetResults(Exception exception, int bytesTransferred, SocketFlags flags)
		{
			this.m_ConnectByNameError = exception;
			this.m_BytesTransferred = bytesTransferred;
			this.m_SocketFlags = flags;
			if (exception == null)
			{
				this.m_SocketError = SocketError.Success;
				return;
			}
			SocketException ex = exception as SocketException;
			if (ex != null)
			{
				this.m_SocketError = ex.SocketErrorCode;
				return;
			}
			this.m_SocketError = SocketError.SocketError;
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x0009F27F File Offset: 0x0009D47F
		private void ExecutionCallback(object ignored)
		{
			this.OnCompleted(this);
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x0009F288 File Offset: 0x0009D488
		internal void Complete()
		{
			this.m_Operating = 0;
			if (this.m_DisposeCalled)
			{
				this.Dispose();
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> instance and optionally disposes of the managed resources.</summary>
		// Token: 0x06002156 RID: 8534 RVA: 0x0009F29F File Offset: 0x0009D49F
		public void Dispose()
		{
			this.m_DisposeCalled = true;
			if (Interlocked.CompareExchange(ref this.m_Operating, 2, 0) != 0)
			{
				return;
			}
			this.FreeOverlapped(false);
			GC.SuppressFinalize(this);
		}

		/// <summary>Frees resources used by the <see cref="T:System.Net.Sockets.SocketAsyncEventArgs" /> class.</summary>
		// Token: 0x06002157 RID: 8535 RVA: 0x0009F2C8 File Offset: 0x0009D4C8
		~SocketAsyncEventArgs()
		{
			this.FreeOverlapped(true);
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x0009F2F8 File Offset: 0x0009D4F8
		private void StartConfiguring()
		{
			int num = Interlocked.CompareExchange(ref this.m_Operating, -1, 0);
			if (num == 1 || num == -1)
			{
				throw new InvalidOperationException(SR.GetString("net_socketopinprogress"));
			}
			if (num == 2)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x0009F340 File Offset: 0x0009D540
		internal void StartOperationCommon(Socket socket)
		{
			if (Interlocked.CompareExchange(ref this.m_Operating, 1, 0) == 0)
			{
				if (ExecutionContext.IsFlowSuppressed())
				{
					this.m_Context = null;
					this.m_ContextCopy = null;
				}
				else
				{
					if (this.m_CompletedChanged || socket != this.m_CurrentSocket)
					{
						this.m_CompletedChanged = false;
						this.m_Context = null;
						this.m_ContextCopy = null;
					}
					if (this.m_Context == null)
					{
						this.m_Context = ExecutionContext.Capture();
					}
					if (this.m_Context != null)
					{
						this.m_ContextCopy = this.m_Context.CreateCopy();
					}
				}
				this.m_CurrentSocket = socket;
				return;
			}
			if (this.m_DisposeCalled)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			throw new InvalidOperationException(SR.GetString("net_socketopinprogress"));
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x0009F3F8 File Offset: 0x0009D5F8
		internal void StartOperationAccept()
		{
			this.m_CompletedOperation = SocketAsyncOperation.Accept;
			this.m_AcceptAddressBufferCount = 2 * (this.m_CurrentSocket.m_RightEndPoint.Serialize().Size + 16);
			if (this.m_Buffer != null)
			{
				if (this.m_Count < this.m_AcceptAddressBufferCount)
				{
					throw new ArgumentException(SR.GetString("net_buffercounttoosmall", new object[] { "Count" }));
				}
			}
			else
			{
				if (this.m_AcceptBuffer == null || this.m_AcceptBuffer.Length < this.m_AcceptAddressBufferCount)
				{
					this.m_AcceptBuffer = new byte[this.m_AcceptAddressBufferCount];
				}
				this.CheckPinSingleBuffer(false);
			}
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x0009F490 File Offset: 0x0009D690
		internal void StartOperationConnect()
		{
			this.m_CompletedOperation = SocketAsyncOperation.Connect;
			this.m_MultipleConnect = null;
			this.m_ConnectSocket = null;
			this.PinSocketAddressBuffer();
			this.CheckPinNoBuffer();
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x0009F4B3 File Offset: 0x0009D6B3
		internal void StartOperationWrapperConnect(MultipleConnectAsync args)
		{
			this.m_CompletedOperation = SocketAsyncOperation.Connect;
			this.m_MultipleConnect = args;
			this.m_ConnectSocket = null;
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x0009F4CA File Offset: 0x0009D6CA
		internal void CancelConnectAsync()
		{
			if (this.m_Operating == 1 && this.m_CompletedOperation == SocketAsyncOperation.Connect)
			{
				if (this.m_MultipleConnect != null)
				{
					this.m_MultipleConnect.Cancel();
					return;
				}
				this.m_CurrentSocket.Close();
			}
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x0009F4FD File Offset: 0x0009D6FD
		internal void StartOperationDisconnect()
		{
			this.m_CompletedOperation = SocketAsyncOperation.Disconnect;
			this.CheckPinNoBuffer();
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x0009F50C File Offset: 0x0009D70C
		internal void StartOperationReceive()
		{
			this.m_CompletedOperation = SocketAsyncOperation.Receive;
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x0009F515 File Offset: 0x0009D715
		internal void StartOperationReceiveFrom()
		{
			this.m_CompletedOperation = SocketAsyncOperation.ReceiveFrom;
			this.PinSocketAddressBuffer();
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x0009F524 File Offset: 0x0009D724
		internal unsafe void StartOperationReceiveMessageFrom()
		{
			this.m_CompletedOperation = SocketAsyncOperation.ReceiveMessageFrom;
			this.PinSocketAddressBuffer();
			if (this.m_WSAMessageBuffer == null)
			{
				this.m_WSAMessageBuffer = new byte[SocketAsyncEventArgs.s_WSAMsgSize];
				this.m_WSAMessageBufferGCHandle = GCHandle.Alloc(this.m_WSAMessageBuffer, GCHandleType.Pinned);
				this.m_PtrWSAMessageBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_WSAMessageBuffer, 0);
			}
			IPAddress ipaddress = ((this.m_SocketAddress.Family == AddressFamily.InterNetworkV6) ? this.m_SocketAddress.GetIPAddress() : null);
			bool flag = this.m_CurrentSocket.AddressFamily == AddressFamily.InterNetwork || (ipaddress != null && ipaddress.IsIPv4MappedToIPv6);
			bool flag2 = this.m_CurrentSocket.AddressFamily == AddressFamily.InterNetworkV6;
			if (flag && (this.m_ControlBuffer == null || this.m_ControlBuffer.Length != SocketAsyncEventArgs.s_ControlDataSize))
			{
				if (this.m_ControlBufferGCHandle.IsAllocated)
				{
					this.m_ControlBufferGCHandle.Free();
				}
				this.m_ControlBuffer = new byte[SocketAsyncEventArgs.s_ControlDataSize];
			}
			else if (flag2 && (this.m_ControlBuffer == null || this.m_ControlBuffer.Length != SocketAsyncEventArgs.s_ControlDataIPv6Size))
			{
				if (this.m_ControlBufferGCHandle.IsAllocated)
				{
					this.m_ControlBufferGCHandle.Free();
				}
				this.m_ControlBuffer = new byte[SocketAsyncEventArgs.s_ControlDataIPv6Size];
			}
			if (!this.m_ControlBufferGCHandle.IsAllocated)
			{
				this.m_ControlBufferGCHandle = GCHandle.Alloc(this.m_ControlBuffer, GCHandleType.Pinned);
				this.m_PtrControlBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_ControlBuffer, 0);
			}
			if (this.m_Buffer != null)
			{
				if (this.m_WSARecvMsgWSABufferArray == null)
				{
					this.m_WSARecvMsgWSABufferArray = new WSABuffer[1];
				}
				this.m_WSARecvMsgWSABufferArray[0].Pointer = this.m_PtrSingleBuffer;
				this.m_WSARecvMsgWSABufferArray[0].Length = this.m_Count;
				this.m_WSARecvMsgWSABufferArrayGCHandle = GCHandle.Alloc(this.m_WSARecvMsgWSABufferArray, GCHandleType.Pinned);
				this.m_PtrWSARecvMsgWSABufferArray = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_WSARecvMsgWSABufferArray, 0);
			}
			else
			{
				this.m_WSARecvMsgWSABufferArrayGCHandle = GCHandle.Alloc(this.m_WSABufferArray, GCHandleType.Pinned);
				this.m_PtrWSARecvMsgWSABufferArray = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_WSABufferArray, 0);
			}
			UnsafeNclNativeMethods.OSSOCK.WSAMsg* ptr = (UnsafeNclNativeMethods.OSSOCK.WSAMsg*)(void*)this.m_PtrWSAMessageBuffer;
			ptr->socketAddress = this.m_PtrSocketAddressBuffer;
			ptr->addressLength = (uint)this.m_SocketAddress.Size;
			ptr->buffers = this.m_PtrWSARecvMsgWSABufferArray;
			if (this.m_Buffer != null)
			{
				ptr->count = 1U;
			}
			else
			{
				ptr->count = (uint)this.m_WSABufferArray.Length;
			}
			if (this.m_ControlBuffer != null)
			{
				ptr->controlBuffer.Pointer = this.m_PtrControlBuffer;
				ptr->controlBuffer.Length = this.m_ControlBuffer.Length;
			}
			ptr->flags = this.m_SocketFlags;
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x0009F79D File Offset: 0x0009D99D
		internal void StartOperationSend()
		{
			this.m_CompletedOperation = SocketAsyncOperation.Send;
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x0009F7A8 File Offset: 0x0009D9A8
		internal void StartOperationSendPackets()
		{
			this.m_CompletedOperation = SocketAsyncOperation.SendPackets;
			if (this.m_SendPacketsElements != null)
			{
				this.m_SendPacketsElementsInternal = (SendPacketsElement[])this.m_SendPacketsElements.Clone();
			}
			this.m_SendPacketsElementsFileCount = 0;
			this.m_SendPacketsElementsBufferCount = 0;
			foreach (SendPacketsElement sendPacketsElement in this.m_SendPacketsElementsInternal)
			{
				if (sendPacketsElement != null)
				{
					if (sendPacketsElement.m_FilePath != null)
					{
						this.m_SendPacketsElementsFileCount++;
					}
					if (sendPacketsElement.m_Buffer != null && sendPacketsElement.m_Count > 0)
					{
						this.m_SendPacketsElementsBufferCount++;
					}
				}
			}
			if (this.m_SendPacketsElementsFileCount > 0)
			{
				this.m_SendPacketsFileStreams = new FileStream[this.m_SendPacketsElementsFileCount];
				this.m_SendPacketsFileHandles = new SafeHandle[this.m_SendPacketsElementsFileCount];
				int num = 0;
				foreach (SendPacketsElement sendPacketsElement2 in this.m_SendPacketsElementsInternal)
				{
					if (sendPacketsElement2 != null && sendPacketsElement2.m_FilePath != null)
					{
						Exception ex = null;
						try
						{
							this.m_SendPacketsFileStreams[num] = new FileStream(sendPacketsElement2.m_FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
						}
						catch (Exception ex2)
						{
							ex = ex2;
						}
						if (ex != null)
						{
							for (int k = 0; k < this.m_SendPacketsElementsFileCount; k++)
							{
								this.m_SendPacketsFileHandles[k] = null;
								if (this.m_SendPacketsFileStreams[k] != null)
								{
									this.m_SendPacketsFileStreams[k].Close();
									this.m_SendPacketsFileStreams[k] = null;
								}
							}
							throw ex;
						}
						ExceptionHelper.UnmanagedPermission.Assert();
						try
						{
							this.m_SendPacketsFileHandles[num] = this.m_SendPacketsFileStreams[num].SafeFileHandle;
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
						num++;
					}
				}
			}
			this.CheckPinSendPackets();
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x0009F960 File Offset: 0x0009DB60
		internal void StartOperationSendTo()
		{
			this.m_CompletedOperation = SocketAsyncOperation.SendTo;
			this.PinSocketAddressBuffer();
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x0009F970 File Offset: 0x0009DB70
		private void CheckPinNoBuffer()
		{
			if (this.m_PinState == SocketAsyncEventArgs.PinState.None)
			{
				this.SetupOverlappedSingle(true);
			}
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x0009F984 File Offset: 0x0009DB84
		private void CheckPinSingleBuffer(bool pinUsersBuffer)
		{
			if (pinUsersBuffer)
			{
				if (this.m_Buffer == null)
				{
					if (this.m_PinState == SocketAsyncEventArgs.PinState.SingleBuffer)
					{
						this.FreeOverlapped(false);
						return;
					}
				}
				else
				{
					if (this.m_PinState != SocketAsyncEventArgs.PinState.SingleBuffer || this.m_PinnedSingleBuffer != this.m_Buffer)
					{
						this.FreeOverlapped(false);
						this.SetupOverlappedSingle(true);
						return;
					}
					if (this.m_Offset != this.m_PinnedSingleBufferOffset)
					{
						this.m_PinnedSingleBufferOffset = this.m_Offset;
						this.m_PtrSingleBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_Buffer, this.m_Offset);
						this.m_WSABuffer.Pointer = this.m_PtrSingleBuffer;
					}
					if (this.m_Count != this.m_PinnedSingleBufferCount)
					{
						this.m_PinnedSingleBufferCount = this.m_Count;
						this.m_WSABuffer.Length = this.m_Count;
						return;
					}
				}
			}
			else if (this.m_PinState != SocketAsyncEventArgs.PinState.SingleAcceptBuffer || this.m_PinnedSingleBuffer != this.m_AcceptBuffer)
			{
				this.FreeOverlapped(false);
				this.SetupOverlappedSingle(false);
			}
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x0009FA6C File Offset: 0x0009DC6C
		private void CheckPinMultipleBuffers()
		{
			if (this.m_BufferList == null)
			{
				if (this.m_PinState == SocketAsyncEventArgs.PinState.MultipleBuffer)
				{
					this.FreeOverlapped(false);
					return;
				}
			}
			else if (this.m_PinState != SocketAsyncEventArgs.PinState.MultipleBuffer || this.m_BufferListChanged)
			{
				this.m_BufferListChanged = false;
				this.FreeOverlapped(false);
				try
				{
					this.SetupOverlappedMultiple();
				}
				catch (Exception)
				{
					this.FreeOverlapped(false);
					throw;
				}
			}
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x0009FAD4 File Offset: 0x0009DCD4
		private void CheckPinSendPackets()
		{
			if (this.m_PinState != SocketAsyncEventArgs.PinState.None)
			{
				this.FreeOverlapped(false);
			}
			this.SetupOverlappedSendPackets();
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x0009FAEC File Offset: 0x0009DCEC
		private void PinSocketAddressBuffer()
		{
			if (this.m_PinnedSocketAddress == this.m_SocketAddress)
			{
				return;
			}
			if (this.m_SocketAddressGCHandle.IsAllocated)
			{
				this.m_SocketAddressGCHandle.Free();
			}
			this.m_SocketAddressGCHandle = GCHandle.Alloc(this.m_SocketAddress.m_Buffer, GCHandleType.Pinned);
			this.m_SocketAddress.CopyAddressSizeIntoBuffer();
			this.m_PtrSocketAddressBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_SocketAddress.m_Buffer, 0);
			this.m_PtrSocketAddressBufferSize = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_SocketAddress.m_Buffer, this.m_SocketAddress.GetAddressSizeOffset());
			this.m_PinnedSocketAddress = this.m_SocketAddress;
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x0009FB88 File Offset: 0x0009DD88
		private void FreeOverlapped(bool checkForShutdown)
		{
			if (!checkForShutdown || !NclUtilities.HasShutdownStarted)
			{
				if (this.m_PtrNativeOverlapped != null && !this.m_PtrNativeOverlapped.IsInvalid)
				{
					this.m_PtrNativeOverlapped.Dispose();
					this.m_PtrNativeOverlapped = null;
					this.m_Overlapped = null;
					this.m_PinState = SocketAsyncEventArgs.PinState.None;
					this.m_PinnedAcceptBuffer = null;
					this.m_PinnedSingleBuffer = null;
					this.m_PinnedSingleBufferOffset = 0;
					this.m_PinnedSingleBufferCount = 0;
				}
				if (this.m_SocketAddressGCHandle.IsAllocated)
				{
					this.m_SocketAddressGCHandle.Free();
				}
				if (this.m_WSAMessageBufferGCHandle.IsAllocated)
				{
					this.m_WSAMessageBufferGCHandle.Free();
				}
				if (this.m_WSARecvMsgWSABufferArrayGCHandle.IsAllocated)
				{
					this.m_WSARecvMsgWSABufferArrayGCHandle.Free();
				}
				if (this.m_ControlBufferGCHandle.IsAllocated)
				{
					this.m_ControlBufferGCHandle.Free();
				}
			}
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x0009FC54 File Offset: 0x0009DE54
		private void SetupOverlappedSingle(bool pinSingleBuffer)
		{
			this.m_Overlapped = new Overlapped();
			if (!pinSingleBuffer)
			{
				this.m_PtrNativeOverlapped = new SafeNativeOverlapped(this.m_Overlapped.UnsafePack(new IOCompletionCallback(this.CompletionPortCallback), this.m_AcceptBuffer));
				this.m_PinnedAcceptBuffer = this.m_AcceptBuffer;
				this.m_PtrAcceptBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_AcceptBuffer, 0);
				this.m_PtrSingleBuffer = IntPtr.Zero;
				this.m_PinState = SocketAsyncEventArgs.PinState.SingleAcceptBuffer;
				return;
			}
			if (this.m_Buffer != null)
			{
				this.m_PtrNativeOverlapped = new SafeNativeOverlapped(this.m_Overlapped.UnsafePack(new IOCompletionCallback(this.CompletionPortCallback), this.m_Buffer));
				this.m_PinnedSingleBuffer = this.m_Buffer;
				this.m_PinnedSingleBufferOffset = this.m_Offset;
				this.m_PinnedSingleBufferCount = this.m_Count;
				this.m_PtrSingleBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_Buffer, this.m_Offset);
				this.m_PtrAcceptBuffer = IntPtr.Zero;
				this.m_WSABuffer.Pointer = this.m_PtrSingleBuffer;
				this.m_WSABuffer.Length = this.m_Count;
				this.m_PinState = SocketAsyncEventArgs.PinState.SingleBuffer;
				return;
			}
			this.m_PtrNativeOverlapped = new SafeNativeOverlapped(this.m_Overlapped.UnsafePack(new IOCompletionCallback(this.CompletionPortCallback), null));
			this.m_PinnedSingleBuffer = null;
			this.m_PinnedSingleBufferOffset = 0;
			this.m_PinnedSingleBufferCount = 0;
			this.m_PtrSingleBuffer = IntPtr.Zero;
			this.m_PtrAcceptBuffer = IntPtr.Zero;
			this.m_WSABuffer.Pointer = this.m_PtrSingleBuffer;
			this.m_WSABuffer.Length = this.m_Count;
			this.m_PinState = SocketAsyncEventArgs.PinState.NoBuffer;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0009FDE8 File Offset: 0x0009DFE8
		private void SetupOverlappedMultiple()
		{
			ArraySegment<byte>[] array = new ArraySegment<byte>[this.m_BufferList.Count];
			this.m_BufferList.CopyTo(array, 0);
			this.m_Overlapped = new Overlapped();
			if (this.m_ObjectsToPin == null || this.m_ObjectsToPin.Length != array.Length)
			{
				this.m_ObjectsToPin = new object[array.Length];
			}
			bool flag = false;
			if (this.m_WSABufferArray == null || this.m_WSABufferArray.Length != array.Length)
			{
				flag = true;
			}
			for (int i = 0; i < array.Length; i++)
			{
				this.m_ObjectsToPin[i] = array[i].Array;
				if (!flag && array[i].Count != this.m_WSABufferArray[i].Length)
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.m_WSABufferArray = new WSABuffer[array.Length];
			}
			this.m_PtrNativeOverlapped = new SafeNativeOverlapped(this.m_Overlapped.UnsafePack(new IOCompletionCallback(this.CompletionPortCallback), this.m_ObjectsToPin));
			for (int j = 0; j < array.Length; j++)
			{
				ArraySegment<byte> arraySegment = array[j];
				ValidationHelper.ValidateSegment(arraySegment);
				this.m_WSABufferArray[j].Pointer = Marshal.UnsafeAddrOfPinnedArrayElement(arraySegment.Array, arraySegment.Offset);
				this.m_WSABufferArray[j].Length = arraySegment.Count;
			}
			this.m_PinState = SocketAsyncEventArgs.PinState.MultipleBuffer;
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0009FF3C File Offset: 0x0009E13C
		private void SetupOverlappedSendPackets()
		{
			this.m_Overlapped = new Overlapped();
			this.m_SendPacketsDescriptor = new UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElement[this.m_SendPacketsElementsFileCount + this.m_SendPacketsElementsBufferCount];
			if (this.m_ObjectsToPin == null || this.m_ObjectsToPin.Length != this.m_SendPacketsElementsBufferCount + 1)
			{
				this.m_ObjectsToPin = new object[this.m_SendPacketsElementsBufferCount + 1];
			}
			this.m_ObjectsToPin[0] = this.m_SendPacketsDescriptor;
			int num = 1;
			foreach (SendPacketsElement sendPacketsElement in this.m_SendPacketsElementsInternal)
			{
				if (sendPacketsElement != null && sendPacketsElement.m_Buffer != null && sendPacketsElement.m_Count > 0)
				{
					this.m_ObjectsToPin[num] = sendPacketsElement.m_Buffer;
					num++;
				}
			}
			this.m_PtrNativeOverlapped = new SafeNativeOverlapped(this.m_Overlapped.UnsafePack(new IOCompletionCallback(this.CompletionPortCallback), this.m_ObjectsToPin));
			this.m_PtrSendPacketsDescriptor = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_SendPacketsDescriptor, 0);
			int num2 = 0;
			int num3 = 0;
			foreach (SendPacketsElement sendPacketsElement2 in this.m_SendPacketsElementsInternal)
			{
				if (sendPacketsElement2 != null)
				{
					if (sendPacketsElement2.m_Buffer != null && sendPacketsElement2.m_Count > 0)
					{
						this.m_SendPacketsDescriptor[num2].buffer = Marshal.UnsafeAddrOfPinnedArrayElement(sendPacketsElement2.m_Buffer, sendPacketsElement2.m_Offset);
						this.m_SendPacketsDescriptor[num2].length = (uint)sendPacketsElement2.m_Count;
						this.m_SendPacketsDescriptor[num2].flags = sendPacketsElement2.m_Flags;
						num2++;
					}
					else if (sendPacketsElement2.m_FilePath != null)
					{
						this.m_SendPacketsDescriptor[num2].fileHandle = this.m_SendPacketsFileHandles[num3].DangerousGetHandle();
						this.m_SendPacketsDescriptor[num2].fileOffset = (long)sendPacketsElement2.m_Offset;
						this.m_SendPacketsDescriptor[num2].length = (uint)sendPacketsElement2.m_Count;
						this.m_SendPacketsDescriptor[num2].flags = sendPacketsElement2.m_Flags;
						num3++;
						num2++;
					}
				}
			}
			this.m_PinState = SocketAsyncEventArgs.PinState.SendPackets;
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x000A014C File Offset: 0x0009E34C
		internal void LogBuffer(int size)
		{
			switch (this.m_PinState)
			{
			case SocketAsyncEventArgs.PinState.SingleAcceptBuffer:
				Logging.Dump(Logging.Sockets, this.m_CurrentSocket, "FinishOperation(" + this.m_CompletedOperation.ToString() + "Async)", this.m_AcceptBuffer, 0, size);
				return;
			case SocketAsyncEventArgs.PinState.SingleBuffer:
				Logging.Dump(Logging.Sockets, this.m_CurrentSocket, "FinishOperation(" + this.m_CompletedOperation.ToString() + "Async)", this.m_Buffer, this.m_Offset, size);
				return;
			case SocketAsyncEventArgs.PinState.MultipleBuffer:
				foreach (WSABuffer wsabuffer in this.m_WSABufferArray)
				{
					Logging.Dump(Logging.Sockets, this.m_CurrentSocket, "FinishOperation(" + this.m_CompletedOperation.ToString() + "Async)", wsabuffer.Pointer, Math.Min(wsabuffer.Length, size));
					if ((size -= wsabuffer.Length) <= 0)
					{
						break;
					}
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x000A025C File Offset: 0x0009E45C
		internal void LogSendPacketsBuffers(int size)
		{
			foreach (SendPacketsElement sendPacketsElement in this.m_SendPacketsElementsInternal)
			{
				if (sendPacketsElement != null)
				{
					if (sendPacketsElement.m_Buffer != null && sendPacketsElement.m_Count > 0)
					{
						Logging.Dump(Logging.Sockets, this.m_CurrentSocket, "FinishOperation(" + this.m_CompletedOperation.ToString() + "Async)Buffer", sendPacketsElement.m_Buffer, sendPacketsElement.m_Offset, Math.Min(sendPacketsElement.m_Count, size));
					}
					else if (sendPacketsElement.m_FilePath != null)
					{
						Logging.PrintInfo(Logging.Sockets, this.m_CurrentSocket, "FinishOperation(" + this.m_CompletedOperation.ToString() + "Async)", SR.GetString("net_log_socket_not_logged_file", new object[] { sendPacketsElement.m_FilePath }));
					}
				}
			}
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x000A033C File Offset: 0x0009E53C
		internal void UpdatePerfCounters(int size, bool sendOp)
		{
			if (sendOp)
			{
				NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketBytesSent, (long)size);
				if (this.m_CurrentSocket.Transport == TransportType.Udp)
				{
					NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketDatagramsSent);
					return;
				}
			}
			else
			{
				NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketBytesReceived, (long)size);
				if (this.m_CurrentSocket.Transport == TransportType.Udp)
				{
					NetworkingPerfCounters.Instance.Increment(NetworkingPerfCounterName.SocketDatagramsReceived);
				}
			}
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x000A0399 File Offset: 0x0009E599
		internal void FinishOperationSyncFailure(SocketError socketError, int bytesTransferred, SocketFlags flags)
		{
			this.SetResults(socketError, bytesTransferred, flags);
			if (this.m_CurrentSocket != null)
			{
				this.m_CurrentSocket.UpdateStatusAfterSocketError(socketError);
			}
			this.Complete();
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x000A03BE File Offset: 0x0009E5BE
		internal void FinishConnectByNameSyncFailure(Exception exception, int bytesTransferred, SocketFlags flags)
		{
			this.SetResults(exception, bytesTransferred, flags);
			if (this.m_CurrentSocket != null)
			{
				this.m_CurrentSocket.UpdateStatusAfterSocketError(this.m_SocketError);
			}
			this.Complete();
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x000A03E8 File Offset: 0x0009E5E8
		internal void FinishOperationAsyncFailure(SocketError socketError, int bytesTransferred, SocketFlags flags)
		{
			this.SetResults(socketError, bytesTransferred, flags);
			if (this.m_CurrentSocket != null)
			{
				this.m_CurrentSocket.UpdateStatusAfterSocketError(socketError);
			}
			this.Complete();
			if (this.m_Context == null)
			{
				this.OnCompleted(this);
				return;
			}
			ExecutionContext.Run(this.m_ContextCopy, this.m_ExecutionCallback, null);
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x000A043C File Offset: 0x0009E63C
		internal void FinishOperationAsyncFailure(Exception exception, int bytesTransferred, SocketFlags flags)
		{
			this.SetResults(exception, bytesTransferred, flags);
			if (this.m_CurrentSocket != null)
			{
				this.m_CurrentSocket.UpdateStatusAfterSocketError(this.m_SocketError);
			}
			this.Complete();
			if (this.m_Context == null)
			{
				this.OnCompleted(this);
				return;
			}
			ExecutionContext.Run(this.m_ContextCopy, this.m_ExecutionCallback, null);
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x000A0494 File Offset: 0x0009E694
		internal void FinishWrapperConnectSuccess(Socket connectSocket, int bytesTransferred, SocketFlags flags)
		{
			this.SetResults(SocketError.Success, bytesTransferred, flags);
			this.m_CurrentSocket = connectSocket;
			this.m_ConnectSocket = connectSocket;
			this.Complete();
			if (this.m_ContextCopy == null)
			{
				this.OnCompleted(this);
				return;
			}
			ExecutionContext.Run(this.m_ContextCopy, this.m_ExecutionCallback, null);
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x000A04E0 File Offset: 0x0009E6E0
		internal unsafe void FinishOperationSuccess(SocketError socketError, int bytesTransferred, SocketFlags flags)
		{
			this.SetResults(socketError, bytesTransferred, flags);
			SocketAddress socketAddress2;
			switch (this.m_CompletedOperation)
			{
			case SocketAsyncOperation.Accept:
			{
				if (bytesTransferred > 0)
				{
					if (SocketAsyncEventArgs.s_LoggingEnabled)
					{
						this.LogBuffer(bytesTransferred);
					}
					if (Socket.s_PerfCountersEnabled)
					{
						this.UpdatePerfCounters(bytesTransferred, false);
					}
				}
				SocketAddress socketAddress = this.m_CurrentSocket.m_RightEndPoint.Serialize();
				try
				{
					IntPtr intPtr;
					int num;
					IntPtr intPtr2;
					this.m_CurrentSocket.GetAcceptExSockaddrs((this.m_PtrSingleBuffer != IntPtr.Zero) ? this.m_PtrSingleBuffer : this.m_PtrAcceptBuffer, (this.m_Count != 0) ? (this.m_Count - this.m_AcceptAddressBufferCount) : 0, this.m_AcceptAddressBufferCount / 2, this.m_AcceptAddressBufferCount / 2, out intPtr, out num, out intPtr2, out socketAddress.m_Size);
					Marshal.Copy(intPtr2, socketAddress.m_Buffer, 0, socketAddress.m_Size);
					IntPtr intPtr3 = this.m_CurrentSocket.SafeHandle.DangerousGetHandle();
					socketError = UnsafeNclNativeMethods.OSSOCK.setsockopt(this.m_AcceptSocket.SafeHandle, SocketOptionLevel.Socket, SocketOptionName.UpdateAcceptContext, ref intPtr3, Marshal.SizeOf(intPtr3));
					if (socketError == SocketError.SocketError)
					{
						socketError = (SocketError)Marshal.GetLastWin32Error();
					}
				}
				catch (ObjectDisposedException)
				{
					socketError = SocketError.OperationAborted;
				}
				if (socketError != SocketError.Success)
				{
					this.SetResults(socketError, bytesTransferred, SocketFlags.None);
					this.m_AcceptSocket = null;
					goto IL_593;
				}
				this.m_AcceptSocket = this.m_CurrentSocket.UpdateAcceptSocket(this.m_AcceptSocket, this.m_CurrentSocket.m_RightEndPoint.Create(socketAddress), false);
				if (SocketAsyncEventArgs.s_LoggingEnabled)
				{
					Logging.PrintInfo(Logging.Sockets, this.m_AcceptSocket, SR.GetString("net_log_socket_accepted", new object[]
					{
						this.m_AcceptSocket.RemoteEndPoint,
						this.m_AcceptSocket.LocalEndPoint
					}));
					goto IL_593;
				}
				goto IL_593;
			}
			case SocketAsyncOperation.Connect:
				if (bytesTransferred > 0)
				{
					if (SocketAsyncEventArgs.s_LoggingEnabled)
					{
						this.LogBuffer(bytesTransferred);
					}
					if (Socket.s_PerfCountersEnabled)
					{
						this.UpdatePerfCounters(bytesTransferred, true);
					}
				}
				try
				{
					socketError = UnsafeNclNativeMethods.OSSOCK.setsockopt(this.m_CurrentSocket.SafeHandle, SocketOptionLevel.Socket, SocketOptionName.UpdateConnectContext, null, 0);
					if (socketError == SocketError.SocketError)
					{
						socketError = (SocketError)Marshal.GetLastWin32Error();
					}
				}
				catch (ObjectDisposedException)
				{
					socketError = SocketError.OperationAborted;
				}
				if (socketError == SocketError.Success)
				{
					if (SocketAsyncEventArgs.s_LoggingEnabled)
					{
						Logging.PrintInfo(Logging.Sockets, this.m_CurrentSocket, SR.GetString("net_log_socket_connected", new object[]
						{
							this.m_CurrentSocket.LocalEndPoint,
							this.m_CurrentSocket.RemoteEndPoint
						}));
					}
					this.m_CurrentSocket.SetToConnected();
					this.m_ConnectSocket = this.m_CurrentSocket;
					goto IL_593;
				}
				goto IL_593;
			case SocketAsyncOperation.Disconnect:
				this.m_CurrentSocket.SetToDisconnected();
				this.m_CurrentSocket.m_RemoteEndPoint = null;
				goto IL_593;
			case SocketAsyncOperation.Receive:
				if (bytesTransferred <= 0)
				{
					goto IL_593;
				}
				if (SocketAsyncEventArgs.s_LoggingEnabled)
				{
					this.LogBuffer(bytesTransferred);
				}
				if (Socket.s_PerfCountersEnabled)
				{
					this.UpdatePerfCounters(bytesTransferred, false);
					goto IL_593;
				}
				goto IL_593;
			case SocketAsyncOperation.ReceiveFrom:
				if (bytesTransferred > 0)
				{
					if (SocketAsyncEventArgs.s_LoggingEnabled)
					{
						this.LogBuffer(bytesTransferred);
					}
					if (Socket.s_PerfCountersEnabled)
					{
						this.UpdatePerfCounters(bytesTransferred, false);
					}
				}
				this.m_SocketAddress.SetSize(this.m_PtrSocketAddressBufferSize);
				socketAddress2 = this.m_RemoteEndPoint.Serialize();
				if (socketAddress2.Equals(this.m_SocketAddress))
				{
					goto IL_593;
				}
				try
				{
					this.m_RemoteEndPoint = this.m_RemoteEndPoint.Create(this.m_SocketAddress);
					goto IL_593;
				}
				catch
				{
					goto IL_593;
				}
				break;
			case SocketAsyncOperation.ReceiveMessageFrom:
				break;
			case SocketAsyncOperation.Send:
				if (bytesTransferred <= 0)
				{
					goto IL_593;
				}
				if (SocketAsyncEventArgs.s_LoggingEnabled)
				{
					this.LogBuffer(bytesTransferred);
				}
				if (Socket.s_PerfCountersEnabled)
				{
					this.UpdatePerfCounters(bytesTransferred, true);
					goto IL_593;
				}
				goto IL_593;
			case SocketAsyncOperation.SendPackets:
				if (bytesTransferred > 0)
				{
					if (SocketAsyncEventArgs.s_LoggingEnabled)
					{
						this.LogSendPacketsBuffers(bytesTransferred);
					}
					if (Socket.s_PerfCountersEnabled)
					{
						this.UpdatePerfCounters(bytesTransferred, true);
					}
				}
				if (this.m_SendPacketsFileStreams != null)
				{
					for (int i = 0; i < this.m_SendPacketsElementsFileCount; i++)
					{
						this.m_SendPacketsFileHandles[i] = null;
						if (this.m_SendPacketsFileStreams[i] != null)
						{
							this.m_SendPacketsFileStreams[i].Close();
							this.m_SendPacketsFileStreams[i] = null;
						}
					}
				}
				this.m_SendPacketsFileStreams = null;
				this.m_SendPacketsFileHandles = null;
				goto IL_593;
			case SocketAsyncOperation.SendTo:
				if (bytesTransferred <= 0)
				{
					goto IL_593;
				}
				if (SocketAsyncEventArgs.s_LoggingEnabled)
				{
					this.LogBuffer(bytesTransferred);
				}
				if (Socket.s_PerfCountersEnabled)
				{
					this.UpdatePerfCounters(bytesTransferred, true);
					goto IL_593;
				}
				goto IL_593;
			default:
				goto IL_593;
			}
			if (bytesTransferred > 0)
			{
				if (SocketAsyncEventArgs.s_LoggingEnabled)
				{
					this.LogBuffer(bytesTransferred);
				}
				if (Socket.s_PerfCountersEnabled)
				{
					this.UpdatePerfCounters(bytesTransferred, false);
				}
			}
			this.m_SocketAddress.SetSize(this.m_PtrSocketAddressBufferSize);
			socketAddress2 = this.m_RemoteEndPoint.Serialize();
			if (!socketAddress2.Equals(this.m_SocketAddress))
			{
				try
				{
					this.m_RemoteEndPoint = this.m_RemoteEndPoint.Create(this.m_SocketAddress);
				}
				catch
				{
				}
			}
			IPAddress ipaddress = null;
			UnsafeNclNativeMethods.OSSOCK.WSAMsg* ptr = (UnsafeNclNativeMethods.OSSOCK.WSAMsg*)(void*)Marshal.UnsafeAddrOfPinnedArrayElement(this.m_WSAMessageBuffer, 0);
			if (this.m_ControlBuffer.Length == SocketAsyncEventArgs.s_ControlDataSize)
			{
				UnsafeNclNativeMethods.OSSOCK.ControlData controlData = (UnsafeNclNativeMethods.OSSOCK.ControlData)Marshal.PtrToStructure(ptr->controlBuffer.Pointer, typeof(UnsafeNclNativeMethods.OSSOCK.ControlData));
				if (controlData.length != UIntPtr.Zero)
				{
					ipaddress = new IPAddress((long)((ulong)controlData.address));
				}
				this.m_ReceiveMessageFromPacketInfo = new IPPacketInformation((ipaddress != null) ? ipaddress : IPAddress.None, (int)controlData.index);
			}
			else if (this.m_ControlBuffer.Length == SocketAsyncEventArgs.s_ControlDataIPv6Size)
			{
				UnsafeNclNativeMethods.OSSOCK.ControlDataIPv6 controlDataIPv = (UnsafeNclNativeMethods.OSSOCK.ControlDataIPv6)Marshal.PtrToStructure(ptr->controlBuffer.Pointer, typeof(UnsafeNclNativeMethods.OSSOCK.ControlDataIPv6));
				if (controlDataIPv.length != UIntPtr.Zero)
				{
					ipaddress = new IPAddress(controlDataIPv.address);
				}
				this.m_ReceiveMessageFromPacketInfo = new IPPacketInformation((ipaddress != null) ? ipaddress : IPAddress.IPv6None, (int)controlDataIPv.index);
			}
			else
			{
				this.m_ReceiveMessageFromPacketInfo = default(IPPacketInformation);
			}
			IL_593:
			if (socketError != SocketError.Success)
			{
				this.SetResults(socketError, bytesTransferred, flags);
				this.m_CurrentSocket.UpdateStatusAfterSocketError(socketError);
			}
			this.Complete();
			if (this.m_ContextCopy == null)
			{
				this.OnCompleted(this);
				return;
			}
			ExecutionContext.Run(this.m_ContextCopy, this.m_ExecutionCallback, null);
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x000A0AF4 File Offset: 0x0009ECF4
		private unsafe void CompletionPortCallback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
		{
			SocketFlags socketFlags = SocketFlags.None;
			SocketError socketError = (SocketError)errorCode;
			if (socketError == SocketError.Success)
			{
				this.FinishOperationSuccess(socketError, (int)numBytes, socketFlags);
				return;
			}
			if (socketError != SocketError.OperationAborted)
			{
				if (this.m_CurrentSocket.CleanedUp)
				{
					socketError = SocketError.OperationAborted;
				}
				else
				{
					try
					{
						bool flag = UnsafeNclNativeMethods.OSSOCK.WSAGetOverlappedResult(this.m_CurrentSocket.SafeHandle, this.m_PtrNativeOverlapped, out numBytes, false, out socketFlags);
						socketError = (SocketError)Marshal.GetLastWin32Error();
					}
					catch
					{
						socketError = SocketError.OperationAborted;
					}
				}
			}
			this.FinishOperationAsyncFailure(socketError, (int)numBytes, socketFlags);
		}

		// Token: 0x04001E61 RID: 7777
		internal static readonly int s_ControlDataSize = Marshal.SizeOf(typeof(UnsafeNclNativeMethods.OSSOCK.ControlData));

		// Token: 0x04001E62 RID: 7778
		internal static readonly int s_ControlDataIPv6Size = Marshal.SizeOf(typeof(UnsafeNclNativeMethods.OSSOCK.ControlDataIPv6));

		// Token: 0x04001E63 RID: 7779
		internal static readonly int s_WSAMsgSize = Marshal.SizeOf(typeof(UnsafeNclNativeMethods.OSSOCK.WSAMsg));

		// Token: 0x04001E64 RID: 7780
		internal Socket m_AcceptSocket;

		// Token: 0x04001E65 RID: 7781
		private Socket m_ConnectSocket;

		// Token: 0x04001E66 RID: 7782
		internal byte[] m_Buffer;

		// Token: 0x04001E67 RID: 7783
		internal WSABuffer m_WSABuffer;

		// Token: 0x04001E68 RID: 7784
		internal IntPtr m_PtrSingleBuffer;

		// Token: 0x04001E69 RID: 7785
		internal int m_Count;

		// Token: 0x04001E6A RID: 7786
		internal int m_Offset;

		// Token: 0x04001E6B RID: 7787
		internal IList<ArraySegment<byte>> m_BufferList;

		// Token: 0x04001E6C RID: 7788
		private bool m_BufferListChanged;

		// Token: 0x04001E6D RID: 7789
		internal WSABuffer[] m_WSABufferArray;

		// Token: 0x04001E6E RID: 7790
		private int m_BytesTransferred;

		// Token: 0x04001E70 RID: 7792
		private bool m_CompletedChanged;

		// Token: 0x04001E71 RID: 7793
		private bool m_DisconnectReuseSocket;

		// Token: 0x04001E72 RID: 7794
		private SocketAsyncOperation m_CompletedOperation;

		// Token: 0x04001E73 RID: 7795
		private IPPacketInformation m_ReceiveMessageFromPacketInfo;

		// Token: 0x04001E74 RID: 7796
		private EndPoint m_RemoteEndPoint;

		// Token: 0x04001E75 RID: 7797
		internal TransmitFileOptions m_SendPacketsFlags;

		// Token: 0x04001E76 RID: 7798
		internal int m_SendPacketsSendSize;

		// Token: 0x04001E77 RID: 7799
		internal SendPacketsElement[] m_SendPacketsElements;

		// Token: 0x04001E78 RID: 7800
		private SendPacketsElement[] m_SendPacketsElementsInternal;

		// Token: 0x04001E79 RID: 7801
		internal int m_SendPacketsElementsFileCount;

		// Token: 0x04001E7A RID: 7802
		internal int m_SendPacketsElementsBufferCount;

		// Token: 0x04001E7B RID: 7803
		private SocketError m_SocketError;

		// Token: 0x04001E7C RID: 7804
		private Exception m_ConnectByNameError;

		// Token: 0x04001E7D RID: 7805
		internal SocketFlags m_SocketFlags;

		// Token: 0x04001E7E RID: 7806
		private object m_UserToken;

		// Token: 0x04001E7F RID: 7807
		internal byte[] m_AcceptBuffer;

		// Token: 0x04001E80 RID: 7808
		internal int m_AcceptAddressBufferCount;

		// Token: 0x04001E81 RID: 7809
		internal IntPtr m_PtrAcceptBuffer;

		// Token: 0x04001E82 RID: 7810
		internal SocketAddress m_SocketAddress;

		// Token: 0x04001E83 RID: 7811
		private GCHandle m_SocketAddressGCHandle;

		// Token: 0x04001E84 RID: 7812
		private SocketAddress m_PinnedSocketAddress;

		// Token: 0x04001E85 RID: 7813
		internal IntPtr m_PtrSocketAddressBuffer;

		// Token: 0x04001E86 RID: 7814
		internal IntPtr m_PtrSocketAddressBufferSize;

		// Token: 0x04001E87 RID: 7815
		private byte[] m_WSAMessageBuffer;

		// Token: 0x04001E88 RID: 7816
		private GCHandle m_WSAMessageBufferGCHandle;

		// Token: 0x04001E89 RID: 7817
		internal IntPtr m_PtrWSAMessageBuffer;

		// Token: 0x04001E8A RID: 7818
		private byte[] m_ControlBuffer;

		// Token: 0x04001E8B RID: 7819
		private GCHandle m_ControlBufferGCHandle;

		// Token: 0x04001E8C RID: 7820
		internal IntPtr m_PtrControlBuffer;

		// Token: 0x04001E8D RID: 7821
		private WSABuffer[] m_WSARecvMsgWSABufferArray;

		// Token: 0x04001E8E RID: 7822
		private GCHandle m_WSARecvMsgWSABufferArrayGCHandle;

		// Token: 0x04001E8F RID: 7823
		private IntPtr m_PtrWSARecvMsgWSABufferArray;

		// Token: 0x04001E90 RID: 7824
		internal FileStream[] m_SendPacketsFileStreams;

		// Token: 0x04001E91 RID: 7825
		internal SafeHandle[] m_SendPacketsFileHandles;

		// Token: 0x04001E92 RID: 7826
		internal UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElement[] m_SendPacketsDescriptor;

		// Token: 0x04001E93 RID: 7827
		internal IntPtr m_PtrSendPacketsDescriptor;

		// Token: 0x04001E94 RID: 7828
		private ExecutionContext m_Context;

		// Token: 0x04001E95 RID: 7829
		private ExecutionContext m_ContextCopy;

		// Token: 0x04001E96 RID: 7830
		private ContextCallback m_ExecutionCallback;

		// Token: 0x04001E97 RID: 7831
		private Socket m_CurrentSocket;

		// Token: 0x04001E98 RID: 7832
		private bool m_DisposeCalled;

		// Token: 0x04001E99 RID: 7833
		private const int Configuring = -1;

		// Token: 0x04001E9A RID: 7834
		private const int Free = 0;

		// Token: 0x04001E9B RID: 7835
		private const int InProgress = 1;

		// Token: 0x04001E9C RID: 7836
		private const int Disposed = 2;

		// Token: 0x04001E9D RID: 7837
		private int m_Operating;

		// Token: 0x04001E9E RID: 7838
		internal SafeNativeOverlapped m_PtrNativeOverlapped;

		// Token: 0x04001E9F RID: 7839
		private Overlapped m_Overlapped;

		// Token: 0x04001EA0 RID: 7840
		private object[] m_ObjectsToPin;

		// Token: 0x04001EA1 RID: 7841
		private SocketAsyncEventArgs.PinState m_PinState;

		// Token: 0x04001EA2 RID: 7842
		private byte[] m_PinnedAcceptBuffer;

		// Token: 0x04001EA3 RID: 7843
		private byte[] m_PinnedSingleBuffer;

		// Token: 0x04001EA4 RID: 7844
		private int m_PinnedSingleBufferOffset;

		// Token: 0x04001EA5 RID: 7845
		private int m_PinnedSingleBufferCount;

		// Token: 0x04001EA6 RID: 7846
		private MultipleConnectAsync m_MultipleConnect;

		// Token: 0x04001EA7 RID: 7847
		private static bool s_LoggingEnabled = Logging.On;

		// Token: 0x020007DE RID: 2014
		private enum PinState
		{
			// Token: 0x040034B3 RID: 13491
			None,
			// Token: 0x040034B4 RID: 13492
			NoBuffer,
			// Token: 0x040034B5 RID: 13493
			SingleAcceptBuffer,
			// Token: 0x040034B6 RID: 13494
			SingleBuffer,
			// Token: 0x040034B7 RID: 13495
			MultipleBuffer,
			// Token: 0x040034B8 RID: 13496
			SendPackets
		}
	}
}
