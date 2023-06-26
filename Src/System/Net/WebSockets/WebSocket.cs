using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	/// <summary>The WebSocket class allows applications to send and receive data after the WebSocket upgrade has completed.</summary>
	// Token: 0x0200022F RID: 559
	public abstract class WebSocket : IDisposable
	{
		/// <summary>Indicates the reason why the remote endpoint initiated the close handshake.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketCloseStatus" />.</returns>
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060014A2 RID: 5282
		public abstract WebSocketCloseStatus? CloseStatus { get; }

		/// <summary>Allows the remote endpoint to describe the reason why the connection was closed.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060014A3 RID: 5283
		public abstract string CloseStatusDescription { get; }

		/// <summary>Gets the subprotocol that was negotiated during the opening handshake.</summary>
		/// <returns>The subprotocol that was negotiated during the opening handshake.</returns>
		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060014A4 RID: 5284
		public abstract string SubProtocol { get; }

		/// <summary>Returns the current state of the WebSocket connection.</summary>
		/// <returns>The current state of the WebSocket connection.</returns>
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060014A5 RID: 5285
		public abstract WebSocketState State { get; }

		/// <summary>Gets the default WebSocket protocol keep-alive interval.</summary>
		/// <returns>The default WebSocket protocol keep-alive interval. The typical value for this interval is 30 seconds (as defined by the OS or the .NET platform). It is used to initialize <see cref="P:System.Net.WebSockets.ClientWebSocketOptions.KeepAliveInterval" /> value.</returns>
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x0006C703 File Offset: 0x0006A903
		public static TimeSpan DefaultKeepAliveInterval
		{
			get
			{
				if (WebSocket.defaultKeepAliveInterval == null)
				{
					if (WebSocketProtocolComponent.IsSupported)
					{
						WebSocket.defaultKeepAliveInterval = new TimeSpan?(WebSocketProtocolComponent.WebSocketGetDefaultKeepAliveInterval());
					}
					else
					{
						WebSocket.defaultKeepAliveInterval = new TimeSpan?(Timeout.InfiniteTimeSpan);
					}
				}
				return WebSocket.defaultKeepAliveInterval.Value;
			}
		}

		/// <summary>Create client buffers to use with this <see cref="T:System.Net.WebSockets.WebSocket" /> instance.</summary>
		/// <param name="receiveBufferSize">The size, in bytes, of the client receive buffer.</param>
		/// <param name="sendBufferSize">The size, in bytes, of the send buffer.</param>
		/// <returns>An array with the client buffers.</returns>
		// Token: 0x060014A7 RID: 5287 RVA: 0x0006C742 File Offset: 0x0006A942
		public static ArraySegment<byte> CreateClientBuffer(int receiveBufferSize, int sendBufferSize)
		{
			WebSocketHelpers.ValidateBufferSizes(receiveBufferSize, sendBufferSize);
			return WebSocketBuffer.CreateInternalBufferArraySegment(receiveBufferSize, sendBufferSize, false);
		}

		/// <summary>Creates a WebSocket server buffer.</summary>
		/// <param name="receiveBufferSize">The size, in bytes, of the desired buffer.</param>
		/// <returns>Returns <see cref="T:System.ArraySegment`1" />.</returns>
		// Token: 0x060014A8 RID: 5288 RVA: 0x0006C753 File Offset: 0x0006A953
		public static ArraySegment<byte> CreateServerBuffer(int receiveBufferSize)
		{
			WebSocketHelpers.ValidateBufferSizes(receiveBufferSize, 16);
			return WebSocketBuffer.CreateInternalBufferArraySegment(receiveBufferSize, 16, true);
		}

		/// <summary>Allows callers to create a client side WebSocket class which will use the WSPC for framing purposes.</summary>
		/// <param name="innerStream">The connection to be used for IO operations.</param>
		/// <param name="subProtocol">The subprotocol accepted by the client.</param>
		/// <param name="receiveBufferSize">The size in bytes of the client WebSocket receive buffer.</param>
		/// <param name="sendBufferSize">The size in bytes of the client WebSocket send buffer.</param>
		/// <param name="keepAliveInterval">Determines how regularly a frame is sent over the connection as a keep-alive. Applies only when the connection is idle.</param>
		/// <param name="useZeroMaskingKey">Indicates whether a random key or a static key (just zeros) should be used for the WebSocket masking.</param>
		/// <param name="internalBuffer">Will be used as the internal buffer in the WPC. The size has to be at least <c>2 * ReceiveBufferSize + SendBufferSize + 256 + 20 (16 on 32-bit)</c>.</param>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocket" />.</returns>
		// Token: 0x060014A9 RID: 5289 RVA: 0x0006C768 File Offset: 0x0006A968
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static WebSocket CreateClientWebSocket(Stream innerStream, string subProtocol, int receiveBufferSize, int sendBufferSize, TimeSpan keepAliveInterval, bool useZeroMaskingKey, ArraySegment<byte> internalBuffer)
		{
			if (!WebSocketProtocolComponent.IsSupported)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
			}
			WebSocketHelpers.ValidateInnerStream(innerStream);
			WebSocketHelpers.ValidateOptions(subProtocol, receiveBufferSize, sendBufferSize, keepAliveInterval);
			WebSocketHelpers.ValidateArraySegment<byte>(internalBuffer, "internalBuffer");
			WebSocketBuffer.Validate(internalBuffer.Count, receiveBufferSize, sendBufferSize, false);
			return new InternalClientWebSocket(innerStream, subProtocol, receiveBufferSize, sendBufferSize, keepAliveInterval, useZeroMaskingKey, internalBuffer);
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x0006C7BC File Offset: 0x0006A9BC
		internal static WebSocket CreateServerWebSocket(Stream innerStream, string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval, ArraySegment<byte> internalBuffer)
		{
			if (!WebSocketProtocolComponent.IsSupported)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
			}
			WebSocketHelpers.ValidateInnerStream(innerStream);
			WebSocketHelpers.ValidateOptions(subProtocol, receiveBufferSize, 16, keepAliveInterval);
			WebSocketHelpers.ValidateArraySegment<byte>(internalBuffer, "internalBuffer");
			WebSocketBuffer.Validate(internalBuffer.Count, receiveBufferSize, 16, true);
			return new ServerWebSocket(innerStream, subProtocol, receiveBufferSize, keepAliveInterval, internalBuffer);
		}

		/// <summary>Allows callers to register prefixes for WebSocket requests (ws and wss).</summary>
		// Token: 0x060014AB RID: 5291 RVA: 0x0006C80C File Offset: 0x0006AA0C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void RegisterPrefixes()
		{
			WebRequest.RegisterPrefix(Uri.UriSchemeWs + ":", new WebSocketHttpRequestCreator(false));
			WebRequest.RegisterPrefix(Uri.UriSchemeWss + ":", new WebSocketHttpRequestCreator(true));
		}

		/// <summary>Returns a value that indicates if the WebSocket instance is targeting .NET Framework 4.5.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.WebSockets.WebSocket" /> is targeting .NET Framework 4.5; otherwise, <see langword="false" />.</returns>
		// Token: 0x060014AC RID: 5292 RVA: 0x0006C844 File Offset: 0x0006AA44
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.")]
		public static bool IsApplicationTargeting45()
		{
			return BinaryCompatibility.TargetsAtLeast_Desktop_V4_5;
		}

		/// <summary>Aborts the WebSocket connection and cancels any pending IO operations.</summary>
		// Token: 0x060014AD RID: 5293
		public abstract void Abort();

		/// <summary>Closes the WebSocket connection as an asynchronous operation using the close handshake defined in the WebSocket protocol specification section 7.</summary>
		/// <param name="closeStatus">Indicates the reason for closing the WebSocket connection.</param>
		/// <param name="statusDescription">Specifies a human readable explanation as to why the connection is closed.</param>
		/// <param name="cancellationToken">The token that can be used to propagate notification that operations should be canceled.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x060014AE RID: 5294
		public abstract Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken);

		/// <summary>Initiates or completes the close handshake defined in the WebSocket protocol specification section 7.</summary>
		/// <param name="closeStatus">Indicates the reason for closing the WebSocket connection.</param>
		/// <param name="statusDescription">Allows applications to specify a human readable explanation as to why the connection is closed.</param>
		/// <param name="cancellationToken">The token that can be used to propagate notification that operations should be canceled.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x060014AF RID: 5295
		public abstract Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken);

		/// <summary>Used to clean up unmanaged resources for ASP.NET and self-hosted implementations.</summary>
		// Token: 0x060014B0 RID: 5296
		public abstract void Dispose();

		/// <summary>Receives data from the <see cref="T:System.Net.WebSockets.WebSocket" /> connection asynchronously.</summary>
		/// <param name="buffer">References the application buffer that is the storage location for the received data.</param>
		/// <param name="cancellationToken">Propagates the notification that operations should be canceled.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the received data.</returns>
		// Token: 0x060014B1 RID: 5297
		public abstract Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken);

		/// <summary>Sends data over the <see cref="T:System.Net.WebSockets.WebSocket" /> connection asynchronously.</summary>
		/// <param name="buffer">The buffer to be sent over the connection.</param>
		/// <param name="messageType">Indicates whether the application is sending a binary or text message.</param>
		/// <param name="endOfMessage">Indicates whether the data in "buffer" is the last part of a message.</param>
		/// <param name="cancellationToken">The token that propagates the notification that operations should be canceled.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x060014B2 RID: 5298
		public abstract Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken);

		/// <summary>Verifies that the connection is in an expected state.</summary>
		/// <param name="state">The current state of the WebSocket to be tested against the list of valid states.</param>
		/// <param name="validStates">List of valid connection states.</param>
		// Token: 0x060014B3 RID: 5299 RVA: 0x0006C84C File Offset: 0x0006AA4C
		protected static void ThrowOnInvalidState(WebSocketState state, params WebSocketState[] validStates)
		{
			string text = string.Empty;
			if (validStates != null && validStates.Length != 0)
			{
				foreach (WebSocketState webSocketState in validStates)
				{
					if (state == webSocketState)
					{
						return;
					}
				}
				text = string.Join<WebSocketState>(", ", validStates);
			}
			throw new WebSocketException(SR.GetString("net_WebSockets_InvalidState", new object[] { state, text }));
		}

		/// <summary>Returns a value that indicates if the state of the WebSocket instance is closed or aborted.</summary>
		/// <param name="state">The current state of the WebSocket.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.WebSockets.WebSocket" /> is closed or aborted; otherwise, <see langword="false" />.</returns>
		// Token: 0x060014B4 RID: 5300 RVA: 0x0006C8AD File Offset: 0x0006AAAD
		protected static bool IsStateTerminal(WebSocketState state)
		{
			return state == WebSocketState.Closed || state == WebSocketState.Aborted;
		}

		// Token: 0x0400163F RID: 5695
		private static TimeSpan? defaultKeepAliveInterval;
	}
}
