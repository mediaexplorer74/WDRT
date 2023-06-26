using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies constants that define which mouse button was pressed.</summary>
	// Token: 0x02000300 RID: 768
	[Flags]
	[ComVisible(true)]
	public enum MouseButtons
	{
		/// <summary>The left mouse button was pressed.</summary>
		// Token: 0x0400143A RID: 5178
		Left = 1048576,
		/// <summary>No mouse button was pressed.</summary>
		// Token: 0x0400143B RID: 5179
		None = 0,
		/// <summary>The right mouse button was pressed.</summary>
		// Token: 0x0400143C RID: 5180
		Right = 2097152,
		/// <summary>The middle mouse button was pressed.</summary>
		// Token: 0x0400143D RID: 5181
		Middle = 4194304,
		/// <summary>The first XButton was pressed.</summary>
		// Token: 0x0400143E RID: 5182
		XButton1 = 8388608,
		/// <summary>The second XButton was pressed.</summary>
		// Token: 0x0400143F RID: 5183
		XButton2 = 16777216
	}
}
