using System;
using System.IO;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services.DataPackageRules
{
	// Token: 0x02000014 RID: 20
	internal class FileHelper
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00005880 File Offset: 0x00003A80
		public string[] GetFilesFromDirectory(string directory, string searchPattern)
		{
			return Directory.GetFiles(directory, searchPattern);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000589C File Offset: 0x00003A9C
		public Stream GetFileStream(string path)
		{
			return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
		}
	}
}
