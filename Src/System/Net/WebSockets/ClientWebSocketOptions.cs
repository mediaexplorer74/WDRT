using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace System.Net.WebSockets
{
	/// <summary>Options to use with a  <see cref="T:System.Net.WebSockets.ClientWebSocket" /> object.</summary>
	// Token: 0x0200022B RID: 555
	public sealed class ClientWebSocketOptions
	{
		// Token: 0x06001475 RID: 5237 RVA: 0x0006C198 File Offset: 0x0006A398
		internal ClientWebSocketOptions()
		{
			this.requestedSubProtocols = new List<string>();
			this.requestHeaders = new WebHeaderCollection(WebHeaderCollectionType.HttpWebRequest);
			this.Proxy = WebRequest.DefaultWebProxy;
			this.receiveBufferSize = 16384;
			this.sendBufferSize = 16384;
			this.keepAliveInterval = WebSocket.DefaultKeepAliveInterval;
		}

		/// <summary>Creates a HTTP request header and its value.</summary>
		/// <param name="headerName">The name of the HTTP header.</param>
		/// <param name="headerValue">The value of the HTTP header.</param>
		// Token: 0x06001476 RID: 5238 RVA: 0x0006C1EE File Offset: 0x0006A3EE
		public void SetRequestHeader(string headerName, string headerValue)
		{
			this.ThrowIfReadOnly();
			this.requestHeaders.Set(headerName, headerValue);
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x0006C203 File Offset: 0x0006A403
		internal WebHeaderCollection RequestHeaders
		{
			get
			{
				return this.requestHeaders;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that indicates if default credentials should be used during WebSocket handshake.</summary>
		/// <returns>
		///   <see langword="true" /> if default credentials should be used during WebSocket handshake; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x0006C20B File Offset: 0x0006A40B
		// (set) Token: 0x06001479 RID: 5241 RVA: 0x0006C213 File Offset: 0x0006A413
		public bool UseDefaultCredentials
		{
			get
			{
				return this.useDefaultCredentials;
			}
			set
			{
				this.ThrowIfReadOnly();
				this.useDefaultCredentials = value;
			}
		}

		/// <summary>Gets or sets the credential information for the client.</summary>
		/// <returns>The credential information for the client.</returns>
		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x0006C222 File Offset: 0x0006A422
		// (set) Token: 0x0600147B RID: 5243 RVA: 0x0006C22A File Offset: 0x0006A42A
		public ICredentials Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.ThrowIfReadOnly();
				this.credentials = value;
			}
		}

		/// <summary>Gets or sets the proxy for WebSocket requests.</summary>
		/// <returns>The proxy for WebSocket requests.</returns>
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x0006C239 File Offset: 0x0006A439
		// (set) Token: 0x0600147D RID: 5245 RVA: 0x0006C241 File Offset: 0x0006A441
		public IWebProxy Proxy
		{
			get
			{
				return this.proxy;
			}
			set
			{
				this.ThrowIfReadOnly();
				this.proxy = value;
			}
		}

		/// <summary>Gets or sets a collection of client side certificates.</summary>
		/// <returns>A collection of client side certificates.</returns>
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x0006C250 File Offset: 0x0006A450
		// (set) Token: 0x0600147F RID: 5247 RVA: 0x0006C26B File Offset: 0x0006A46B
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this.clientCertificates == null)
				{
					this.clientCertificates = new X509CertificateCollection();
				}
				return this.clientCertificates;
			}
			set
			{
				this.ThrowIfReadOnly();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.clientCertificates = value;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x0006C288 File Offset: 0x0006A488
		internal X509CertificateCollection InternalClientCertificates
		{
			get
			{
				return this.clientCertificates;
			}
		}

		/// <summary>Gets or sets the cookies associated with the request.</summary>
		/// <returns>The cookies associated with the request.</returns>
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x0006C290 File Offset: 0x0006A490
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x0006C298 File Offset: 0x0006A498
		public CookieContainer Cookies
		{
			get
			{
				return this.cookies;
			}
			set
			{
				this.ThrowIfReadOnly();
				this.cookies = value;
			}
		}

		/// <summary>Sets the client buffer parameters.</summary>
		/// <param name="receiveBufferSize">The size, in bytes, of the client receive buffer.</param>
		/// <param name="sendBufferSize">The size, in bytes, of the client send buffer.</param>
		// Token: 0x06001483 RID: 5251 RVA: 0x0006C2A7 File Offset: 0x0006A4A7
		public void SetBuffer(int receiveBufferSize, int sendBufferSize)
		{
			this.ThrowIfReadOnly();
			WebSocketHelpers.ValidateBufferSizes(receiveBufferSize, sendBufferSize);
			this.buffer = null;
			this.receiveBufferSize = receiveBufferSize;
			this.sendBufferSize = sendBufferSize;
		}

		/// <summary>Sets client buffer parameters.</summary>
		/// <param name="receiveBufferSize">The size, in bytes, of the client receive buffer.</param>
		/// <param name="sendBufferSize">The size, in bytes, of the client send buffer.</param>
		/// <param name="buffer">The receive buffer to use.</param>
		// Token: 0x06001484 RID: 5252 RVA: 0x0006C2D0 File Offset: 0x0006A4D0
		public void SetBuffer(int receiveBufferSize, int sendBufferSize, ArraySegment<byte> buffer)
		{
			this.ThrowIfReadOnly();
			WebSocketHelpers.ValidateBufferSizes(receiveBufferSize, sendBufferSize);
			WebSocketHelpers.ValidateArraySegment<byte>(buffer, "buffer");
			WebSocketBuffer.Validate(buffer.Count, receiveBufferSize, sendBufferSize, false);
			this.receiveBufferSize = receiveBufferSize;
			this.sendBufferSize = sendBufferSize;
			if (AppDomain.CurrentDomain.IsFullyTrusted)
			{
				this.buffer = new ArraySegment<byte>?(buffer);
				return;
			}
			this.buffer = null;
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x0006C337 File Offset: 0x0006A537
		internal int ReceiveBufferSize
		{
			get
			{
				return this.receiveBufferSize;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x0006C33F File Offset: 0x0006A53F
		internal int SendBufferSize
		{
			get
			{
				return this.sendBufferSize;
			}
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0006C347 File Offset: 0x0006A547
		internal ArraySegment<byte> GetOrCreateBuffer()
		{
			if (this.buffer == null)
			{
				this.buffer = new ArraySegment<byte>?(WebSocket.CreateClientBuffer(this.receiveBufferSize, this.sendBufferSize));
			}
			return this.buffer.Value;
		}

		/// <summary>Adds a sub-protocol to be negotiated during the WebSocket connection handshake.</summary>
		/// <param name="subProtocol">The WebSocket sub-protocol to add.</param>
		// Token: 0x06001488 RID: 5256 RVA: 0x0006C380 File Offset: 0x0006A580
		public void AddSubProtocol(string subProtocol)
		{
			this.ThrowIfReadOnly();
			WebSocketHelpers.ValidateSubprotocol(subProtocol);
			foreach (string text in this.requestedSubProtocols)
			{
				if (string.Equals(text, subProtocol, StringComparison.OrdinalIgnoreCase))
				{
					throw new ArgumentException(System.SR.GetString("net_WebSockets_NoDuplicateProtocol", new object[] { subProtocol }), "subProtocol");
				}
			}
			this.requestedSubProtocols.Add(subProtocol);
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0006C408 File Offset: 0x0006A608
		internal IList<string> RequestedSubProtocols
		{
			get
			{
				return this.requestedSubProtocols;
			}
		}

		/// <summary>Gets or sets the WebSocket protocol keep-alive interval.</summary>
		/// <returns>The WebSocket protocol keep-alive interval.</returns>
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x0006C410 File Offset: 0x0006A610
		// (set) Token: 0x0600148B RID: 5259 RVA: 0x0006C418 File Offset: 0x0006A618
		public TimeSpan KeepAliveInterval
		{
			get
			{
				return this.keepAliveInterval;
			}
			set
			{
				this.ThrowIfReadOnly();
				if (value < Timeout.InfiniteTimeSpan)
				{
					throw new ArgumentOutOfRangeException("value", value, System.SR.GetString("net_WebSockets_ArgumentOutOfRange_TooSmall", new object[] { Timeout.InfiniteTimeSpan.ToString() }));
				}
				this.keepAliveInterval = value;
			}
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0006C476 File Offset: 0x0006A676
		internal void SetToReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0006C47F File Offset: 0x0006A67F
		private void ThrowIfReadOnly()
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(System.SR.GetString("net_WebSockets_AlreadyStarted"));
			}
		}

		// Token: 0x04001623 RID: 5667
		private bool isReadOnly;

		// Token: 0x04001624 RID: 5668
		private readonly IList<string> requestedSubProtocols;

		// Token: 0x04001625 RID: 5669
		private readonly WebHeaderCollection requestHeaders;

		// Token: 0x04001626 RID: 5670
		private TimeSpan keepAliveInterval;

		// Token: 0x04001627 RID: 5671
		private int receiveBufferSize;

		// Token: 0x04001628 RID: 5672
		private int sendBufferSize;

		// Token: 0x04001629 RID: 5673
		private ArraySegment<byte>? buffer;

		// Token: 0x0400162A RID: 5674
		private bool useDefaultCredentials;

		// Token: 0x0400162B RID: 5675
		private ICredentials credentials;

		// Token: 0x0400162C RID: 5676
		private IWebProxy proxy;

		// Token: 0x0400162D RID: 5677
		private X509CertificateCollection clientCertificates;

		// Token: 0x0400162E RID: 5678
		private CookieContainer cookies;
	}
}
