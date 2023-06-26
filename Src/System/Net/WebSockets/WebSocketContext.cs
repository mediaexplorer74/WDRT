using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Principal;

namespace System.Net.WebSockets
{
	/// <summary>Used for accessing the information in the WebSocket handshake.</summary>
	// Token: 0x02000234 RID: 564
	public abstract class WebSocketContext
	{
		/// <summary>The URI requested by the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.Uri" />.</returns>
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600151B RID: 5403
		public abstract Uri RequestUri { get; }

		/// <summary>The HTTP headers that were sent to the server during the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Specialized.NameValueCollection" />.</returns>
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600151C RID: 5404
		public abstract NameValueCollection Headers { get; }

		/// <summary>The value of the Origin HTTP header included in the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600151D RID: 5405
		public abstract string Origin { get; }

		/// <summary>The value of the SecWebSocketKey HTTP header included in the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.IEnumerable`1" />.</returns>
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x0600151E RID: 5406
		public abstract IEnumerable<string> SecWebSocketProtocols { get; }

		/// <summary>The list of subprotocols requested by the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600151F RID: 5407
		public abstract string SecWebSocketVersion { get; }

		/// <summary>The value of the SecWebSocketKey HTTP header included in the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001520 RID: 5408
		public abstract string SecWebSocketKey { get; }

		/// <summary>The cookies that were passed to the server during the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Net.CookieCollection" />.</returns>
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001521 RID: 5409
		public abstract CookieCollection CookieCollection { get; }

		/// <summary>An object used to obtain identity, authentication information, and security roles for the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.Security.Principal.IPrincipal" />.</returns>
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001522 RID: 5410
		public abstract IPrincipal User { get; }

		/// <summary>Whether the WebSocket client is authenticated.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001523 RID: 5411
		public abstract bool IsAuthenticated { get; }

		/// <summary>Whether the WebSocket client connected from the local machine.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001524 RID: 5412
		public abstract bool IsLocal { get; }

		/// <summary>Whether the WebSocket connection is secured using Secure Sockets Layer (SSL).</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001525 RID: 5413
		public abstract bool IsSecureConnection { get; }

		/// <summary>The WebSocket instance used to interact (send/receive/close/etc) with the WebSocket connection.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocket" />.</returns>
		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001526 RID: 5414
		public abstract WebSocket WebSocket { get; }
	}
}
