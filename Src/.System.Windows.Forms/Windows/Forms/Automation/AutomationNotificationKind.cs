using System;

namespace System.Windows.Forms.Automation
{
	/// <summary>Indicates the type of notification when raising the UI automation notification event.</summary>
	// Token: 0x020004F5 RID: 1269
	public enum AutomationNotificationKind
	{
		/// <summary>Something that should be presented to the user was added to the current element.</summary>
		// Token: 0x04003649 RID: 13897
		ItemAdded,
		/// <summary>Something that should be presented to the user was removed from the current element.</summary>
		// Token: 0x0400364A RID: 13898
		ItemRemoved,
		/// <summary>The current element has a notification that an action was completed.</summary>
		// Token: 0x0400364B RID: 13899
		ActionCompleted,
		/// <summary>The current element has a notification that an action was abandoned.</summary>
		// Token: 0x0400364C RID: 13900
		ActionAborted,
		/// <summary>The current element has a notification that is not due to an addition or removal of something, or to a completed or aborted action.</summary>
		// Token: 0x0400364D RID: 13901
		Other
	}
}
