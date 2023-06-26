using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E5 RID: 2533
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	internal sealed class ReadOnlyDictionaryValueCollection<TKey, TValue> : IEnumerable<TValue>, IEnumerable
	{
		// Token: 0x060064AB RID: 25771 RVA: 0x0015845F File Offset: 0x0015665F
		public ReadOnlyDictionaryValueCollection(IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
		}

		// Token: 0x060064AC RID: 25772 RVA: 0x0015847C File Offset: 0x0015667C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<TValue>)this).GetEnumerator();
		}

		// Token: 0x060064AD RID: 25773 RVA: 0x00158484 File Offset: 0x00156684
		public IEnumerator<TValue> GetEnumerator()
		{
			return new ReadOnlyDictionaryValueEnumerator<TKey, TValue>(this.dictionary);
		}

		// Token: 0x04002CFD RID: 11517
		private readonly IReadOnlyDictionary<TKey, TValue> dictionary;
	}
}
