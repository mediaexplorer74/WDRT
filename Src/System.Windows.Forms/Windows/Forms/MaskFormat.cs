using System;

namespace System.Windows.Forms
{
	/// <summary>Defines how to format the text inside of a <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
	// Token: 0x020002E6 RID: 742
	public enum MaskFormat
	{
		/// <summary>Return text input by the user as well as any instances of the prompt character.</summary>
		// Token: 0x04001384 RID: 4996
		IncludePrompt = 1,
		/// <summary>Return text input by the user as well as any literal characters defined in the mask.</summary>
		// Token: 0x04001385 RID: 4997
		IncludeLiterals,
		/// <summary>Return text input by the user as well as any literal characters defined in the mask and any instances of the prompt character.</summary>
		// Token: 0x04001386 RID: 4998
		IncludePromptAndLiterals,
		/// <summary>Return only text input by the user.</summary>
		// Token: 0x04001387 RID: 4999
		ExcludePromptAndLiterals = 0
	}
}
