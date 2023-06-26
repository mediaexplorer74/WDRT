using System;

namespace System.Runtime.Serialization
{
	// Token: 0x0200074D RID: 1869
	[Serializable]
	internal class LongList
	{
		// Token: 0x060052CF RID: 21199 RVA: 0x0012440A File Offset: 0x0012260A
		internal LongList()
			: this(2)
		{
		}

		// Token: 0x060052D0 RID: 21200 RVA: 0x00124413 File Offset: 0x00122613
		internal LongList(int startingSize)
		{
			this.m_count = 0;
			this.m_totalItems = 0;
			this.m_values = new long[startingSize];
		}

		// Token: 0x060052D1 RID: 21201 RVA: 0x00124438 File Offset: 0x00122638
		internal void Add(long value)
		{
			if (this.m_totalItems == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			long[] values = this.m_values;
			int totalItems = this.m_totalItems;
			this.m_totalItems = totalItems + 1;
			values[totalItems] = value;
			this.m_count++;
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x060052D2 RID: 21202 RVA: 0x00124482 File Offset: 0x00122682
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x060052D3 RID: 21203 RVA: 0x0012448A File Offset: 0x0012268A
		internal void StartEnumeration()
		{
			this.m_currentItem = -1;
		}

		// Token: 0x060052D4 RID: 21204 RVA: 0x00124494 File Offset: 0x00122694
		internal bool MoveNext()
		{
			int num;
			do
			{
				num = this.m_currentItem + 1;
				this.m_currentItem = num;
			}
			while (num < this.m_totalItems && this.m_values[this.m_currentItem] == -1L);
			return this.m_currentItem != this.m_totalItems;
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x060052D5 RID: 21205 RVA: 0x001244DC File Offset: 0x001226DC
		internal long Current
		{
			get
			{
				return this.m_values[this.m_currentItem];
			}
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x001244EC File Offset: 0x001226EC
		internal bool RemoveElement(long value)
		{
			int num = 0;
			while (num < this.m_totalItems && this.m_values[num] != value)
			{
				num++;
			}
			if (num == this.m_totalItems)
			{
				return false;
			}
			this.m_values[num] = -1L;
			return true;
		}

		// Token: 0x060052D7 RID: 21207 RVA: 0x0012452C File Offset: 0x0012272C
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
			long[] array = new long[num];
			Array.Copy(this.m_values, array, this.m_count);
			this.m_values = array;
		}

		// Token: 0x040024B0 RID: 9392
		private const int InitialSize = 2;

		// Token: 0x040024B1 RID: 9393
		private long[] m_values;

		// Token: 0x040024B2 RID: 9394
		private int m_count;

		// Token: 0x040024B3 RID: 9395
		private int m_totalItems;

		// Token: 0x040024B4 RID: 9396
		private int m_currentItem;
	}
}
