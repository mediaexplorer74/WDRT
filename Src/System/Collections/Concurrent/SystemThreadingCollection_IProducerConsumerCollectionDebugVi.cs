using System;
using System.Diagnostics;

namespace System.Collections.Concurrent
{
	// Token: 0x020003D3 RID: 979
	internal sealed class SystemThreadingCollection_IProducerConsumerCollectionDebugView<T>
	{
		// Token: 0x060025A1 RID: 9633 RVA: 0x000AEED4 File Offset: 0x000AD0D4
		public SystemThreadingCollection_IProducerConsumerCollectionDebugView(IProducerConsumerCollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.m_collection = collection;
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x060025A2 RID: 9634 RVA: 0x000AEEF1 File Offset: 0x000AD0F1
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this.m_collection.ToArray();
			}
		}

		// Token: 0x0400204A RID: 8266
		private IProducerConsumerCollection<T> m_collection;
	}
}
