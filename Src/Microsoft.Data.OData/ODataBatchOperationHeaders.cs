using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x020001B5 RID: 437
	internal sealed class ODataBatchOperationHeaders : IEnumerable<KeyValuePair<string, string>>, IEnumerable
	{
		// Token: 0x06000D8C RID: 3468 RVA: 0x0002F568 File Offset: 0x0002D768
		public ODataBatchOperationHeaders()
		{
			this.caseSensitiveDictionary = new Dictionary<string, string>(StringComparer.Ordinal);
		}

		// Token: 0x170002EE RID: 750
		public string this[string key]
		{
			get
			{
				string text;
				if (this.TryGetValue(key, out text))
				{
					return text;
				}
				throw new KeyNotFoundException(Strings.ODataBatchOperationHeaderDictionary_KeyNotFound(key));
			}
			set
			{
				this.caseSensitiveDictionary[key] = value;
			}
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0002F5B4 File Offset: 0x0002D7B4
		public void Add(string key, string value)
		{
			this.caseSensitiveDictionary.Add(key, value);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0002F5C3 File Offset: 0x0002D7C3
		public bool ContainsKeyOrdinal(string key)
		{
			return this.caseSensitiveDictionary.ContainsKey(key);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0002F5D1 File Offset: 0x0002D7D1
		public bool Remove(string key)
		{
			if (this.caseSensitiveDictionary.Remove(key))
			{
				return true;
			}
			key = this.FindKeyIgnoreCase(key);
			return key != null && this.caseSensitiveDictionary.Remove(key);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0002F5FD File Offset: 0x0002D7FD
		public bool TryGetValue(string key, out string value)
		{
			if (this.caseSensitiveDictionary.TryGetValue(key, out value))
			{
				return true;
			}
			key = this.FindKeyIgnoreCase(key);
			if (key == null)
			{
				value = null;
				return false;
			}
			return this.caseSensitiveDictionary.TryGetValue(key, out value);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0002F62E File Offset: 0x0002D82E
		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			return this.caseSensitiveDictionary.GetEnumerator();
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0002F640 File Offset: 0x0002D840
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.caseSensitiveDictionary.GetEnumerator();
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0002F654 File Offset: 0x0002D854
		private string FindKeyIgnoreCase(string key)
		{
			string text = null;
			foreach (string text2 in this.caseSensitiveDictionary.Keys)
			{
				if (string.Compare(text2, key, StringComparison.OrdinalIgnoreCase) == 0)
				{
					if (text != null)
					{
						throw new ODataException(Strings.ODataBatchOperationHeaderDictionary_DuplicateCaseInsensitiveKeys(key));
					}
					text = text2;
				}
			}
			return text;
		}

		// Token: 0x0400048F RID: 1167
		private readonly Dictionary<string, string> caseSensitiveDictionary;
	}
}
