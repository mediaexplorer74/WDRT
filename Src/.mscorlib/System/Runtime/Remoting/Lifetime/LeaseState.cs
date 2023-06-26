using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	/// <summary>Indicates the possible lease states of a lifetime lease.</summary>
	// Token: 0x02000821 RID: 2081
	[ComVisible(true)]
	[Serializable]
	public enum LeaseState
	{
		/// <summary>The lease is not initialized.</summary>
		// Token: 0x040028B9 RID: 10425
		Null,
		/// <summary>The lease has been created, but is not yet active.</summary>
		// Token: 0x040028BA RID: 10426
		Initial,
		/// <summary>The lease is active and has not expired.</summary>
		// Token: 0x040028BB RID: 10427
		Active,
		/// <summary>The lease has expired and is seeking sponsorship.</summary>
		// Token: 0x040028BC RID: 10428
		Renewing,
		/// <summary>The lease has expired and cannot be renewed.</summary>
		// Token: 0x040028BD RID: 10429
		Expired
	}
}
