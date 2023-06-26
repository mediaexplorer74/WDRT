using System;

namespace System.Windows.Forms
{
	/// <summary>Represents the insertion mode used by text boxes.</summary>
	// Token: 0x020002B7 RID: 695
	public enum InsertKeyMode
	{
		/// <summary>Honors the current INSERT key mode of the keyboard.</summary>
		// Token: 0x04001200 RID: 4608
		Default,
		/// <summary>Indicates that the insertion mode is enabled regardless of the INSERT key mode of the keyboard.</summary>
		// Token: 0x04001201 RID: 4609
		Insert,
		/// <summary>Indicates that the overwrite mode is enabled regardless of the INSERT key mode of the keyboard.</summary>
		// Token: 0x04001202 RID: 4610
		Overwrite
	}
}
