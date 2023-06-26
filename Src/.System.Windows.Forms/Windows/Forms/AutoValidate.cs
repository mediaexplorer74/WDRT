using System;

namespace System.Windows.Forms
{
	/// <summary>Determines how a control validates its data when it loses user input focus.</summary>
	// Token: 0x02000129 RID: 297
	public enum AutoValidate
	{
		/// <summary>Implicit validation will not occur. Setting this value will not interfere with explicit calls to <see cref="M:System.Windows.Forms.ContainerControl.Validate" /> or <see cref="M:System.Windows.Forms.ContainerControl.ValidateChildren" />.</summary>
		// Token: 0x04000611 RID: 1553
		Disable,
		/// <summary>Implicit validation occurs when the control loses focus.</summary>
		// Token: 0x04000612 RID: 1554
		EnablePreventFocusChange,
		/// <summary>Implicit validation occurs, but if validation fails, focus will still change to the new control. If validation fails, the <see cref="E:System.Windows.Forms.Control.Validated" /> event will not fire.</summary>
		// Token: 0x04000613 RID: 1555
		EnableAllowFocusChange,
		/// <summary>The control inherits its <see cref="T:System.Windows.Forms.AutoValidate" /> behavior from its container (such as a form or another control). If there is no container control, it defaults to <see cref="F:System.Windows.Forms.AutoValidate.EnablePreventFocusChange" />.</summary>
		// Token: 0x04000614 RID: 1556
		Inherit = -1
	}
}
