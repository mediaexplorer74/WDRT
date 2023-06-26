using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the function of an access control entry (ACE).</summary>
	// Token: 0x02000205 RID: 517
	public enum AceQualifier
	{
		/// <summary>Allow access.</summary>
		// Token: 0x04000AF2 RID: 2802
		AccessAllowed,
		/// <summary>Deny access.</summary>
		// Token: 0x04000AF3 RID: 2803
		AccessDenied,
		/// <summary>Cause a system audit.</summary>
		// Token: 0x04000AF4 RID: 2804
		SystemAudit,
		/// <summary>Cause a system alarm.</summary>
		// Token: 0x04000AF5 RID: 2805
		SystemAlarm
	}
}
