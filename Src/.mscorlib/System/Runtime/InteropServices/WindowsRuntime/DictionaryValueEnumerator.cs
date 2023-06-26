using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009CE RID: 2510
	[Serializable]
	internal sealed class DictionaryValueEnumerator<TKey, TValue> : IEnumerator<TValue>, IDisposable, IEnumerator
	{
		// Token: 0x06006410 RID: 25616 RVA: 0x0015674E File Offset: 0x0015494E
		public DictionaryValueEnumerator(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
			this.enumeration = dictionary.GetEnumerator();
		}

		// Token: 0x06006411 RID: 25617 RVA: 0x00156777 File Offset: 0x00154977
		void IDisposable.Dispose()
		{
			this.enumeration.Dispose();
		}

		// Token: 0x06006412 RID: 25618 RVA: 0x00156784 File Offset: 0x00154984
		public bool MoveNext()
		{
			return this.enumeration.MoveNext();
		}

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x06006413 RID: 25619 RVA: 0x00156791 File Offset: 0x00154991
		object IEnumerator.Current
		{
			get
			{
				return ((IEnumerator<TValue>)this).Current;
			}
		}

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x06006414 RID: 25620 RVA: 0x001567A0 File Offset: 0x001549A0
		public TValue Current
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this.enumeration.Current;
				return keyValuePair.Value;
			}
		}

		// Token: 0x06006415 RID: 25621 RVA: 0x001567C0 File Offset: 0x001549C0
		public void Reset()
		{
			this.enumeration = this.dictionary.GetEnumerator();
		}

		// Token: 0x04002CF1 RID: 11505
		private readonly IDictionary<TKey, TValue> dictionary;

		// Token: 0x04002CF2 RID: 11506
		private IEnumerator<KeyValuePair<TKey, TValue>> enumeration;
	}
}
