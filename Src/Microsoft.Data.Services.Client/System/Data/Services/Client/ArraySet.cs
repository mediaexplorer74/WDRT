using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data.Services.Client
{
	// Token: 0x020000E1 RID: 225
	[DebuggerDisplay("Count = {count}")]
	internal struct ArraySet<T> : IEnumerable<T>, IEnumerable where T : class
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x0001F78C File Offset: 0x0001D98C
		public ArraySet(int capacity)
		{
			this.items = new T[capacity];
			this.count = 0;
			this.version = 0;
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0001F7A8 File Offset: 0x0001D9A8
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x170001AF RID: 431
		public T this[int index]
		{
			get
			{
				return this.items[index];
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001F7C0 File Offset: 0x0001D9C0
		public bool Add(T item, Func<T, T, bool> equalityComparer)
		{
			if (equalityComparer != null && this.Contains(item, equalityComparer))
			{
				return false;
			}
			int num = this.count++;
			if (this.items == null || num == this.items.Length)
			{
				Array.Resize<T>(ref this.items, Math.Min(Math.Max(num, 16), 1073741823) * 2);
			}
			this.items[num] = item;
			this.version++;
			return true;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001F83C File Offset: 0x0001DA3C
		public bool Contains(T item, Func<T, T, bool> equalityComparer)
		{
			return 0 <= this.IndexOf(item, equalityComparer);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001F8FC File Offset: 0x0001DAFC
		public IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < this.count; i++)
			{
				yield return this.items[i];
			}
			yield break;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001F91D File Offset: 0x0001DB1D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001F925 File Offset: 0x0001DB25
		public int IndexOf(T item, Func<T, T, bool> comparer)
		{
			return this.IndexOf<T>(item, new Func<T, T>(ArraySet<T>.IdentitySelect), comparer);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001F93C File Offset: 0x0001DB3C
		public int IndexOf<K>(K item, Func<T, K> select, Func<K, K, bool> comparer)
		{
			T[] array = this.items;
			if (array != null)
			{
				int num = this.count;
				for (int i = 0; i < num; i++)
				{
					if (comparer(item, select(array[i])))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001F980 File Offset: 0x0001DB80
		public T Remove(T item, Func<T, T, bool> equalityComparer)
		{
			int num = this.IndexOf(item, equalityComparer);
			if (0 <= num)
			{
				item = this.items[num];
				this.RemoveAt(num);
				return item;
			}
			return default(T);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001F9BC File Offset: 0x0001DBBC
		public void RemoveAt(int index)
		{
			T[] array = this.items;
			int num = --this.count;
			array[index] = array[num];
			array[num] = default(T);
			if (num == 0 && 256 <= array.Length)
			{
				this.items = null;
			}
			else if (256 < array.Length && num < array.Length / 4)
			{
				Array.Resize<T>(ref this.items, array.Length / 2);
			}
			this.version++;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001FA48 File Offset: 0x0001DC48
		public void Sort<K>(Func<T, K> selector, Func<K, K, int> comparer)
		{
			if (this.items != null)
			{
				ArraySet<T>.SelectorComparer<K> selectorComparer;
				selectorComparer.Selector = selector;
				selectorComparer.Comparer = comparer;
				Array.Sort<T>(this.items, 0, this.count, selectorComparer);
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001FA85 File Offset: 0x0001DC85
		public void TrimToSize()
		{
			Array.Resize<T>(ref this.items, this.count);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001FA98 File Offset: 0x0001DC98
		private static T IdentitySelect(T arg)
		{
			return arg;
		}

		// Token: 0x0400048A RID: 1162
		private T[] items;

		// Token: 0x0400048B RID: 1163
		private int count;

		// Token: 0x0400048C RID: 1164
		private int version;

		// Token: 0x020000E2 RID: 226
		private struct SelectorComparer<K> : IComparer<T>
		{
			// Token: 0x0600076A RID: 1898 RVA: 0x0001FA9B File Offset: 0x0001DC9B
			int IComparer<T>.Compare(T x, T y)
			{
				return this.Comparer(this.Selector(x), this.Selector(y));
			}

			// Token: 0x0400048D RID: 1165
			internal Func<T, K> Selector;

			// Token: 0x0400048E RID: 1166
			internal Func<K, K, int> Comparer;
		}
	}
}
