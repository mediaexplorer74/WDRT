using System;

namespace System.Net.Sockets
{
	/// <summary>The result of a <see cref="M:System.Net.Sockets.SocketTaskExtensions.ReceiveFromAsync(System.Net.Sockets.Socket,System.ArraySegment{System.Byte},System.Net.Sockets.SocketFlags,System.Net.EndPoint)" /> operation.</summary>
	// Token: 0x02000382 RID: 898
	public struct SocketReceiveFromResult
	{
		/// <summary>The number of bytes received. If the <see cref="M:System.Net.Sockets.SocketTaskExtensions.ReceiveFromAsync(System.Net.Sockets.Socket,System.ArraySegment{System.Byte},System.Net.Sockets.SocketFlags,System.Net.EndPoint)" /> operation was unsuccessful, then 0.</summary>
		// Token: 0x04001F1D RID: 7965
		public int ReceivedBytes;

		/// <summary>The source <see cref="T:System.Net.EndPoint" />.</summary>
		// Token: 0x04001F1E RID: 7966
		public EndPoint RemoteEndPoint;
	}
}
