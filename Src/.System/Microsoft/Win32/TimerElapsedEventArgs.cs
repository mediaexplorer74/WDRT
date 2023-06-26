using System;
using System.Security.Permissions;

namespace Microsoft.Win32
{
	/// <summary>Provides data for the <see cref="E:Microsoft.Win32.SystemEvents.TimerElapsed" /> event.</summary>
	// Token: 0x0200001F RID: 31
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class TimerElapsedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.TimerElapsedEventArgs" /> class.</summary>
		/// <param name="timerId">The ID number for the timer.</param>
		// Token: 0x06000212 RID: 530 RVA: 0x0000F273 File Offset: 0x0000D473
		public TimerElapsedEventArgs(IntPtr timerId)
		{
			this.timerId = timerId;
		}

		/// <summary>Gets the ID number for the timer.</summary>
		/// <returns>The ID number for the timer.</returns>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000F282 File Offset: 0x0000D482
		public IntPtr TimerId
		{
			get
			{
				return this.timerId;
			}
		}

		// Token: 0x0400033A RID: 826
		private readonly IntPtr timerId;
	}
}
