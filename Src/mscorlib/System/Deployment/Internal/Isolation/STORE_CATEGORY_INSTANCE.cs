using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200067A RID: 1658
	internal struct STORE_CATEGORY_INSTANCE
	{
		// Token: 0x040021FD RID: 8701
		public IDefinitionAppId DefinitionAppId_Application;

		// Token: 0x040021FE RID: 8702
		[MarshalAs(UnmanagedType.LPWStr)]
		public string XMLSnippet;
	}
}
