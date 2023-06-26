using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the type of protocol that an instance of the <see cref="T:System.Net.Sockets.Socket" /> class can use.</summary>
	// Token: 0x02000371 RID: 881
	public enum ProtocolFamily
	{
		/// <summary>Unknown protocol.</summary>
		// Token: 0x04001DDB RID: 7643
		Unknown = -1,
		/// <summary>Unspecified protocol.</summary>
		// Token: 0x04001DDC RID: 7644
		Unspecified,
		/// <summary>Unix local to host protocol.</summary>
		// Token: 0x04001DDD RID: 7645
		Unix,
		/// <summary>IP version 4 protocol.</summary>
		// Token: 0x04001DDE RID: 7646
		InterNetwork,
		/// <summary>ARPANET IMP protocol.</summary>
		// Token: 0x04001DDF RID: 7647
		ImpLink,
		/// <summary>PUP protocol.</summary>
		// Token: 0x04001DE0 RID: 7648
		Pup,
		/// <summary>MIT CHAOS protocol.</summary>
		// Token: 0x04001DE1 RID: 7649
		Chaos,
		/// <summary>Xerox NS protocol.</summary>
		// Token: 0x04001DE2 RID: 7650
		NS,
		/// <summary>IPX or SPX protocol.</summary>
		// Token: 0x04001DE3 RID: 7651
		Ipx = 6,
		/// <summary>ISO protocol.</summary>
		// Token: 0x04001DE4 RID: 7652
		Iso,
		/// <summary>OSI protocol.</summary>
		// Token: 0x04001DE5 RID: 7653
		Osi = 7,
		/// <summary>European Computer Manufacturers Association (ECMA) protocol.</summary>
		// Token: 0x04001DE6 RID: 7654
		Ecma,
		/// <summary>DataKit protocol.</summary>
		// Token: 0x04001DE7 RID: 7655
		DataKit,
		/// <summary>CCITT protocol, such as X.25.</summary>
		// Token: 0x04001DE8 RID: 7656
		Ccitt,
		/// <summary>IBM SNA protocol.</summary>
		// Token: 0x04001DE9 RID: 7657
		Sna,
		/// <summary>DECNet protocol.</summary>
		// Token: 0x04001DEA RID: 7658
		DecNet,
		/// <summary>Direct data link protocol.</summary>
		// Token: 0x04001DEB RID: 7659
		DataLink,
		/// <summary>LAT protocol.</summary>
		// Token: 0x04001DEC RID: 7660
		Lat,
		/// <summary>NSC HyperChannel protocol.</summary>
		// Token: 0x04001DED RID: 7661
		HyperChannel,
		/// <summary>AppleTalk protocol.</summary>
		// Token: 0x04001DEE RID: 7662
		AppleTalk,
		/// <summary>NetBIOS protocol.</summary>
		// Token: 0x04001DEF RID: 7663
		NetBios,
		/// <summary>VoiceView protocol.</summary>
		// Token: 0x04001DF0 RID: 7664
		VoiceView,
		/// <summary>FireFox protocol.</summary>
		// Token: 0x04001DF1 RID: 7665
		FireFox,
		/// <summary>Banyan protocol.</summary>
		// Token: 0x04001DF2 RID: 7666
		Banyan = 21,
		/// <summary>Native ATM services protocol.</summary>
		// Token: 0x04001DF3 RID: 7667
		Atm,
		/// <summary>IP version 6 protocol.</summary>
		// Token: 0x04001DF4 RID: 7668
		InterNetworkV6,
		/// <summary>Microsoft Cluster products protocol.</summary>
		// Token: 0x04001DF5 RID: 7669
		Cluster,
		/// <summary>IEEE 1284.4 workgroup protocol.</summary>
		// Token: 0x04001DF6 RID: 7670
		Ieee12844,
		/// <summary>IrDA protocol.</summary>
		// Token: 0x04001DF7 RID: 7671
		Irda,
		/// <summary>Network Designers OSI gateway enabled protocol.</summary>
		// Token: 0x04001DF8 RID: 7672
		NetworkDesigners = 28,
		/// <summary>MAX protocol.</summary>
		// Token: 0x04001DF9 RID: 7673
		Max
	}
}
