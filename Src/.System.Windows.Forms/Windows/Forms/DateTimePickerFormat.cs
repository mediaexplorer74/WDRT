using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the date and time format the <see cref="T:System.Windows.Forms.DateTimePicker" /> control displays.</summary>
	// Token: 0x0200022A RID: 554
	public enum DateTimePickerFormat
	{
		/// <summary>The <see cref="T:System.Windows.Forms.DateTimePicker" /> control displays the date/time value in the long date format set by the user's operating system.</summary>
		// Token: 0x04000ECF RID: 3791
		Long = 1,
		/// <summary>The <see cref="T:System.Windows.Forms.DateTimePicker" /> control displays the date/time value in the short date format set by the user's operating system.</summary>
		// Token: 0x04000ED0 RID: 3792
		Short,
		/// <summary>The <see cref="T:System.Windows.Forms.DateTimePicker" /> control displays the date/time value in the time format set by the user's operating system.</summary>
		// Token: 0x04000ED1 RID: 3793
		Time = 4,
		/// <summary>The <see cref="T:System.Windows.Forms.DateTimePicker" /> control displays the date/time value in a custom format. For more information, see <see cref="P:System.Windows.Forms.DateTimePicker.CustomFormat" />.</summary>
		// Token: 0x04000ED2 RID: 3794
		Custom = 8
	}
}
