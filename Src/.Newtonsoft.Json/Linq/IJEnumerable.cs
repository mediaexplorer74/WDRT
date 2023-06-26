using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000B8 RID: 184
	[NullableContext(1)]
	public interface IJEnumerable<[Nullable(0)] out T> : IEnumerable<T>, IEnumerable where T : JToken
	{
		// Token: 0x170001BA RID: 442
		IJEnumerable<JToken> this[object key] { get; }
	}
}
