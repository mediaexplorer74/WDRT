using System;

namespace System.Drawing.Printing
{
	/// <summary>Specifies the printer's duplex setting.</summary>
	// Token: 0x02000053 RID: 83
	[Serializable]
	public enum Duplex
	{
		/// <summary>The printer's default duplex setting.</summary>
		// Token: 0x04000607 RID: 1543
		Default = -1,
		/// <summary>Single-sided printing.</summary>
		// Token: 0x04000608 RID: 1544
		Simplex = 1,
		/// <summary>Double-sided, horizontal printing.</summary>
		// Token: 0x04000609 RID: 1545
		Horizontal = 3,
		/// <summary>Double-sided, vertical printing.</summary>
		// Token: 0x0400060A RID: 1546
		Vertical = 2
	}
}
