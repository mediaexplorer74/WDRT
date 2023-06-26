using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.BusinessLogic;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;

namespace Microsoft.WindowsDeviceRecoveryTool.Controllers
{
	// Token: 0x02000092 RID: 146
	[Export("Microsoft.WindowsDeviceRecoveryTool.Controllers.FfuController", typeof(IController))]
	public class FfuController : BaseController
	{
		// Token: 0x06000506 RID: 1286 RVA: 0x0001A725 File Offset: 0x00018925
		[ImportingConstructor]
		public FfuController(ICommandRepository commandRepository, LogicContext logics, EventAggregator eventAggragator)
			: base(commandRepository, eventAggragator)
		{
			this.logics = logics;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001A738 File Offset: 0x00018938
		[CustomCommand(IsAsynchronous = true)]
		public void ReadFfuFilePlatformId(string ffuFilePath, CancellationToken token)
		{
			this.logics.FfuFileInfoService.ClearDataForFfuFile(ffuFilePath);
			PlatformId platformId = this.logics.FfuFileInfoService.ReadFfuFilePlatformId(ffuFilePath);
			string text = null;
			bool flag = !this.logics.FfuFileInfoService.TryReadFfuSoftwareVersion(ffuFilePath, out text);
			if (flag)
			{
				Tracer<FfuController>.WriteWarning("Could not read ffu software version: {0}", new object[] { ffuFilePath });
			}
			IEnumerable<PlatformId> enumerable = new List<PlatformId> { platformId };
			bool flag2 = !this.logics.FfuFileInfoService.TryReadAllFfuPlatformIds(ffuFilePath, out enumerable);
			if (flag2)
			{
				Tracer<FfuController>.WriteWarning("Could not read ffu platform ids list: {0}", new object[] { ffuFilePath });
			}
			base.EventAggregator.Publish<FfuFilePlatformIdMessage>(new FfuFilePlatformIdMessage(platformId, text)
			{
				AllPlatformIds = enumerable
			});
		}

		// Token: 0x04000235 RID: 565
		private readonly LogicContext logics;
	}
}
