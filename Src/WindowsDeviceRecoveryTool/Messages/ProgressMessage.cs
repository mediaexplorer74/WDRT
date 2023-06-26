using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000AC RID: 172
	public class ProgressMessage
	{
		// Token: 0x0600057E RID: 1406 RVA: 0x0001B666 File Offset: 0x00019866
		public ProgressMessage(int progress, string message)
		{
			this.Progress = progress;
			this.Message = message;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001B680 File Offset: 0x00019880
		public ProgressMessage(int progress, string message, long downloadedSize, long totalSize, double bytesPerSecond, long secondsLeft)
			: this(progress, message)
		{
			this.DownloadedSize = downloadedSize;
			this.TotalSize = totalSize;
			this.BytesPerSecond = bytesPerSecond;
			this.SecondsLeft = secondsLeft;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0001B6AF File Offset: 0x000198AF
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x0001B6B7 File Offset: 0x000198B7
		public int Progress { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0001B6C0 File Offset: 0x000198C0
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x0001B6C8 File Offset: 0x000198C8
		public string Message { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0001B6D1 File Offset: 0x000198D1
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x0001B6D9 File Offset: 0x000198D9
		public long TotalSize { get; private set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0001B6E2 File Offset: 0x000198E2
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x0001B6EA File Offset: 0x000198EA
		public long DownloadedSize { get; private set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0001B6F3 File Offset: 0x000198F3
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x0001B6FB File Offset: 0x000198FB
		public long SecondsLeft { get; private set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0001B704 File Offset: 0x00019904
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x0001B70C File Offset: 0x0001990C
		public double BytesPerSecond { get; private set; }
	}
}
