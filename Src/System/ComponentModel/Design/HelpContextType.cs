using System;

namespace System.ComponentModel.Design
{
	/// <summary>Defines identifiers that indicate information about the context in which a request for Help information originated.</summary>
	// Token: 0x020005DF RID: 1503
	public enum HelpContextType
	{
		/// <summary>A general context.</summary>
		// Token: 0x04002AEC RID: 10988
		Ambient,
		/// <summary>A window.</summary>
		// Token: 0x04002AED RID: 10989
		Window,
		/// <summary>A selection.</summary>
		// Token: 0x04002AEE RID: 10990
		Selection,
		/// <summary>A tool window selection.</summary>
		// Token: 0x04002AEF RID: 10991
		ToolWindowSelection
	}
}
