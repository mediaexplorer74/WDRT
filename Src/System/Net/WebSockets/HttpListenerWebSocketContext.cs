using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Principal;

namespace System.Net.WebSockets
{
	/// <summary>Provides access to information received by the <see cref="T:System.Net.HttpListener" /> class when accepting WebSocket connections.</summary>
	// Token: 0x0200022D RID: 557
	public class HttpListenerWebSocketContext : WebSocketContext
	{
		// Token: 0x06001491 RID: 5265 RVA: 0x0006C520 File Offset: 0x0006A720
		internal HttpListenerWebSocketContext(Uri requestUri, NameValueCollection headers, CookieCollection cookieCollection, IPrincipal user, bool isAuthenticated, bool isLocal, bool isSecureConnection, string origin, IEnumerable<string> secWebSocketProtocols, string secWebSocketVersion, string secWebSocketKey, WebSocket webSocket)
		{
			this.m_CookieCollection = new CookieCollection();
			this.m_CookieCollection.Add(cookieCollection);
			this.m_Headers = new NameValueCollection(headers);
			this.m_User = HttpListenerWebSocketContext.CopyPrincipal(user);
			this.m_RequestUri = requestUri;
			this.m_IsAuthenticated = isAuthenticated;
			this.m_IsLocal = isLocal;
			this.m_IsSecureConnection = isSecureConnection;
			this.m_Origin = origin;
			this.m_SecWebSocketProtocols = secWebSocketProtocols;
			this.m_SecWebSocketVersion = secWebSocketVersion;
			this.m_SecWebSocketKey = secWebSocketKey;
			this.m_WebSocket = webSocket;
		}

		/// <summary>Gets the URI requested by the WebSocket client.</summary>
		/// <returns>The URI requested by the WebSocket client.</returns>
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x0006C5AA File Offset: 0x0006A7AA
		public override Uri RequestUri
		{
			get
			{
				return this.m_RequestUri;
			}
		}

		/// <summary>Gets the HTTP headers received by the <see cref="T:System.Net.HttpListener" /> object in the WebSocket opening handshake.</summary>
		/// <returns>The HTTP headers received by the <see cref="T:System.Net.HttpListener" /> object.</returns>
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x0006C5B2 File Offset: 0x0006A7B2
		public override NameValueCollection Headers
		{
			get
			{
				return this.m_Headers;
			}
		}

		/// <summary>Gets the value of the Origin HTTP header included in the WebSocket opening handshake.</summary>
		/// <returns>The value of the Origin HTTP header.</returns>
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x0006C5BA File Offset: 0x0006A7BA
		public override string Origin
		{
			get
			{
				return this.m_Origin;
			}
		}

		/// <summary>Gets the list of the Secure WebSocket protocols included in the WebSocket opening handshake.</summary>
		/// <returns>The list of the Secure WebSocket protocols.</returns>
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x0006C5C2 File Offset: 0x0006A7C2
		public override IEnumerable<string> SecWebSocketProtocols
		{
			get
			{
				return this.m_SecWebSocketProtocols;
			}
		}

		/// <summary>Gets the list of sub-protocols requested by the WebSocket client.</summary>
		/// <returns>The list of sub-protocols requested by the WebSocket client.</returns>
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x0006C5CA File Offset: 0x0006A7CA
		public override string SecWebSocketVersion
		{
			get
			{
				return this.m_SecWebSocketVersion;
			}
		}

		/// <summary>Gets the value of the SecWebSocketKey HTTP header included in the WebSocket opening handshake.</summary>
		/// <returns>The value of the SecWebSocketKey HTTP header.</returns>
		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x0006C5D2 File Offset: 0x0006A7D2
		public override string SecWebSocketKey
		{
			get
			{
				return this.m_SecWebSocketKey;
			}
		}

		/// <summary>Gets the cookies received by the <see cref="T:System.Net.HttpListener" /> object in the WebSocket opening handshake.</summary>
		/// <returns>The cookies received by the <see cref="T:System.Net.HttpListener" /> object.</returns>
		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x0006C5DA File Offset: 0x0006A7DA
		public override CookieCollection CookieCollection
		{
			get
			{
				return this.m_CookieCollection;
			}
		}

		/// <summary>Gets an object used to obtain identity, authentication information, and security roles for the WebSocket client.</summary>
		/// <returns>The identity, authentication information, and security roles for the WebSocket client.</returns>
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001499 RID: 5273 RVA: 0x0006C5E2 File Offset: 0x0006A7E2
		public override IPrincipal User
		{
			get
			{
				return this.m_User;
			}
		}

		/// <summary>Gets a value that indicates if the WebSocket client is authenticated.</summary>
		/// <returns>
		///   <see langword="true" /> if the WebSocket client is authenticated; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x0006C5EA File Offset: 0x0006A7EA
		public override bool IsAuthenticated
		{
			get
			{
				return this.m_IsAuthenticated;
			}
		}

		/// <summary>Gets a value that indicates if the WebSocket client connected from the local machine.</summary>
		/// <returns>
		///   <see langword="true" /> if the WebSocket client connected from the local machine; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x0006C5F2 File Offset: 0x0006A7F2
		public override bool IsLocal
		{
			get
			{
				return this.m_IsLocal;
			}
		}

		/// <summary>Gets a value that indicates if the WebSocket connection is secured using Secure Sockets Layer (SSL).</summary>
		/// <returns>
		///   <see langword="true" /> if the WebSocket connection is secured using SSL; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x0006C5FA File Offset: 0x0006A7FA
		public override bool IsSecureConnection
		{
			get
			{
				return this.m_IsSecureConnection;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.WebSockets.WebSocket" /> instance used to send and receive data over the <see cref="T:System.Net.WebSockets.WebSocket" /> connection.</summary>
		/// <returns>The <see cref="T:System.Net.WebSockets.WebSocket" /> instance used to send and receive data over the <see cref="T:System.Net.WebSockets.WebSocket" /> connection.</returns>
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x0006C602 File Offset: 0x0006A802
		public override WebSocket WebSocket
		{
			get
			{
				return this.m_WebSocket;
			}
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0006C60C File Offset: 0x0006A80C
		private static IPrincipal CopyPrincipal(IPrincipal user)
		{
			IPrincipal principal = null;
			if (user != null)
			{
				if (!(user is WindowsPrincipal))
				{
					HttpListenerBasicIdentity httpListenerBasicIdentity = user.Identity as HttpListenerBasicIdentity;
					if (httpListenerBasicIdentity != null)
					{
						principal = new GenericPrincipal(new HttpListenerBasicIdentity(httpListenerBasicIdentity.Name, httpListenerBasicIdentity.Password), null);
					}
				}
				else
				{
					WindowsIdentity windowsIdentity = (WindowsIdentity)user.Identity;
					principal = new WindowsPrincipal(HttpListener.CreateWindowsIdentity(windowsIdentity.Token, windowsIdentity.AuthenticationType, WindowsAccountType.Normal, true));
				}
			}
			return principal;
		}

		// Token: 0x04001631 RID: 5681
		private Uri m_RequestUri;

		// Token: 0x04001632 RID: 5682
		private NameValueCollection m_Headers;

		// Token: 0x04001633 RID: 5683
		private CookieCollection m_CookieCollection;

		// Token: 0x04001634 RID: 5684
		private IPrincipal m_User;

		// Token: 0x04001635 RID: 5685
		private bool m_IsAuthenticated;

		// Token: 0x04001636 RID: 5686
		private bool m_IsLocal;

		// Token: 0x04001637 RID: 5687
		private bool m_IsSecureConnection;

		// Token: 0x04001638 RID: 5688
		private string m_Origin;

		// Token: 0x04001639 RID: 5689
		private IEnumerable<string> m_SecWebSocketProtocols;

		// Token: 0x0400163A RID: 5690
		private string m_SecWebSocketVersion;

		// Token: 0x0400163B RID: 5691
		private string m_SecWebSocketKey;

		// Token: 0x0400163C RID: 5692
		private WebSocket m_WebSocket;
	}
}
