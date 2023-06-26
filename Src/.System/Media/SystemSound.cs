using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Media
{
	/// <summary>Represents a system sound type.</summary>
	// Token: 0x020003A6 RID: 934
	[HostProtection(SecurityAction.LinkDemand, UI = true)]
	public class SystemSound
	{
		// Token: 0x060022D2 RID: 8914 RVA: 0x000A5C74 File Offset: 0x000A3E74
		internal SystemSound(int soundType)
		{
			this.soundType = soundType;
		}

		/// <summary>Plays the system sound type.</summary>
		// Token: 0x060022D3 RID: 8915 RVA: 0x000A5C84 File Offset: 0x000A3E84
		public void Play()
		{
			IntSecurity.UnmanagedCode.Assert();
			try
			{
				SystemSound.SafeNativeMethods.MessageBeep(this.soundType);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
		}

		// Token: 0x04001F9F RID: 8095
		private int soundType;

		// Token: 0x020007E5 RID: 2021
		private class SafeNativeMethods
		{
			// Token: 0x060043B9 RID: 17337 RVA: 0x0011CE71 File Offset: 0x0011B071
			private SafeNativeMethods()
			{
			}

			// Token: 0x060043BA RID: 17338
			[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
			internal static extern bool MessageBeep(int type);
		}
	}
}
