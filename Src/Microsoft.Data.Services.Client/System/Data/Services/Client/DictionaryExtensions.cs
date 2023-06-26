using System;
using System.Collections.Generic;

namespace System.Data.Services.Client
{
	// Token: 0x02000058 RID: 88
	internal static class DictionaryExtensions
	{
		// Token: 0x060002EB RID: 747 RVA: 0x0000DBC0 File Offset: 0x0000BDC0
		internal static TValue FindOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> createValue)
		{
			TValue tvalue;
			if (!dictionary.TryGetValue(key, out tvalue))
			{
				tvalue = (dictionary[key] = createValue());
			}
			return tvalue;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000DBE8 File Offset: 0x0000BDE8
		internal static void SetRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> valuesToCopy)
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in valuesToCopy)
			{
				dictionary[keyValuePair.Key] = keyValuePair.Value;
			}
		}
	}
}
