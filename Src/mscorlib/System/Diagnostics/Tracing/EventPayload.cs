using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000444 RID: 1092
	internal class EventPayload : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x06003625 RID: 13861 RVA: 0x000D3BFD File Offset: 0x000D1DFD
		internal EventPayload(List<string> payloadNames, List<object> payloadValues)
		{
			this.m_names = payloadNames;
			this.m_values = payloadValues;
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06003626 RID: 13862 RVA: 0x000D3C13 File Offset: 0x000D1E13
		public ICollection<string> Keys
		{
			get
			{
				return this.m_names;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06003627 RID: 13863 RVA: 0x000D3C1B File Offset: 0x000D1E1B
		public ICollection<object> Values
		{
			get
			{
				return this.m_values;
			}
		}

		// Token: 0x17000800 RID: 2048
		public object this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = 0;
				foreach (string text in this.m_names)
				{
					if (text == key)
					{
						return this.m_values[num];
					}
					num++;
				}
				throw new KeyNotFoundException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x000D3CAB File Offset: 0x000D1EAB
		public void Add(string key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x000D3CB2 File Offset: 0x000D1EB2
		public void Add(KeyValuePair<string, object> payloadEntry)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x000D3CB9 File Offset: 0x000D1EB9
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x000D3CC0 File Offset: 0x000D1EC0
		public bool Contains(KeyValuePair<string, object> entry)
		{
			return this.ContainsKey(entry.Key);
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x000D3CD0 File Offset: 0x000D1ED0
		public bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			foreach (string text in this.m_names)
			{
				if (text == key)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x0600362F RID: 13871 RVA: 0x000D3D3C File Offset: 0x000D1F3C
		public int Count
		{
			get
			{
				return this.m_names.Count;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06003630 RID: 13872 RVA: 0x000D3D49 File Offset: 0x000D1F49
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x000D3D4C File Offset: 0x000D1F4C
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.Keys.Count; i = num + 1)
			{
				yield return new KeyValuePair<string, object>(this.m_names[i], this.m_values[i]);
				num = i;
			}
			yield break;
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x000D3D5C File Offset: 0x000D1F5C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<string, object>>)this).GetEnumerator();
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x000D3D71 File Offset: 0x000D1F71
		public void CopyTo(KeyValuePair<string, object>[] payloadEntries, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x000D3D78 File Offset: 0x000D1F78
		public bool Remove(string key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x000D3D7F File Offset: 0x000D1F7F
		public bool Remove(KeyValuePair<string, object> entry)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x000D3D88 File Offset: 0x000D1F88
		public bool TryGetValue(string key, out object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = 0;
			foreach (string text in this.m_names)
			{
				if (text == key)
				{
					value = this.m_values[num];
					return true;
				}
				num++;
			}
			value = null;
			return false;
		}

		// Token: 0x04001837 RID: 6199
		private List<string> m_names;

		// Token: 0x04001838 RID: 6200
		private List<object> m_values;
	}
}
