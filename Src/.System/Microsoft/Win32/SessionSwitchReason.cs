using System;

namespace Microsoft.Win32
{
	/// <summary>Defines identifiers used to represent the type of a session switch event.</summary>
	// Token: 0x0200001D RID: 29
	public enum SessionSwitchReason
	{
		/// <summary>A session has been connected from the console.</summary>
		// Token: 0x04000308 RID: 776
		ConsoleConnect = 1,
		/// <summary>A session has been disconnected from the console.</summary>
		// Token: 0x04000309 RID: 777
		ConsoleDisconnect,
		/// <summary>A session has been connected from a remote connection.</summary>
		// Token: 0x0400030A RID: 778
		RemoteConnect,
		/// <summary>A session has been disconnected from a remote connection.</summary>
		// Token: 0x0400030B RID: 779
		RemoteDisconnect,
		/// <summary>A user has logged on to a session.</summary>
		// Token: 0x0400030C RID: 780
		SessionLogon,
		/// <summary>A user has logged off from a session.</summary>
		// Token: 0x0400030D RID: 781
		SessionLogoff,
		/// <summary>A session has been locked.</summary>
		// Token: 0x0400030E RID: 782
		SessionLock,
		/// <summary>A session has been unlocked.</summary>
		// Token: 0x0400030F RID: 783
		SessionUnlock,
		/// <summary>A session has changed its status to or from remote controlled mode.</summary>
		// Token: 0x04000310 RID: 784
		SessionRemoteControl
	}
}
