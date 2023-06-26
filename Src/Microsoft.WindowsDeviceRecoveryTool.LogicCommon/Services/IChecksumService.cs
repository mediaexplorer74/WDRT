using System;
using System.IO;
using System.Threading;

namespace Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services
{
	// Token: 0x0200000A RID: 10
	public interface IChecksumService
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000076 RID: 118
		// (remove) Token: 0x06000077 RID: 119
		event Action<int> ProgressEvent;

		// Token: 0x06000078 RID: 120
		bool IsOfType(string checksumTypeName);

		// Token: 0x06000079 RID: 121
		byte[] CalculateChecksum(string filePath, CancellationToken cancellationToken);

		// Token: 0x0600007A RID: 122
		byte[] CalculateChecksum(FileStream fileStream, CancellationToken cancellationToken);
	}
}
