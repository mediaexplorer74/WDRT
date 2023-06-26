using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000022 RID: 34
	public abstract class JsonNameTable
	{
		// Token: 0x0600009F RID: 159
		[NullableContext(1)]
		[return: Nullable(2)]
		public abstract string Get(char[] key, int start, int length);
	}
}
