using System;

namespace System.Collections.Generic
{
	// Token: 0x020004BA RID: 1210
	[Serializable]
	internal class NullableComparer<T> : Comparer<T?> where T : struct, IComparable<T>
	{
		// Token: 0x06003A47 RID: 14919 RVA: 0x000DF5C2 File Offset: 0x000DD7C2
		public override int Compare(T? x, T? y)
		{
			if (x != null)
			{
				if (y != null)
				{
					return x.value.CompareTo(y.value);
				}
				return 1;
			}
			else
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x06003A48 RID: 14920 RVA: 0x000DF600 File Offset: 0x000DD800
		public override bool Equals(object obj)
		{
			NullableComparer<T> nullableComparer = obj as NullableComparer<T>;
			return nullableComparer != null;
		}

		// Token: 0x06003A49 RID: 14921 RVA: 0x000DF618 File Offset: 0x000DD818
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
