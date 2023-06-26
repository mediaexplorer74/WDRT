using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the border styles for a form.</summary>
	// Token: 0x0200025D RID: 605
	[ComVisible(true)]
	public enum FormBorderStyle
	{
		/// <summary>No border.</summary>
		// Token: 0x04001031 RID: 4145
		None,
		/// <summary>A fixed, single-line border.</summary>
		// Token: 0x04001032 RID: 4146
		FixedSingle,
		/// <summary>A fixed, three-dimensional border.</summary>
		// Token: 0x04001033 RID: 4147
		Fixed3D,
		/// <summary>A thick, fixed dialog-style border.</summary>
		// Token: 0x04001034 RID: 4148
		FixedDialog,
		/// <summary>A resizable border.</summary>
		// Token: 0x04001035 RID: 4149
		Sizable,
		/// <summary>A tool window border that is not resizable. A tool window does not appear in the taskbar or in the window that appears when the user presses ALT+TAB. Although forms that specify <see cref="F:System.Windows.Forms.FormBorderStyle.FixedToolWindow" /> typically are not shown in the taskbar, you must also ensure that the <see cref="P:System.Windows.Forms.Form.ShowInTaskbar" /> property is set to <see langword="false" />, since its default value is <see langword="true" />.</summary>
		// Token: 0x04001036 RID: 4150
		FixedToolWindow,
		/// <summary>A resizable tool window border. A tool window does not appear in the taskbar or in the window that appears when the user presses ALT+TAB.</summary>
		// Token: 0x04001037 RID: 4151
		SizableToolWindow
	}
}
