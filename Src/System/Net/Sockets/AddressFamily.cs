using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the addressing scheme that an instance of the <see cref="T:System.Net.Sockets.Socket" /> class can use.</summary>
	// Token: 0x02000365 RID: 869
	[global::__DynamicallyInvokable]
	public enum AddressFamily
	{
		/// <summary>Unknown address family.</summary>
		// Token: 0x04001D63 RID: 7523
		[global::__DynamicallyInvokable]
		Unknown = -1,
		/// <summary>Unspecified address family.</summary>
		// Token: 0x04001D64 RID: 7524
		[global::__DynamicallyInvokable]
		Unspecified,
		/// <summary>Unix local to host address.</summary>
		// Token: 0x04001D65 RID: 7525
		[global::__DynamicallyInvokable]
		Unix,
		/// <summary>Address for IP version 4.</summary>
		// Token: 0x04001D66 RID: 7526
		[global::__DynamicallyInvokable]
		InterNetwork,
		/// <summary>ARPANET IMP address.</summary>
		// Token: 0x04001D67 RID: 7527
		[global::__DynamicallyInvokable]
		ImpLink,
		/// <summary>Address for PUP protocols.</summary>
		// Token: 0x04001D68 RID: 7528
		[global::__DynamicallyInvokable]
		Pup,
		/// <summary>Address for MIT CHAOS protocols.</summary>
		// Token: 0x04001D69 RID: 7529
		[global::__DynamicallyInvokable]
		Chaos,
		/// <summary>Address for Xerox NS protocols.</summary>
		// Token: 0x04001D6A RID: 7530
		[global::__DynamicallyInvokable]
		NS,
		/// <summary>IPX or SPX address.</summary>
		// Token: 0x04001D6B RID: 7531
		[global::__DynamicallyInvokable]
		Ipx = 6,
		/// <summary>Address for ISO protocols.</summary>
		// Token: 0x04001D6C RID: 7532
		[global::__DynamicallyInvokable]
		Iso,
		/// <summary>Address for OSI protocols.</summary>
		// Token: 0x04001D6D RID: 7533
		[global::__DynamicallyInvokable]
		Osi = 7,
		/// <summary>European Computer Manufacturers Association (ECMA) address.</summary>
		// Token: 0x04001D6E RID: 7534
		[global::__DynamicallyInvokable]
		Ecma,
		/// <summary>Address for Datakit protocols.</summary>
		// Token: 0x04001D6F RID: 7535
		[global::__DynamicallyInvokable]
		DataKit,
		/// <summary>Addresses for CCITT protocols, such as X.25.</summary>
		// Token: 0x04001D70 RID: 7536
		[global::__DynamicallyInvokable]
		Ccitt,
		/// <summary>IBM SNA address.</summary>
		// Token: 0x04001D71 RID: 7537
		[global::__DynamicallyInvokable]
		Sna,
		/// <summary>DECnet address.</summary>
		// Token: 0x04001D72 RID: 7538
		[global::__DynamicallyInvokable]
		DecNet,
		/// <summary>Direct data-link interface address.</summary>
		// Token: 0x04001D73 RID: 7539
		[global::__DynamicallyInvokable]
		DataLink,
		/// <summary>LAT address.</summary>
		// Token: 0x04001D74 RID: 7540
		[global::__DynamicallyInvokable]
		Lat,
		/// <summary>NSC Hyperchannel address.</summary>
		// Token: 0x04001D75 RID: 7541
		[global::__DynamicallyInvokable]
		HyperChannel,
		/// <summary>AppleTalk address.</summary>
		// Token: 0x04001D76 RID: 7542
		[global::__DynamicallyInvokable]
		AppleTalk,
		/// <summary>NetBios address.</summary>
		// Token: 0x04001D77 RID: 7543
		[global::__DynamicallyInvokable]
		NetBios,
		/// <summary>VoiceView address.</summary>
		// Token: 0x04001D78 RID: 7544
		[global::__DynamicallyInvokable]
		VoiceView,
		/// <summary>FireFox address.</summary>
		// Token: 0x04001D79 RID: 7545
		[global::__DynamicallyInvokable]
		FireFox,
		/// <summary>Banyan address.</summary>
		// Token: 0x04001D7A RID: 7546
		[global::__DynamicallyInvokable]
		Banyan = 21,
		/// <summary>Native ATM services address.</summary>
		// Token: 0x04001D7B RID: 7547
		[global::__DynamicallyInvokable]
		Atm,
		/// <summary>Address for IP version 6.</summary>
		// Token: 0x04001D7C RID: 7548
		[global::__DynamicallyInvokable]
		InterNetworkV6,
		/// <summary>Address for Microsoft cluster products.</summary>
		// Token: 0x04001D7D RID: 7549
		[global::__DynamicallyInvokable]
		Cluster,
		/// <summary>IEEE 1284.4 workgroup address.</summary>
		// Token: 0x04001D7E RID: 7550
		[global::__DynamicallyInvokable]
		Ieee12844,
		/// <summary>IrDA address.</summary>
		// Token: 0x04001D7F RID: 7551
		[global::__DynamicallyInvokable]
		Irda,
		/// <summary>Address for Network Designers OSI gateway-enabled protocols.</summary>
		// Token: 0x04001D80 RID: 7552
		[global::__DynamicallyInvokable]
		NetworkDesigners = 28,
		/// <summary>MAX address.</summary>
		// Token: 0x04001D81 RID: 7553
		Max
	}
}
