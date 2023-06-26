using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.OData
{
	// Token: 0x02000122 RID: 290
	internal sealed class HttpHeaderValue : Dictionary<string, HttpHeaderValueElement>
	{
		// Token: 0x060007C5 RID: 1989 RVA: 0x00019EC5 File Offset: 0x000180C5
		internal HttpHeaderValue()
			: base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00019EDC File Offset: 0x000180DC
		public override string ToString()
		{
			if (base.Count != 0)
			{
				return string.Join(",", base.Values.Select((HttpHeaderValueElement element) => element.ToString()).ToArray<string>());
			}
			return null;
		}
	}
}
