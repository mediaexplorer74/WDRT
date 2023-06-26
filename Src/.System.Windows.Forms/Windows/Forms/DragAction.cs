using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies how and if a drag-and-drop operation should continue.</summary>
	// Token: 0x02000234 RID: 564
	[ComVisible(true)]
	public enum DragAction
	{
		/// <summary>The operation will continue.</summary>
		// Token: 0x04000F0A RID: 3850
		Continue,
		/// <summary>The operation will stop with a drop.</summary>
		// Token: 0x04000F0B RID: 3851
		Drop,
		/// <summary>The operation is canceled with no drop message.</summary>
		// Token: 0x04000F0C RID: 3852
		Cancel
	}
}
