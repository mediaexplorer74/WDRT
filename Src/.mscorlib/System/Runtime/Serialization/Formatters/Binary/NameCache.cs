using System;
using System.Collections.Concurrent;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000797 RID: 1943
	internal sealed class NameCache
	{
		// Token: 0x0600546A RID: 21610 RVA: 0x0012AA14 File Offset: 0x00128C14
		internal object GetCachedValue(string name)
		{
			this.name = name;
			object obj;
			if (!NameCache.ht.TryGetValue(name, out obj))
			{
				return null;
			}
			return obj;
		}

		// Token: 0x0600546B RID: 21611 RVA: 0x0012AA3A File Offset: 0x00128C3A
		internal void SetCachedValue(object value)
		{
			NameCache.ht[this.name] = value;
		}

		// Token: 0x0400265E RID: 9822
		private static ConcurrentDictionary<string, object> ht = new ConcurrentDictionary<string, object>();

		// Token: 0x0400265F RID: 9823
		private string name;
	}
}
