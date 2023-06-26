using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020003CD RID: 973
	internal class SortedSetDebugView<T>
	{
		// Token: 0x06002530 RID: 9520 RVA: 0x000AD5C8 File Offset: 0x000AB7C8
		public SortedSetDebugView(SortedSet<T> set)
		{
			if (set == null)
			{
				throw new ArgumentNullException("set");
			}
			this.set = set;
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06002531 RID: 9521 RVA: 0x000AD5E5 File Offset: 0x000AB7E5
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this.set.ToArray();
			}
		}

		// Token: 0x04002033 RID: 8243
		private SortedSet<T> set;
	}
}
