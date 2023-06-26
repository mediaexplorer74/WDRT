using System;

namespace System.Collections.Generic
{
	// Token: 0x020004B9 RID: 1209
	[Serializable]
	internal class GenericComparer<T> : Comparer<T> where T : IComparable<T>
	{
		// Token: 0x06003A43 RID: 14915 RVA: 0x000DF55F File Offset: 0x000DD75F
		public override int Compare(T x, T y)
		{
			if (x != null)
			{
				if (y != null)
				{
					return x.CompareTo(y);
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

		// Token: 0x06003A44 RID: 14916 RVA: 0x000DF590 File Offset: 0x000DD790
		public override bool Equals(object obj)
		{
			GenericComparer<T> genericComparer = obj as GenericComparer<T>;
			return genericComparer != null;
		}

		// Token: 0x06003A45 RID: 14917 RVA: 0x000DF5A8 File Offset: 0x000DD7A8
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
