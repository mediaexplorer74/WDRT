using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies options on a <see cref="T:System.Windows.Forms.MessageBox" />.</summary>
	// Token: 0x020002FC RID: 764
	[Flags]
	public enum MessageBoxOptions
	{
		/// <summary>The message box is displayed on the active desktop.</summary>
		// Token: 0x040013FF RID: 5119
		ServiceNotification = 2097152,
		/// <summary>The message box is displayed on the active desktop.</summary>
		// Token: 0x04001400 RID: 5120
		DefaultDesktopOnly = 131072,
		/// <summary>The message box text is right-aligned.</summary>
		// Token: 0x04001401 RID: 5121
		RightAlign = 524288,
		/// <summary>Specifies that the message box text is displayed with right to left reading order.</summary>
		// Token: 0x04001402 RID: 5122
		RtlReading = 1048576
	}
}
