using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A1D RID: 2589
	[Guid("e480ce40-a338-4ada-adcf-272272e48cb9")]
	[ComImport]
	internal interface IMapView<K, V> : IIterable<IKeyValuePair<K, V>>, IEnumerable<IKeyValuePair<K, V>>, IEnumerable
	{
		// Token: 0x06006611 RID: 26129
		V Lookup(K key);

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x06006612 RID: 26130
		uint Size { get; }

		// Token: 0x06006613 RID: 26131
		bool HasKey(K key);

		// Token: 0x06006614 RID: 26132
		void Split(out IMapView<K, V> first, out IMapView<K, V> second);
	}
}
