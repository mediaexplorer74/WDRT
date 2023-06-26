using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A1C RID: 2588
	[Guid("3c2925fe-8519-45c1-aa79-197b6718c1c1")]
	[ComImport]
	internal interface IMap<K, V> : IIterable<IKeyValuePair<K, V>>, IEnumerable<IKeyValuePair<K, V>>, IEnumerable
	{
		// Token: 0x0600660A RID: 26122
		V Lookup(K key);

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x0600660B RID: 26123
		uint Size { get; }

		// Token: 0x0600660C RID: 26124
		bool HasKey(K key);

		// Token: 0x0600660D RID: 26125
		IReadOnlyDictionary<K, V> GetView();

		// Token: 0x0600660E RID: 26126
		bool Insert(K key, V value);

		// Token: 0x0600660F RID: 26127
		void Remove(K key);

		// Token: 0x06006610 RID: 26128
		void Clear();
	}
}
