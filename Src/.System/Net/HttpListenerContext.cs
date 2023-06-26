using System;
using System.ComponentModel;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading.Tasks;

namespace System.Net
{
	/// <summary>Provides access to the request and response objects used by the <see cref="T:System.Net.HttpListener" /> class. This class cannot be inherited.</summary>
	// Token: 0x020000F8 RID: 248
	public sealed class HttpListenerContext
	{
		// Token: 0x060008DB RID: 2267 RVA: 0x00032690 File Offset: 0x00030890
		internal unsafe HttpListenerContext(HttpListener httpListener, RequestContextBase memoryBlob)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.HttpListener, this, ".ctor", "httpListener#" + ValidationHelper.HashString(httpListener) + " requestBlob=" + ValidationHelper.HashString((IntPtr)((void*)memoryBlob.RequestBlob)));
			}
			this.m_Listener = httpListener;
			this.m_Request = new HttpListenerRequest(this, memoryBlob);
			this.m_AuthenticationSchemes = httpListener.AuthenticationSchemes;
			this.m_ExtendedProtectionPolicy = httpListener.ExtendedProtectionPolicy;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00032710 File Offset: 0x00030910
		internal void SetIdentity(IPrincipal principal, string mutualAuthentication)
		{
			this.m_MutualAuthentication = mutualAuthentication;
			this.m_User = principal;
		}

		/// <summary>Gets the <see cref="T:System.Net.HttpListenerRequest" /> that represents a client's request for a resource.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerRequest" /> object that represents the client request.</returns>
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x00032720 File Offset: 0x00030920
		public HttpListenerRequest Request
		{
			get
			{
				return this.m_Request;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.HttpListenerResponse" /> object that will be sent to the client in response to the client's request.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerResponse" /> object used to send a response back to the client.</returns>
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x00032728 File Offset: 0x00030928
		public HttpListenerResponse Response
		{
			get
			{
				if (Logging.On)
				{
					Logging.Enter(Logging.HttpListener, this, "Response", "");
				}
				if (this.m_Response == null)
				{
					this.m_Response = new HttpListenerResponse(this);
				}
				if (Logging.On)
				{
					Logging.Exit(Logging.HttpListener, this, "Response", "");
				}
				return this.m_Response;
			}
		}

		/// <summary>Gets an object used to obtain identity, authentication information, and security roles for the client whose request is represented by this <see cref="T:System.Net.HttpListenerContext" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Principal.IPrincipal" /> object that describes the client, or <see langword="null" /> if the <see cref="T:System.Net.HttpListener" /> that supplied this <see cref="T:System.Net.HttpListenerContext" /> does not require authentication.</returns>
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x00032787 File Offset: 0x00030987
		public IPrincipal User
		{
			get
			{
				if (!(this.m_User is WindowsPrincipal))
				{
					return this.m_User;
				}
				new SecurityPermission(SecurityPermissionFlag.ControlPrincipal).Demand();
				return this.m_User;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x000327B2 File Offset: 0x000309B2
		// (set) Token: 0x060008E1 RID: 2273 RVA: 0x000327BA File Offset: 0x000309BA
		internal AuthenticationSchemes AuthenticationSchemes
		{
			get
			{
				return this.m_AuthenticationSchemes;
			}
			set
			{
				this.m_AuthenticationSchemes = value;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x000327C3 File Offset: 0x000309C3
		// (set) Token: 0x060008E3 RID: 2275 RVA: 0x000327CB File Offset: 0x000309CB
		internal ExtendedProtectionPolicy ExtendedProtectionPolicy
		{
			get
			{
				return this.m_ExtendedProtectionPolicy;
			}
			set
			{
				this.m_ExtendedProtectionPolicy = value;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x000327D4 File Offset: 0x000309D4
		internal bool PromoteCookiesToRfc2965
		{
			get
			{
				return this.m_PromoteCookiesToRfc2965;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x000327DC File Offset: 0x000309DC
		internal string MutualAuthentication
		{
			get
			{
				return this.m_MutualAuthentication;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x000327E4 File Offset: 0x000309E4
		internal HttpListener Listener
		{
			get
			{
				return this.m_Listener;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x000327EC File Offset: 0x000309EC
		internal CriticalHandle RequestQueueHandle
		{
			get
			{
				return this.m_Listener.RequestQueueHandle;
			}
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x000327F9 File Offset: 0x000309F9
		internal void EnsureBoundHandle()
		{
			this.m_Listener.EnsureBoundHandle();
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00032806 File Offset: 0x00030A06
		internal ulong RequestId
		{
			get
			{
				return this.Request.RequestId;
			}
		}

		/// <summary>Accept a WebSocket connection as an asynchronous operation.</summary>
		/// <param name="subProtocol">The supported WebSocket sub-protocol.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.WebSockets.HttpListenerWebSocketContext" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subProtocol" /> is an empty string  
		/// -or-  
		/// <paramref name="subProtocol" /> contains illegal characters.</exception>
		/// <exception cref="T:System.Net.WebSockets.WebSocketException">An error occurred when sending the response to complete the WebSocket handshake.</exception>
		// Token: 0x060008EA RID: 2282 RVA: 0x00032813 File Offset: 0x00030A13
		public Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(string subProtocol)
		{
			return this.AcceptWebSocketAsync(subProtocol, 16384, WebSocket.DefaultKeepAliveInterval);
		}

		/// <summary>Accept a WebSocket connection specifying the supported WebSocket sub-protocol  and WebSocket keep-alive interval as an asynchronous operation.</summary>
		/// <param name="subProtocol">The supported WebSocket sub-protocol.</param>
		/// <param name="keepAliveInterval">The WebSocket protocol keep-alive interval in milliseconds.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.WebSockets.HttpListenerWebSocketContext" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subProtocol" /> is an empty string  
		/// -or-  
		/// <paramref name="subProtocol" /> contains illegal characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="keepAliveInterval" /> is too small.</exception>
		/// <exception cref="T:System.Net.WebSockets.WebSocketException">An error occurred when sending the response to complete the WebSocket handshake.</exception>
		// Token: 0x060008EB RID: 2283 RVA: 0x00032826 File Offset: 0x00030A26
		public Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(string subProtocol, TimeSpan keepAliveInterval)
		{
			return this.AcceptWebSocketAsync(subProtocol, 16384, keepAliveInterval);
		}

		/// <summary>Accept a WebSocket connection specifying the supported WebSocket sub-protocol, receive buffer size, and WebSocket keep-alive interval as an asynchronous operation.</summary>
		/// <param name="subProtocol">The supported WebSocket sub-protocol.</param>
		/// <param name="receiveBufferSize">The receive buffer size in bytes.</param>
		/// <param name="keepAliveInterval">The WebSocket protocol keep-alive interval in milliseconds.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.WebSockets.HttpListenerWebSocketContext" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subProtocol" /> is an empty string  
		/// -or-  
		/// <paramref name="subProtocol" /> contains illegal characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="keepAliveInterval" /> is too small.  
		/// -or-  
		/// <paramref name="receiveBufferSize" /> is less than 16 bytes  
		/// -or-  
		/// <paramref name="receiveBufferSize" /> is greater than 64K bytes.</exception>
		/// <exception cref="T:System.Net.WebSockets.WebSocketException">An error occurred when sending the response to complete the WebSocket handshake.</exception>
		// Token: 0x060008EC RID: 2284 RVA: 0x00032838 File Offset: 0x00030A38
		public Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval)
		{
			WebSocketHelpers.ValidateOptions(subProtocol, receiveBufferSize, 16, keepAliveInterval);
			ArraySegment<byte> arraySegment = WebSocketBuffer.CreateInternalBufferArraySegment(receiveBufferSize, 16, true);
			return this.AcceptWebSocketAsync(subProtocol, receiveBufferSize, keepAliveInterval, arraySegment);
		}

		/// <summary>Accept a WebSocket connection specifying the supported WebSocket sub-protocol, receive buffer size, WebSocket keep-alive interval, and the internal buffer as an asynchronous operation.</summary>
		/// <param name="subProtocol">The supported WebSocket sub-protocol.</param>
		/// <param name="receiveBufferSize">The receive buffer size in bytes.</param>
		/// <param name="keepAliveInterval">The WebSocket protocol keep-alive interval in milliseconds.</param>
		/// <param name="internalBuffer">An internal buffer to use for this operation.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.WebSockets.HttpListenerWebSocketContext" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subProtocol" /> is an empty string  
		/// -or-  
		/// <paramref name="subProtocol" /> contains illegal characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="keepAliveInterval" /> is too small.  
		/// -or-  
		/// <paramref name="receiveBufferSize" /> is less than 16 bytes  
		/// -or-  
		/// <paramref name="receiveBufferSize" /> is greater than 64K bytes.</exception>
		/// <exception cref="T:System.Net.WebSockets.WebSocketException">An error occurred when sending the response to complete the WebSocket handshake.</exception>
		// Token: 0x060008ED RID: 2285 RVA: 0x00032863 File Offset: 0x00030A63
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval, ArraySegment<byte> internalBuffer)
		{
			return WebSocketHelpers.AcceptWebSocketAsync(this, subProtocol, receiveBufferSize, keepAliveInterval, internalBuffer);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00032870 File Offset: 0x00030A70
		internal void Close()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "Close()", "");
			}
			try
			{
				if (this.m_Response != null)
				{
					this.m_Response.Close();
				}
			}
			finally
			{
				try
				{
					this.m_Request.Close();
				}
				finally
				{
					IDisposable disposable = ((this.m_User == null) ? null : (this.m_User.Identity as IDisposable));
					if (disposable != null && this.m_User.Identity.AuthenticationType != "NTLM" && !this.m_Listener.UnsafeConnectionNtlmAuthentication)
					{
						disposable.Dispose();
					}
				}
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "Close", "");
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00032948 File Offset: 0x00030B48
		internal void Abort()
		{
			if (Logging.On)
			{
				Logging.Enter(Logging.HttpListener, this, "Abort", "");
			}
			this.ForceCancelRequest(this.RequestQueueHandle, this.m_Request.RequestId);
			try
			{
				this.m_Request.Close();
			}
			finally
			{
				IDisposable disposable = ((this.m_User == null) ? null : (this.m_User.Identity as IDisposable));
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			if (Logging.On)
			{
				Logging.Exit(Logging.HttpListener, this, "Abort", "");
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x000329E8 File Offset: 0x00030BE8
		internal UnsafeNclNativeMethods.HttpApi.HTTP_VERB GetKnownMethod()
		{
			return UnsafeNclNativeMethods.HttpApi.GetKnownVerb(this.Request.RequestBuffer, this.Request.OriginalBlobAddress);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00032A08 File Offset: 0x00030C08
		internal static void CancelRequest(CriticalHandle requestQueueHandle, ulong requestId)
		{
			uint num = UnsafeNclNativeMethods.HttpApi.HttpCancelHttpRequest(requestQueueHandle, requestId, IntPtr.Zero);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00032A24 File Offset: 0x00030C24
		internal void ForceCancelRequest(CriticalHandle requestQueueHandle, ulong requestId)
		{
			uint num = UnsafeNclNativeMethods.HttpApi.HttpCancelHttpRequest(requestQueueHandle, requestId, IntPtr.Zero);
			if (num == 1229U)
			{
				this.m_Response.CancelLastWrite(requestQueueHandle);
			}
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00032A52 File Offset: 0x00030C52
		internal void SetAuthenticationHeaders()
		{
			this.Listener.SetAuthenticationHeaders(this);
		}

		// Token: 0x04000DFB RID: 3579
		private HttpListener m_Listener;

		// Token: 0x04000DFC RID: 3580
		private HttpListenerRequest m_Request;

		// Token: 0x04000DFD RID: 3581
		private HttpListenerResponse m_Response;

		// Token: 0x04000DFE RID: 3582
		private IPrincipal m_User;

		// Token: 0x04000DFF RID: 3583
		private string m_MutualAuthentication;

		// Token: 0x04000E00 RID: 3584
		private AuthenticationSchemes m_AuthenticationSchemes;

		// Token: 0x04000E01 RID: 3585
		private ExtendedProtectionPolicy m_ExtendedProtectionPolicy;

		// Token: 0x04000E02 RID: 3586
		private bool m_PromoteCookiesToRfc2965;

		// Token: 0x04000E03 RID: 3587
		internal const string NTLM = "NTLM";
	}
}
