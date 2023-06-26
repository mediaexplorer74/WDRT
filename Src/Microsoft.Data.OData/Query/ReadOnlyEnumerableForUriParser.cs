using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000087 RID: 135
	internal sealed class ReadOnlyEnumerableForUriParser<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000338 RID: 824 RVA: 0x0000B587 File Offset: 0x00009787
		internal ReadOnlyEnumerableForUriParser(IEnumerable<T> sourceEnumerable)
		{
			this.sourceEnumerable = sourceEnumerable;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000B596 File Offset: 0x00009796
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.sourceEnumerable.GetEnumerator();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000B5A3 File Offset: 0x000097A3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.sourceEnumerable.GetEnumerator();
		}

		// Token: 0x040000F7 RID: 247
		private IEnumerable<T> sourceEnumerable;
	}
}
