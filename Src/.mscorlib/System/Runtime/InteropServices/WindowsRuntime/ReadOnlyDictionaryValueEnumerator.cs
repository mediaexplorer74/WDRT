using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E6 RID: 2534
	[Serializable]
	internal sealed class ReadOnlyDictionaryValueEnumerator<TKey, TValue> : IEnumerator<TValue>, IDisposable, IEnumerator
	{
		// Token: 0x060064AE RID: 25774 RVA: 0x00158491 File Offset: 0x00156691
		public ReadOnlyDictionaryValueEnumerator(IReadOnlyDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dictionary = dictionary;
			this.enumeration = dictionary.GetEnumerator();
		}

		// Token: 0x060064AF RID: 25775 RVA: 0x001584BA File Offset: 0x001566BA
		void IDisposable.Dispose()
		{
			this.enumeration.Dispose();
		}

		// Token: 0x060064B0 RID: 25776 RVA: 0x001584C7 File Offset: 0x001566C7
		public bool MoveNext()
		{
			return this.enumeration.MoveNext();
		}

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x060064B1 RID: 25777 RVA: 0x001584D4 File Offset: 0x001566D4
		object IEnumerator.Current
		{
			get
			{
				return ((IEnumerator<TValue>)this).Current;
			}
		}

		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x060064B2 RID: 25778 RVA: 0x001584E4 File Offset: 0x001566E4
		public TValue Current
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this.enumeration.Current;
				return keyValuePair.Value;
			}
		}

		// Token: 0x060064B3 RID: 25779 RVA: 0x00158504 File Offset: 0x00156704
		public void Reset()
		{
			this.enumeration = this.dictionary.GetEnumerator();
		}

		// Token: 0x04002CFE RID: 11518
		private readonly IReadOnlyDictionary<TKey, TValue> dictionary;

		// Token: 0x04002CFF RID: 11519
		private IEnumerator<KeyValuePair<TKey, TValue>> enumeration;
	}
}
