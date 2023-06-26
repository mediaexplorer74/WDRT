using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Net.WebSockets
{
	// Token: 0x0200022E RID: 558
	internal sealed class ServerWebSocket : WebSocketBase
	{
		// Token: 0x0600149F RID: 5279 RVA: 0x0006C678 File Offset: 0x0006A878
		public ServerWebSocket(Stream innerStream, string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval, ArraySegment<byte> internalBuffer)
			: base(innerStream, subProtocol, keepAliveInterval, WebSocketBuffer.CreateServerBuffer(internalBuffer, receiveBufferSize))
		{
			this.m_Properties = base.InternalBuffer.CreateProperties(false);
			this.m_SessionHandle = this.CreateWebSocketHandle();
			if (this.m_SessionHandle == null || this.m_SessionHandle.IsInvalid)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
			}
			base.StartKeepAliveTimer();
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x0006C6D5 File Offset: 0x0006A8D5
		internal override SafeHandle SessionHandle
		{
			get
			{
				return this.m_SessionHandle;
			}
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0006C6E0 File Offset: 0x0006A8E0
		private SafeHandle CreateWebSocketHandle()
		{
			SafeWebSocketHandle safeWebSocketHandle;
			WebSocketProtocolComponent.WebSocketCreateServerHandle(this.m_Properties, this.m_Properties.Length, out safeWebSocketHandle);
			return safeWebSocketHandle;
		}

		// Token: 0x0400163D RID: 5693
		private readonly SafeHandle m_SessionHandle;

		// Token: 0x0400163E RID: 5694
		private readonly WebSocketProtocolComponent.Property[] m_Properties;
	}
}
