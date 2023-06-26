using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsDeviceRecoveryTool.Core;
using Microsoft.WindowsDeviceRecoveryTool.Core.Cache;

namespace Microsoft.WindowsDeviceRecoveryTool.Lucid.Mtp
{
	// Token: 0x02000008 RID: 8
	[Export(typeof(IMtpDeviceInfoProvider))]
	internal sealed class MtpDeviceInfoProviderCacheDecorator : IMtpDeviceInfoProvider, IDeviceInformationProvider<MtpInterfaceInfo>
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000026BF File Offset: 0x000008BF
		[ImportingConstructor]
		public MtpDeviceInfoProviderCacheDecorator(MtpDeviceInfoProvider deviceInfoProvider, IDeviceInformationCacheManager cacheManager)
		{
			this.deviceInfoProvider = deviceInfoProvider;
			this.cacheManager = cacheManager;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026D5 File Offset: 0x000008D5
		public Task<MtpInterfaceInfo> ReadInformationAsync(string devicePath, CancellationToken token)
		{
			return this.deviceInfoProvider.ReadInformationAsync(devicePath, this.cacheManager, token);
		}

		// Token: 0x04000008 RID: 8
		private readonly MtpDeviceInfoProvider deviceInfoProvider;

		// Token: 0x04000009 RID: 9
		private readonly IDeviceInformationCacheManager cacheManager;
	}
}
