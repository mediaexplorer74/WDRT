using System;

namespace System.Net.Sockets
{
	/// <summary>Defines the polling modes for the <see cref="M:System.Net.Sockets.Socket.Poll(System.Int32,System.Net.Sockets.SelectMode)" /> method.</summary>
	// Token: 0x02000373 RID: 883
	public enum SelectMode
	{
		/// <summary>Read status mode.</summary>
		// Token: 0x04001E15 RID: 7701
		SelectRead,
		/// <summary>Write status mode.</summary>
		// Token: 0x04001E16 RID: 7702
		SelectWrite,
		/// <summary>Error status mode.</summary>
		// Token: 0x04001E17 RID: 7703
		SelectError
	}
}
