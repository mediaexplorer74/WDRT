using System;

namespace System.Net.Sockets
{
	/// <summary>Presents the packet information from a call to <see cref="M:System.Net.Sockets.Socket.ReceiveMessageFrom(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> or <see cref="M:System.Net.Sockets.Socket.EndReceiveMessageFrom(System.IAsyncResult,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" />.</summary>
	// Token: 0x020003A0 RID: 928
	public struct IPPacketInformation
	{
		// Token: 0x06002290 RID: 8848 RVA: 0x000A4BE6 File Offset: 0x000A2DE6
		internal IPPacketInformation(IPAddress address, int networkInterface)
		{
			this.address = address;
			this.networkInterface = networkInterface;
		}

		/// <summary>Gets the origin information of the packet that was received as a result of calling the <see cref="M:System.Net.Sockets.Socket.ReceiveMessageFrom(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> method or <see cref="M:System.Net.Sockets.Socket.EndReceiveMessageFrom(System.IAsyncResult,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> method.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> that indicates the origin information of the packet that was received as a result of calling the <see cref="M:System.Net.Sockets.Socket.ReceiveMessageFrom(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> method or <see cref="M:System.Net.Sockets.Socket.EndReceiveMessageFrom(System.IAsyncResult,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> method. For packets that were sent from a unicast address, the <see cref="P:System.Net.Sockets.IPPacketInformation.Address" /> property will return the <see cref="T:System.Net.IPAddress" /> of the sender; for multicast or broadcast packets, the <see cref="P:System.Net.Sockets.IPPacketInformation.Address" /> property will return the multicast or broadcast <see cref="T:System.Net.IPAddress" />.</returns>
		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06002291 RID: 8849 RVA: 0x000A4BF6 File Offset: 0x000A2DF6
		public IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		/// <summary>Gets the network interface information that is associated with a call to <see cref="M:System.Net.Sockets.Socket.ReceiveMessageFrom(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" /> or <see cref="M:System.Net.Sockets.Socket.EndReceiveMessageFrom(System.IAsyncResult,System.Net.Sockets.SocketFlags@,System.Net.EndPoint@,System.Net.Sockets.IPPacketInformation@)" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value, which represents the index of the network interface. You can use this index with <see cref="M:System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces" /> to get more information about the relevant interface.</returns>
		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06002292 RID: 8850 RVA: 0x000A4BFE File Offset: 0x000A2DFE
		public int Interface
		{
			get
			{
				return this.networkInterface;
			}
		}

		/// <summary>Tests whether two specified <see cref="T:System.Net.Sockets.IPPacketInformation" /> instances are equivalent.</summary>
		/// <param name="packetInformation1">The <see cref="T:System.Net.Sockets.IPPacketInformation" /> instance that is to the left of the equality operator.</param>
		/// <param name="packetInformation2">The <see cref="T:System.Net.Sockets.IPPacketInformation" /> instance that is to the right of the equality operator.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="packetInformation1" /> and <paramref name="packetInformation2" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002293 RID: 8851 RVA: 0x000A4C06 File Offset: 0x000A2E06
		public static bool operator ==(IPPacketInformation packetInformation1, IPPacketInformation packetInformation2)
		{
			return packetInformation1.Equals(packetInformation2);
		}

		/// <summary>Tests whether two specified <see cref="T:System.Net.Sockets.IPPacketInformation" /> instances are not equal.</summary>
		/// <param name="packetInformation1">The <see cref="T:System.Net.Sockets.IPPacketInformation" /> instance that is to the left of the inequality operator.</param>
		/// <param name="packetInformation2">The <see cref="T:System.Net.Sockets.IPPacketInformation" /> instance that is to the right of the inequality operator.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="packetInformation1" /> and <paramref name="packetInformation2" /> are unequal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002294 RID: 8852 RVA: 0x000A4C1B File Offset: 0x000A2E1B
		public static bool operator !=(IPPacketInformation packetInformation1, IPPacketInformation packetInformation2)
		{
			return !packetInformation1.Equals(packetInformation2);
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="comparand">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="comparand" /> is an instance of <see cref="T:System.Net.Sockets.IPPacketInformation" /> and equals the value of the instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002295 RID: 8853 RVA: 0x000A4C34 File Offset: 0x000A2E34
		public override bool Equals(object comparand)
		{
			if (comparand == null)
			{
				return false;
			}
			if (!(comparand is IPPacketInformation))
			{
				return false;
			}
			IPPacketInformation ippacketInformation = (IPPacketInformation)comparand;
			return this.address.Equals(ippacketInformation.address) && this.networkInterface == ippacketInformation.networkInterface;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>An Int32 hash code.</returns>
		// Token: 0x06002296 RID: 8854 RVA: 0x000A4C7B File Offset: 0x000A2E7B
		public override int GetHashCode()
		{
			return this.address.GetHashCode() + this.networkInterface.GetHashCode();
		}

		// Token: 0x04001F81 RID: 8065
		private IPAddress address;

		// Token: 0x04001F82 RID: 8066
		private int networkInterface;
	}
}
