using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the conditions for auditing attempts to access a securable object.</summary>
	// Token: 0x020001F9 RID: 505
	[Flags]
	public enum AuditFlags
	{
		/// <summary>No access attempts are to be audited.</summary>
		// Token: 0x04000AA5 RID: 2725
		None = 0,
		/// <summary>Successful access attempts are to be audited.</summary>
		// Token: 0x04000AA6 RID: 2726
		Success = 1,
		/// <summary>Failed access attempts are to be audited.</summary>
		// Token: 0x04000AA7 RID: 2727
		Failure = 2
	}
}
