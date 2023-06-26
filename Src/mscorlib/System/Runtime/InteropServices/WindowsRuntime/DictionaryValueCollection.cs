using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009CD RID: 2509
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	internal sealed class DictionaryValueCollection<TKey, TValue> : ICollection<TValue>, IEnumerable<TValue>, IEnumerable
	{
		// Token: 0x06006406 RID: 25606 RVA: 0x001565C7 File Offset: 0x001547C7
		public DictionaryValueCollection(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
		}

		// Token: 0x06006407 RID: 25607 RVA: 0x001565E4 File Offset: 0x001547E4
		public void CopyTo(TValue[] array, int index)
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
				array[num++] = keyValuePair.Value;
			}
		}

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x06006408 RID: 25608 RVA: 0x0015669C File Offset: 0x0015489C
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x06006409 RID: 25609 RVA: 0x001566A9 File Offset: 0x001548A9
		bool ICollection<TValue>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600640A RID: 25610 RVA: 0x001566AC File Offset: 0x001548AC
		void ICollection<TValue>.Add(TValue item)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_ValueCollectionSet"));
		}

		// Token: 0x0600640B RID: 25611 RVA: 0x001566BD File Offset: 0x001548BD
		void ICollection<TValue>.Clear()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_ValueCollectionSet"));
		}

		// Token: 0x0600640C RID: 25612 RVA: 0x001566D0 File Offset: 0x001548D0
		public bool Contains(TValue item)
		{
			EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			foreach (TValue tvalue in this)
			{
				if (@default.Equals(item, tvalue))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600640D RID: 25613 RVA: 0x00156728 File Offset: 0x00154928
		bool ICollection<TValue>.Remove(TValue item)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_ValueCollectionSet"));
		}

		// Token: 0x0600640E RID: 25614 RVA: 0x00156739 File Offset: 0x00154939
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<TValue>)this).GetEnumerator();
		}

		// Token: 0x0600640F RID: 25615 RVA: 0x00156741 File Offset: 0x00154941
		public IEnumerator<TValue> GetEnumerator()
		{
			return new DictionaryValueEnumerator<TKey, TValue>(this.dictionary);
		}

		// Token: 0x04002CF0 RID: 11504
		private readonly IDictionary<TKey, TValue> dictionary;
	}
}
