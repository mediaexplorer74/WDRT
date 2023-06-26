using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009CB RID: 2507
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	internal sealed class DictionaryKeyCollection<TKey, TValue> : ICollection<TKey>, IEnumerable<TKey>, IEnumerable
	{
		// Token: 0x060063F6 RID: 25590 RVA: 0x00156406 File Offset: 0x00154606
		public DictionaryKeyCollection(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
		}

		// Token: 0x060063F7 RID: 25591 RVA: 0x00156424 File Offset: 0x00154624
		public void CopyTo(TKey[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (array.Length <= index && this.Count > 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_IndexOutOfRangeException"));
			}
			if (array.Length - index < this.dictionary.Count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InsufficientSpaceToCopyCollection"));
			}
			int num = index;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this.dictionary)
			{
				array[num++] = keyValuePair.Key;
			}
		}

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x060063F8 RID: 25592 RVA: 0x001564DC File Offset: 0x001546DC
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x060063F9 RID: 25593 RVA: 0x001564E9 File Offset: 0x001546E9
		bool ICollection<TKey>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060063FA RID: 25594 RVA: 0x001564EC File Offset: 0x001546EC
		void ICollection<TKey>.Add(TKey item)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_KeyCollectionSet"));
		}

		// Token: 0x060063FB RID: 25595 RVA: 0x001564FD File Offset: 0x001546FD
		void ICollection<TKey>.Clear()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_KeyCollectionSet"));
		}

		// Token: 0x060063FC RID: 25596 RVA: 0x0015650E File Offset: 0x0015470E
		public bool Contains(TKey item)
		{
			return this.dictionary.ContainsKey(item);
		}

		// Token: 0x060063FD RID: 25597 RVA: 0x0015651C File Offset: 0x0015471C
		bool ICollection<TKey>.Remove(TKey item)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_KeyCollectionSet"));
		}

		// Token: 0x060063FE RID: 25598 RVA: 0x0015652D File Offset: 0x0015472D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<TKey>)this).GetEnumerator();
		}

		// Token: 0x060063FF RID: 25599 RVA: 0x00156535 File Offset: 0x00154735
		public IEnumerator<TKey> GetEnumerator()
		{
			return new DictionaryKeyEnumerator<TKey, TValue>(this.dictionary);
		}

		// Token: 0x04002CED RID: 11501
		private readonly IDictionary<TKey, TValue> dictionary;
	}
}
