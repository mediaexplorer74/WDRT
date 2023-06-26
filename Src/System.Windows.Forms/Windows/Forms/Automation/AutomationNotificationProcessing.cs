using System;

namespace System.Windows.Forms.Automation
{
	/// <summary>Specifies the order in which to process a notification.</summary>
	// Token: 0x020004F6 RID: 1270
	public enum AutomationNotificationProcessing
	{
		/// <summary>These notifications should be presented to the user as soon as possible. All of the notifications from this source should be delivered to the user.IMPORTANT: Limit the use of this option, since delivery of all notifications to the user can cause a overwhelming flood of information.</summary>
		// Token: 0x0400364F RID: 13903
		ImportantAll,
		/// <summary>These notifications should be presented to the user as soon as possible. The most recent notifications from this source should be delivered to the user because the most recent notification supersedes all other notifications.</summary>
		// Token: 0x04003650 RID: 13904
		ImportantMostRecent,
		/// <summary>These notifications should be presented to the user when possible. All the notifications from this source should be delivered to the user.</summary>
		// Token: 0x04003651 RID: 13905
		All,
		/// <summary>These notifications should be presented to the user when possible. Interrupt the current notification for this one.</summary>
		// Token: 0x04003652 RID: 13906
		MostRecent,
		/// <summary>These notifications should be presented to the user when possible. don't interrupt the current notification for this one. If new notifications come in from the same source while the current notification is being presented, keep the most recent and ignore the rest until the current processing is completed. Then use the most recent message as the current message.</summary>
		// Token: 0x04003653 RID: 13907
		CurrentThenMostRecent
	}
}
