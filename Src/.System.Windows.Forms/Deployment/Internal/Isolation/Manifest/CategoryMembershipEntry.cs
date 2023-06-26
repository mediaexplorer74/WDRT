using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200009A RID: 154
	[StructLayout(LayoutKind.Sequential)]
	internal class CategoryMembershipEntry
	{
		// Token: 0x04000291 RID: 657
		public IDefinitionIdentity Identity;

		// Token: 0x04000292 RID: 658
		public ISection SubcategoryMembership;
	}
}
