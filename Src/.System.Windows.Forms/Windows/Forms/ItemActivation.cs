using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the user action that is required to activate items in a list view control and the feedback that is given as the user moves the mouse pointer over an item.</summary>
	// Token: 0x020002A4 RID: 676
	public enum ItemActivation
	{
		/// <summary>The user must double-click to activate items. No feedback is given as the user moves the mouse pointer over an item.</summary>
		// Token: 0x04001117 RID: 4375
		Standard,
		/// <summary>The user must single-click to activate items. The cursor changes to a hand pointer cursor, and the item text changes color as the user moves the mouse pointer over the item.</summary>
		// Token: 0x04001118 RID: 4376
		OneClick,
		/// <summary>The user must click an item twice to activate it. This is different from the standard double-click because the two clicks can have any duration between them. The item text changes color as the user moves the mouse pointer over the item.</summary>
		// Token: 0x04001119 RID: 4377
		TwoClick
	}
}
