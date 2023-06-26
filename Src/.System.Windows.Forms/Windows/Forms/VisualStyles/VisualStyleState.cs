using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies how visual styles are applied to the current application.</summary>
	// Token: 0x02000455 RID: 1109
	public enum VisualStyleState
	{
		/// <summary>Visual styles are not applied to the application.</summary>
		// Token: 0x0400325B RID: 12891
		NoneEnabled,
		/// <summary>Visual styles are applied only to the client area.</summary>
		// Token: 0x0400325C RID: 12892
		ClientAreaEnabled = 2,
		/// <summary>Visual styles are applied only to the nonclient area.</summary>
		// Token: 0x0400325D RID: 12893
		NonClientAreaEnabled = 1,
		/// <summary>Visual styles are applied to client and nonclient areas.</summary>
		// Token: 0x0400325E RID: 12894
		ClientAndNonClientAreasEnabled = 3
	}
}
