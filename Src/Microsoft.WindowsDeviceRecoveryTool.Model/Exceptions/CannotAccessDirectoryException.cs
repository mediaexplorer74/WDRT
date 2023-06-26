using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	public class CannotAccessDirectoryException : Exception
	{
		// Token: 0x06000207 RID: 519 RVA: 0x00006350 File Offset: 0x00004550
		public CannotAccessDirectoryException(string path)
			: base(path)
		{
		}
	}
}
