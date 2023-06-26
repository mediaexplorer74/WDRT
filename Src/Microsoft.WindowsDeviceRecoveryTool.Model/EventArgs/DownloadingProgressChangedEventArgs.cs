using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs
{
	// Token: 0x0200001F RID: 31
	public class DownloadingProgressChangedEventArgs
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x000061B1 File Offset: 0x000043B1
		public DownloadingProgressChangedEventArgs(int percentage, long downloadedSize, long totalSize, double bytesPerSecond, long secondsLeft, string message = null)
		{
			this.Percentage = percentage;
			this.Message = message;
			this.DownloadedSize = downloadedSize;
			this.TotalSize = totalSize;
			this.BytesPerSecond = bytesPerSecond;
			this.SecondsLeft = secondsLeft;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000061EE File Offset: 0x000043EE
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x000061F6 File Offset: 0x000043F6
		public int Percentage { get; private set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x000061FF File Offset: 0x000043FF
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00006207 File Offset: 0x00004407
		public string Message { get; private set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00006210 File Offset: 0x00004410
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00006218 File Offset: 0x00004418
		public long TotalSize { get; private set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00006221 File Offset: 0x00004421
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00006229 File Offset: 0x00004429
		public long DownloadedSize { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00006232 File Offset: 0x00004432
		// (set) Token: 0x060001CC RID: 460 RVA: 0x0000623A File Offset: 0x0000443A
		public long SecondsLeft { get; private set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00006243 File Offset: 0x00004443
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000624B File Offset: 0x0000444B
		public double BytesPerSecond { get; private set; }
	}
}
