using System;
using System.IO;

namespace System.Data.Services.Client
{
	// Token: 0x0200005D RID: 93
	internal class DataServiceSaveStream
	{
		// Token: 0x06000315 RID: 789 RVA: 0x0000E04A File Offset: 0x0000C24A
		internal DataServiceSaveStream(Stream stream, bool close, DataServiceRequestArgs args)
		{
			this.stream = stream;
			this.close = close;
			this.args = args;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000E067 File Offset: 0x0000C267
		internal Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000E06F File Offset: 0x0000C26F
		// (set) Token: 0x06000318 RID: 792 RVA: 0x0000E077 File Offset: 0x0000C277
		internal DataServiceRequestArgs Args
		{
			get
			{
				return this.args;
			}
			set
			{
				this.args = value;
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000E080 File Offset: 0x0000C280
		internal void Close()
		{
			if (this.stream != null && this.close)
			{
				this.stream.Close();
			}
		}

		// Token: 0x0400027D RID: 637
		private DataServiceRequestArgs args;

		// Token: 0x0400027E RID: 638
		private Stream stream;

		// Token: 0x0400027F RID: 639
		private bool close;
	}
}
