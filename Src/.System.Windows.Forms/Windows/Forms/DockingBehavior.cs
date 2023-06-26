using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how a control should be docked by default when added through a designer.</summary>
	// Token: 0x0200022F RID: 559
	public enum DockingBehavior
	{
		/// <summary>Do not prompt the user for the desired docking behavior.</summary>
		// Token: 0x04000EF3 RID: 3827
		Never,
		/// <summary>Prompt the user for the desired docking behavior.</summary>
		// Token: 0x04000EF4 RID: 3828
		Ask,
		/// <summary>Set the control's <see cref="P:System.Windows.Forms.Control.Dock" /> property to <see cref="F:System.Windows.Forms.DockStyle.Fill" /> when it is dropped into a container with no other child controls.</summary>
		// Token: 0x04000EF5 RID: 3829
		AutoDock
	}
}
