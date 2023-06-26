using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200004D RID: 77
	internal interface IWrappedDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060004A6 RID: 1190
		[Nullable(1)]
		object UnderlyingDictionary
		{
			[NullableContext(1)]
			get;
		}
	}
}
