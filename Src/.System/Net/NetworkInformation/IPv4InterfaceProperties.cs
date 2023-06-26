using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about network interfaces that support Internet Protocol version 4 (IPv4).</summary>
	// Token: 0x020002B2 RID: 690
	[global::__DynamicallyInvokable]
	public abstract class IPv4InterfaceProperties
	{
		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether an interface uses Windows Internet Name Service (WINS).</summary>
		/// <returns>
		///   <see langword="true" /> if the interface uses WINS; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600199F RID: 6559
		[global::__DynamicallyInvokable]
		public abstract bool UsesWins
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the interface is configured to use a Dynamic Host Configuration Protocol (DHCP) server to obtain an IP address.</summary>
		/// <returns>
		///   <see langword="true" /> if the interface is configured to obtain an IP address from a DHCP server; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060019A0 RID: 6560
		[global::__DynamicallyInvokable]
		public abstract bool IsDhcpEnabled
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface has an automatic private IP addressing (APIPA) address.</summary>
		/// <returns>
		///   <see langword="true" /> if the interface uses an APIPA address; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060019A1 RID: 6561
		[global::__DynamicallyInvokable]
		public abstract bool IsAutomaticPrivateAddressingActive
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface has automatic private IP addressing (APIPA) enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the interface uses APIPA; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060019A2 RID: 6562
		[global::__DynamicallyInvokable]
		public abstract bool IsAutomaticPrivateAddressingEnabled
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the index of the network interface associated with the Internet Protocol version 4 (IPv4) address.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the index of the IPv4 interface.</returns>
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060019A3 RID: 6563
		[global::__DynamicallyInvokable]
		public abstract int Index
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface can forward (route) packets.</summary>
		/// <returns>
		///   <see langword="true" /> if this interface routes packets; otherwise <see langword="false" />.</returns>
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060019A4 RID: 6564
		[global::__DynamicallyInvokable]
		public abstract bool IsForwardingEnabled
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the maximum transmission unit (MTU) for this network interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the MTU.</returns>
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060019A5 RID: 6565
		[global::__DynamicallyInvokable]
		public abstract int Mtu
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IPv4InterfaceProperties" /> class.</summary>
		// Token: 0x060019A6 RID: 6566 RVA: 0x0007DEB4 File Offset: 0x0007C0B4
		[global::__DynamicallyInvokable]
		protected IPv4InterfaceProperties()
		{
		}
	}
}
