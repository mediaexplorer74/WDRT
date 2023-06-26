using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Resources
{
	// Token: 0x02000393 RID: 915
	[FriendAccessAllowed]
	[SecurityCritical]
	internal class WindowsRuntimeResourceManagerBase
	{
		// Token: 0x06002D2C RID: 11564 RVA: 0x000ABADD File Offset: 0x000A9CDD
		[SecurityCritical]
		public virtual bool Initialize(string libpath, string reswFilename, out PRIExceptionInfo exceptionInfo)
		{
			exceptionInfo = null;
			return false;
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x000ABAE3 File Offset: 0x000A9CE3
		[SecurityCritical]
		public virtual string GetString(string stringName, string startingCulture, string neutralResourcesCulture)
		{
			return null;
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06002D2E RID: 11566 RVA: 0x000ABAE6 File Offset: 0x000A9CE6
		public virtual CultureInfo GlobalResourceContextBestFitCultureInfo
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x000ABAE9 File Offset: 0x000A9CE9
		[SecurityCritical]
		public virtual bool SetGlobalResourceContextDefaultCulture(CultureInfo ci)
		{
			return false;
		}
	}
}
