using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Messages
{
	// Token: 0x020000A7 RID: 167
	public class PackageDirectoryMessage
	{
		// Token: 0x06000563 RID: 1379 RVA: 0x0001B516 File Offset: 0x00019716
		public PackageDirectoryMessage(string directory)
		{
			this.Directory = directory;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x0001B528 File Offset: 0x00019728
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x0001B530 File Offset: 0x00019730
		public string Directory { get; private set; }
	}
}
