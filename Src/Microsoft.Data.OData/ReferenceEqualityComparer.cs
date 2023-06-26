using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Data.OData
{
	// Token: 0x0200027B RID: 635
	internal sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T : class
	{
		// Token: 0x060014F7 RID: 5367 RVA: 0x0004E2C8 File Offset: 0x0004C4C8
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x0004E2D0 File Offset: 0x0004C4D0
		internal static ReferenceEqualityComparer<T> Instance
		{
			get
			{
				if (ReferenceEqualityComparer<T>.instance == null)
				{
					ReferenceEqualityComparer<T> referenceEqualityComparer = new ReferenceEqualityComparer<T>();
					Interlocked.CompareExchange<ReferenceEqualityComparer<T>>(ref ReferenceEqualityComparer<T>.instance, referenceEqualityComparer, null);
				}
				return ReferenceEqualityComparer<T>.instance;
			}
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0004E2FC File Offset: 0x0004C4FC
		public bool Equals(T x, T y)
		{
			return object.ReferenceEquals(x, y);
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x0004E30F File Offset: 0x0004C50F
		public int GetHashCode(T obj)
		{
			if (obj != null)
			{
				return obj.GetHashCode();
			}
			return 0;
		}

		// Token: 0x0400079E RID: 1950
		private static ReferenceEqualityComparer<T> instance;
	}
}
