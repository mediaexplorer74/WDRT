using System;

namespace Microsoft.Data.Edm.Internal
{
	// Token: 0x020001C8 RID: 456
	internal static class TupleInternal
	{
		// Token: 0x06000AC3 RID: 2755 RVA: 0x0001FD7A File Offset: 0x0001DF7A
		public static TupleInternal<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new TupleInternal<T1, T2>(item1, item2);
		}
	}
}
