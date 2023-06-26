using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the button style within a toolbar.</summary>
	// Token: 0x020003AC RID: 940
	public enum ToolBarButtonStyle
	{
		/// <summary>A standard, three-dimensional button.</summary>
		// Token: 0x04002421 RID: 9249
		PushButton = 1,
		/// <summary>A toggle button that appears sunken when clicked and retains the sunken appearance until clicked again.</summary>
		// Token: 0x04002422 RID: 9250
		ToggleButton,
		/// <summary>A space or line between toolbar buttons. The appearance depends on the value of the <see cref="P:System.Windows.Forms.ToolBar.Appearance" /> property.</summary>
		// Token: 0x04002423 RID: 9251
		Separator,
		/// <summary>A drop-down control that displays a menu or other window when clicked.</summary>
		// Token: 0x04002424 RID: 9252
		DropDownButton
	}
}
