using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006E1 RID: 1761
	[StructLayout(LayoutKind.Sequential)]
	internal class CategoryMembershipEntry
	{
		// Token: 0x04002343 RID: 9027
		public IDefinitionIdentity Identity;

		// Token: 0x04002344 RID: 9028
		public ISection SubcategoryMembership;
	}
}
