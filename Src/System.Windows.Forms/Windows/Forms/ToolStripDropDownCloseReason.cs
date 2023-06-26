using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the reason that a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed.</summary>
	// Token: 0x020003B8 RID: 952
	public enum ToolStripDropDownCloseReason
	{
		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed because another application has received the focus.</summary>
		// Token: 0x040024CE RID: 9422
		AppFocusChange,
		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed because an application was launched.</summary>
		// Token: 0x040024CF RID: 9423
		AppClicked,
		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed because one of its items was clicked.</summary>
		// Token: 0x040024D0 RID: 9424
		ItemClicked,
		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed because of keyboard activity, such as the ESC key being pressed.</summary>
		// Token: 0x040024D1 RID: 9425
		Keyboard,
		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed because the <see cref="M:System.Windows.Forms.ToolStripDropDown.Close" /> method was called.</summary>
		// Token: 0x040024D2 RID: 9426
		CloseCalled
	}
}
