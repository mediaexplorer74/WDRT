using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.PowerModeChanged" /> event.</summary>
	// Token: 0x02000012 RID: 18
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class PowerModeChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.PowerModeChangedEventArgs" /> class using the specified power mode event type.</summary>
		/// <param name="mode">One of the <see cref="T:Microsoft.Win32.PowerModes" /> values that represents the type of power mode event.</param>
		// Token: 0x06000199 RID: 409 RVA: 0x0000D441 File Offset: 0x0000B641
		public PowerModeChangedEventArgs(PowerModes mode)
		{
			this.mode = mode;
		}

		/// <summary>Gets an identifier that indicates the type of the power mode event that has occurred.</summary>
		/// <returns>One of the <see cref="T:Microsoft.Win32.PowerModes" /> values.</returns>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000D450 File Offset: 0x0000B650
		public PowerModes Mode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x040002F1 RID: 753
		private readonly PowerModes mode;
	}
}
