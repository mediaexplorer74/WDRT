using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;
using Microsoft.WindowsDeviceRecoveryTool.Controllers;
using Microsoft.WindowsDeviceRecoveryTool.Detection;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Localization;
using Microsoft.WindowsDeviceRecoveryTool.Messages;
using Microsoft.WindowsDeviceRecoveryTool.Model;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Preparing
{
	// Token: 0x02000037 RID: 55
	[Export]
	public sealed class AutomaticManufacturerSelectionViewModel : BaseViewModel, ICanHandle<SupportedManufacturersMessage>, ICanHandle, INotifyLiveRegionChanged
	{
		// Token: 0x0600024D RID: 589 RVA: 0x0000DB18 File Offset: 0x0000BD18
		[ImportingConstructor]
		internal AutomaticManufacturerSelectionViewModel(Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext, DetectionHandlerFactory detectionHandlerFactory)
		{
			this.appContext = appContext;
			this.detectionHandlerFactory = detectionHandlerFactory;
			this.attachedDeviceIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			this.switchToDeviceSelectionTimer = new DispatcherTimer
			{
				Interval = TimeSpan.FromSeconds(0.75)
			};
			this.switchToDeviceSelectionTimer.Tick += delegate(object s, EventArgs e)
			{
				this.OnSwitchToDeviceSelectionTimerTick();
			};
			this.DeviceNotDetectedCommand = new DelegateCommand<object>(new Action<object>(this.OnDeviceNotDetectedCommandExecuted));
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600024E RID: 590 RVA: 0x0000DB9C File Offset: 0x0000BD9C
		// (remove) Token: 0x0600024F RID: 591 RVA: 0x0000DBD4 File Offset: 0x0000BDD4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler LiveRegionChanged;

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000DC09 File Offset: 0x0000BE09
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000DC11 File Offset: 0x0000BE11
		public ICommand DeviceNotDetectedCommand { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000DC1C File Offset: 0x0000BE1C
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000DC34 File Offset: 0x0000BE34
		public Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext AppContext
		{
			get
			{
				return this.appContext;
			}
			set
			{
				base.SetValue<Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext>(() => this.AppContext, ref this.appContext, value);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000DC74 File Offset: 0x0000BE74
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0000DC8C File Offset: 0x0000BE8C
		public bool AnalogSupported
		{
			get
			{
				return this.analogSupported;
			}
			set
			{
				base.SetValue<bool>(() => this.AnalogSupported, ref this.analogSupported, value);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000DCCC File Offset: 0x0000BECC
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0000DCE4 File Offset: 0x0000BEE4
		public string LiveText
		{
			get
			{
				return this.liveText;
			}
			set
			{
				base.SetValue<string>(() => this.LiveText, ref this.liveText, value);
				bool flag = !string.IsNullOrWhiteSpace(this.liveText);
				if (flag)
				{
					this.OnLiveRegionChanged();
				}
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000DD4C File Offset: 0x0000BF4C
		public override async void OnStarted()
		{
			base.OnStarted();
			base.EventAggregator.Publish<HeaderMessage>(new HeaderMessage(LocalizationManager.GetTranslation("WelcomeHeader"), ""));
			base.EventAggregator.Publish<BlockWindowMessage>(new BlockWindowMessage(false, null, null));
			base.EventAggregator.Publish<IsBackButtonMessage>(new IsBackButtonMessage(false));
			this.AppContext.CurrentPhone = null;
			this.LiveText = null;
			base.Commands.Run((FlowController c) => c.GetSupportedManufacturers());
			this.attachedDeviceIds.Clear();
			using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
			{
				using (IDetectionHandler deviceMonitor = this.detectionHandlerFactory.CreateDetectionHandler())
				{
					this.internalTokenSource = cancellationTokenSource;
					try
					{
						CancellationToken cancellationToken = cancellationTokenSource.Token;
						while (!cancellationToken.IsCancellationRequested)
						{
							DeviceInfoEventArgs deviceInfoEventArgs = await deviceMonitor.TakeDeviceInfoEventAsync(cancellationToken);
							DeviceInfoEventArgs deviceInfoEvent = deviceInfoEventArgs;
							deviceInfoEventArgs = null;
							if (deviceInfoEvent.DeviceInfoAction == DeviceInfoAction.Attached)
							{
								DeviceInfo device = deviceInfoEvent.DeviceInfo;
								Tracer<AutomaticManufacturerSelectionViewModel>.WriteInformation("Attached device detected: {0}", new object[] { device.DeviceIdentifier });
								if (deviceInfoEvent.IsEnumerated)
								{
									this.OnDeviceAttachedOnStartup(device);
								}
								else
								{
									this.OnDeviceAttached(device);
								}
								device = null;
							}
							else if (deviceInfoEvent.DeviceInfoAction == DeviceInfoAction.Detached)
							{
								DeviceInfo device2 = deviceInfoEvent.DeviceInfo;
								Tracer<AutomaticManufacturerSelectionViewModel>.WriteInformation("Detached device detected: {0}", new object[] { device2.DeviceIdentifier });
								this.OnDeviceDetached(device2);
								device2 = null;
							}
							deviceInfoEvent = null;
						}
						cancellationToken = default(CancellationToken);
					}
					catch (OperationCanceledException)
					{
						Tracer<AutomaticManufacturerSelectionViewModel>.WriteInformation("Detection cancelled");
					}
					catch (Exception ex)
					{
						Tracer<AutomaticManufacturerSelectionViewModel>.WriteError(ex.ToString(), new object[0]);
						throw;
					}
					finally
					{
						this.internalTokenSource = null;
					}
				}
				IDetectionHandler deviceMonitor = null;
			}
			CancellationTokenSource cancellationTokenSource = null;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000DD88 File Offset: 0x0000BF88
		public override void OnStopped()
		{
			base.OnStopped();
			this.switchToDeviceSelectionTimer.IsEnabled = false;
			bool flag = this.internalTokenSource != null;
			if (flag)
			{
				this.internalTokenSource.Cancel();
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000DDC8 File Offset: 0x0000BFC8
		public void Handle(SupportedManufacturersMessage message)
		{
			bool isStarted = base.IsStarted;
			if (isStarted)
			{
				foreach (ManufacturerInfo manufacturerInfo in message.Manufacturers)
				{
					bool flag = manufacturerInfo.Type == PhoneTypes.Analog;
					if (flag)
					{
						this.AnalogSupported = true;
						break;
					}
				}
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000DE40 File Offset: 0x0000C040
		private void OnDeviceNotDetectedCommandExecuted(object obj)
		{
			this.SwitchToManufacturerSelection();
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000DE4C File Offset: 0x0000C04C
		private void OnDeviceDetached(DeviceInfo device)
		{
			bool flag = this.attachedDeviceIds.Remove(device.DeviceIdentifier);
			if (flag)
			{
				this.LiveText = LocalizationManager.GetTranslation("DeviceDisconnected");
			}
			bool flag2 = this.attachedDeviceIds.Count == 0;
			if (flag2)
			{
				this.switchToDeviceSelectionTimer.IsEnabled = false;
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000DEA4 File Offset: 0x0000C0A4
		private void OnDeviceAttached(DeviceInfo device)
		{
			bool flag = this.attachedDeviceIds.Add(device.DeviceIdentifier);
			if (flag)
			{
				this.LiveText = LocalizationManager.GetTranslation("DeviceConnected");
			}
			this.switchToDeviceSelectionTimer.IsEnabled = true;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000DEE7 File Offset: 0x0000C0E7
		private void OnDeviceAttachedOnStartup(DeviceInfo device)
		{
			this.SwitchToDeviceSelection();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000DEE7 File Offset: 0x0000C0E7
		private void OnSwitchToDeviceSelectionTimerTick()
		{
			this.SwitchToDeviceSelection();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000DEF1 File Offset: 0x0000C0F1
		private void SwitchToDeviceSelection()
		{
			this.SwitchToState("DeviceSelectionState");
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000DF00 File Offset: 0x0000C100
		private void SwitchToManufacturerSelection()
		{
			this.SwitchToState("ManualManufacturerSelectionState");
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000DF10 File Offset: 0x0000C110
		private void SwitchToState(string nextState)
		{
			this.switchToDeviceSelectionTimer.IsEnabled = false;
			base.Commands.Run((AppController c) => c.SwitchToState(nextState));
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000DFA8 File Offset: 0x0000C1A8
		private void OnLiveRegionChanged()
		{
			EventHandler liveRegionChanged = this.LiveRegionChanged;
			bool flag = liveRegionChanged != null;
			if (flag)
			{
				liveRegionChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x0400011C RID: 284
		private readonly DetectionHandlerFactory detectionHandlerFactory;

		// Token: 0x0400011D RID: 285
		private readonly DispatcherTimer switchToDeviceSelectionTimer;

		// Token: 0x0400011E RID: 286
		private readonly HashSet<string> attachedDeviceIds;

		// Token: 0x0400011F RID: 287
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x04000120 RID: 288
		private string liveText;

		// Token: 0x04000121 RID: 289
		private bool analogSupported;

		// Token: 0x04000122 RID: 290
		private CancellationTokenSource internalTokenSource;
	}
}
