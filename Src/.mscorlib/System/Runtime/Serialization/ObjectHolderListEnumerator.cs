using System;

namespace System.Runtime.Serialization
{
	// Token: 0x0200074F RID: 1871
	internal class ObjectHolderListEnumerator
	{
		// Token: 0x060052DF RID: 21215 RVA: 0x0012465E File Offset: 0x0012285E
		internal ObjectHolderListEnumerator(ObjectHolderList list, bool isFixupEnumerator)
		{
			this.m_list = list;
			this.m_startingVersion = this.m_list.Version;
			this.m_currPos = -1;
			this.m_isFixupEnumerator = isFixupEnumerator;
		}

		// Token: 0x060052E0 RID: 21216 RVA: 0x0012468C File Offset: 0x0012288C
		internal bool MoveNext()
		{
			if (this.m_isFixupEnumerator)
			{
				int num;
				do
				{
					num = this.m_currPos + 1;
					this.m_currPos = num;
				}
				while (num < this.m_list.Count && this.m_list.m_values[this.m_currPos].CompletelyFixed);
				return this.m_currPos != this.m_list.Count;
			}
			this.m_currPos++;
			return this.m_currPos != this.m_list.Count;
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x060052E1 RID: 21217 RVA: 0x00124713 File Offset: 0x00122913
		internal ObjectHolder Current
		{
			get
			{
				return this.m_list.m_values[this.m_currPos];
			}
		}

		// Token: 0x040024B8 RID: 9400
		private bool m_isFixupEnumerator;

		// Token: 0x040024B9 RID: 9401
		private ObjectHolderList m_list;

		// Token: 0x040024BA RID: 9402
		private int m_startingVersion;

		// Token: 0x040024BB RID: 9403
		private int m_currPos;
	}
}
