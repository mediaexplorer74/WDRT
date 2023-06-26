using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.SessionSwitch" /> event.</summary>
	// Token: 0x0200001B RID: 27
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class SessionSwitchEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SessionSwitchEventArgs" /> class using the specified session change event type identifer.</summary>
		/// <param name="reason">A <see cref="T:Microsoft.Win32.SessionSwitchReason" /> that indicates the type of session change event.</param>
		// Token: 0x060001C1 RID: 449 RVA: 0x0000D4E7 File Offset: 0x0000B6E7
		public SessionSwitchEventArgs(SessionSwitchReason reason)
		{
			this.reason = reason;
		}

		/// <summary>Gets an identifier that indicates the type of session change event.</summary>
		/// <returns>A <see cref="T:Microsoft.Win32.SessionSwitchReason" /> indicating the type of the session change event.</returns>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000D4F6 File Offset: 0x0000B6F6
		public SessionSwitchReason Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04000306 RID: 774
		private readonly SessionSwitchReason reason;
	}
}
