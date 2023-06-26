using System;

namespace System.Net.WebSockets
{
	/// <summary>Defines the different states a WebSockets instance can be in.</summary>
	// Token: 0x0200023C RID: 572
	public enum WebSocketState
	{
		/// <summary>Reserved for future use.</summary>
		// Token: 0x040016C2 RID: 5826
		None,
		/// <summary>The connection is negotiating the handshake with the remote endpoint.</summary>
		// Token: 0x040016C3 RID: 5827
		Connecting,
		/// <summary>The initial state after the HTTP handshake has been completed.</summary>
		// Token: 0x040016C4 RID: 5828
		Open,
		/// <summary>A close message was sent to the remote endpoint.</summary>
		// Token: 0x040016C5 RID: 5829
		CloseSent,
		/// <summary>A close message was received from the remote endpoint.</summary>
		// Token: 0x040016C6 RID: 5830
		CloseReceived,
		/// <summary>Indicates the WebSocket close handshake completed gracefully.</summary>
		// Token: 0x040016C7 RID: 5831
		Closed,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x040016C8 RID: 5832
		Aborted
	}
}
