using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	/// <summary>Provides a client for connecting to WebSocket services.</summary>
	// Token: 0x0200022A RID: 554
	public sealed class ClientWebSocket : WebSocket
	{
		// Token: 0x06001461 RID: 5217 RVA: 0x0006BAF2 File Offset: 0x00069CF2
		static ClientWebSocket()
		{
			WebSocket.RegisterPrefixes();
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> class.</summary>
		// Token: 0x06001462 RID: 5218 RVA: 0x0006BAFC File Offset: 0x00069CFC
		public ClientWebSocket()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.WebSockets, this, ".ctor", null);
			}
			if (!WebSocketProtocolComponent.IsSupported)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
			}
			this.state = 0;
			this.options = new ClientWebSocketOptions();
			this.cts = new CancellationTokenSource();
			if (Logging.On)
			{
				Logging.Exit(Logging.WebSockets, this, ".ctor", null);
			}
		}

		/// <summary>Gets the WebSocket options for the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</summary>
		/// <returns>The WebSocket options for the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</returns>
		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x0006BB68 File Offset: 0x00069D68
		public ClientWebSocketOptions Options
		{
			get
			{
				return this.options;
			}
		}

		/// <summary>Gets the reason why the close handshake was initiated on <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</summary>
		/// <returns>The reason why the close handshake was initiated.</returns>
		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x0006BB70 File Offset: 0x00069D70
		public override WebSocketCloseStatus? CloseStatus
		{
			get
			{
				if (this.innerWebSocket != null)
				{
					return this.innerWebSocket.CloseStatus;
				}
				return null;
			}
		}

		/// <summary>Gets a description of the reason why the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance was closed.</summary>
		/// <returns>The description of the reason why the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance was closed.</returns>
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x0006BB9A File Offset: 0x00069D9A
		public override string CloseStatusDescription
		{
			get
			{
				if (this.innerWebSocket != null)
				{
					return this.innerWebSocket.CloseStatusDescription;
				}
				return null;
			}
		}

		/// <summary>Gets the supported WebSocket sub-protocol for the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</summary>
		/// <returns>The supported WebSocket sub-protocol.</returns>
		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x0006BBB1 File Offset: 0x00069DB1
		public override string SubProtocol
		{
			get
			{
				if (this.innerWebSocket != null)
				{
					return this.innerWebSocket.SubProtocol;
				}
				return null;
			}
		}

		/// <summary>Gets the WebSocket state of the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</summary>
		/// <returns>The WebSocket state of the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</returns>
		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x0006BBC8 File Offset: 0x00069DC8
		public override WebSocketState State
		{
			get
			{
				if (this.innerWebSocket != null)
				{
					return this.innerWebSocket.State;
				}
				switch (this.state)
				{
				case 0:
					return WebSocketState.None;
				case 1:
					return WebSocketState.Connecting;
				case 3:
					return WebSocketState.Closed;
				}
				return WebSocketState.Closed;
			}
		}

		/// <summary>Connect to a WebSocket server as an asynchronous operation.</summary>
		/// <param name="uri">The URI of the WebSocket server to connect to.</param>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that the  operation should be canceled.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06001468 RID: 5224 RVA: 0x0006BC10 File Offset: 0x00069E10
		public Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (!uri.IsAbsoluteUri)
			{
				throw new ArgumentException(SR.GetString("net_uri_NotAbsolute"), "uri");
			}
			if (uri.Scheme != Uri.UriSchemeWs && uri.Scheme != Uri.UriSchemeWss)
			{
				throw new ArgumentException(SR.GetString("net_WebSockets_Scheme"), "uri");
			}
			int num = Interlocked.CompareExchange(ref this.state, 1, 0);
			if (num == 3)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (num != 0)
			{
				throw new InvalidOperationException(SR.GetString("net_WebSockets_AlreadyStarted"));
			}
			this.options.SetToReadOnly();
			return this.ConnectAsyncCore(uri, cancellationToken);
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0006BCD0 File Offset: 0x00069ED0
		private async Task ConnectAsyncCore(Uri uri, CancellationToken cancellationToken)
		{
			HttpWebResponse response = null;
			CancellationTokenRegistration connectCancellation = default(CancellationTokenRegistration);
			try
			{
				HttpWebRequest request = this.CreateAndConfigureRequest(uri);
				if (Logging.On)
				{
					Logging.Associate(Logging.WebSockets, this, request);
				}
				connectCancellation = cancellationToken.Register(new Action<object>(this.AbortRequest), request, false);
				WebResponse webResponse = await request.GetResponseAsync().SuppressContextFlow<WebResponse>();
				response = webResponse as HttpWebResponse;
				if (Logging.On)
				{
					Logging.Associate(Logging.WebSockets, this, response);
				}
				string text = this.ValidateResponse(request, response);
				this.innerWebSocket = WebSocket.CreateClientWebSocket(response.GetResponseStream(), text, this.options.ReceiveBufferSize, this.options.SendBufferSize, this.options.KeepAliveInterval, false, this.options.GetOrCreateBuffer());
				if (Logging.On)
				{
					Logging.Associate(Logging.WebSockets, this, this.innerWebSocket);
				}
				if (Interlocked.CompareExchange(ref this.state, 2, 1) != 1)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				request = null;
			}
			catch (WebException ex)
			{
				this.ConnectExceptionCleanup(response);
				WebSocketException ex2 = new WebSocketException(SR.GetString("net_webstatus_ConnectFailure"), ex);
				if (Logging.On)
				{
					Logging.Exception(Logging.WebSockets, this, "ConnectAsync", ex2);
				}
				throw ex2;
			}
			catch (Exception ex3)
			{
				this.ConnectExceptionCleanup(response);
				if (Logging.On)
				{
					Logging.Exception(Logging.WebSockets, this, "ConnectAsync", ex3);
				}
				throw;
			}
			finally
			{
				connectCancellation.Dispose();
			}
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0006BD23 File Offset: 0x00069F23
		private void ConnectExceptionCleanup(HttpWebResponse response)
		{
			this.Dispose();
			if (response != null)
			{
				response.Dispose();
			}
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x0006BD34 File Offset: 0x00069F34
		private HttpWebRequest CreateAndConfigureRequest(Uri uri)
		{
			HttpWebRequest httpWebRequest = WebRequest.Create(uri) as HttpWebRequest;
			if (httpWebRequest == null)
			{
				throw new InvalidOperationException(SR.GetString("net_WebSockets_InvalidRegistration"));
			}
			foreach (object obj in this.options.RequestHeaders.Keys)
			{
				string text = (string)obj;
				httpWebRequest.Headers.Add(text, this.options.RequestHeaders[text]);
			}
			if (this.options.RequestedSubProtocols.Count > 0)
			{
				httpWebRequest.Headers.Add("Sec-WebSocket-Protocol", string.Join(", ", this.options.RequestedSubProtocols));
			}
			if (this.options.UseDefaultCredentials)
			{
				httpWebRequest.UseDefaultCredentials = true;
			}
			else if (this.options.Credentials != null)
			{
				httpWebRequest.Credentials = this.options.Credentials;
			}
			if (this.options.InternalClientCertificates != null)
			{
				httpWebRequest.ClientCertificates = this.options.InternalClientCertificates;
			}
			httpWebRequest.Proxy = this.options.Proxy;
			httpWebRequest.CookieContainer = this.options.Cookies;
			this.cts.Token.Register(new Action<object>(this.AbortRequest), httpWebRequest, false);
			return httpWebRequest;
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0006BE9C File Offset: 0x0006A09C
		private string ValidateResponse(HttpWebRequest request, HttpWebResponse response)
		{
			if (response.StatusCode != HttpStatusCode.SwitchingProtocols)
			{
				throw new WebSocketException(SR.GetString("net_WebSockets_Connect101Expected", new object[] { (int)response.StatusCode }));
			}
			string text = response.Headers["Upgrade"];
			if (!string.Equals(text, "websocket", StringComparison.OrdinalIgnoreCase))
			{
				throw new WebSocketException(SR.GetString("net_WebSockets_InvalidResponseHeader", new object[] { "Upgrade", text }));
			}
			string text2 = response.Headers["Connection"];
			if (!string.Equals(text2, "Upgrade", StringComparison.OrdinalIgnoreCase))
			{
				throw new WebSocketException(SR.GetString("net_WebSockets_InvalidResponseHeader", new object[] { "Connection", text2 }));
			}
			string text3 = response.Headers["Sec-WebSocket-Accept"];
			string secWebSocketAcceptString = WebSocketHelpers.GetSecWebSocketAcceptString(request.Headers["Sec-WebSocket-Key"]);
			if (!string.Equals(text3, secWebSocketAcceptString, StringComparison.OrdinalIgnoreCase))
			{
				throw new WebSocketException(SR.GetString("net_WebSockets_InvalidResponseHeader", new object[] { "Sec-WebSocket-Accept", text3 }));
			}
			string text4 = response.Headers["Sec-WebSocket-Protocol"];
			if (!string.IsNullOrWhiteSpace(text4) && this.options.RequestedSubProtocols.Count > 0)
			{
				bool flag = false;
				foreach (string text5 in this.options.RequestedSubProtocols)
				{
					if (string.Equals(text5, text4, StringComparison.OrdinalIgnoreCase))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					throw new WebSocketException(SR.GetString("net_WebSockets_AcceptUnsupportedProtocol", new object[]
					{
						string.Join(", ", this.options.RequestedSubProtocols),
						text4
					}));
				}
			}
			if (!string.IsNullOrWhiteSpace(text4))
			{
				return text4;
			}
			return null;
		}

		/// <summary>Send data on <see cref="T:System.Net.WebSockets.ClientWebSocket" /> as an asynchronous operation.</summary>
		/// <param name="buffer">The buffer containing the message to be sent.</param>
		/// <param name="messageType">Specifies whether the buffer is clear text or in a binary format.</param>
		/// <param name="endOfMessage">Specifies whether this is the final asynchronous send. Set to <see langword="true" /> if this is the final send; <see langword="false" /> otherwise.</param>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that this  operation should be canceled.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x0600146D RID: 5229 RVA: 0x0006C07C File Offset: 0x0006A27C
		public override Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this.innerWebSocket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);
		}

		/// <summary>Receives data on <see cref="T:System.Net.WebSockets.ClientWebSocket" /> as an asynchronous operation.</summary>
		/// <param name="buffer">The buffer to receive the response.</param>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that this  operation should be canceled.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x0600146E RID: 5230 RVA: 0x0006C094 File Offset: 0x0006A294
		public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this.innerWebSocket.ReceiveAsync(buffer, cancellationToken);
		}

		/// <summary>Close the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance as an asynchronous operation.</summary>
		/// <param name="closeStatus">The WebSocket close status.</param>
		/// <param name="statusDescription">A description of the close status.</param>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that this  operation should be canceled.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x0600146F RID: 5231 RVA: 0x0006C0A9 File Offset: 0x0006A2A9
		public override Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this.innerWebSocket.CloseAsync(closeStatus, statusDescription, cancellationToken);
		}

		/// <summary>Close the output for the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance as an asynchronous operation.</summary>
		/// <param name="closeStatus">The WebSocket close status.</param>
		/// <param name="statusDescription">A description of the close status.</param>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that this  operation should be canceled.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		// Token: 0x06001470 RID: 5232 RVA: 0x0006C0BF File Offset: 0x0006A2BF
		public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this.innerWebSocket.CloseOutputAsync(closeStatus, statusDescription, cancellationToken);
		}

		/// <summary>Aborts the connection and cancels any pending IO operations.</summary>
		// Token: 0x06001471 RID: 5233 RVA: 0x0006C0D5 File Offset: 0x0006A2D5
		public override void Abort()
		{
			if (this.state == 3)
			{
				return;
			}
			if (this.innerWebSocket != null)
			{
				this.innerWebSocket.Abort();
			}
			this.Dispose();
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x0006C0FC File Offset: 0x0006A2FC
		private void AbortRequest(object obj)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)obj;
			httpWebRequest.Abort();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</summary>
		// Token: 0x06001473 RID: 5235 RVA: 0x0006C118 File Offset: 0x0006A318
		public override void Dispose()
		{
			int num = Interlocked.Exchange(ref this.state, 3);
			if (num == 3)
			{
				return;
			}
			this.cts.Cancel(false);
			this.cts.Dispose();
			if (this.innerWebSocket != null)
			{
				this.innerWebSocket.Dispose();
			}
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x0006C161 File Offset: 0x0006A361
		private void ThrowIfNotConnected()
		{
			if (this.state == 3)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (this.state != 2)
			{
				throw new InvalidOperationException(SR.GetString("net_WebSockets_NotConnected"));
			}
		}

		// Token: 0x0400161B RID: 5659
		private readonly ClientWebSocketOptions options;

		// Token: 0x0400161C RID: 5660
		private WebSocket innerWebSocket;

		// Token: 0x0400161D RID: 5661
		private readonly CancellationTokenSource cts;

		// Token: 0x0400161E RID: 5662
		private int state;

		// Token: 0x0400161F RID: 5663
		private const int created = 0;

		// Token: 0x04001620 RID: 5664
		private const int connecting = 1;

		// Token: 0x04001621 RID: 5665
		private const int connected = 2;

		// Token: 0x04001622 RID: 5666
		private const int disposed = 3;
	}
}
