using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004CC RID: 1228
	internal sealed class Mscorlib_DictionaryDebugView<K, V>
	{
		// Token: 0x06003AD7 RID: 15063 RVA: 0x000E1158 File Offset: 0x000DF358
		public Mscorlib_DictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
			}
			this.dict = dictionary;
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06003AD8 RID: 15064 RVA: 0x000E1170 File Offset: 0x000DF370
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

		// Token: 0x0400195E RID: 6494
		private IDictionary<K, V> dict;
	}
}
