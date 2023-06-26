using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Specifies the start or end of a code region.</summary>
	// Token: 0x0200064F RID: 1615
	[ComVisible(true)]
	[Serializable]
	public enum CodeRegionMode
	{
		/// <summary>Not used.</summary>
		// Token: 0x04002BFC RID: 11260
		None,
		/// <summary>Start of the region.</summary>
		// Token: 0x04002BFD RID: 11261
		Start,
		/// <summary>End of the region.</summary>
		// Token: 0x04002BFE RID: 11262
		End
	}
}
