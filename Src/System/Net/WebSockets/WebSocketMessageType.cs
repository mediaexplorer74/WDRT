using System;

namespace System.Net.WebSockets
{
	/// <summary>Indicates the message type.</summary>
	// Token: 0x02000239 RID: 569
	public enum WebSocketMessageType
	{
		/// <summary>The message is clear text.</summary>
		// Token: 0x040016B2 RID: 5810
		Text,
		/// <summary>The message is in binary format.</summary>
		// Token: 0x040016B3 RID: 5811
		Binary,
		/// <summary>A receive has completed because a close message was received.</summary>
		// Token: 0x040016B4 RID: 5812
		Close
	}
}
