using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020003C0 RID: 960
	internal sealed class System_DictionaryKeyCollectionDebugView<TKey, TValue>
	{
		// Token: 0x0600240C RID: 9228 RVA: 0x000A9044 File Offset: 0x000A7244
		public System_DictionaryKeyCollectionDebugView(ICollection<TKey> collection)
		{
			if (collection == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.collection);
			}
			this.collection = collection;
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x0600240D RID: 9229 RVA: 0x000A905C File Offset: 0x000A725C
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

		// Token: 0x04001FEA RID: 8170
		private ICollection<TKey> collection;
	}
}
