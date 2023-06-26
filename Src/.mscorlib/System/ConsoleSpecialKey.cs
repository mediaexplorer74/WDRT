using System;

namespace System
{
	/// <summary>Specifies combinations of modifier and console keys that can interrupt the current process.</summary>
	// Token: 0x020000C9 RID: 201
	[Serializable]
	public enum ConsoleSpecialKey
	{
		/// <summary>The <see cref="F:System.ConsoleModifiers.Control" /> modifier key plus the <see cref="F:System.ConsoleKey.C" /> console key.</summary>
		// Token: 0x0400052F RID: 1327
		ControlC,
		/// <summary>The <see cref="F:System.ConsoleModifiers.Control" /> modifier key plus the BREAK console key.</summary>
		// Token: 0x04000530 RID: 1328
		ControlBreak
	}
}
