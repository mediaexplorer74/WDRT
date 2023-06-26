using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about a network interface address.</summary>
	// Token: 0x0200029F RID: 671
	[global::__DynamicallyInvokable]
	public abstract class IPAddressInformation
	{
		/// <summary>Gets the Internet Protocol (IP) address.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> instance that contains the IP address of an interface.</returns>
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060018F5 RID: 6389
		[global::__DynamicallyInvokable]
		public abstract IPAddress Address
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the Internet Protocol (IP) address is valid to appear in a Domain Name System (DNS) server database.</summary>
		/// <returns>
		///   <see langword="true" /> if the address can appear in a DNS database; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060018F6 RID: 6390
		[global::__DynamicallyInvokable]
		public abstract bool IsDnsEligible
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the Internet Protocol (IP) address is transient (a cluster address).</summary>
		/// <returns>
		///   <see langword="true" /> if the address is transient; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060018F7 RID: 6391
		[global::__DynamicallyInvokable]
		public abstract bool IsTransient
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> class.</summary>
		// Token: 0x060018F8 RID: 6392 RVA: 0x0007DA71 File Offset: 0x0007BC71
		[global::__DynamicallyInvokable]
		protected IPAddressInformation()
		{
		}
	}
}
