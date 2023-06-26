using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the kind of action to take if a match is found when combining menu items on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	// Token: 0x020002F6 RID: 758
	public enum MergeAction
	{
		/// <summary>Appends the item to the end of the collection, ignoring match results.</summary>
		// Token: 0x040013D6 RID: 5078
		Append,
		/// <summary>Inserts the item to the target's collection immediately preceding the matched item. A match of the end of the list results in the item being appended to the list. If there is no match or the match is at the beginning of the list, the item is inserted at the beginning of the collection.</summary>
		// Token: 0x040013D7 RID: 5079
		Insert,
		/// <summary>Replaces the matched item with the source item. The original item's drop-down items do not become children of the incoming item.</summary>
		// Token: 0x040013D8 RID: 5080
		Replace,
		/// <summary>Removes the matched item.</summary>
		// Token: 0x040013D9 RID: 5081
		Remove,
		/// <summary>A match is required, but no action is taken. Use this for tree creation and successful access to nested layouts.</summary>
		// Token: 0x040013DA RID: 5082
		MatchOnly
	}
}
