using System;

namespace System.Net.Sockets
{
	/// <summary>Defines constants that are used by the <see cref="M:System.Net.Sockets.Socket.Shutdown(System.Net.Sockets.SocketShutdown)" /> method.</summary>
	// Token: 0x02000381 RID: 897
	public enum SocketShutdown
	{
		/// <summary>Disables a <see cref="T:System.Net.Sockets.Socket" /> for receiving. This field is constant.</summary>
		// Token: 0x04001F1A RID: 7962
		Receive,
		/// <summary>Disables a <see cref="T:System.Net.Sockets.Socket" /> for sending. This field is constant.</summary>
		// Token: 0x04001F1B RID: 7963
		Send,
		/// <summary>Disables a <see cref="T:System.Net.Sockets.Socket" /> for both sending and receiving. This field is constant.</summary>
		// Token: 0x04001F1C RID: 7964
		Both
	}
}
