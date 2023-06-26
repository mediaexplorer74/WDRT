using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000025 RID: 37
	internal struct STORE_CATEGORY_INSTANCE
	{
		// Token: 0x04000113 RID: 275
		public IDefinitionAppId DefinitionAppId_Application;

		// Token: 0x04000114 RID: 276
		[MarshalAs(UnmanagedType.LPWStr)]
		public string XMLSnippet;
	}
}
