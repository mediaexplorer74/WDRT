using System;

namespace System.Diagnostics
{
	/// <summary>Defines access levels used by <see cref="T:System.Diagnostics.EventLog" /> permission classes.</summary>
	// Token: 0x020004D0 RID: 1232
	[Flags]
	public enum EventLogPermissionAccess
	{
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> has no permissions.</summary>
		// Token: 0x0400275E RID: 10078
		None = 0,
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> can write to existing logs, and create event sources and logs.</summary>
		// Token: 0x0400275F RID: 10079
		Write = 16,
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> can create an event source, read existing logs, delete event sources or logs, respond to entries, clear an event log, listen to events, and access a collection of all event logs.</summary>
		// Token: 0x04002760 RID: 10080
		Administer = 48,
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> can read existing logs. <see langword="Note" /> This member is now obsolete, use <see cref="F:System.Diagnostics.EventLogPermissionAccess.Administer" /> instead.</summary>
		// Token: 0x04002761 RID: 10081
		[Obsolete("This member has been deprecated.  Please use System.Diagnostics.EventLogPermissionAccess.Administer instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Browse = 2,
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> can read or write to existing logs, and create event sources and logs. <see langword="Note" /> This member is now obsolete, use <see cref="F:System.Diagnostics.EventLogPermissionAccess.Write" /> instead.</summary>
		// Token: 0x04002762 RID: 10082
		[Obsolete("This member has been deprecated.  Please use System.Diagnostics.EventLogPermissionAccess.Write instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Instrument = 6,
		/// <summary>The <see cref="T:System.Diagnostics.EventLog" /> can read existing logs, delete event sources or logs, respond to entries, clear an event log, listen to events, and access a collection of all event logs. <see langword="Note" /> This member is now obsolete, use <see cref="F:System.Diagnostics.EventLogPermissionAccess.Administer" /> instead.</summary>
		// Token: 0x04002763 RID: 10083
		[Obsolete("This member has been deprecated.  Please use System.Diagnostics.EventLogPermissionAccess.Administer instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Audit = 10
	}
}
