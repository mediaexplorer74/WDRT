using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies the visual state of a text box that is drawn with visual styles.</summary>
	// Token: 0x02000460 RID: 1120
	public enum TextBoxState
	{
		/// <summary>The text box appears normal.</summary>
		// Token: 0x040032A6 RID: 12966
		Normal = 1,
		/// <summary>The text box appears hot.</summary>
		// Token: 0x040032A7 RID: 12967
		Hot,
		/// <summary>The text box appears selected.</summary>
		// Token: 0x040032A8 RID: 12968
		Selected,
		/// <summary>The text box appears disabled.</summary>
		// Token: 0x040032A9 RID: 12969
		Disabled,
		/// <summary>The text box appears read-only.</summary>
		// Token: 0x040032AA RID: 12970
		Readonly = 6,
		/// <summary>The text box appears in assist mode.</summary>
		// Token: 0x040032AB RID: 12971
		Assist
	}
}
