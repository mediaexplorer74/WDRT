using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004EB RID: 1259
	[FriendAccessAllowed]
	[SecurityCritical]
	internal class WinRTSynchronizationContextFactoryBase
	{
		// Token: 0x06003BA7 RID: 15271 RVA: 0x000E3D08 File Offset: 0x000E1F08
		[SecurityCritical]
		public virtual SynchronizationContext Create(object coreDispatcher)
		{
			return null;
		}
	}
}
