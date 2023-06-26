using System;
using System.Collections;

namespace Microsoft.Data.OData
{
	// Token: 0x02000245 RID: 581
	internal class ReadOnlyEnumerable : IEnumerable
	{
		// Token: 0x060012AA RID: 4778 RVA: 0x0004672D File Offset: 0x0004492D
		internal ReadOnlyEnumerable(IEnumerable sourceEnumerable)
		{
			this.sourceEnumerable = sourceEnumerable;
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0004673C File Offset: 0x0004493C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.sourceEnumerable.GetEnumerator();
		}

		// Token: 0x040006B3 RID: 1715
		private readonly IEnumerable sourceEnumerable;
	}
}
