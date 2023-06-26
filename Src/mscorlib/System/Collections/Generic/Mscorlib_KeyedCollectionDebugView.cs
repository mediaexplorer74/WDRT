using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004CD RID: 1229
	internal sealed class Mscorlib_KeyedCollectionDebugView<K, T>
	{
		// Token: 0x06003AD9 RID: 15065 RVA: 0x000E119C File Offset: 0x000DF39C
		public Mscorlib_KeyedCollectionDebugView(KeyedCollection<K, T> keyedCollection)
		{
			if (keyedCollection == null)
			{
				throw new ArgumentNullException("keyedCollection");
			}
			this.kc = keyedCollection;
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06003ADA RID: 15066 RVA: 0x000E11BC File Offset: 0x000DF3BC
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				T[] array = new T[this.kc.Count];
				this.kc.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x0400195F RID: 6495
		private KeyedCollection<K, T> kc;
	}
}
