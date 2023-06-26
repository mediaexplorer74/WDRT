using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the selection behavior of a list box.</summary>
	// Token: 0x02000364 RID: 868
	[ComVisible(true)]
	public enum SelectionMode
	{
		/// <summary>No items can be selected.</summary>
		// Token: 0x040021C6 RID: 8646
		None,
		/// <summary>Only one item can be selected.</summary>
		// Token: 0x040021C7 RID: 8647
		One,
		/// <summary>Multiple items can be selected.</summary>
		// Token: 0x040021C8 RID: 8648
		MultiSimple,
		/// <summary>Multiple items can be selected, and the user can use the SHIFT, CTRL, and arrow keys to make selections</summary>
		// Token: 0x040021C9 RID: 8649
		MultiExtended
	}
}
