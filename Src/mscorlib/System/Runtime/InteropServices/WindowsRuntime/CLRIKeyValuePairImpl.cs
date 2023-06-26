using System;
using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A1F RID: 2591
	internal sealed class CLRIKeyValuePairImpl<K, V> : IKeyValuePair<K, V>
	{
		// Token: 0x06006617 RID: 26135 RVA: 0x0015B3A0 File Offset: 0x001595A0
		public CLRIKeyValuePairImpl([In] ref KeyValuePair<K, V> pair)
		{
			this._pair = pair;
		}

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x06006618 RID: 26136 RVA: 0x0015B3B4 File Offset: 0x001595B4
		public K Key
		{
			get
			{
				return this._pair.Key;
			}
		}

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x06006619 RID: 26137 RVA: 0x0015B3D0 File Offset: 0x001595D0
		public V Value
		{
			get
			{
				return this._pair.Value;
			}
		}

		// Token: 0x0600661A RID: 26138 RVA: 0x0015B3EC File Offset: 0x001595EC
		internal static object BoxHelper(object pair)
		{
			KeyValuePair<K, V> keyValuePair = (KeyValuePair<K, V>)pair;
			return new CLRIKeyValuePairImpl<K, V>(ref keyValuePair);
		}

		// Token: 0x0600661B RID: 26139 RVA: 0x0015B408 File Offset: 0x00159608
		internal static object UnboxHelper(object wrapper)
		{
			CLRIKeyValuePairImpl<K, V> clrikeyValuePairImpl = (CLRIKeyValuePairImpl<K, V>)wrapper;
			return clrikeyValuePairImpl._pair;
		}

		// Token: 0x0600661C RID: 26140 RVA: 0x0015B428 File Offset: 0x00159628
		public override string ToString()
		{
			return this._pair.ToString();
		}

		// Token: 0x04002D4C RID: 11596
		private readonly KeyValuePair<K, V> _pair;
	}
}
