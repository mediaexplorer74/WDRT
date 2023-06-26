using System;

namespace System.Threading
{
	// Token: 0x02000548 RID: 1352
	internal class SparselyPopulatedArrayFragment<T> where T : class
	{
		// Token: 0x06003F8A RID: 16266 RVA: 0x000EDAD6 File Offset: 0x000EBCD6
		internal SparselyPopulatedArrayFragment(int size)
			: this(size, null)
		{
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x000EDAE0 File Offset: 0x000EBCE0
		internal SparselyPopulatedArrayFragment(int size, SparselyPopulatedArrayFragment<T> prev)
		{
			this.m_elements = new T[size];
			this.m_freeCount = size;
			this.m_prev = prev;
		}

		// Token: 0x17000964 RID: 2404
		internal T this[int index]
		{
			get
			{
				return Volatile.Read<T>(ref this.m_elements[index]);
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06003F8D RID: 16269 RVA: 0x000EDB19 File Offset: 0x000EBD19
		internal int Length
		{
			get
			{
				return this.m_elements.Length;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06003F8E RID: 16270 RVA: 0x000EDB23 File Offset: 0x000EBD23
		internal SparselyPopulatedArrayFragment<T> Prev
		{
			get
			{
				return this.m_prev;
			}
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x000EDB30 File Offset: 0x000EBD30
		internal T SafeAtomicRemove(int index, T expectedElement)
		{
			T t = Interlocked.CompareExchange<T>(ref this.m_elements[index], default(T), expectedElement);
			if (t != null)
			{
				this.m_freeCount++;
			}
			return t;
		}

		// Token: 0x04001AC0 RID: 6848
		internal readonly T[] m_elements;

		// Token: 0x04001AC1 RID: 6849
		internal volatile int m_freeCount;

		// Token: 0x04001AC2 RID: 6850
		internal volatile SparselyPopulatedArrayFragment<T> m_next;

		// Token: 0x04001AC3 RID: 6851
		internal volatile SparselyPopulatedArrayFragment<T> m_prev;
	}
}
