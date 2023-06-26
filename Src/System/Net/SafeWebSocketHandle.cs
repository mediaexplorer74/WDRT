using System;
using System.Net.WebSockets;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x02000207 RID: 519
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeWebSocketHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001363 RID: 4963 RVA: 0x0006604E File Offset: 0x0006424E
		internal SafeWebSocketHandle()
			: base(true)
		{
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00066057 File Offset: 0x00064257
		protected override bool ReleaseHandle()
		{
			if (this.IsInvalid)
			{
				return true;
			}
			WebSocketProtocolComponent.WebSocketDeleteHandle(this.handle);
			return true;
		}
	}
}
