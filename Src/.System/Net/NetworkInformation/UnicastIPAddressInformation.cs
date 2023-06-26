using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about a network interface's unicast address.</summary>
	// Token: 0x020002AA RID: 682
	[global::__DynamicallyInvokable]
	public abstract class UnicastIPAddressInformation : IPAddressInformation
	{
		/// <summary>Gets the number of seconds remaining during which this address is the preferred address.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the number of seconds left for this address to remain preferred.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600195A RID: 6490
		[global::__DynamicallyInvokable]
		public abstract long AddressPreferredLifetime
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the number of seconds remaining during which this address is valid.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the number of seconds left for this address to remain assigned.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600195B RID: 6491
		[global::__DynamicallyInvokable]
		public abstract long AddressValidLifetime
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Specifies the amount of time remaining on the Dynamic Host Configuration Protocol (DHCP) lease for this IP address.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that contains the number of seconds remaining before the computer must release the <see cref="T:System.Net.IPAddress" /> instance.</returns>
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x0600195C RID: 6492
		[global::__DynamicallyInvokable]
		public abstract long DhcpLeaseLifetime
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a value that indicates the state of the duplicate address detection algorithm.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.DuplicateAddressDetectionState" /> values that indicates the progress of the algorithm in determining the uniqueness of this IP address.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x0600195D RID: 6493
		[global::__DynamicallyInvokable]
		public abstract DuplicateAddressDetectionState DuplicateAddressDetectionState
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a value that identifies the source of a unicast Internet Protocol (IP) address prefix.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.PrefixOrigin" /> values that identifies how the prefix information was obtained.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x0600195E RID: 6494
		[global::__DynamicallyInvokable]
		public abstract PrefixOrigin PrefixOrigin
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a value that identifies the source of a unicast Internet Protocol (IP) address suffix.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.SuffixOrigin" /> values that identifies how the suffix information was obtained.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600195F RID: 6495
		[global::__DynamicallyInvokable]
		public abstract SuffixOrigin SuffixOrigin
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the IPv4 mask.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> object that contains the IPv4 mask.</returns>
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001960 RID: 6496
		[global::__DynamicallyInvokable]
		public abstract IPAddress IPv4Mask
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets the length, in bits, of the prefix or network part of the IP address.</summary>
		/// <returns>The length, in bits, of the prefix or network part of the IP address.</returns>
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x0007DB9A File Offset: 0x0007BD9A
		[global::__DynamicallyInvokable]
		public virtual int PrefixLength
		{
			[global::__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> class.</summary>
		// Token: 0x06001962 RID: 6498 RVA: 0x0007DBA1 File Offset: 0x0007BDA1
		[global::__DynamicallyInvokable]
		protected UnicastIPAddressInformation()
		{
		}
	}
}
