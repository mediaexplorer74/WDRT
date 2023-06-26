using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020003BF RID: 959
	internal sealed class System_DictionaryDebugView<K, V>
	{
		// Token: 0x0600240A RID: 9226 RVA: 0x000A8FF8 File Offset: 0x000A71F8
		public System_DictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dict = dictionary;
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x0600240B RID: 9227 RVA: 0x000A9018 File Offset: 0x000A7218
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<K, V>[] Items
		{
			get
			{
				KeyValuePair<K, V>[] array = new KeyValuePair<K, V>[this.dict.Count];
				this.dict.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04001FE9 RID: 8169
		private IDictionary<K, V> dict;
	}
}
