using System;

namespace System.Windows.Forms
{
	/// <summary>Provides <see cref="T:System.Windows.Forms.RichTextBox" /> settings for Input Method Editor (IME) and Asian language support.</summary>
	// Token: 0x02000348 RID: 840
	[Flags]
	public enum RichTextBoxLanguageOptions
	{
		/// <summary>Specifies that the control automatically changes fonts when the user explicitly changes to a different keyboard layout.</summary>
		// Token: 0x0400212A RID: 8490
		AutoFont = 2,
		/// <summary>Specifies that font-bound font sizes are scaled from the insertion point size according to a script. For example, Asian fonts are slightly larger than Western fonts. This is the default.</summary>
		// Token: 0x0400212B RID: 8491
		AutoFontSizeAdjust = 16,
		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.RichTextBox" /> control automatically changes the keyboard layout when the user explicitly changes to a different font, or when the user explicitly changes the insertion point to a new location in the text.</summary>
		// Token: 0x0400212C RID: 8492
		AutoKeyboard = 1,
		/// <summary>Sets the control to dual-font mode. Used for Asian language text. The <see cref="T:System.Windows.Forms.RichTextBox" /> control uses an English font for ASCII text and an Asian font for Asian text.</summary>
		// Token: 0x0400212D RID: 8493
		DualFont = 128,
		/// <summary>Specifies how the client is notified during IME composition. A setting of 0 specifies that no EN_CHANGE or EN_SELCHANGE events occur during an undetermined state. Notification is sent when the final string comes in. This is the default. A setting of 1 specifies that EN_CHANGE and EN_SELCHANGE events occur during an undetermined state.</summary>
		// Token: 0x0400212E RID: 8494
		ImeAlwaysSendNotify = 8,
		/// <summary>Specifies how the control uses the composition string of an Input Method Editor (IME) if the user cancels it. If this flag is set, the control discards the composition string. If this flag is not set, the control uses the composition string as the result string.</summary>
		// Token: 0x0400212F RID: 8495
		ImeCancelComplete = 4,
		/// <summary>Specifies that user-interface default fonts be used. This option is turned off by default.</summary>
		// Token: 0x04002130 RID: 8496
		UIFonts = 32
	}
}
