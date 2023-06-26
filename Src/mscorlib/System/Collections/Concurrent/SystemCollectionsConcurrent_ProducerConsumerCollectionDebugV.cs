using System;
using System.Diagnostics;

namespace System.Collections.Concurrent
{
	// Token: 0x020004AA RID: 1194
	internal sealed class SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView<T>
	{
		// Token: 0x0600393E RID: 14654 RVA: 0x000DC324 File Offset: 0x000DA524
		public SystemCollectionsConcurrent_ProducerConsumerCollectionDebugView(IProducerConsumerCollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.m_collection = collection;
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x0600393F RID: 14655 RVA: 0x000DC341 File Offset: 0x000DA541
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this.m_collection.ToArray();
			}
		}

		// Token: 0x0400191B RID: 6427
		private IProducerConsumerCollection<T> m_collection;
	}
}
