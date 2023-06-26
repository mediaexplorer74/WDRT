using System;
using System.ComponentModel;

namespace System.Net.Sockets
{
	/// <summary>Specifies the method to download a client access policy file.</summary>
	// Token: 0x0200037B RID: 891
	[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public enum SocketClientAccessPolicyProtocol
	{
		/// <summary>The socket policy file is downloaded using a custom TCP protocol running on TCP port 943.</summary>
		// Token: 0x04001E5F RID: 7775
		Tcp,
		/// <summary>The socket policy file is downloaded using the HTTP protocol running on TCP port 943.</summary>
		// Token: 0x04001E60 RID: 7776
		Http
	}
}
