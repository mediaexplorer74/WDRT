using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004CB RID: 1227
	internal sealed class Mscorlib_DictionaryValueCollectionDebugView<TKey, TValue>
	{
		// Token: 0x06003AD5 RID: 15061 RVA: 0x000E1114 File Offset: 0x000DF314
		public Mscorlib_DictionaryValueCollectionDebugView(ICollection<TValue> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			this.collection = collection;
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06003AD6 RID: 15062 RVA: 0x000E112C File Offset: 0x000DF32C
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TValue[] Items
		{
			get
			{
				TValue[] array = new TValue[this.collection.Count];
				this.collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x0400195D RID: 6493
		private ICollection<TValue> collection;
	}
}
