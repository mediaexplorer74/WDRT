using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies whether the tabs in a tab control are owner-drawn (drawn by the parent window), or drawn by the operating system.</summary>
	// Token: 0x0200038B RID: 907
	public enum TabDrawMode
	{
		/// <summary>The tabs are drawn by the operating system, and are of the same size.</summary>
		// Token: 0x04002373 RID: 9075
		Normal,
		/// <summary>The tabs are drawn by the parent window, and are of the same size.</summary>
		// Token: 0x04002374 RID: 9076
		OwnerDrawFixed
	}
}
