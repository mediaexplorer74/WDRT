using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the Network Basic Input/Output System (NetBIOS) node type.</summary>
	// Token: 0x020002E5 RID: 741
	[global::__DynamicallyInvokable]
	public enum NetBiosNodeType
	{
		/// <summary>An unknown node type.</summary>
		// Token: 0x04001A47 RID: 6727
		[global::__DynamicallyInvokable]
		Unknown,
		/// <summary>A broadcast node.</summary>
		// Token: 0x04001A48 RID: 6728
		[global::__DynamicallyInvokable]
		Broadcast,
		/// <summary>A peer-to-peer node.</summary>
		// Token: 0x04001A49 RID: 6729
		[global::__DynamicallyInvokable]
		Peer2Peer,
		/// <summary>A mixed node.</summary>
		// Token: 0x04001A4A RID: 6730
		[global::__DynamicallyInvokable]
		Mixed = 4,
		/// <summary>A hybrid node.</summary>
		// Token: 0x04001A4B RID: 6731
		[global::__DynamicallyInvokable]
		Hybrid = 8
	}
}
