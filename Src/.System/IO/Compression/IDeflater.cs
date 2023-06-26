using System;

namespace System.IO.Compression
{
	// Token: 0x0200041F RID: 1055
	internal interface IDeflater : IDisposable
	{
		// Token: 0x06002768 RID: 10088
		bool NeedsInput();

		// Token: 0x06002769 RID: 10089
		void SetInput(byte[] inputBuffer, int startIndex, int count);

		// Token: 0x0600276A RID: 10090
		int GetDeflateOutput(byte[] outputBuffer);

		// Token: 0x0600276B RID: 10091
		bool Finish(byte[] outputBuffer, out int bytesRead);
	}
}
