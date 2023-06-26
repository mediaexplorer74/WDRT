using System;

namespace System.Collections.Generic
{
	// Token: 0x020004BF RID: 1215
	[Serializable]
	internal class GenericEqualityComparer<T> : EqualityComparer<T> where T : IEquatable<T>
	{
		// Token: 0x06003A91 RID: 14993 RVA: 0x000E080C File Offset: 0x000DEA0C
		public override bool Equals(T x, T y)
		{
			if (x != null)
			{
				return y != null && x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x000E083A File Offset: 0x000DEA3A
		public override int GetHashCode(T obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x000E0854 File Offset: 0x000DEA54
		internal override int IndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex + count;
			if (value == null)
			{
				for (int i = startIndex; i < num; i++)
				{
					if (array[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j < num; j++)
				{
					if (array[j] != null && array[j].Equals(value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x000E08C0 File Offset: 0x000DEAC0
		internal override int LastIndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			if (value == null)
			{
				for (int i = startIndex; i >= num; i--)
				{
					if (array[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j >= num; j--)
				{
					if (array[j] != null && array[j].Equals(value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x000E0930 File Offset: 0x000DEB30
		public override bool Equals(object obj)
		{
			GenericEqualityComparer<T> genericEqualityComparer = obj as GenericEqualityComparer<T>;
			return genericEqualityComparer != null;
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x000E0948 File Offset: 0x000DEB48
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
