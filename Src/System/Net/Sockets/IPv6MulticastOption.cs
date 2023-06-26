using System;

namespace System.Net.Sockets
{
	/// <summary>Contains option values for joining an IPv6 multicast group.</summary>
	// Token: 0x0200036F RID: 879
	public class IPv6MulticastOption
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.IPv6MulticastOption" /> class with the specified IP multicast group and the local interface address.</summary>
		/// <param name="group">The group <see cref="T:System.Net.IPAddress" />.</param>
		/// <param name="ifindex">The local interface address.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="ifindex" /> is less than 0.  
		/// -or-  
		/// <paramref name="ifindex" /> is greater than 0x00000000FFFFFFFF.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is <see langword="null" />.</exception>
		// Token: 0x06001FD1 RID: 8145 RVA: 0x00094D25 File Offset: 0x00092F25
		public IPv6MulticastOption(IPAddress group, long ifindex)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (ifindex < 0L || ifindex > (long)((ulong)(-1)))
			{
				throw new ArgumentOutOfRangeException("ifindex");
			}
			this.Group = group;
			this.InterfaceIndex = ifindex;
		}

		/// <summary>Initializes a new version of the <see cref="T:System.Net.Sockets.IPv6MulticastOption" /> class for the specified IP multicast group.</summary>
		/// <param name="group">The <see cref="T:System.Net.IPAddress" /> of the multicast group.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is <see langword="null" />.</exception>
		// Token: 0x06001FD2 RID: 8146 RVA: 0x00094D5E File Offset: 0x00092F5E
		public IPv6MulticastOption(IPAddress group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			this.Group = group;
			this.InterfaceIndex = 0L;
		}

		/// <summary>Gets or sets the IP address of a multicast group.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> that contains the Internet address of a multicast group.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is <see langword="null" />.</exception>
		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x00094D83 File Offset: 0x00092F83
		// (set) Token: 0x06001FD4 RID: 8148 RVA: 0x00094D8B File Offset: 0x00092F8B
		public IPAddress Group
		{
			get
			{
				return this.m_Group;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_Group = value;
			}
		}

		/// <summary>Gets or sets the interface index that is associated with a multicast group.</summary>
		/// <returns>A <see cref="T:System.UInt64" /> value that specifies the address of the interface.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value that is specified for a set operation is less than 0 or greater than 0x00000000FFFFFFFF.</exception>
		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06001FD5 RID: 8149 RVA: 0x00094DA2 File Offset: 0x00092FA2
		// (set) Token: 0x06001FD6 RID: 8150 RVA: 0x00094DAA File Offset: 0x00092FAA
		public long InterfaceIndex
		{
			get
			{
				return this.m_Interface;
			}
			set
			{
				if (value < 0L || value > (long)((ulong)(-1)))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.m_Interface = value;
			}
		}

		// Token: 0x04001DD0 RID: 7632
		private IPAddress m_Group;

		// Token: 0x04001DD1 RID: 7633
		private long m_Interface;
	}
}
