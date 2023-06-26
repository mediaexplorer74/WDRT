using System;

namespace System.Net
{
	// Token: 0x02000196 RID: 406
	internal class BufferOffsetSize
	{
		// Token: 0x06000FAE RID: 4014 RVA: 0x000520F8 File Offset: 0x000502F8
		internal BufferOffsetSize(byte[] buffer, int offset, int size, bool copyBuffer)
		{
			if (copyBuffer)
			{
				byte[] array = new byte[size];
				System.Buffer.BlockCopy(buffer, offset, array, 0, size);
				offset = 0;
				buffer = array;
			}
			this.Buffer = buffer;
			this.Offset = offset;
			this.Size = size;
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0005213B File Offset: 0x0005033B
		internal BufferOffsetSize(byte[] buffer, bool copyBuffer)
			: this(buffer, 0, buffer.Length, copyBuffer)
		{
		}

		// Token: 0x040012CE RID: 4814
		internal byte[] Buffer;

		// Token: 0x040012CF RID: 4815
		internal int Offset;

		// Token: 0x040012D0 RID: 4816
		internal int Size;
	}
}
