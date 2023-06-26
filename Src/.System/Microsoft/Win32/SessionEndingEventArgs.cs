using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.SessionEnding" /> event.</summary>
	// Token: 0x02000018 RID: 24
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class SessionEndingEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SessionEndingEventArgs" /> class using the specified value indicating the type of session close event that is occurring.</summary>
		/// <param name="reason">One of the <see cref="T:Microsoft.Win32.SessionEndReasons" /> that specifies how the session ends.</param>
		// Token: 0x060001B9 RID: 441 RVA: 0x0000D4BF File Offset: 0x0000B6BF
		public SessionEndingEventArgs(SessionEndReasons reason)
		{
			this.reason = reason;
		}

		/// <summary>Gets or sets a value indicating whether to cancel the user request to end the session.</summary>
		/// <returns>
		///   <see langword="true" /> to cancel the user request to end the session; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000D4CE File Offset: 0x0000B6CE
		// (set) Token: 0x060001BB RID: 443 RVA: 0x0000D4D6 File Offset: 0x0000B6D6
		public bool Cancel
		{
			get
			{
				return this.cancel;
			}
			set
			{
				this.cancel = value;
			}
		}

		/// <summary>Gets the reason the session is ending.</summary>
		/// <returns>One of the <see cref="T:Microsoft.Win32.SessionEndReasons" /> values that specifies how the session is ending.</returns>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000D4DF File Offset: 0x0000B6DF
		public SessionEndReasons Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04000301 RID: 769
		private bool cancel;

		// Token: 0x04000302 RID: 770
		private readonly SessionEndReasons reason;
	}
}
