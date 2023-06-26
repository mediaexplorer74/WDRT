using System;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x0200038E RID: 910
	internal class ConnectOverlappedAsyncResult : BaseOverlappedAsyncResult
	{
		// Token: 0x06002226 RID: 8742 RVA: 0x000A3678 File Offset: 0x000A1878
		internal ConnectOverlappedAsyncResult(Socket socket, EndPoint endPoint, object asyncState, AsyncCallback asyncCallback)
			: base(socket, asyncState, asyncCallback)
		{
			this.m_EndPoint = endPoint;
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x000A368C File Offset: 0x000A188C
		internal override object PostCompletion(int numBytes)
		{
			SocketError socketError = (SocketError)base.ErrorCode;
			Socket socket = (Socket)base.AsyncObject;
			if (socketError == SocketError.Success)
			{
				try
				{
					socketError = UnsafeNclNativeMethods.OSSOCK.setsockopt(socket.SafeHandle, SocketOptionLevel.Socket, SocketOptionName.UpdateConnectContext, null, 0);
					if (socketError == SocketError.SocketError)
					{
						socketError = (SocketError)Marshal.GetLastWin32Error();
					}
				}
				catch (ObjectDisposedException)
				{
					socketError = SocketError.OperationAborted;
				}
				base.ErrorCode = (int)socketError;
			}
			if (socketError == SocketError.Success)
			{
				socket.SetToConnected();
				return socket;
			}
			return null;
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06002228 RID: 8744 RVA: 0x000A3700 File Offset: 0x000A1900
		internal EndPoint RemoteEndPoint
		{
			get
			{
				return this.m_EndPoint;
			}
		}

		// Token: 0x04001F54 RID: 8020
		private EndPoint m_EndPoint;
	}
}
