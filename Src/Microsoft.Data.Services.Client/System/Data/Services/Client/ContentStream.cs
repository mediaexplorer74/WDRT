using System;
using System.IO;

namespace System.Data.Services.Client
{
	// Token: 0x02000033 RID: 51
	internal sealed class ContentStream
	{
		// Token: 0x06000187 RID: 391 RVA: 0x00008F1A File Offset: 0x0000711A
		public ContentStream(Stream stream, bool isKnownMemoryStream)
		{
			this.stream = stream;
			this.isKnownMemoryStream = isKnownMemoryStream;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00008F30 File Offset: 0x00007130
		public Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00008F38 File Offset: 0x00007138
		public bool IsKnownMemoryStream
		{
			get
			{
				return this.isKnownMemoryStream;
			}
		}

		// Token: 0x040001F5 RID: 501
		private readonly Stream stream;

		// Token: 0x040001F6 RID: 502
		private readonly bool isKnownMemoryStream;
	}
}
