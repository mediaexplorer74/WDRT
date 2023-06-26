using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies constants that define the state of the link.</summary>
	// Token: 0x020002C7 RID: 711
	public enum LinkState
	{
		/// <summary>The state of a link in its normal state (none of the other states apply).</summary>
		// Token: 0x0400123F RID: 4671
		Normal,
		/// <summary>The state of a link over which a mouse pointer is resting.</summary>
		// Token: 0x04001240 RID: 4672
		Hover,
		/// <summary>The state of a link that has been clicked.</summary>
		// Token: 0x04001241 RID: 4673
		Active,
		/// <summary>The state of a link that has been visited.</summary>
		// Token: 0x04001242 RID: 4674
		Visited = 4
	}
}
