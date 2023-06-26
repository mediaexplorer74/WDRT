using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020003C1 RID: 961
	internal sealed class System_DictionaryValueCollectionDebugView<TKey, TValue>
	{
		// Token: 0x0600240E RID: 9230 RVA: 0x000A9088 File Offset: 0x000A7288
		public System_DictionaryValueCollectionDebugView(ICollection<TValue> collection)
		{
			if (collection == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.collection);
			}
			this.collection = collection;
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x0600240F RID: 9231 RVA: 0x000A90A0 File Offset: 0x000A72A0
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

		// Token: 0x04001FEB RID: 8171
		private ICollection<TValue> collection;
	}
}
