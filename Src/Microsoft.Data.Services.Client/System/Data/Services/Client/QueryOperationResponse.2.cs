using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Services.Client
{
	// Token: 0x0200010F RID: 271
	public sealed class QueryOperationResponse<T> : QueryOperationResponse, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060008E3 RID: 2275 RVA: 0x00024B7C File Offset: 0x00022D7C
		internal QueryOperationResponse(HeaderCollection headers, DataServiceRequest query, MaterializeAtom results)
			: base(headers, query, results)
		{
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x00024B87 File Offset: 0x00022D87
		public override long TotalCount
		{
			get
			{
				if (base.Results != null && base.Results.IsCountable)
				{
					return base.Results.CountValue();
				}
				throw new InvalidOperationException(Strings.MaterializeFromAtom_CountNotPresent);
			}
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00024BB4 File Offset: 0x00022DB4
		public new DataServiceQueryContinuation<T> GetContinuation()
		{
			return (DataServiceQueryContinuation<T>)base.GetContinuation();
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00024BD3 File Offset: 0x00022DD3
		public new IEnumerator<T> GetEnumerator()
		{
			return base.GetEnumeratorHelper<IEnumerator<T>>(() => base.Results.Cast<T>().GetEnumerator());
		}
	}
}
