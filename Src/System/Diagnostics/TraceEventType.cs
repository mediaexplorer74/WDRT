using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Identifies the type of event that has caused the trace.</summary>
	// Token: 0x020004AE RID: 1198
	public enum TraceEventType
	{
		/// <summary>Fatal error or application crash.</summary>
		// Token: 0x040026BC RID: 9916
		Critical = 1,
		/// <summary>Recoverable error.</summary>
		// Token: 0x040026BD RID: 9917
		Error,
		/// <summary>Noncritical problem.</summary>
		// Token: 0x040026BE RID: 9918
		Warning = 4,
		/// <summary>Informational message.</summary>
		// Token: 0x040026BF RID: 9919
		Information = 8,
		/// <summary>Debugging trace.</summary>
		// Token: 0x040026C0 RID: 9920
		Verbose = 16,
		/// <summary>Starting of a logical operation.</summary>
		// Token: 0x040026C1 RID: 9921
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Start = 256,
		/// <summary>Stopping of a logical operation.</summary>
		// Token: 0x040026C2 RID: 9922
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Stop = 512,
		/// <summary>Suspension of a logical operation.</summary>
		// Token: 0x040026C3 RID: 9923
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Suspend = 1024,
		/// <summary>Resumption of a logical operation.</summary>
		// Token: 0x040026C4 RID: 9924
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Resume = 2048,
		/// <summary>Changing of correlation identity.</summary>
		// Token: 0x040026C5 RID: 9925
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Transfer = 4096
	}
}
