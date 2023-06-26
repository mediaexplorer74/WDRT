using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x020003F0 RID: 1008
	// (Invoke) Token: 0x0600334A RID: 13130
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	[Serializable]
	internal delegate void LogMessageEventHandler(LoggingLevels level, LogSwitch category, string message, StackTrace location);
}
