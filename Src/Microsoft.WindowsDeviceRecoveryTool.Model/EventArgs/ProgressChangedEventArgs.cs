using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs
{
	// Token: 0x02000020 RID: 32
	public class ProgressChangedEventArgs
	{
		// Token: 0x060001CF RID: 463 RVA: 0x00006254 File Offset: 0x00004454
		public ProgressChangedEventArgs(int percentage, string message = null)
		{
			this.Percentage = percentage;
			this.Message = message;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000626E File Offset: 0x0000446E
		public ProgressChangedEventArgs(int percentage, string message, long downloadedSize, long totalSize, double bytesPerSecond, long secondsLeft)
			: this(percentage, message)
		{
			this.DownloadedSize = downloadedSize;
			this.TotalSize = totalSize;
			this.BytesPerSecond = bytesPerSecond;
			this.SecondsLeft = secondsLeft;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000629D File Offset: 0x0000449D
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x000062A5 File Offset: 0x000044A5
		public int Percentage { get; private set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000062AE File Offset: 0x000044AE
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x000062B6 File Offset: 0x000044B6
		public string Message { get; private set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000062BF File Offset: 0x000044BF
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x000062C7 File Offset: 0x000044C7
		public long TotalSize { get; private set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x000062D0 File Offset: 0x000044D0
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x000062D8 File Offset: 0x000044D8
		public long DownloadedSize { get; private set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000062E1 File Offset: 0x000044E1
		// (set) Token: 0x060001DA RID: 474 RVA: 0x000062E9 File Offset: 0x000044E9
		public long SecondsLeft { get; private set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000062F2 File Offset: 0x000044F2
		// (set) Token: 0x060001DC RID: 476 RVA: 0x000062FA File Offset: 0x000044FA
		public double BytesPerSecond { get; private set; }
	}
}
