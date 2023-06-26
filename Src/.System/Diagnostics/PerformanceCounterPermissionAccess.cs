using System;

namespace System.Diagnostics
{
	/// <summary>Defines access levels used by <see cref="T:System.Diagnostics.PerformanceCounter" /> permission classes.</summary>
	// Token: 0x020004E9 RID: 1257
	[Flags]
	public enum PerformanceCounterPermissionAccess
	{
		/// <summary>The <see cref="T:System.Diagnostics.PerformanceCounter" /> can read categories.</summary>
		// Token: 0x040027EE RID: 10222
		[Obsolete("This member has been deprecated.  Use System.Diagnostics.PerformanceCounter.PerformanceCounterPermissionAccess.Read instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Browse = 1,
		/// <summary>The <see cref="T:System.Diagnostics.PerformanceCounter" /> can read and write categories.</summary>
		// Token: 0x040027EF RID: 10223
		[Obsolete("This member has been deprecated.  Use System.Diagnostics.PerformanceCounter.PerformanceCounterPermissionAccess.Write instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		Instrument = 3,
		/// <summary>The <see cref="T:System.Diagnostics.PerformanceCounter" /> has no permissions.</summary>
		// Token: 0x040027F0 RID: 10224
		None = 0,
		/// <summary>The <see cref="T:System.Diagnostics.PerformanceCounter" /> can read categories.</summary>
		// Token: 0x040027F1 RID: 10225
		Read = 1,
		/// <summary>The <see cref="T:System.Diagnostics.PerformanceCounter" /> can write categories.</summary>
		// Token: 0x040027F2 RID: 10226
		Write = 2,
		/// <summary>The <see cref="T:System.Diagnostics.PerformanceCounter" /> can read, write, and create categories.</summary>
		// Token: 0x040027F3 RID: 10227
		Administer = 7
	}
}
