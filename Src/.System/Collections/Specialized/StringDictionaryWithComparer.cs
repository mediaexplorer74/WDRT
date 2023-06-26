using System;

namespace System.Collections.Specialized
{
	// Token: 0x020003B8 RID: 952
	[Serializable]
	internal class StringDictionaryWithComparer : StringDictionary
	{
		// Token: 0x060023D2 RID: 9170 RVA: 0x000A88B5 File Offset: 0x000A6AB5
		public StringDictionaryWithComparer()
			: this(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x000A88C2 File Offset: 0x000A6AC2
		public StringDictionaryWithComparer(IEqualityComparer comparer)
		{
			base.ReplaceHashtable(new Hashtable(comparer));
		}

		// Token: 0x17000913 RID: 2323
		public override string this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return (string)this.contents[key];
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.contents[key] = value;
			}
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000A8914 File Offset: 0x000A6B14
		public override void Add(string key, string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Add(key, value);
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x000A8931 File Offset: 0x000A6B31
		public override bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.contents.ContainsKey(key);
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000A894D File Offset: 0x000A6B4D
		public override void Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Remove(key);
		}
	}
}
