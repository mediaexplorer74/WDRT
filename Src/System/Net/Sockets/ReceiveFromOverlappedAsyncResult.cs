using System;

namespace System.Net.Sockets
{
	// Token: 0x0200039F RID: 927
	internal class ReceiveFromOverlappedAsyncResult : OverlappedAsyncResult
	{
		// Token: 0x0600228E RID: 8846 RVA: 0x000A4BC1 File Offset: 0x000A2DC1
		internal ReceiveFromOverlappedAsyncResult(Socket socket, object asyncState, AsyncCallback asyncCallback)
			: base(socket, asyncState, asyncCallback)
		{
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x000A4BCC File Offset: 0x000A2DCC
		internal override object PostCompletion(int numBytes)
		{
			base.SocketAddress.SetSize(base.GetSocketAddressSizePtr());
			return base.PostCompletion(numBytes);
		}
	}
}
