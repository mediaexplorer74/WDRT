using System;

namespace System.Net.Sockets
{
	// Token: 0x0200038F RID: 911
	internal class DisconnectOverlappedAsyncResult : BaseOverlappedAsyncResult
	{
		// Token: 0x06002229 RID: 8745 RVA: 0x000A3708 File Offset: 0x000A1908
		internal DisconnectOverlappedAsyncResult(Socket socket, object asyncState, AsyncCallback asyncCallback)
			: base(socket, asyncState, asyncCallback)
		{
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x000A3714 File Offset: 0x000A1914
		internal override object PostCompletion(int numBytes)
		{
			if (base.ErrorCode == 0)
			{
				Socket socket = (Socket)base.AsyncObject;
				socket.SetToDisconnected();
				socket.m_RemoteEndPoint = null;
			}
			return base.PostCompletion(numBytes);
		}
	}
}
