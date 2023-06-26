using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008DD RID: 2269
	[AttributeUsage(AttributeTargets.All)]
	[ComVisible(false)]
	internal sealed class DecoratedNameAttribute : Attribute
	{
		// Token: 0x06005DEE RID: 24046 RVA: 0x0014B0FF File Offset: 0x001492FF
		public DecoratedNameAttribute(string decoratedName)
		{
		}
	}
}
