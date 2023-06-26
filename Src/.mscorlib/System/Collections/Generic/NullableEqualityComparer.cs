using System;

namespace System.Collections.Generic
{
	// Token: 0x020004C0 RID: 1216
	[Serializable]
	internal class NullableEqualityComparer<T> : EqualityComparer<T?> where T : struct, IEquatable<T>
	{
		// Token: 0x06003A98 RID: 15000 RVA: 0x000E0962 File Offset: 0x000DEB62
		public override bool Equals(T? x, T? y)
		{
			if (x != null)
			{
				return y != null && x.value.Equals(y.value);
			}
			return y == null;
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x000E099D File Offset: 0x000DEB9D
		public override int GetHashCode(T? obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x000E09AC File Offset: 0x000DEBAC
		internal override int IndexOf(T?[] array, T? value, int startIndex, int count)
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
					if (array[j] != null && array[j].value.Equals(value.value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x000E0A24 File Offset: 0x000DEC24
		internal override int LastIndexOf(T?[] array, T? value, int startIndex, int count)
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
					if (array[j] != null && array[j].value.Equals(value.value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x000E0A9C File Offset: 0x000DEC9C
		public override bool Equals(object obj)
		{
			NullableEqualityComparer<T> nullableEqualityComparer = obj as NullableEqualityComparer<T>;
			return nullableEqualityComparer != null;
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x000E0AB4 File Offset: 0x000DECB4
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
