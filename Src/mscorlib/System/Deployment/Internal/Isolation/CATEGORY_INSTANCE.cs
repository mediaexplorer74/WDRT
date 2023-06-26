using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200067D RID: 1661
	internal struct CATEGORY_INSTANCE
	{
		// Token: 0x04002201 RID: 8705
		public IDefinitionAppId DefinitionAppId_Application;

		// Token: 0x04002202 RID: 8706
		[MarshalAs(UnmanagedType.LPWStr)]
		public string XMLSnippet;
	}
}
