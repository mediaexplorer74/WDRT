using System;

namespace Microsoft.Win32
{
	/// <summary>Specifies which registry view to target on a 64-bit operating system.</summary>
	// Token: 0x02000017 RID: 23
	public enum RegistryView
	{
		/// <summary>The default view.</summary>
		// Token: 0x040001AB RID: 427
		Default,
		/// <summary>The 64-bit view.</summary>
		// Token: 0x040001AC RID: 428
		Registry64 = 256,
		/// <summary>The 32-bit view.</summary>
		// Token: 0x040001AD RID: 429
		Registry32 = 512
	}
}
