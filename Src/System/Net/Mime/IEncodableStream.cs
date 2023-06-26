using System;
using System.IO;

namespace System.Net.Mime
{
	// Token: 0x02000246 RID: 582
	internal interface IEncodableStream
	{
		// Token: 0x06001608 RID: 5640
		int DecodeBytes(byte[] buffer, int offset, int count);

		// Token: 0x06001609 RID: 5641
		int EncodeBytes(byte[] buffer, int offset, int count);

		// Token: 0x0600160A RID: 5642
		string GetEncodedString();

		// Token: 0x0600160B RID: 5643
		Stream GetStream();
	}
}
