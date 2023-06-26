using System;
using System.Reflection;

namespace System.Resources
{
	// Token: 0x020000F2 RID: 242
	internal interface IAliasResolver
	{
		// Token: 0x060003B7 RID: 951
		AssemblyName ResolveAlias(string alias);

		// Token: 0x060003B8 RID: 952
		void PushAlias(string alias, AssemblyName name);
	}
}
