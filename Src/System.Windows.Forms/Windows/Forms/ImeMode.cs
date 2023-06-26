using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies a value that determines the Input Method Editor (IME) status of an object when the object is selected.</summary>
	// Token: 0x02000296 RID: 662
	[ComVisible(true)]
	public enum ImeMode
	{
		/// <summary>Inherits the IME mode of the parent control.</summary>
		// Token: 0x04001100 RID: 4352
		Inherit = -1,
		/// <summary>None (Default).</summary>
		// Token: 0x04001101 RID: 4353
		NoControl,
		/// <summary>The IME is on. This value indicates that the IME is on and characters specific to Chinese or Japanese can be entered. This setting is valid for Japanese, Simplified Chinese, and Traditional Chinese IME only.</summary>
		// Token: 0x04001102 RID: 4354
		On,
		/// <summary>The IME is off. This mode indicates that the IME is off, meaning that the object behaves the same as English entry mode. This setting is valid for Japanese, Simplified Chinese, and Traditional Chinese IME only.</summary>
		// Token: 0x04001103 RID: 4355
		Off,
		/// <summary>The IME is disabled. With this setting, the users cannot turn the IME on from the keyboard, and the IME floating window is hidden.</summary>
		// Token: 0x04001104 RID: 4356
		Disable,
		/// <summary>Hiragana DBC. This setting is valid for the Japanese IME only.</summary>
		// Token: 0x04001105 RID: 4357
		Hiragana,
		/// <summary>Katakana DBC. This setting is valid for the Japanese IME only.</summary>
		// Token: 0x04001106 RID: 4358
		Katakana,
		/// <summary>Katakana SBC. This setting is valid for the Japanese IME only.</summary>
		// Token: 0x04001107 RID: 4359
		KatakanaHalf,
		/// <summary>Alphanumeric double-byte characters. This setting is valid for Korean and Japanese IME only.</summary>
		// Token: 0x04001108 RID: 4360
		AlphaFull,
		/// <summary>Alphanumeric single-byte characters(SBC). This setting is valid for Korean and Japanese IME only.</summary>
		// Token: 0x04001109 RID: 4361
		Alpha,
		/// <summary>Hangul DBC. This setting is valid for the Korean IME only.</summary>
		// Token: 0x0400110A RID: 4362
		HangulFull,
		/// <summary>Hangul SBC. This setting is valid for the Korean IME only.</summary>
		// Token: 0x0400110B RID: 4363
		Hangul,
		/// <summary>IME closed. This setting is valid for Chinese IME only.</summary>
		// Token: 0x0400110C RID: 4364
		Close,
		/// <summary>IME on HalfShape. This setting is valid for Chinese IME only.</summary>
		// Token: 0x0400110D RID: 4365
		OnHalf
	}
}
