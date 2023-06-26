using System;

namespace System.Diagnostics
{
	/// <summary>Specifies what messages to output for the <see cref="T:System.Diagnostics.Debug" />, <see cref="T:System.Diagnostics.Trace" /> and <see cref="T:System.Diagnostics.TraceSwitch" /> classes.</summary>
	// Token: 0x020004B1 RID: 1201
	public enum TraceLevel
	{
		/// <summary>Output no tracing and debugging messages.</summary>
		// Token: 0x040026D1 RID: 9937
		Off,
		/// <summary>Output error-handling messages.</summary>
		// Token: 0x040026D2 RID: 9938
		Error,
		/// <summary>Output warnings and error-handling messages.</summary>
		// Token: 0x040026D3 RID: 9939
		Warning,
		/// <summary>Output informational messages, warnings, and error-handling messages.</summary>
		// Token: 0x040026D4 RID: 9940
		Info,
		/// <summary>Output all debugging and tracing messages.</summary>
		// Token: 0x040026D5 RID: 9941
		Verbose
	}
}
