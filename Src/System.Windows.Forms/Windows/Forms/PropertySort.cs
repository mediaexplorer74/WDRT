using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies how properties are sorted in the <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
	// Token: 0x02000330 RID: 816
	[ComVisible(true)]
	public enum PropertySort
	{
		/// <summary>Properties are displayed in the order in which they are retrieved from the <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		// Token: 0x04001F3D RID: 7997
		NoSort,
		/// <summary>Properties are sorted in an alphabetical list.</summary>
		// Token: 0x04001F3E RID: 7998
		Alphabetical,
		/// <summary>Properties are displayed according to their category in a group. The categories are defined by the properties themselves.</summary>
		// Token: 0x04001F3F RID: 7999
		Categorized,
		/// <summary>Properties are displayed according to their category in a group. The properties are further sorted alphabetically within the group. The categories are defined by the properties themselves.</summary>
		// Token: 0x04001F40 RID: 8000
		CategorizedAlphabetical
	}
}
