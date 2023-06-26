using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.SessionEnded" /> event.</summary>
	// Token: 0x02000016 RID: 22
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class SessionEndedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SessionEndedEventArgs" /> class.</summary>
		/// <param name="reason">One of the <see cref="T:Microsoft.Win32.SessionEndReasons" /> values indicating how the session ended.</param>
		// Token: 0x060001B3 RID: 435 RVA: 0x0000D4A8 File Offset: 0x0000B6A8
		public SessionEndedEventArgs(SessionEndReasons reason)
		{
			this.reason = reason;
		}

		/// <summary>Gets an identifier that indicates how the session ended.</summary>
		/// <returns>One of the <see cref="T:Microsoft.Win32.SessionEndReasons" /> values that indicates how the session ended.</returns>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000D4B7 File Offset: 0x0000B6B7
		public SessionEndReasons Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04000300 RID: 768
		private readonly SessionEndReasons reason;
	}
}
