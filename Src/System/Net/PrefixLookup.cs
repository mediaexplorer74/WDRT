using System;
using System.Collections.Generic;

namespace System.Net
{
	// Token: 0x020001DD RID: 477
	internal class PrefixLookup
	{
		// Token: 0x060012AC RID: 4780 RVA: 0x0006329A File Offset: 0x0006149A
		public PrefixLookup()
			: this(100)
		{
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x000632A4 File Offset: 0x000614A4
		public PrefixLookup(int capacity)
		{
			this.capacity = capacity;
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x000632C0 File Offset: 0x000614C0
		public void Add(string prefix, object value)
		{
			if (this.capacity == 0 || prefix == null || prefix.Length == 0 || value == null)
			{
				return;
			}
			LinkedList<PrefixLookup.PrefixValuePair> linkedList = this.lruList;
			lock (linkedList)
			{
				if (this.lruList.First != null && this.lruList.First.Value.prefix.Equals(prefix))
				{
					this.lruList.First.Value.value = value;
				}
				else
				{
					this.lruList.AddFirst(new PrefixLookup.PrefixValuePair(prefix, value));
					while (this.lruList.Count > this.capacity)
					{
						this.lruList.RemoveLast();
					}
				}
			}
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0006338C File Offset: 0x0006158C
		public object Lookup(string lookupKey)
		{
			if (lookupKey == null || lookupKey.Length == 0 || this.lruList.Count == 0)
			{
				return null;
			}
			LinkedListNode<PrefixLookup.PrefixValuePair> linkedListNode = null;
			LinkedList<PrefixLookup.PrefixValuePair> linkedList = this.lruList;
			lock (linkedList)
			{
				int num = 0;
				for (LinkedListNode<PrefixLookup.PrefixValuePair> linkedListNode2 = this.lruList.First; linkedListNode2 != null; linkedListNode2 = linkedListNode2.Next)
				{
					string prefix = linkedListNode2.Value.prefix;
					if (prefix.Length > num && lookupKey.StartsWith(prefix))
					{
						num = prefix.Length;
						linkedListNode = linkedListNode2;
						if (num == lookupKey.Length)
						{
							break;
						}
					}
				}
				if (linkedListNode != null && linkedListNode != this.lruList.First)
				{
					this.lruList.Remove(linkedListNode);
					this.lruList.AddFirst(linkedListNode);
				}
			}
			if (linkedListNode == null)
			{
				return null;
			}
			return linkedListNode.Value.value;
		}

		// Token: 0x04001500 RID: 5376
		private const int defaultCapacity = 100;

		// Token: 0x04001501 RID: 5377
		private volatile int capacity;

		// Token: 0x04001502 RID: 5378
		private readonly LinkedList<PrefixLookup.PrefixValuePair> lruList = new LinkedList<PrefixLookup.PrefixValuePair>();

		// Token: 0x02000753 RID: 1875
		private class PrefixValuePair
		{
			// Token: 0x060041DC RID: 16860 RVA: 0x0011170F File Offset: 0x0010F90F
			public PrefixValuePair(string pre, object val)
			{
				this.prefix = pre;
				this.value = val;
			}

			// Token: 0x040031F3 RID: 12787
			public string prefix;

			// Token: 0x040031F4 RID: 12788
			public object value;
		}
	}
}
