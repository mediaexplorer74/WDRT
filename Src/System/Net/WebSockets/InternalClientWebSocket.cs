using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Net.WebSockets
{
	// Token: 0x0200022C RID: 556
	internal sealed class InternalClientWebSocket : WebSocketBase
	{
		// Token: 0x0600148E RID: 5262 RVA: 0x0006C49C File Offset: 0x0006A69C
		public InternalClientWebSocket(Stream innerStream, string subProtocol, int receiveBufferSize, int sendBufferSize, TimeSpan keepAliveInterval, bool useZeroMaskingKey, ArraySegment<byte> internalBuffer)
			: base(innerStream, subProtocol, keepAliveInterval, WebSocketBuffer.CreateClientBuffer(internalBuffer, receiveBufferSize, sendBufferSize))
		{
			this.m_Properties = base.InternalBuffer.CreateProperties(useZeroMaskingKey);
			this.m_SessionHandle = this.CreateWebSocketHandle();
			if (this.m_SessionHandle == null || this.m_SessionHandle.IsInvalid)
			{
				WebSocketHelpers.ThrowPlatformNotSupportedException_WSPC();
			}
			base.StartKeepAliveTimer();
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x0006C4FC File Offset: 0x0006A6FC
		internal override SafeHandle SessionHandle
		{
			get
			{
				return this.m_SessionHandle;
			}
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x0006C504 File Offset: 0x0006A704
		private SafeHandle CreateWebSocketHandle()
		{
			SafeWebSocketHandle safeWebSocketHandle;
			WebSocketProtocolComponent.WebSocketCreateClientHandle(this.m_Properties, out safeWebSocketHandle);
			return safeWebSocketHandle;
		}

		// Token: 0x0400162F RID: 5679
		private readonly SafeHandle m_SessionHandle;

		// Token: 0x04001630 RID: 5680
		private readonly WebSocketProtocolComponent.Property[] m_Properties;
	}
}
