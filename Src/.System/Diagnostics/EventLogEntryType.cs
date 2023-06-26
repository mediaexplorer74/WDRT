using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the event type of an event log entry.</summary>
	// Token: 0x020004CE RID: 1230
	public enum EventLogEntryType
	{
		/// <summary>An error event. This indicates a significant problem the user should know about; usually a loss of functionality or data.</summary>
		// Token: 0x04002757 RID: 10071
		Error = 1,
		/// <summary>A warning event. This indicates a problem that is not immediately significant, but that may signify conditions that could cause future problems.</summary>
		// Token: 0x04002758 RID: 10072
		Warning,
		/// <summary>An information event. This indicates a significant, successful operation.</summary>
		// Token: 0x04002759 RID: 10073
		Information = 4,
		/// <summary>A success audit event. This indicates a security event that occurs when an audited access attempt is successful; for example, logging on successfully.</summary>
		// Token: 0x0400275A RID: 10074
		SuccessAudit = 8,
		/// <summary>A failure audit event. This indicates a security event that occurs when an audited access attempt fails; for example, a failed attempt to open a file.</summary>
		// Token: 0x0400275B RID: 10075
		FailureAudit = 16
	}
}
