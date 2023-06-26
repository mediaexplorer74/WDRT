using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009CC RID: 2508
	[Serializable]
	internal sealed class DictionaryKeyEnumerator<TKey, TValue> : IEnumerator<TKey>, IDisposable, IEnumerator
	{
		// Token: 0x06006400 RID: 25600 RVA: 0x00156542 File Offset: 0x00154742
		public DictionaryKeyEnumerator(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
			this.enumeration = dictionary.GetEnumerator();
		}

		// Token: 0x06006401 RID: 25601 RVA: 0x0015656B File Offset: 0x0015476B
		void IDisposable.Dispose()
		{
			this.enumeration.Dispose();
		}

		// Token: 0x06006402 RID: 25602 RVA: 0x00156578 File Offset: 0x00154778
		public bool MoveNext()
		{
			return this.enumeration.MoveNext();
		}

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x06006403 RID: 25603 RVA: 0x00156585 File Offset: 0x00154785
		object IEnumerator.Current
		{
			get
			{
				return ((IEnumerator<TKey>)this).Current;
			}
		}

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x06006404 RID: 25604 RVA: 0x00156594 File Offset: 0x00154794
		public TKey Current
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this.enumeration.Current;
				return keyValuePair.Key;
			}
		}

		// Token: 0x06006405 RID: 25605 RVA: 0x001565B4 File Offset: 0x001547B4
		public void Reset()
		{
			this.enumeration = this.dictionary.GetEnumerator();
		}

		// Token: 0x04002CEE RID: 11502
		private readonly IDictionary<TKey, TValue> dictionary;

		// Token: 0x04002CEF RID: 11503
		private IEnumerator<KeyValuePair<TKey, TValue>> enumeration;
	}
}
