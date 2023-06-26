using System;

namespace System.Windows.Forms
{
	/// <summary>Provides the functionality for a control to act as a parent for other controls.</summary>
	// Token: 0x02000289 RID: 649
	public interface IContainerControl
	{
		/// <summary>Gets or sets the control that is active on the container control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that is currently active on the container control.</returns>
		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x0600298F RID: 10639
		// (set) Token: 0x06002990 RID: 10640
		Control ActiveControl { get; set; }

		/// <summary>Activates a specified control.</summary>
		/// <param name="active">The <see cref="T:System.Windows.Forms.Control" /> being activated.</param>
		/// <returns>
		///   <see langword="true" /> if the control is successfully activated; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002991 RID: 10641
		bool ActivateControl(Control active);
	}
}
