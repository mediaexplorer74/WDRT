using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Specifies the scheduling priority of a <see cref="T:System.Threading.Thread" />.</summary>
	// Token: 0x02000525 RID: 1317
	[ComVisible(true)]
	[Serializable]
	public enum ThreadPriority
	{
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with any other priority.</summary>
		// Token: 0x04001A26 RID: 6694
		Lowest,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with <see langword="Normal" /> priority and before those with <see langword="Lowest" /> priority.</summary>
		// Token: 0x04001A27 RID: 6695
		BelowNormal,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with <see langword="AboveNormal" /> priority and before those with <see langword="BelowNormal" /> priority. Threads have <see langword="Normal" /> priority by default.</summary>
		// Token: 0x04001A28 RID: 6696
		Normal,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled after threads with <see langword="Highest" /> priority and before those with <see langword="Normal" /> priority.</summary>
		// Token: 0x04001A29 RID: 6697
		AboveNormal,
		/// <summary>The <see cref="T:System.Threading.Thread" /> can be scheduled before threads with any other priority.</summary>
		// Token: 0x04001A2A RID: 6698
		Highest
	}
}
