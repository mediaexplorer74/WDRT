using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Net;
using Microsoft.WindowsDeviceRecoveryTool.BusinessLogic.Services;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Contracts;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Services;
using Microsoft.WindowsDeviceRecoveryTool.LumiaAdaptation.Services;

namespace Microsoft.WindowsDeviceRecoveryTool.BusinessLogic
{
	// Token: 0x02000002 RID: 2
	[Export]
	public class LogicContext : IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[ImportingConstructor]
		public LogicContext(CompositionContainer container)
		{
			try
			{
				container.SatisfyImportsOnce(this);
				this.ffuFileInfoService = container.GetExportedValue<FfuFileInfoService>();
				this.lumiaAdaptation = container.GetExportedValue<LumiaAdaptation>();
				this.adaptationManager = container.GetExportedValue<AdaptationManager>();
				this.autoUpdateService = container.GetExportedValue<AutoUpdateService>();
				this.reportingService = container.GetExportedValue<ReportingService>();
				this.msrReportingService = container.GetExportedValue<MsrReportingService>();
				this.msrReportingService.ManufacturerDataProvider = this.AdaptationManager;
				this.thor2Service = container.GetExportedValue<Thor2Service>();
				this.InitializeAdaptationManager();
			}
			catch (Exception ex)
			{
				Tracer<LogicContext>.WriteError(ex);
				throw;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020FC File Offset: 0x000002FC
		~LogicContext()
		{
			this.Dispose(false);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002130 File Offset: 0x00000330
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002138 File Offset: 0x00000338
		[ImportMany]
		public IEnumerable<Lazy<IAdaptation, IExportAdaptationMetadata>> AdaptationsWithMetadata { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002141 File Offset: 0x00000341
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002149 File Offset: 0x00000349
		[ImportMany]
		private IEnumerable<Lazy<IUseProxy>> UsingProxyServices { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002154 File Offset: 0x00000354
		public FfuFileInfoService FfuFileInfoService
		{
			get
			{
				FfuFileInfoService ffuFileInfoService = this.ffuFileInfoService;
				FfuFileInfoService ffuFileInfoService2;
				lock (ffuFileInfoService)
				{
					ffuFileInfoService2 = this.ffuFileInfoService;
				}
				return ffuFileInfoService2;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000219C File Offset: 0x0000039C
		public LumiaAdaptation LumiaAdaptation
		{
			get
			{
				LumiaAdaptation lumiaAdaptation = this.lumiaAdaptation;
				LumiaAdaptation lumiaAdaptation2;
				lock (lumiaAdaptation)
				{
					lumiaAdaptation2 = this.lumiaAdaptation;
				}
				return lumiaAdaptation2;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021E4 File Offset: 0x000003E4
		public AdaptationManager AdaptationManager
		{
			get
			{
				AdaptationManager adaptationManager = this.adaptationManager;
				AdaptationManager adaptationManager2;
				lock (adaptationManager)
				{
					adaptationManager2 = this.adaptationManager;
				}
				return adaptationManager2;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000222C File Offset: 0x0000042C
		public ReportingService ReportingService
		{
			get
			{
				ReportingService reportingService = this.reportingService;
				ReportingService reportingService2;
				lock (reportingService)
				{
					reportingService2 = this.reportingService;
				}
				return reportingService2;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002274 File Offset: 0x00000474
		public MsrReportingService MsrReportingService
		{
			get
			{
				MsrReportingService msrReportingService = this.msrReportingService;
				MsrReportingService msrReportingService2;
				lock (msrReportingService)
				{
					msrReportingService2 = this.msrReportingService;
				}
				return msrReportingService2;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000022BC File Offset: 0x000004BC
		public AutoUpdateService AutoUpdateService
		{
			get
			{
				AutoUpdateService autoUpdateService = this.autoUpdateService;
				AutoUpdateService autoUpdateService2;
				lock (autoUpdateService)
				{
					autoUpdateService2 = this.autoUpdateService;
				}
				return autoUpdateService2;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002304 File Offset: 0x00000504
		public Thor2Service Thor2Service
		{
			get
			{
				Thor2Service thor2Service = this.thor2Service;
				Thor2Service thor2Service2;
				lock (thor2Service)
				{
					thor2Service2 = this.thor2Service;
				}
				return thor2Service2;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000234C File Offset: 0x0000054C
		private void InitializeAdaptationManager()
		{
			this.AdaptationManager.AddAdaptation(this.lumiaAdaptation);
			foreach (Lazy<IAdaptation, IExportAdaptationMetadata> lazy in this.AdaptationsWithMetadata)
			{
				this.AdaptationManager.AddAdaptation((BaseAdaptation)lazy.Value);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000023C0 File Offset: 0x000005C0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023D4 File Offset: 0x000005D4
		public void SetProxy(IWebProxy proxy)
		{
			foreach (Lazy<IUseProxy> lazy in this.UsingProxyServices)
			{
				lazy.Value.SetProxy(proxy);
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000242C File Offset: 0x0000062C
		private void Dispose(bool disposing)
		{
			bool flag = this.disposed;
			if (!flag)
			{
				if (disposing)
				{
					this.AdaptationManager.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly FfuFileInfoService ffuFileInfoService;

		// Token: 0x04000002 RID: 2
		private readonly LumiaAdaptation lumiaAdaptation;

		// Token: 0x04000003 RID: 3
		private readonly AdaptationManager adaptationManager;

		// Token: 0x04000004 RID: 4
		private readonly AutoUpdateService autoUpdateService;

		// Token: 0x04000005 RID: 5
		private readonly ReportingService reportingService;

		// Token: 0x04000006 RID: 6
		private readonly MsrReportingService msrReportingService;

		// Token: 0x04000007 RID: 7
		private readonly Thor2Service thor2Service;

		// Token: 0x04000008 RID: 8
		private bool disposed;
	}
}
