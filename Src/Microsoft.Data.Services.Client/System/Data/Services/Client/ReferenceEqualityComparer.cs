using System;
using System.Collections;

namespace System.Data.Services.Client
{
	// Token: 0x020000B1 RID: 177
	internal class ReferenceEqualityComparer : IEqualityComparer
	{
		// Token: 0x060005B8 RID: 1464 RVA: 0x00015C15 File Offset: 0x00013E15
		protected ReferenceEqualityComparer()
		{
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00015C1D File Offset: 0x00013E1D
		bool IEqualityComparer.Equals(object x, object y)
		{
			return object.ReferenceEquals(x, y);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00015C26 File Offset: 0x00013E26
		int IEqualityComparer.GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}
	}
}
