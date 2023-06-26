using System;

namespace Microsoft.Win32
{
	/// <summary>Defines identifiers that represent how the current logon session is ending.</summary>
	// Token: 0x0200001A RID: 26
	public enum SessionEndReasons
	{
		/// <summary>The user is logging off and ending the current user session. The operating system continues to run.</summary>
		// Token: 0x04000304 RID: 772
		Logoff = 1,
		/// <summary>The operating system is shutting down.</summary>
		// Token: 0x04000305 RID: 773
		SystemShutdown
	}
}
