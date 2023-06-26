using System;

namespace System.IO.Compression
{
	// Token: 0x0200042C RID: 1068
	internal interface IFileFormatReader
	{
		// Token: 0x06002802 RID: 10242
		bool ReadHeader(InputBuffer input);

		// Token: 0x06002803 RID: 10243
		bool ReadFooter(InputBuffer input);

		// Token: 0x06002804 RID: 10244
		void UpdateWithBytesRead(byte[] buffer, int offset, int bytesToCopy);

		// Token: 0x06002805 RID: 10245
		void Validate();
	}
}
