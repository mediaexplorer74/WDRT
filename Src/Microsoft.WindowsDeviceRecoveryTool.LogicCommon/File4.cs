using System;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon
{
	// Token: 0x02000004 RID: 4
	public sealed class File4
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002670 File Offset: 0x00000870
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002688 File Offset: 0x00000888
		public string RelativePath
		{
			get
			{
				return this.relativePath;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000026A0 File Offset: 0x000008A0
		public long FileSize
		{
			get
			{
				return this.fileSize;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000026B8 File Offset: 0x000008B8
		public string DownloadUrl
		{
			get
			{
				return this.downloadUrl;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000026D0 File Offset: 0x000008D0
		public string Checksum
		{
			get
			{
				return this.checksum;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000026E8 File Offset: 0x000008E8
		public int ChecksumType
		{
			get
			{
				return this.checksumType;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002700 File Offset: 0x00000900
		public File4(string fileName, string relativePath, long fileSize, string downloadUrl, string checksum, int checksumType)
		{
			this.fileName = fileName;
			this.relativePath = relativePath;
			this.fileSize = fileSize;
			this.downloadUrl = downloadUrl;
			this.checksum = checksum;
			this.checksumType = checksumType;
		}

		// Token: 0x04000010 RID: 16
		private readonly string fileName;

		// Token: 0x04000011 RID: 17
		private readonly string relativePath;

		// Token: 0x04000012 RID: 18
		private readonly long fileSize;

		// Token: 0x04000013 RID: 19
		private readonly string downloadUrl;

		// Token: 0x04000014 RID: 20
		private readonly string checksum;

		// Token: 0x04000015 RID: 21
		private readonly int checksumType;
	}
}
