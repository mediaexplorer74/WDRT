using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Specifies the type of user interface (UI) the trust manager should use for trust decisions.</summary>
	// Token: 0x0200035A RID: 858
	[ComVisible(true)]
	public enum TrustManagerUIContext
	{
		/// <summary>An Install UI.</summary>
		// Token: 0x0400114C RID: 4428
		Install,
		/// <summary>An Upgrade UI.</summary>
		// Token: 0x0400114D RID: 4429
		Upgrade,
		/// <summary>A Run UI.</summary>
		// Token: 0x0400114E RID: 4430
		Run
	}
}
