using System;

namespace System.Net.Sockets
{
	/// <summary>Contains <see cref="T:System.Net.IPAddress" /> values used to join and drop multicast groups.</summary>
	// Token: 0x0200036E RID: 878
	public class MulticastOption
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.MulticastOption" /> class with the specified IP multicast group address and local IP address associated with a network interface.</summary>
		/// <param name="group">The group <see cref="T:System.Net.IPAddress" />.</param>
		/// <param name="mcint">The local <see cref="T:System.Net.IPAddress" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="mcint" /> is <see langword="null" />.</exception>
		// Token: 0x06001FC8 RID: 8136 RVA: 0x00094C38 File Offset: 0x00092E38
		public MulticastOption(IPAddress group, IPAddress mcint)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (mcint == null)
			{
				throw new ArgumentNullException("mcint");
			}
			this.Group = group;
			this.LocalAddress = mcint;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.MulticastOption" /> class with the specified IP multicast group address and interface index.</summary>
		/// <param name="group">The <see cref="T:System.Net.IPAddress" /> of the multicast group.</param>
		/// <param name="interfaceIndex">The index of the interface that is used to send and receive multicast packets.</param>
		// Token: 0x06001FC9 RID: 8137 RVA: 0x00094C6A File Offset: 0x00092E6A
		public MulticastOption(IPAddress group, int interfaceIndex)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (interfaceIndex < 0 || interfaceIndex > 16777215)
			{
				throw new ArgumentOutOfRangeException("interfaceIndex");
			}
			this.Group = group;
			this.ifIndex = interfaceIndex;
		}

		/// <summary>Initializes a new version of the <see cref="T:System.Net.Sockets.MulticastOption" /> class for the specified IP multicast group.</summary>
		/// <param name="group">The <see cref="T:System.Net.IPAddress" /> of the multicast group.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is <see langword="null" />.</exception>
		// Token: 0x06001FCA RID: 8138 RVA: 0x00094CA5 File Offset: 0x00092EA5
		public MulticastOption(IPAddress group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			this.Group = group;
			this.LocalAddress = IPAddress.Any;
		}

		/// <summary>Gets or sets the IP address of a multicast group.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> that contains the Internet address of a multicast group.</returns>
		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06001FCB RID: 8139 RVA: 0x00094CCD File Offset: 0x00092ECD
		// (set) Token: 0x06001FCC RID: 8140 RVA: 0x00094CD5 File Offset: 0x00092ED5
		public IPAddress Group
		{
			get
			{
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		/// <summary>Gets or sets the local address associated with a multicast group.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> that contains the local address associated with a multicast group.</returns>
		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x00094CDE File Offset: 0x00092EDE
		// (set) Token: 0x06001FCE RID: 8142 RVA: 0x00094CE6 File Offset: 0x00092EE6
		public IPAddress LocalAddress
		{
			get
			{
				return this.localAddress;
			}
			set
			{
				this.ifIndex = 0;
				this.localAddress = value;
			}
		}

		/// <summary>Gets or sets the index of the interface that is used to send and receive multicast packets.</summary>
		/// <returns>An integer that represents the index of a <see cref="T:System.Net.NetworkInformation.NetworkInterface" /> array element.</returns>
		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06001FCF RID: 8143 RVA: 0x00094CF6 File Offset: 0x00092EF6
		// (set) Token: 0x06001FD0 RID: 8144 RVA: 0x00094CFE File Offset: 0x00092EFE
		public int InterfaceIndex
		{
			get
			{
				return this.ifIndex;
			}
			set
			{
				if (value < 0 || value > 16777215)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.localAddress = null;
				this.ifIndex = value;
			}
		}

		// Token: 0x04001DCD RID: 7629
		private IPAddress group;

		// Token: 0x04001DCE RID: 7630
		private IPAddress localAddress;

		// Token: 0x04001DCF RID: 7631
		private int ifIndex;
	}
}
