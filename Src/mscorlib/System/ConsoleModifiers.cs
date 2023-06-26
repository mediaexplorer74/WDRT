using System;

namespace System
{
	/// <summary>Represents the SHIFT, ALT, and CTRL modifier keys on a keyboard.</summary>
	// Token: 0x020000C8 RID: 200
	[Flags]
	[Serializable]
	public enum ConsoleModifiers
	{
		/// <summary>The left or right ALT modifier key.</summary>
		// Token: 0x0400052B RID: 1323
		Alt = 1,
		/// <summary>The left or right SHIFT modifier key.</summary>
		// Token: 0x0400052C RID: 1324
		Shift = 2,
		/// <summary>The left or right CTRL modifier key.</summary>
		// Token: 0x0400052D RID: 1325
		Control = 4
	}
}
