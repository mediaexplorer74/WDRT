using System;

namespace System.IO.Compression
{
	// Token: 0x02000430 RID: 1072
	internal class GZipFormatter : IFileFormatWriter
	{
		// Token: 0x06002821 RID: 10273 RVA: 0x000B83D7 File Offset: 0x000B65D7
		internal GZipFormatter()
			: this(3)
		{
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x000B83E0 File Offset: 0x000B65E0
		internal GZipFormatter(int compressionLevel)
		{
			if (compressionLevel == 10)
			{
				this.headerBytes[8] = 2;
			}
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x000B840E File Offset: 0x000B660E
		public byte[] GetHeader()
		{
			return this.headerBytes;
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x000B8418 File Offset: 0x000B6618
		public void UpdateWithBytesRead(byte[] buffer, int offset, int bytesToCopy)
		{
			this._crc32 = Crc32Helper.UpdateCrc32(this._crc32, buffer, offset, bytesToCopy);
			long num = this._inputStreamSizeModulo + (long)((ulong)bytesToCopy);
			if (num >= 4294967296L)
			{
				num %= 4294967296L;
			}
			this._inputStreamSizeModulo = num;
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x000B8464 File Offset: 0x000B6664
		public byte[] GetFooter()
		{
			byte[] array = new byte[8];
			this.WriteUInt32(array, this._crc32, 0);
			this.WriteUInt32(array, (uint)this._inputStreamSizeModulo, 4);
			return array;
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x000B8496 File Offset: 0x000B6696
		internal void WriteUInt32(byte[] b, uint value, int startIndex)
		{
			b[startIndex] = (byte)value;
			b[startIndex + 1] = (byte)(value >> 8);
			b[startIndex + 2] = (byte)(value >> 16);
			b[startIndex + 3] = (byte)(value >> 24);
		}

		// Token: 0x040021CF RID: 8655
		private byte[] headerBytes = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 0 };

		// Token: 0x040021D0 RID: 8656
		private uint _crc32;

		// Token: 0x040021D1 RID: 8657
		private long _inputStreamSizeModulo;
	}
}
