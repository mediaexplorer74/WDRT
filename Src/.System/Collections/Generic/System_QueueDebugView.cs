using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020003BD RID: 957
	internal sealed class System_QueueDebugView<T>
	{
		// Token: 0x06002406 RID: 9222 RVA: 0x000A8FA4 File Offset: 0x000A71A4
		public System_QueueDebugView(Queue<T> queue)
		{
			if (queue == null)
			{
				throw new ArgumentNullException("queue");
			}
			this.queue = queue;
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002407 RID: 9223 RVA: 0x000A8FC1 File Offset: 0x000A71C1
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this.queue.ToArray();
			}
		}

		// Token: 0x04001FE7 RID: 8167
		private Queue<T> queue;
	}
}
