using System;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000259 RID: 601
	internal class BufferBuilder
	{
		// Token: 0x060016E1 RID: 5857 RVA: 0x00075DD5 File Offset: 0x00073FD5
		internal BufferBuilder()
			: this(256)
		{
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00075DE2 File Offset: 0x00073FE2
		internal BufferBuilder(int initialSize)
		{
			this.buffer = new byte[initialSize];
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00075DF8 File Offset: 0x00073FF8
		private void EnsureBuffer(int count)
		{
			if (count > this.buffer.Length - this.offset)
			{
				byte[] array = new byte[(this.buffer.Length * 2 > this.buffer.Length + count) ? (this.buffer.Length * 2) : (this.buffer.Length + count)];
				Buffer.BlockCopy(this.buffer, 0, array, 0, this.offset);
				this.buffer = array;
			}
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00075E64 File Offset: 0x00074064
		internal void Append(byte value)
		{
			this.EnsureBuffer(1);
			byte[] array = this.buffer;
			int num = this.offset;
			this.offset = num + 1;
			array[num] = value;
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00075E91 File Offset: 0x00074091
		internal void Append(byte[] value)
		{
			this.Append(value, 0, value.Length);
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00075E9E File Offset: 0x0007409E
		internal void Append(byte[] value, int offset, int count)
		{
			this.EnsureBuffer(count);
			Buffer.BlockCopy(value, offset, this.buffer, this.offset, count);
			this.offset += count;
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00075EC9 File Offset: 0x000740C9
		internal void Append(string value)
		{
			this.Append(value, false);
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00075ED3 File Offset: 0x000740D3
		internal void Append(string value, bool allowUnicode)
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			this.Append(value, 0, value.Length, allowUnicode);
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00075EF0 File Offset: 0x000740F0
		internal void Append(string value, int offset, int count, bool allowUnicode)
		{
			if (allowUnicode)
			{
				byte[] bytes = Encoding.UTF8.GetBytes(value.ToCharArray(), offset, count);
				this.Append(bytes);
				return;
			}
			this.Append(value, offset, count);
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00075F28 File Offset: 0x00074128
		internal void Append(string value, int offset, int count)
		{
			this.EnsureBuffer(count);
			for (int i = 0; i < count; i++)
			{
				char c = value[offset + i];
				if (c > 'ÿ')
				{
					throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter", new object[] { c }));
				}
				this.buffer[this.offset + i] = (byte)c;
			}
			this.offset += count;
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x00075F98 File Offset: 0x00074198
		internal int Length
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x00075FA0 File Offset: 0x000741A0
		internal byte[] GetBuffer()
		{
			return this.buffer;
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00075FA8 File Offset: 0x000741A8
		internal void Reset()
		{
			this.offset = 0;
		}

		// Token: 0x0400175D RID: 5981
		private byte[] buffer;

		// Token: 0x0400175E RID: 5982
		private int offset;
	}
}
