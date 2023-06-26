using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the initial position of a form.</summary>
	// Token: 0x02000263 RID: 611
	[ComVisible(true)]
	public enum FormStartPosition
	{
		/// <summary>The position of the form is determined by the <see cref="P:System.Windows.Forms.Control.Location" /> property.</summary>
		// Token: 0x0400103C RID: 4156
		Manual,
		/// <summary>The form is centered on the current display, and has the dimensions specified in the form's size.</summary>
		// Token: 0x0400103D RID: 4157
		CenterScreen,
		/// <summary>The form is positioned at the Windows default location and has the dimensions specified in the form's size.</summary>
		// Token: 0x0400103E RID: 4158
		WindowsDefaultLocation,
		/// <summary>The form is positioned at the Windows default location and has the bounds determined by Windows default.</summary>
		// Token: 0x0400103F RID: 4159
		WindowsDefaultBounds,
		/// <summary>The form is centered within the bounds of its parent form.</summary>
		// Token: 0x04001040 RID: 4160
		CenterParent
	}
}
