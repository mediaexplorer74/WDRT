using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies values for navigating among accessible objects.</summary>
	// Token: 0x02000116 RID: 278
	public enum AccessibleNavigation
	{
		/// <summary>Navigation to a sibling object located below the starting object.</summary>
		// Token: 0x04000528 RID: 1320
		Down = 2,
		/// <summary>Navigation to the first child of the object.</summary>
		// Token: 0x04000529 RID: 1321
		FirstChild = 7,
		/// <summary>Navigation to the last child of the object.</summary>
		// Token: 0x0400052A RID: 1322
		LastChild,
		/// <summary>Navigation to the sibling object located to the left of the starting object.</summary>
		// Token: 0x0400052B RID: 1323
		Left = 3,
		/// <summary>Navigation to the next logical object, typically from a sibling object to the starting object.</summary>
		// Token: 0x0400052C RID: 1324
		Next = 5,
		/// <summary>Navigation to the previous logical object, typically from a sibling object to the starting object.</summary>
		// Token: 0x0400052D RID: 1325
		Previous,
		/// <summary>Navigation to the sibling object located to the right of the starting object.</summary>
		// Token: 0x0400052E RID: 1326
		Right = 4,
		/// <summary>Navigation to a sibling object located above the starting object.</summary>
		// Token: 0x0400052F RID: 1327
		Up = 1
	}
}
