using System;

namespace System.Drawing.Printing
{
	/// <summary>Specifies a printer resolution.</summary>
	// Token: 0x02000063 RID: 99
	[Serializable]
	public enum PrinterResolutionKind
	{
		/// <summary>High resolution.</summary>
		// Token: 0x040006CD RID: 1741
		High = -4,
		/// <summary>Medium resolution.</summary>
		// Token: 0x040006CE RID: 1742
		Medium,
		/// <summary>Low resolution.</summary>
		// Token: 0x040006CF RID: 1743
		Low,
		/// <summary>Draft-quality resolution.</summary>
		// Token: 0x040006D0 RID: 1744
		Draft,
		/// <summary>Custom resolution.</summary>
		// Token: 0x040006D1 RID: 1745
		Custom
	}
}
