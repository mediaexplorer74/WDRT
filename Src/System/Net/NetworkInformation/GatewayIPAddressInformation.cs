using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Represents the IP address of the network gateway. This class cannot be instantiated.</summary>
	// Token: 0x020002B0 RID: 688
	[global::__DynamicallyInvokable]
	public abstract class GatewayIPAddressInformation
	{
		/// <summary>Gets the IP address of the gateway.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> object that contains the IP address of the gateway.</returns>
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001991 RID: 6545
		[global::__DynamicallyInvokable]
		public abstract IPAddress Address
		{
			[global::__DynamicallyInvokable]
			get;
		}

		/// <summary>Initializes the members of this class.</summary>
		// Token: 0x06001992 RID: 6546 RVA: 0x0007DE08 File Offset: 0x0007C008
		[global::__DynamicallyInvokable]
		protected GatewayIPAddressInformation()
		{
		}
	}
}
