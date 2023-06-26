using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about a network interface's multicast address.</summary>
	// Token: 0x020002AC RID: 684
	[global::__DynamicallyInvokable]
	public abstract class MulticastIPAddressInformation : IPAddressInformation
	{
		/// <summary>Gets the number of seconds remaining during which this address is the preferred address.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the number of seconds left for this address to remain preferred.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600196F RID: 6511
		[global::__DynamicallyInvokable]
		public abstract long AddressPreferredLifetime
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of seconds remaining during which this address is valid.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the number of seconds left for this address to remain assigned.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001970 RID: 6512
		[global::__DynamicallyInvokable]
		public abstract long AddressValidLifetime
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Specifies the amount of time remaining on the Dynamic Host Configuration Protocol (DHCP) lease for this IP address.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that contains the number of seconds remaining before the computer must release the <see cref="T:System.Net.IPAddress" /> instance.</returns>
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001971 RID: 6513
		[global::__DynamicallyInvokable]
		public abstract long DhcpLeaseLifetime
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a value that indicates the state of the duplicate address detection algorithm.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.DuplicateAddressDetectionState" /> values that indicates the progress of the algorithm in determining the uniqueness of this IP address.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001972 RID: 6514
		[global::__DynamicallyInvokable]
		public abstract DuplicateAddressDetectionState DuplicateAddressDetectionState
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a value that identifies the source of a Multicast Internet Protocol (IP) address prefix.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.PrefixOrigin" /> values that identifies how the prefix information was obtained.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001973 RID: 6515
		[global::__DynamicallyInvokable]
		public abstract PrefixOrigin PrefixOrigin
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a value that identifies the source of a Multicast Internet Protocol (IP) address suffix.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.SuffixOrigin" /> values that identifies how the suffix information was obtained.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001974 RID: 6516
		[global::__DynamicallyInvokable]
		public abstract SuffixOrigin SuffixOrigin
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> class.</summary>
		// Token: 0x06001975 RID: 6517 RVA: 0x0007DC4D File Offset: 0x0007BE4D
		[global::__DynamicallyInvokable]
		protected MulticastIPAddressInformation()
		{
		}
	}
}
