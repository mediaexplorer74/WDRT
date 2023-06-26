using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies constants defining which information to display.</summary>
	// Token: 0x020002FB RID: 763
	public enum MessageBoxIcon
	{
		/// <summary>The message box contains no symbols.</summary>
		// Token: 0x040013F5 RID: 5109
		None,
		/// <summary>The message box contains a symbol consisting of a white X in a circle with a red background.</summary>
		// Token: 0x040013F6 RID: 5110
		Hand = 16,
		/// <summary>The message box contains a symbol consisting of a question mark in a circle. The question mark message icon is no longer recommended because it does not clearly represent a specific type of message and because the phrasing of a message as a question could apply to any message type. In addition, users can confuse the question mark symbol with a help information symbol. Therefore, do not use this question mark symbol in your message boxes. The system continues to support its inclusion only for backward compatibility.</summary>
		// Token: 0x040013F7 RID: 5111
		Question = 32,
		/// <summary>The message box contains a symbol consisting of an exclamation point in a triangle with a yellow background.</summary>
		// Token: 0x040013F8 RID: 5112
		Exclamation = 48,
		/// <summary>The message box contains a symbol consisting of a lowercase letter i in a circle.</summary>
		// Token: 0x040013F9 RID: 5113
		Asterisk = 64,
		/// <summary>The message box contains a symbol consisting of white X in a circle with a red background.</summary>
		// Token: 0x040013FA RID: 5114
		Stop = 16,
		/// <summary>The message box contains a symbol consisting of white X in a circle with a red background.</summary>
		// Token: 0x040013FB RID: 5115
		Error = 16,
		/// <summary>The message box contains a symbol consisting of an exclamation point in a triangle with a yellow background.</summary>
		// Token: 0x040013FC RID: 5116
		Warning = 48,
		/// <summary>The message box contains a symbol consisting of a lowercase letter i in a circle.</summary>
		// Token: 0x040013FD RID: 5117
		Information = 64
	}
}
