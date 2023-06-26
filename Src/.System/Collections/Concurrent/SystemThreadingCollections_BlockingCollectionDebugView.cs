using System;
using System.Diagnostics;

namespace System.Collections.Concurrent
{
	// Token: 0x020003D1 RID: 977
	internal sealed class SystemThreadingCollections_BlockingCollectionDebugView<T>
	{
		// Token: 0x0600257E RID: 9598 RVA: 0x000AE5AC File Offset: 0x000AC7AC
		public SystemThreadingCollections_BlockingCollectionDebugView(BlockingCollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.m_blockingCollection = collection;
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x000AE5C9 File Offset: 0x000AC7C9
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this.m_blockingCollection.ToArray();
			}
		}

		// Token: 0x04002044 RID: 8260
		private BlockingCollection<T> m_blockingCollection;
	}
}
