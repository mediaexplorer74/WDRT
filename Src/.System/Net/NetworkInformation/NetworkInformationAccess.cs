using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies permission to access information about network interfaces and traffic statistics.</summary>
	// Token: 0x020002E0 RID: 736
	[Flags]
	public enum NetworkInformationAccess
	{
		/// <summary>No access to network information.</summary>
		// Token: 0x04001A3C RID: 6716
		None = 0,
		/// <summary>Read access to network information.</summary>
		// Token: 0x04001A3D RID: 6717
		Read = 1,
		/// <summary>Ping access to network information.</summary>
		// Token: 0x04001A3E RID: 6718
		Ping = 4
	}
}
