using System;

namespace System.Security
{
	/// <summary>Identifies the set of security rules the common language runtime should enforce for an assembly.</summary>
	// Token: 0x020001CB RID: 459
	public enum SecurityRuleSet : byte
	{
		/// <summary>Unsupported. Using this value results in a <see cref="T:System.IO.FileLoadException" /> being thrown.</summary>
		// Token: 0x040009C7 RID: 2503
		None,
		/// <summary>Indicates that the runtime will enforce level 1 (.NET Framework version 2.0) transparency rules.</summary>
		// Token: 0x040009C8 RID: 2504
		Level1,
		/// <summary>Indicates that the runtime will enforce level 2 transparency rules.</summary>
		// Token: 0x040009C9 RID: 2505
		Level2
	}
}
