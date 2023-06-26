using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E4 RID: 2532
	[Serializable]
	internal sealed class ReadOnlyDictionaryKeyEnumerator<TKey, TValue> : IEnumerator<TKey>, IDisposable, IEnumerator
	{
		// Token: 0x060064A5 RID: 25765 RVA: 0x001583DA File Offset: 0x001565DA
		public ReadOnlyDictionaryKeyEnumerator(IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
			this.enumeration = dictionary.GetEnumerator();
		}

		// Token: 0x060064A6 RID: 25766 RVA: 0x00158403 File Offset: 0x00156603
		void IDisposable.Dispose()
		{
			this.enumeration.Dispose();
		}

		// Token: 0x060064A7 RID: 25767 RVA: 0x00158410 File Offset: 0x00156610
		public bool MoveNext()
		{
			return this.enumeration.MoveNext();
		}

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x060064A8 RID: 25768 RVA: 0x0015841D File Offset: 0x0015661D
		object IEnumerator.Current
		{
			get
			{
				return ((IEnumerator<TKey>)this).Current;
			}
		}

		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x060064A9 RID: 25769 RVA: 0x0015842C File Offset: 0x0015662C
		public TKey Current
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this.enumeration.Current;
				return keyValuePair.Key;
			}
		}

		// Token: 0x060064AA RID: 25770 RVA: 0x0015844C File Offset: 0x0015664C
		public void Reset()
		{
			this.enumeration = this.dictionary.GetEnumerator();
		}

		// Token: 0x04002CFB RID: 11515
		private readonly IReadOnlyDictionary<TKey, TValue> dictionary;

		// Token: 0x04002CFC RID: 11516
		private IEnumerator<KeyValuePair<TKey, TValue>> enumeration;
	}
}
