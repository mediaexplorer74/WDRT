using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies socket send and receive behaviors.</summary>
	// Token: 0x0200037E RID: 894
	[Flags]
	public enum SocketFlags
	{
		/// <summary>Use no flags for this call.</summary>
		// Token: 0x04001EDA RID: 7898
		None = 0,
		/// <summary>Process out-of-band data.</summary>
		// Token: 0x04001EDB RID: 7899
		OutOfBand = 1,
		/// <summary>Peek at the incoming message.</summary>
		// Token: 0x04001EDC RID: 7900
		Peek = 2,
		/// <summary>Send without using routing tables.</summary>
		// Token: 0x04001EDD RID: 7901
		DontRoute = 4,
		/// <summary>Provides a standard value for the number of WSABUF structures that are used to send and receive data. This value is not used or supported on .NET Framework 4.5.</summary>
		// Token: 0x04001EDE RID: 7902
		MaxIOVectorLength = 16,
		/// <summary>The message was too large to fit into the specified buffer and was truncated.</summary>
		// Token: 0x04001EDF RID: 7903
		Truncated = 256,
		/// <summary>Indicates that the control data did not fit into an internal 64-KB buffer and was truncated.</summary>
		// Token: 0x04001EE0 RID: 7904
		ControlDataTruncated = 512,
		/// <summary>Indicates a broadcast packet.</summary>
		// Token: 0x04001EE1 RID: 7905
		Broadcast = 1024,
		/// <summary>Indicates a multicast packet.</summary>
		// Token: 0x04001EE2 RID: 7906
		Multicast = 2048,
		/// <summary>Partial send or receive for message.</summary>
		// Token: 0x04001EE3 RID: 7907
		Partial = 32768
	}
}
