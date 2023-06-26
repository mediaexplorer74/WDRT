using System;

namespace System.Runtime.Serialization
{
	// Token: 0x0200074E RID: 1870
	internal class ObjectHolderList
	{
		// Token: 0x060052D8 RID: 21208 RVA: 0x00124586 File Offset: 0x00122786
		internal ObjectHolderList()
			: this(8)
		{
		}

		// Token: 0x060052D9 RID: 21209 RVA: 0x0012458F File Offset: 0x0012278F
		internal ObjectHolderList(int startingSize)
		{
			this.m_count = 0;
			this.m_values = new ObjectHolder[startingSize];
		}

		// Token: 0x060052DA RID: 21210 RVA: 0x001245AC File Offset: 0x001227AC
		internal virtual void Add(ObjectHolder value)
		{
			if (this.m_count == this.m_values.Length)
			{
				this.EnlargeArray();
			}
			ObjectHolder[] values = this.m_values;
			int count = this.m_count;
			this.m_count = count + 1;
			values[count] = value;
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x001245E8 File Offset: 0x001227E8
		internal ObjectHolderListEnumerator GetFixupEnumerator()
		{
			return new ObjectHolderListEnumerator(this, true);
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x001245F4 File Offset: 0x001227F4
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
			ObjectHolder[] array = new ObjectHolder[num];
			Array.Copy(this.m_values, array, this.m_count);
			this.m_values = array;
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x060052DD RID: 21213 RVA: 0x0012464E File Offset: 0x0012284E
		internal int Version
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x060052DE RID: 21214 RVA: 0x00124656 File Offset: 0x00122856
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x040024B5 RID: 9397
		internal const int DefaultInitialSize = 8;

		// Token: 0x040024B6 RID: 9398
		internal ObjectHolder[] m_values;

		// Token: 0x040024B7 RID: 9399
		internal int m_count;
	}
}
