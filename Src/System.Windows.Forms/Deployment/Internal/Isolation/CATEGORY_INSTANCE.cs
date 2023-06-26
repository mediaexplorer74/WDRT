using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000028 RID: 40
	internal struct CATEGORY_INSTANCE
	{
		// Token: 0x04000117 RID: 279
		public IDefinitionAppId DefinitionAppId_Application;

		// Token: 0x04000118 RID: 280
		[MarshalAs(UnmanagedType.LPWStr)]
		public string XMLSnippet;
	}
}
