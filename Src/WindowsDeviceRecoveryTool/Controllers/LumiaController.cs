using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.BusinessLogic;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Helpers;
using Microsoft.WindowsDeviceRecoveryTool.LogicCommon.Reporting.Enums;
using Microsoft.WindowsDeviceRecoveryTool.LumiaAdaptation.Services;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;
using Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs;
using Microsoft.WindowsDeviceRecoveryTool.Model.Exceptions;

namespace Microsoft.WindowsDeviceRecoveryTool.Controllers
{
	// Token: 0x02000091 RID: 145
	[Export("Microsoft.WindowsDeviceRecoveryTool.Controllers.LumiaController", typeof(IController))]
	public class LumiaController : BaseController
	{
		// Token: 0x060004F5 RID: 1269 RVA: 0x00019CAC File Offset: 0x00017EAC
		[ImportingConstructor]
		public LumiaController(ICommandRepository commandRepository, Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext, LogicContext logics, EventAggregator eventAggregator)
			: base(commandRepository, eventAggregator)
		{
			this.appContext = appContext;
			this.logics = logics;
			this.logics.LumiaAdaptation.DeviceDisconnected += this.LumiaServiceOnDeviceDisconnectedEvent;
			this.logics.LumiaAdaptation.DeviceConnected += this.LumiaServiceOnNewDeviceConnectedEvent;
			this.logics.LumiaAdaptation.DeviceReadyChanged += this.LumiaAdaptationDeviceReadyChanged;
			this.logics.AdaptationManager.ProgressChanged += this.OnCurrentOperationProgressChanged;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00018F2C File Offset: 0x0001712C
		private void OnCurrentOperationProgressChanged(Microsoft.WindowsDeviceRecoveryTool.Model.EventArgs.ProgressChangedEventArgs progressMessage)
		{
			base.EventAggregator.Publish<ProgressMessage>(new ProgressMessage(progressMessage.Percentage, progressMessage.Message, progressMessage.DownloadedSize, progressMessage.TotalSize, progressMessage.BytesPerSecond, progressMessage.SecondsLeft));
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00019D58 File Offset: 0x00017F58
		private void LumiaServiceOnNewDeviceConnectedEvent(Phone phone)
		{
			this.appContext.CurrentPhone = phone;
			bool flag = this.currentDetectionType == DetectionType.RecoveryModeAfterEmergencyFlashing;
			if (flag)
			{
				base.Commands.Run((FlowController c) => c.FinishAwaitRecoveryAfterEmergency(false, CancellationToken.None));
			}
			else
			{
				bool flag2 = this.currentDetectionType == DetectionType.RecoveryMode && this.appContext.CurrentPhone.IsDeviceInEmergencyMode();
				if (flag2)
				{
					base.EventAggregator.Publish<SwitchStateMessage>(new SwitchStateMessage("ManualDeviceTypeSelectionState"));
				}
				else
				{
					base.EventAggregator.Publish<DeviceConnectedMessage>(new DeviceConnectedMessage(phone));
					CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
					List<CancellationTokenSource> list = this.readDeviceInfoCtsList;
					lock (list)
					{
						this.readDeviceInfoCtsList.Add(cancellationTokenSource);
					}
					bool deviceReady = phone.DeviceReady;
					if (deviceReady)
					{
						this.ReadDeviceInfo(cancellationTokenSource.Token);
					}
				}
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00019EB4 File Offset: 0x000180B4
		private void TryFinishWaitingForDevice()
		{
			bool flag = this.appContext.CurrentPhone != null && this.appContext.CurrentPhone.DeviceReady;
			if (flag)
			{
				base.EventAggregator.Publish<SwitchStateMessage>(new SwitchStateMessage("CheckLatestPackageState"));
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00019F00 File Offset: 0x00018100
		private void LumiaServiceOnDeviceDisconnectedEvent(Phone phone)
		{
			bool flag = this.appContext.CurrentPhone != null && this.appContext.CurrentPhone.PortId == phone.PortId;
			if (flag)
			{
				base.EventAggregator.Publish<DeviceConnectedMessage>(new DeviceConnectedMessage(phone));
				this.appContext.CurrentPhone = null;
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00019F60 File Offset: 0x00018160
		private void LumiaAdaptationDeviceReadyChanged(Phone phone)
		{
			bool flag = this.appContext.CurrentPhone != null && this.appContext.CurrentPhone.PortId == phone.PortId;
			if (flag)
			{
				this.appContext.CurrentPhone = phone;
				bool deviceReady = phone.DeviceReady;
				if (deviceReady)
				{
					CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
					List<CancellationTokenSource> list = this.readDeviceInfoCtsList;
					lock (list)
					{
						this.readDeviceInfoCtsList.Add(cancellationTokenSource);
					}
					this.ReadDeviceInfo(cancellationTokenSource.Token);
					base.EventAggregator.Publish<DeviceConnectedMessage>(new DeviceConnectedMessage(phone));
				}
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001A020 File Offset: 0x00018220
		[CustomCommand(IsAsynchronous = true)]
		public void TryReadMissingInfoWithThor(object parameter, CancellationToken token)
		{
			Thor2Service thor2Service = this.logics.Thor2Service;
			int num = 3;
			Phone currentPhone = this.appContext.CurrentPhone;
			Exception ex = null;
			while (currentPhone.IsProductCodeTypeEmpty() && num-- > 0)
			{
				try
				{
					Tracer<LumiaController>.WriteInformation("Trying to read missing info with Thor2");
					thor2Service.TryReadMissingInfoWithThor(currentPhone, token, true);
					ex = null;
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
			}
			bool flag = num < 3;
			if (flag)
			{
				thor2Service.RestartToNormalMode(currentPhone, token);
			}
			bool flag2 = ex != null;
			if (flag2)
			{
				ex = new ReadPhoneInformationException(ex.Message);
				this.logics.ReportingService.OperationFailed(currentPhone, ReportOperationType.ReadDeviceInfo, UriData.ProductCodeReadFailed, ex);
				throw ex;
			}
			bool flag3 = currentPhone.IsProductCodeTypeEmpty();
			if (flag3)
			{
				ex = new ReadPhoneInformationException();
				this.logics.ReportingService.OperationFailed(currentPhone, ReportOperationType.ReadDeviceInfo, UriData.ProductCodeReadFailed, ex);
				throw ex;
			}
			base.EventAggregator.Publish<SwitchStateMessage>(new SwitchStateMessage("CheckLatestPackageState"));
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001A120 File Offset: 0x00018320
		[CustomCommand(IsAsynchronous = true)]
		public void StartLumiaDetection(DetectionType detectionType, CancellationToken token)
		{
			this.currentDetectionType = detectionType;
			this.logics.LumiaAdaptation.StartDetection(detectionType);
			this.FindCurrentlyConnectedPhone();
			bool flag = this.appContext.CurrentPhone != null;
			if (flag)
			{
				base.EventAggregator.Publish<DeviceConnectedMessage>(new DeviceConnectedMessage(this.appContext.CurrentPhone));
				bool flag2 = detectionType != DetectionType.RecoveryModeAfterEmergencyFlashing && this.appContext.CurrentPhone.DeviceReady;
				if (flag2)
				{
					this.ReadDeviceInfo(token);
				}
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001A1A4 File Offset: 0x000183A4
		[CustomCommand(IsAsynchronous = true)]
		public void StartCurrentLumiaDetection(DetectionType detectionType, CancellationToken token)
		{
			this.currentDetectionType = detectionType;
			this.logics.LumiaAdaptation.StartDetection(detectionType);
			bool flag = this.selectedPhone == null;
			if (flag)
			{
				throw new Exception("No phone from DeviceSelection state.");
			}
			this.FindCurrentPhone(this.selectedPhone);
			bool flag2 = this.appContext.CurrentPhone != null;
			if (flag2)
			{
				base.EventAggregator.Publish<DeviceConnectedMessage>(new DeviceConnectedMessage(this.appContext.CurrentPhone));
				bool deviceReady = this.appContext.CurrentPhone.DeviceReady;
				if (deviceReady)
				{
					this.ReadDeviceInfo(token);
				}
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001A23F File Offset: 0x0001843F
		[CustomCommand(IsAsynchronous = true)]
		public void SetSelectedPhone(Phone phone, CancellationToken token)
		{
			this.selectedPhone = phone;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001A24C File Offset: 0x0001844C
		private void ReadNormalModeDeviceInfo(CancellationToken token)
		{
			bool flag = this.appContext.CurrentPhone == null;
			if (flag)
			{
				Tracer<LumiaController>.WriteInformation("Current phone is null. Unable to read device information.");
				throw new DeviceNotFoundException();
			}
			try
			{
				this.logics.LumiaAdaptation.FillLumiaDeviceInfo(this.appContext.CurrentPhone, token);
			}
			catch (OperationCanceledException)
			{
				Tracer<LumiaController>.WriteInformation("Reading device info cancelled.");
			}
			catch (Win32Exception ex)
			{
				throw new RestartApplicationException("Reading device info failed!", ex);
			}
			catch (Exception ex2)
			{
				Tracer<LumiaController>.WriteInformation(ex2.Message);
				throw;
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001A2F8 File Offset: 0x000184F8
		private void ReadDeviceInfo(CancellationToken cancellationToken)
		{
			bool flag = this.appContext.CurrentPhone == null || (!this.appContext.CurrentPhone.DeviceReady && this.currentDetectionType != DetectionType.RecoveryMode);
			if (flag)
			{
				Tracer<LumiaController>.WriteInformation("Current phone is empty. Unable to read device information.");
				throw new DeviceNotFoundException();
			}
			DetectionType detectionType = this.currentDetectionType;
			DetectionType detectionType2 = detectionType;
			if (detectionType2 != DetectionType.NormalMode)
			{
				if (detectionType2 == DetectionType.RecoveryMode)
				{
					this.logics.LumiaAdaptation.StopDetection();
					this.logics.LumiaAdaptation.TryReadMissingInfoWithThor(this.appContext.CurrentPhone, cancellationToken);
				}
			}
			else
			{
				this.ReadNormalModeDeviceInfo(cancellationToken);
			}
			bool isCancellationRequested = cancellationToken.IsCancellationRequested;
			if (!isCancellationRequested)
			{
				bool flag2 = this.appContext.CurrentPhone == null;
				if (flag2)
				{
					Tracer<LumiaController>.WriteInformation("Current phone is null. Unable to read device information.");
					throw new DeviceNotFoundException();
				}
				bool flag3 = this.appContext.CurrentPhone.IsProductCodeTypeEmpty();
				if (flag3)
				{
					base.Commands.Run((AppController c) => c.SwitchToState("ReadingDeviceInfoWithThorState"));
				}
				else
				{
					this.TryFinishWaitingForDevice();
				}
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001A458 File Offset: 0x00018658
		private void FindCurrentlyConnectedPhone()
		{
			ReadOnlyCollection<Phone> allPhones = this.logics.LumiaAdaptation.GetAllPhones();
			bool flag = this.appContext.CurrentPhone == null;
			if (flag)
			{
				this.appContext.CurrentPhone = allPhones.FirstOrDefault<Phone>();
			}
			else
			{
				Phone phone2 = allPhones.FirstOrDefault((Phone phone) => phone.PortId == this.appContext.CurrentPhone.PortId);
				this.appContext.CurrentPhone = phone2;
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001A4C4 File Offset: 0x000186C4
		private void FindCurrentPhone(Phone phone)
		{
			ReadOnlyCollection<Phone> allPhones = this.logics.LumiaAdaptation.GetAllPhones();
			try
			{
				Phone phone2 = allPhones.First((Phone p) => p.PortId == phone.PortId);
				this.appContext.CurrentPhone = phone2;
			}
			catch (Exception)
			{
				this.appContext.CurrentPhone = null;
				throw new DeviceNotFoundException(string.Format("Phone name: {0}", phone.SalesName));
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001A550 File Offset: 0x00018750
		[CustomCommand]
		public void StopLumiaDetection()
		{
			try
			{
				((IAsyncDelegateCommand)base.Commands["StartLumiaDetection"]).Cancel();
				List<CancellationTokenSource> list = this.readDeviceInfoCtsList;
				lock (list)
				{
					foreach (CancellationTokenSource cancellationTokenSource in this.readDeviceInfoCtsList)
					{
						cancellationTokenSource.Cancel();
					}
					this.readDeviceInfoCtsList.Clear();
				}
				this.logics.LumiaAdaptation.StopDetection();
			}
			catch (Exception ex)
			{
				Tracer<LumiaController>.WriteError(ex);
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001A62C File Offset: 0x0001882C
		[CustomCommand]
		public void StopCurrentLumiaDetection()
		{
			try
			{
				((IAsyncDelegateCommand)base.Commands["StartCurrentLumiaDetection"]).Cancel();
				List<CancellationTokenSource> list = this.readDeviceInfoCtsList;
				lock (list)
				{
					foreach (CancellationTokenSource cancellationTokenSource in this.readDeviceInfoCtsList)
					{
						cancellationTokenSource.Cancel();
					}
					this.readDeviceInfoCtsList.Clear();
				}
				this.logics.LumiaAdaptation.StopDetection();
			}
			catch (Exception ex)
			{
				Tracer<LumiaController>.WriteError(ex);
			}
		}

		// Token: 0x04000230 RID: 560
		private readonly Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x04000231 RID: 561
		private readonly LogicContext logics;

		// Token: 0x04000232 RID: 562
		private readonly List<CancellationTokenSource> readDeviceInfoCtsList = new List<CancellationTokenSource>();

		// Token: 0x04000233 RID: 563
		private DetectionType currentDetectionType = DetectionType.None;

		// Token: 0x04000234 RID: 564
		private Phone selectedPhone;
	}
}
