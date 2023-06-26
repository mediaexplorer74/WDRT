using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies constants indicating when the error icon, supplied by an <see cref="T:System.Windows.Forms.ErrorProvider" />, should blink to alert the user that an error has occurred.</summary>
	// Token: 0x02000249 RID: 585
	public enum ErrorBlinkStyle
	{
		/// <summary>Blinks when the icon is already displayed and a new error string is set for the control.</summary>
		// Token: 0x04000F61 RID: 3937
		BlinkIfDifferentError,
		/// <summary>Always blink when the error icon is first displayed, or when a error description string is set for the control and the error icon is already displayed.</summary>
		// Token: 0x04000F62 RID: 3938
		AlwaysBlink,
		/// <summary>Never blink the error icon.</summary>
		// Token: 0x04000F63 RID: 3939
		NeverBlink
	}
}
