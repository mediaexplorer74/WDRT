using System;

namespace System.Collections.Generic
{
	// Token: 0x020004C1 RID: 1217
	[Serializable]
	internal class ObjectEqualityComparer<T> : EqualityComparer<T>
	{
		// Token: 0x06003A9F RID: 15007 RVA: 0x000E0ACE File Offset: 0x000DECCE
		public override bool Equals(T x, T y)
		{
			if (x != null)
			{
				return y != null && x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x000E0B01 File Offset: 0x000DED01
		public override int GetHashCode(T obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x000E0B1C File Offset: 0x000DED1C
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

		// Token: 0x06003AA2 RID: 15010 RVA: 0x000E0B90 File Offset: 0x000DED90
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

		// Token: 0x06003AA3 RID: 15011 RVA: 0x000E0C04 File Offset: 0x000DEE04
		public override bool Equals(object obj)
		{
			ObjectEqualityComparer<T> objectEqualityComparer = obj as ObjectEqualityComparer<T>;
			return objectEqualityComparer != null;
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x000E0C1C File Offset: 0x000DEE1C
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
