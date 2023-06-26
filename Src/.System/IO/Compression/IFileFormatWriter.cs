using System;

namespace System.IO.Compression
{
	// Token: 0x0200042B RID: 1067
	internal interface IFileFormatWriter
	{
		// Token: 0x060027FF RID: 10239
		byte[] GetHeader();

		// Token: 0x06002800 RID: 10240
		void UpdateWithBytesRead(byte[] buffer, int offset, int bytesToCopy);

		// Token: 0x06002801 RID: 10241
		byte[] GetFooter();
	}
}
