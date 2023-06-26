using System;

namespace System.Net.Sockets
{
	/// <summary>The result of a <see cref="M:System.Net.Sockets.SocketTaskExtensions.ReceiveMessageFromAsync(System.Net.Sockets.Socket,System.ArraySegment{System.Byte},System.Net.Sockets.SocketFlags,System.Net.EndPoint)" /> operation.</summary>
	// Token: 0x02000383 RID: 899
	public struct SocketReceiveMessageFromResult
	{
		/// <summary>The number of bytes received. If the <see cref="M:System.Net.Sockets.SocketTaskExtensions.ReceiveMessageFromAsync(System.Net.Sockets.Socket,System.ArraySegment{System.Byte},System.Net.Sockets.SocketFlags,System.Net.EndPoint)" /> operation is unsuccessful, this value will be 0.</summary>
		// Token: 0x04001F1F RID: 7967
		public int ReceivedBytes;

		/// <summary>A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values for the received packet.</summary>
		// Token: 0x04001F20 RID: 7968
		public SocketFlags SocketFlags;

		/// <summary>The source <see cref="T:System.Net.EndPoint" />.</summary>
		// Token: 0x04001F21 RID: 7969
		public EndPoint RemoteEndPoint;

		/// <summary>An <see cref="T:System.Net.Sockets.IPPacketInformation" /> holding address and interface information.</summary>
		// Token: 0x04001F22 RID: 7970
		public IPPacketInformation PacketInformation;
	}
}
