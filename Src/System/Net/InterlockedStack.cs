using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x020000CE RID: 206
	internal sealed class InterlockedStack
	{
		// Token: 0x060006DC RID: 1756 RVA: 0x00025F29 File Offset: 0x00024129
		internal InterlockedStack()
		{
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00025F3C File Offset: 0x0002413C
		internal void Push(object pooledStream)
		{
			if (pooledStream == null)
			{
				throw new ArgumentNullException("pooledStream");
			}
			object syncRoot = this._stack.SyncRoot;
			lock (syncRoot)
			{
				this._stack.Push(pooledStream);
				this._count = this._stack.Count;
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00025FA8 File Offset: 0x000241A8
		internal object Pop()
		{
			object syncRoot = this._stack.SyncRoot;
			object obj2;
			lock (syncRoot)
			{
				object obj = null;
				if (0 < this._stack.Count)
				{
					obj = this._stack.Pop();
					this._count = this._stack.Count;
				}
				obj2 = obj;
			}
			return obj2;
		}

		// Token: 0x04000CAC RID: 3244
		private readonly Stack _stack = new Stack();

		// Token: 0x04000CAD RID: 3245
		private int _count;
	}
}
