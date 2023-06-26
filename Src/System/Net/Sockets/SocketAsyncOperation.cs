using System;

namespace System.Net.Sockets
{
	/// <summary>The type of asynchronous socket operation most recently performed with this context object.</summary>
	// Token: 0x02000379 RID: 889
	public enum SocketAsyncOperation
	{
		/// <summary>None of the socket operations.</summary>
		// Token: 0x04001E4F RID: 7759
		None,
		/// <summary>A socket Accept operation.</summary>
		// Token: 0x04001E50 RID: 7760
		Accept,
		/// <summary>A socket Connect operation.</summary>
		// Token: 0x04001E51 RID: 7761
		Connect,
		/// <summary>A socket Disconnect operation.</summary>
		// Token: 0x04001E52 RID: 7762
		Disconnect,
		/// <summary>A socket Receive operation.</summary>
		// Token: 0x04001E53 RID: 7763
		Receive,
		/// <summary>A socket ReceiveFrom operation.</summary>
		// Token: 0x04001E54 RID: 7764
		ReceiveFrom,
		/// <summary>A socket ReceiveMessageFrom operation.</summary>
		// Token: 0x04001E55 RID: 7765
		ReceiveMessageFrom,
		/// <summary>A socket Send operation.</summary>
		// Token: 0x04001E56 RID: 7766
		Send,
		/// <summary>A socket SendPackets operation.</summary>
		// Token: 0x04001E57 RID: 7767
		SendPackets,
		/// <summary>A socket SendTo operation.</summary>
		// Token: 0x04001E58 RID: 7768
		SendTo
	}
}
