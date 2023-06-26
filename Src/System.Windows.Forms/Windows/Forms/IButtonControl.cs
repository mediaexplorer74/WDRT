using System;

namespace System.Windows.Forms
{
	/// <summary>Allows a control to act like a button on a form.</summary>
	// Token: 0x02000287 RID: 647
	public interface IButtonControl
	{
		/// <summary>Gets or sets the value returned to the parent form when the button is clicked.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x0600298A RID: 10634
		// (set) Token: 0x0600298B RID: 10635
		DialogResult DialogResult { get; set; }

		/// <summary>Notifies a control that it is the default button so that its appearance and behavior is adjusted accordingly.</summary>
		/// <param name="value">
		///   <see langword="true" /> if the control should behave as a default button; otherwise <see langword="false" />.</param>
		// Token: 0x0600298C RID: 10636
		void NotifyDefault(bool value);

		/// <summary>Generates a <see cref="E:System.Windows.Forms.Control.Click" /> event for the control.</summary>
		// Token: 0x0600298D RID: 10637
		void PerformClick();
	}
}
