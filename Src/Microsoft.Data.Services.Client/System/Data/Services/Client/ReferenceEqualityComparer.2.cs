using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Data.Services.Client
{
	// Token: 0x020000B2 RID: 178
	internal sealed class ReferenceEqualityComparer<T> : ReferenceEqualityComparer, IEqualityComparer<T>
	{
		// Token: 0x060005BB RID: 1467 RVA: 0x00015C33 File Offset: 0x00013E33
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00015C3C File Offset: 0x00013E3C
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

		// Token: 0x060005BD RID: 1469 RVA: 0x00015C68 File Offset: 0x00013E68
		public bool Equals(T x, T y)
		{
			return object.ReferenceEquals(x, y);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00015C7B File Offset: 0x00013E7B
		public int GetHashCode(T obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x04000317 RID: 791
		private static ReferenceEqualityComparer<T> instance;
	}
}
