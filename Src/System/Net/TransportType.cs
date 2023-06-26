using System;

namespace System.Net
{
	/// <summary>Defines transport types for the <see cref="T:System.Net.SocketPermission" /> and <see cref="T:System.Net.Sockets.Socket" /> classes.</summary>
	// Token: 0x02000165 RID: 357
	public enum TransportType
	{
		/// <summary>UDP transport.</summary>
		// Token: 0x040011C4 RID: 4548
		Udp = 1,
		/// <summary>The transport type is connectionless, such as UDP. Specifying this value has the same effect as specifying <see cref="F:System.Net.TransportType.Udp" />.</summary>
		// Token: 0x040011C5 RID: 4549
		Connectionless = 1,
		/// <summary>TCP transport.</summary>
		// Token: 0x040011C6 RID: 4550
		Tcp,
		/// <summary>The transport is connection oriented, such as TCP. Specifying this value has the same effect as specifying <see cref="F:System.Net.TransportType.Tcp" />.</summary>
		// Token: 0x040011C7 RID: 4551
		ConnectionOriented = 2,
		/// <summary>All transport types.</summary>
		// Token: 0x040011C8 RID: 4552
		All
	}
}
