﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace SoftwareRepository.Streaming
{
	// Token: 0x02000011 RID: 17
	[SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
	public struct DownloadProgressInfo
	{
		// Token: 0x06000063 RID: 99 RVA: 0x000035D8 File Offset: 0x000017D8
		public DownloadProgressInfo(long bytesReceived, long totalBytes, string fileName)
		{
			this.BytesReceived = bytesReceived;
			this.TotalBytes = totalBytes;
			this.FileName = fileName;
		}

		// Token: 0x04000040 RID: 64
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Performance")]
		public readonly long BytesReceived;

		// Token: 0x04000041 RID: 65
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Performance")]
		public readonly string FileName;

		// Token: 0x04000042 RID: 66
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Performance")]
		public readonly long TotalBytes;
	}
}
