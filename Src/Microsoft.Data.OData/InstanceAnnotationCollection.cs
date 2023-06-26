using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x0200013B RID: 315
	[Obsolete("The InstanceAnnotationCollection class is deprecated, use the InstanceAnnotations property on objects that support instance annotations instead.")]
	public sealed class InstanceAnnotationCollection : IEnumerable<KeyValuePair<string, ODataValue>>, IEnumerable
	{
		// Token: 0x06000867 RID: 2151 RVA: 0x0001B6FD File Offset: 0x000198FD
		public InstanceAnnotationCollection()
		{
			this.inner = new Dictionary<string, ODataValue>(StringComparer.Ordinal);
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x0001B715 File Offset: 0x00019915
		public int Count
		{
			get
			{
				return this.inner.Count;
			}
		}

		// Token: 0x1700020F RID: 527
		public ODataValue this[string key]
		{
			get
			{
				return this.inner[key];
			}
			set
			{
				this.inner[key] = value;
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001B73F File Offset: 0x0001993F
		public bool ContainsKey(string key)
		{
			return this.inner.ContainsKey(key);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001B74D File Offset: 0x0001994D
		public IEnumerator<KeyValuePair<string, ODataValue>> GetEnumerator()
		{
			return this.inner.GetEnumerator();
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001B75F File Offset: 0x0001995F
		public void Clear()
		{
			this.inner.Clear();
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001B76C File Offset: 0x0001996C
		public void Add(string key, ODataValue value)
		{
			ODataInstanceAnnotation.ValidateName(key);
			ODataInstanceAnnotation.ValidateValue(value);
			this.inner.Add(key, value);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001B787 File Offset: 0x00019987
		public bool Remove(string key)
		{
			return this.inner.Remove(key);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001B795 File Offset: 0x00019995
		public bool TryGetValue(string key, out ODataValue value)
		{
			return this.inner.TryGetValue(key, out value);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001B7A4 File Offset: 0x000199A4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000333 RID: 819
		private readonly Dictionary<string, ODataValue> inner;
	}
}
