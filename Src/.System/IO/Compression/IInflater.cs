using System;

namespace System.IO.Compression
{
	// Token: 0x02000420 RID: 1056
	internal interface IInflater : IDisposable
	{
		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x0600276C RID: 10092
		int AvailableOutput { get; }

		// Token: 0x0600276D RID: 10093
		int Inflate(byte[] bytes, int offset, int length);

		// Token: 0x0600276E RID: 10094
		bool Finished();

		// Token: 0x0600276F RID: 10095
		bool NeedsInput();

		// Token: 0x06002770 RID: 10096
		void SetInput(byte[] inputBytes, int offset, int length);
	}
}
