using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x020000D9 RID: 217
	[Serializable]
	internal class PathList
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x00029625 File Offset: 0x00027825
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00029634 File Offset: 0x00027834
		public int GetCookiesCount()
		{
			int num = 0;
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in this.m_list.Values)
				{
					CookieCollection cookieCollection = (CookieCollection)obj;
					num += cookieCollection.Count;
				}
			}
			return num;
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x000296C4 File Offset: 0x000278C4
		public ICollection Values
		{
			get
			{
				return this.m_list.Values;
			}
		}

		// Token: 0x1700015F RID: 351
		public object this[string s]
		{
			get
			{
				return this.m_list[s];
			}
			set
			{
				object syncRoot = this.SyncRoot;
				lock (syncRoot)
				{
					this.m_list[s] = value;
				}
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00029728 File Offset: 0x00027928
		public IEnumerator GetEnumerator()
		{
			return this.m_list.GetEnumerator();
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x00029735 File Offset: 0x00027935
		public object SyncRoot
		{
			get
			{
				return this.m_list.SyncRoot;
			}
		}

		// Token: 0x04000D13 RID: 3347
		private SortedList m_list = SortedList.Synchronized(new SortedList(PathList.PathListComparer.StaticInstance));

		// Token: 0x020006F4 RID: 1780
		[Serializable]
		private class PathListComparer : IComparer
		{
			// Token: 0x06004057 RID: 16471 RVA: 0x0010DBD0 File Offset: 0x0010BDD0
			int IComparer.Compare(object ol, object or)
			{
				string text = CookieParser.CheckQuoted((string)ol);
				string text2 = CookieParser.CheckQuoted((string)or);
				int length = text.Length;
				int length2 = text2.Length;
				int num = Math.Min(length, length2);
				for (int i = 0; i < num; i++)
				{
					if (text[i] != text2[i])
					{
						return (int)(text[i] - text2[i]);
					}
				}
				return length2 - length;
			}

			// Token: 0x04003081 RID: 12417
			internal static readonly PathList.PathListComparer StaticInstance = new PathList.PathListComparer();
		}
	}
}
