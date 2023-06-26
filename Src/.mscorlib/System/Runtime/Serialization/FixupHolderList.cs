using System;

namespace System.Runtime.Serialization
{
	// Token: 0x0200074C RID: 1868
	[Serializable]
	internal class FixupHolderList
	{
		// Token: 0x060052CA RID: 21194 RVA: 0x001242FB File Offset: 0x001224FB
		internal FixupHolderList()
			: this(2)
		{
		}

		// Token: 0x060052CB RID: 21195 RVA: 0x00124304 File Offset: 0x00122504
		internal FixupHolderList(int startingSize)
		{
			this.m_count = 0;
			this.m_values = new FixupHolder[startingSize];
		}

		// Token: 0x060052CC RID: 21196 RVA: 0x00124320 File Offset: 0x00122520
		internal virtual void Add(long id, object fixupInfo)
		{
			if (this.m_count == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			this.m_values[this.m_count].m_id = id;
			FixupHolder[] values = this.m_values;
			int count = this.m_count;
			this.m_count = count + 1;
			values[count].m_fixupInfo = fixupInfo;
		}

		// Token: 0x060052CD RID: 21197 RVA: 0x00124374 File Offset: 0x00122574
		internal virtual void Add(FixupHolder fixup)
		{
			if (this.m_count == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			FixupHolder[] values = this.m_values;
			int count = this.m_count;
			this.m_count = count + 1;
			values[count] = fixup;
		}

		// Token: 0x060052CE RID: 21198 RVA: 0x001243B0 File Offset: 0x001225B0
		private void EnlargeArray()
		{
			int num = this.m_values.Length * 2;
			if (num < 0)
			{
				if (num == 2147483647)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_TooManyElements"));
				}
				num = int.MaxValue;
			}
			FixupHolder[] array = new FixupHolder[num];
			Array.Copy(this.m_values, array, this.m_count);
			this.m_values = array;
		}

		// Token: 0x040024AD RID: 9389
		internal const int InitialSize = 2;

		// Token: 0x040024AE RID: 9390
		internal FixupHolder[] m_values;

		// Token: 0x040024AF RID: 9391
		internal int m_count;
	}
}
