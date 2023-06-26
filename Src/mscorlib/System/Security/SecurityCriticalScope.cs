using System;

namespace System.Security
{
	/// <summary>Specifies the scope of a <see cref="T:System.Security.SecurityCriticalAttribute" />.</summary>
	// Token: 0x020001C6 RID: 454
	[Obsolete("SecurityCriticalScope is only used for .NET 2.0 transparency compatibility.")]
	public enum SecurityCriticalScope
	{
		/// <summary>The attribute applies only to the immediate target.</summary>
		// Token: 0x040009C3 RID: 2499
		Explicit,
		/// <summary>The attribute applies to all code that follows it.</summary>
		// Token: 0x040009C4 RID: 2500
		Everything
	}
}
