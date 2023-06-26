using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020004C9 RID: 1225
	internal sealed class Mscorlib_CollectionDebugView<T>
	{
		// Token: 0x06003AD1 RID: 15057 RVA: 0x000E108A File Offset: 0x000DF28A
		public Mscorlib_CollectionDebugView(ICollection<T> collection)
		{
			if (collection == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
			}
			this.collection = collection;
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06003AD2 RID: 15058 RVA: 0x000E10A4 File Offset: 0x000DF2A4
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

		// Token: 0x0400195B RID: 6491
		private ICollection<T> collection;
	}
}
