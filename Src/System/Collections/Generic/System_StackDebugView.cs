using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020003BE RID: 958
	internal sealed class System_StackDebugView<T>
	{
		// Token: 0x06002408 RID: 9224 RVA: 0x000A8FCE File Offset: 0x000A71CE
		public System_StackDebugView(Stack<T> stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack");
			}
			this.stack = stack;
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06002409 RID: 9225 RVA: 0x000A8FEB File Offset: 0x000A71EB
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this.stack.ToArray();
			}
		}

		// Token: 0x04001FE8 RID: 8168
		private Stack<T> stack;
	}
}
