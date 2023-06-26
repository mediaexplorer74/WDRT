using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the state of the user interface.</summary>
	// Token: 0x02000425 RID: 1061
	[Flags]
	public enum UICues
	{
		/// <summary>Focus rectangles are displayed after the change.</summary>
		// Token: 0x040027B1 RID: 10161
		ShowFocus = 1,
		/// <summary>Keyboard cues are underlined after the change.</summary>
		// Token: 0x040027B2 RID: 10162
		ShowKeyboard = 2,
		/// <summary>Focus rectangles are displayed and keyboard cues are underlined after the change.</summary>
		// Token: 0x040027B3 RID: 10163
		Shown = 3,
		/// <summary>The state of the focus cues has changed.</summary>
		// Token: 0x040027B4 RID: 10164
		ChangeFocus = 4,
		/// <summary>The state of the keyboard cues has changed.</summary>
		// Token: 0x040027B5 RID: 10165
		ChangeKeyboard = 8,
		/// <summary>The state of the focus cues and keyboard cues has changed.</summary>
		// Token: 0x040027B6 RID: 10166
		Changed = 12,
		/// <summary>No change was made.</summary>
		// Token: 0x040027B7 RID: 10167
		None = 0
	}
}
