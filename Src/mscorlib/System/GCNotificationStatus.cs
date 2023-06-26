using System;

namespace System
{
	/// <summary>Provides information about the current registration for notification of the next full garbage collection.</summary>
	// Token: 0x020000E8 RID: 232
	[Serializable]
	public enum GCNotificationStatus
	{
		/// <summary>The notification was successful and the registration was not canceled.</summary>
		// Token: 0x04000587 RID: 1415
		Succeeded,
		/// <summary>The notification failed for any reason.</summary>
		// Token: 0x04000588 RID: 1416
		Failed,
		/// <summary>The current registration was canceled by the user.</summary>
		// Token: 0x04000589 RID: 1417
		Canceled,
		/// <summary>The time specified by the <paramref name="millisecondsTimeout" /> parameter for either <see cref="M:System.GC.WaitForFullGCApproach(System.Int32)" /> or <see cref="M:System.GC.WaitForFullGCComplete(System.Int32)" /> has elapsed.</summary>
		// Token: 0x0400058A RID: 1418
		Timeout,
		/// <summary>This result can be caused by the following: there is no current registration for a garbage collection notification, concurrent garbage collection is enabled, or the time specified for the <paramref name="millisecondsTimeout" /> parameter has expired and no garbage collection notification was obtained. (See the &lt;gcConcurrent&gt; runtime setting for information about how to disable concurrent garbage collection.)</summary>
		// Token: 0x0400058B RID: 1419
		NotApplicable
	}
}
