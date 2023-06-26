using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers that indicate the type of a refresh of the Properties window.</summary>
	// Token: 0x020005C2 RID: 1474
	public enum RefreshProperties
	{
		/// <summary>No refresh is necessary.</summary>
		// Token: 0x04002AC1 RID: 10945
		None,
		/// <summary>The properties should be requeried and the view should be refreshed.</summary>
		// Token: 0x04002AC2 RID: 10946
		All,
		/// <summary>The view should be refreshed.</summary>
		// Token: 0x04002AC3 RID: 10947
		Repaint
	}
}
