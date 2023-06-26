using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Data.Edm.Internal
{
	// Token: 0x020001C4 RID: 452
	internal class HashSetInternal<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000ABA RID: 2746 RVA: 0x0001FD0B File Offset: 0x0001DF0B
		public HashSetInternal()
		{
			this.wrappedDictionary = new Dictionary<T, object>();
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0001FD1E File Offset: 0x0001DF1E
		public bool Add(T thingToAdd)
		{
			if (this.wrappedDictionary.ContainsKey(thingToAdd))
			{
				return false;
			}
			this.wrappedDictionary[thingToAdd] = null;
			return true;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0001FD3E File Offset: 0x0001DF3E
		public bool Contains(T item)
		{
			return this.wrappedDictionary.ContainsKey(item);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0001FD4C File Offset: 0x0001DF4C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0001FD54 File Offset: 0x0001DF54
		public IEnumerator<T> GetEnumerator()
		{
			return this.wrappedDictionary.Keys.GetEnumerator();
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0001FD6B File Offset: 0x0001DF6B
		public void Remove(T item)
		{
			this.wrappedDictionary.Remove(item);
		}

		// Token: 0x04000513 RID: 1299
		private readonly Dictionary<T, object> wrappedDictionary;
	}
}
