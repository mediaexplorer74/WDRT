using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Specifies the apartment state of a <see cref="T:System.Threading.Thread" />.</summary>
	// Token: 0x02000534 RID: 1332
	[ComVisible(true)]
	[Serializable]
	public enum ApartmentState
	{
		/// <summary>The <see cref="T:System.Threading.Thread" /> will create and enter a single-threaded apartment.</summary>
		// Token: 0x04001A5C RID: 6748
		STA,
		/// <summary>The <see cref="T:System.Threading.Thread" /> will create and enter a multithreaded apartment.</summary>
		// Token: 0x04001A5D RID: 6749
		MTA,
		/// <summary>The <see cref="P:System.Threading.Thread.ApartmentState" /> property has not been set.</summary>
		// Token: 0x04001A5E RID: 6750
		Unknown
	}
}
