using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how the elements of a control are drawn.</summary>
	// Token: 0x02000241 RID: 577
	public enum DrawMode
	{
		/// <summary>All the elements in a control are drawn by the operating system and are of the same size.</summary>
		// Token: 0x04000F47 RID: 3911
		Normal,
		/// <summary>All the elements in the control are drawn manually and are of the same size.</summary>
		// Token: 0x04000F48 RID: 3912
		OwnerDrawFixed,
		/// <summary>All the elements in the control are drawn manually and can differ in size.</summary>
		// Token: 0x04000F49 RID: 3913
		OwnerDrawVariable
	}
}
