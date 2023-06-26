using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E3 RID: 2531
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	internal sealed class ReadOnlyDictionaryKeyCollection<TKey, TValue> : IEnumerable<TKey>, IEnumerable
	{
		// Token: 0x060064A2 RID: 25762 RVA: 0x001583A8 File Offset: 0x001565A8
		public ReadOnlyDictionaryKeyCollection(IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
		}

		// Token: 0x060064A3 RID: 25763 RVA: 0x001583C5 File Offset: 0x001565C5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<TKey>)this).GetEnumerator();
		}

		// Token: 0x060064A4 RID: 25764 RVA: 0x001583CD File Offset: 0x001565CD
		public IEnumerator<TKey> GetEnumerator()
		{
			return new ReadOnlyDictionaryKeyEnumerator<TKey, TValue>(this.dictionary);
		}

		// Token: 0x04002CFA RID: 11514
		private readonly IReadOnlyDictionary<TKey, TValue> dictionary;
	}
}
