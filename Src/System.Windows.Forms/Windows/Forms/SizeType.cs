using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how rows or columns of user interface (UI) elements should be sized relative to their container.</summary>
	// Token: 0x02000393 RID: 915
	public enum SizeType
	{
		/// <summary>The row or column should be automatically sized to share space with its peers.</summary>
		// Token: 0x0400238A RID: 9098
		AutoSize,
		/// <summary>The row or column should be sized to an exact number of pixels.</summary>
		// Token: 0x0400238B RID: 9099
		Absolute,
		/// <summary>The row or column should be sized as a percentage of the parent container.</summary>
		// Token: 0x0400238C RID: 9100
		Percent
	}
}
