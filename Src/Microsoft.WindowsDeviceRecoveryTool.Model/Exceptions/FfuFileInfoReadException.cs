using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	public class FfuFileInfoReadException : Exception
	{
		// Token: 0x06000213 RID: 531 RVA: 0x000063FB File Offset: 0x000045FB
		public FfuFileInfoReadException(int errorCode, string path)
			: base(string.Format("WP8 FFU file reading failed with error {0}, file path: {1}.", errorCode, path))
		{
			this.ErrorCode = errorCode;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000641E File Offset: 0x0000461E
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00006426 File Offset: 0x00004626
		public int ErrorCode { get; private set; }
	}
}
