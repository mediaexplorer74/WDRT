using System;
using System.Threading.Tasks;

namespace Microsoft.WindowsDeviceRecoveryTool.Core.Cache
{
	// Token: 0x0200000B RID: 11
	public interface IDevicePathBasedCacheObject
	{
		// Token: 0x0600002D RID: 45
		bool TryGetReadInformationTaskForReader<T>(Type readerType, out Task<T> readInformationTask);

		// Token: 0x0600002E RID: 46
		void AddReadInformationTaskForReader<T>(Type readerType, Task<T> readInformationTask);
	}
}
