using System;
using System.Runtime.InteropServices;

namespace System.Net.Mail
{
	// Token: 0x02000261 RID: 609
	[StructLayout(LayoutKind.Sequential)]
	internal class _METADATA_HANDLE_INFO
	{
		// Token: 0x060016F0 RID: 5872 RVA: 0x0007610A File Offset: 0x0007430A
		private _METADATA_HANDLE_INFO()
		{
			this.dwMDPermissions = 0;
			this.dwMDSystemChangeNumber = 0;
		}

		// Token: 0x04001781 RID: 6017
		internal int dwMDPermissions;

		// Token: 0x04001782 RID: 6018
		internal int dwMDSystemChangeNumber;
	}
}
