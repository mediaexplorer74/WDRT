using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004CA RID: 1226
	internal sealed class Mscorlib_DictionaryKeyCollectionDebugView<TKey, TValue>
	{
		// Token: 0x06003AD3 RID: 15059 RVA: 0x000E10D0 File Offset: 0x000DF2D0
		public Mscorlib_DictionaryKeyCollectionDebugView(ICollection<TKey> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			this.collection = collection;
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06003AD4 RID: 15060 RVA: 0x000E10E8 File Offset: 0x000DF2E8
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TKey[] Items
		{
			get
			{
				TKey[] array = new TKey[this.collection.Count];
				this.collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x0400195C RID: 6492
		private ICollection<TKey> collection;
	}
}
