using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020003BC RID: 956
	internal sealed class System_CollectionDebugView<T>
	{
		// Token: 0x06002404 RID: 9220 RVA: 0x000A8F5A File Offset: 0x000A715A
		public System_CollectionDebugView(ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.collection = collection;
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002405 RID: 9221 RVA: 0x000A8F78 File Offset: 0x000A7178
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				T[] array = new T[this.collection.Count];
				this.collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04001FE6 RID: 8166
		private ICollection<T> collection;
	}
}
