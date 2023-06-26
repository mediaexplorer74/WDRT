using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000056 RID: 86
	[NullableContext(1)]
	[Nullable(0)]
	internal class EnumInfo
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x0001527B File Offset: 0x0001347B
		public EnumInfo(bool isFlags, ulong[] values, string[] names, string[] resolvedNames)
		{
			this.IsFlags = isFlags;
			this.Values = values;
			this.Names = names;
			this.ResolvedNames = resolvedNames;
		}

		// Token: 0x040001C2 RID: 450
		public readonly bool IsFlags;

		// Token: 0x040001C3 RID: 451
		public readonly ulong[] Values;

		// Token: 0x040001C4 RID: 452
		public readonly string[] Names;

		// Token: 0x040001C5 RID: 453
		public readonly string[] ResolvedNames;
	}
}
