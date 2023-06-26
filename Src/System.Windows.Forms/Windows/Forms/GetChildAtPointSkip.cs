using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies which child controls to skip.</summary>
	// Token: 0x0200026D RID: 621
	[Flags]
	public enum GetChildAtPointSkip
	{
		/// <summary>Does not skip any child windows.</summary>
		// Token: 0x0400105B RID: 4187
		None = 0,
		/// <summary>Skips invisible child windows.</summary>
		// Token: 0x0400105C RID: 4188
		Invisible = 1,
		/// <summary>Skips disabled child windows.</summary>
		// Token: 0x0400105D RID: 4189
		Disabled = 2,
		/// <summary>Skips transparent child windows.</summary>
		// Token: 0x0400105E RID: 4190
		Transparent = 4
	}
}
