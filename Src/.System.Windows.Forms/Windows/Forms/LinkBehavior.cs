using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the behaviors of a link in a <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
	// Token: 0x020002C0 RID: 704
	public enum LinkBehavior
	{
		/// <summary>The behavior of this setting depends on the options set using the Internet Options dialog box in Control Panel or Internet Explorer.</summary>
		// Token: 0x04001224 RID: 4644
		SystemDefault,
		/// <summary>The link always displays with underlined text.</summary>
		// Token: 0x04001225 RID: 4645
		AlwaysUnderline,
		/// <summary>The link displays underlined text only when the mouse is hovered over the link text.</summary>
		// Token: 0x04001226 RID: 4646
		HoverUnderline,
		/// <summary>The link text is never underlined. The link can still be distinguished from other text by use of the <see cref="P:System.Windows.Forms.LinkLabel.LinkColor" /> property of the <see cref="T:System.Windows.Forms.LinkLabel" /> control.</summary>
		// Token: 0x04001227 RID: 4647
		NeverUnderline
	}
}
