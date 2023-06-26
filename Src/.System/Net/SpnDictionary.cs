using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Net
{
	// Token: 0x0200020C RID: 524
	internal class SpnDictionary : StringDictionary
	{
		// Token: 0x0600137B RID: 4987 RVA: 0x00066A7B File Offset: 0x00064C7B
		internal SpnDictionary()
		{
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x00066A93 File Offset: 0x00064C93
		public override int Count
		{
			get
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				return this.m_SyncTable.Count;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x00066AAA File Offset: 0x00064CAA
		public override bool IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00066AB0 File Offset: 0x00064CB0
		internal SpnToken InternalGet(string canonicalKey)
		{
			int num = 0;
			string text = null;
			object syncRoot = this.m_SyncTable.SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in this.m_SyncTable.Keys)
				{
					string text2 = (string)obj;
					if (text2 != null && text2.Length > num && string.Compare(text2, 0, canonicalKey, 0, text2.Length, StringComparison.OrdinalIgnoreCase) == 0)
					{
						num = text2.Length;
						text = text2;
					}
				}
			}
			if (text == null)
			{
				return null;
			}
			return (SpnToken)this.m_SyncTable[text];
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00066B88 File Offset: 0x00064D88
		internal void InternalSet(string canonicalKey, SpnToken spnToken)
		{
			this.m_SyncTable[canonicalKey] = spnToken;
		}

		// Token: 0x1700041C RID: 1052
		public override string this[string key]
		{
			get
			{
				key = SpnDictionary.GetCanonicalKey(key);
				SpnToken spnToken = this.InternalGet(key);
				if (spnToken != null)
				{
					return spnToken.Spn;
				}
				return null;
			}
			set
			{
				key = SpnDictionary.GetCanonicalKey(key);
				this.InternalSet(key, new SpnToken(value));
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x00066BD7 File Offset: 0x00064DD7
		public override ICollection Keys
		{
			get
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				return this.m_SyncTable.Keys;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x00066BEE File Offset: 0x00064DEE
		public override object SyncRoot
		{
			[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
			get
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				return this.m_SyncTable;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x00066C00 File Offset: 0x00064E00
		public override ICollection Values
		{
			get
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
				if (this.m_ValuesWrapper == null)
				{
					this.m_ValuesWrapper = new SpnDictionary.ValueCollection(this);
				}
				return this.m_ValuesWrapper;
			}
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00066C26 File Offset: 0x00064E26
		public override void Add(string key, string value)
		{
			key = SpnDictionary.GetCanonicalKey(key);
			this.m_SyncTable.Add(key, new SpnToken(value));
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00066C42 File Offset: 0x00064E42
		public override void Clear()
		{
			ExceptionHelper.WebPermissionUnrestricted.Demand();
			this.m_SyncTable.Clear();
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00066C59 File Offset: 0x00064E59
		public override bool ContainsKey(string key)
		{
			key = SpnDictionary.GetCanonicalKey(key);
			return this.m_SyncTable.ContainsKey(key);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00066C70 File Offset: 0x00064E70
		public override bool ContainsValue(string value)
		{
			ExceptionHelper.WebPermissionUnrestricted.Demand();
			foreach (object obj in this.m_SyncTable.Values)
			{
				SpnToken spnToken = (SpnToken)obj;
				if (spnToken.Spn == value)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00066CE8 File Offset: 0x00064EE8
		public override void CopyTo(Array array, int index)
		{
			ExceptionHelper.WebPermissionUnrestricted.Demand();
			SpnDictionary.CheckCopyToArguments(array, index, this.Count);
			int num = 0;
			foreach (object obj in this)
			{
				array.SetValue(obj, num + index);
				num++;
			}
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00066D58 File Offset: 0x00064F58
		public override IEnumerator GetEnumerator()
		{
			ExceptionHelper.WebPermissionUnrestricted.Demand();
			foreach (object obj in this.m_SyncTable.Keys)
			{
				string text = (string)obj;
				SpnToken spnToken = (SpnToken)this.m_SyncTable[text];
				yield return new DictionaryEntry(text, spnToken.Spn);
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00066D67 File Offset: 0x00064F67
		public override void Remove(string key)
		{
			key = SpnDictionary.GetCanonicalKey(key);
			this.m_SyncTable.Remove(key);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00066D80 File Offset: 0x00064F80
		private static string GetCanonicalKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			try
			{
				Uri uri = new Uri(key);
				key = uri.GetParts(UriComponents.Scheme | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.SafeUnescaped);
				new WebPermission(NetworkAccess.Connect, new Uri(key)).Demand();
			}
			catch (UriFormatException ex)
			{
				throw new ArgumentException(SR.GetString("net_mustbeuri", new object[] { "key" }), "key", ex);
			}
			return key;
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x00066DF8 File Offset: 0x00064FF8
		private static void CheckCopyToArguments(Array array, int index, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SR.GetString("Arg_RankMultiDimNotSupported"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < count)
			{
				throw new ArgumentException(SR.GetString("Arg_ArrayPlusOffTooSmall"));
			}
		}

		// Token: 0x04001554 RID: 5460
		private Hashtable m_SyncTable = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04001555 RID: 5461
		private SpnDictionary.ValueCollection m_ValuesWrapper;

		// Token: 0x02000759 RID: 1881
		private class ValueCollection : ICollection, IEnumerable
		{
			// Token: 0x060041EC RID: 16876 RVA: 0x00111A7B File Offset: 0x0010FC7B
			internal ValueCollection(SpnDictionary spnDictionary)
			{
				this.spnDictionary = spnDictionary;
			}

			// Token: 0x060041ED RID: 16877 RVA: 0x00111A8C File Offset: 0x0010FC8C
			public void CopyTo(Array array, int index)
			{
				SpnDictionary.CheckCopyToArguments(array, index, this.Count);
				int num = 0;
				foreach (object obj in this)
				{
					array.SetValue(obj, num + index);
					num++;
				}
			}

			// Token: 0x17000F11 RID: 3857
			// (get) Token: 0x060041EE RID: 16878 RVA: 0x00111AF4 File Offset: 0x0010FCF4
			public int Count
			{
				get
				{
					return this.spnDictionary.m_SyncTable.Values.Count;
				}
			}

			// Token: 0x17000F12 RID: 3858
			// (get) Token: 0x060041EF RID: 16879 RVA: 0x00111B0B File Offset: 0x0010FD0B
			public bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F13 RID: 3859
			// (get) Token: 0x060041F0 RID: 16880 RVA: 0x00111B0E File Offset: 0x0010FD0E
			public object SyncRoot
			{
				get
				{
					return this.spnDictionary.m_SyncTable.SyncRoot;
				}
			}

			// Token: 0x060041F1 RID: 16881 RVA: 0x00111B20 File Offset: 0x0010FD20
			public IEnumerator GetEnumerator()
			{
				foreach (object obj in this.spnDictionary.m_SyncTable.Values)
				{
					SpnToken spnToken = (SpnToken)obj;
					yield return (spnToken != null) ? spnToken.Spn : null;
				}
				IEnumerator enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x0400320B RID: 12811
			private SpnDictionary spnDictionary;
		}
	}
}
