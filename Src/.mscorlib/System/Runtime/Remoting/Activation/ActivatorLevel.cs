using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Activation
{
	/// <summary>Defines the appropriate position for a <see cref="T:System.Activator" /> in the chain of activators.</summary>
	// Token: 0x02000898 RID: 2200
	[ComVisible(true)]
	[Serializable]
	public enum ActivatorLevel
	{
		/// <summary>Constructs a blank object and runs the constructor.</summary>
		// Token: 0x040029FE RID: 10750
		Construction = 4,
		/// <summary>Finds or creates a suitable context.</summary>
		// Token: 0x040029FF RID: 10751
		Context = 8,
		/// <summary>Finds or creates a <see cref="T:System.AppDomain" />.</summary>
		// Token: 0x04002A00 RID: 10752
		AppDomain = 12,
		/// <summary>Starts a process.</summary>
		// Token: 0x04002A01 RID: 10753
		Process = 16,
		/// <summary>Finds a suitable computer.</summary>
		// Token: 0x04002A02 RID: 10754
		Machine = 20
	}
}
