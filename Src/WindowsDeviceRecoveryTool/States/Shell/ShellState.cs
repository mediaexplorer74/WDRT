using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.Model.Enums;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.DefaultTypes;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.Transitions;
using Microsoft.WindowsDeviceRecoveryTool.States.BaseStates;
using Microsoft.WindowsDeviceRecoveryTool.States.Error;
using Microsoft.WindowsDeviceRecoveryTool.States.Help;
using Microsoft.WindowsDeviceRecoveryTool.States.Preparing;
using Microsoft.WindowsDeviceRecoveryTool.States.Settings;
using Microsoft.WindowsDeviceRecoveryTool.States.Workflow;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Shell
{
	// Token: 0x02000009 RID: 9
	[Export]
	public class ShellState : BaseStateMachineState
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000027BE File Offset: 0x000009BE
		public void StartStateMachine()
		{
			base.Machine.Start();
			this.appContext.IsMachineStateRunning = true;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000027DA File Offset: 0x000009DA
		public override void InitializeStateMachine()
		{
			base.InitializeStateMachine();
			base.Machine.AddState(this.checkAppAutoUpdateState);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000027F8 File Offset: 0x000009F8
		protected override void OnCurrentStateChanged(BaseState oldValue, BaseState newValue)
		{
			bool flag = oldValue != newValue;
			if (flag)
			{
				this.previousTransition.SetNextState(oldValue);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002820 File Offset: 0x00000A20
		protected override void ConfigureStates()
		{
			this.ConfigureHelpState();
			this.ConfigureCheckAppAutoUpdateState();
			this.ConfigureAppAutoUpdateState();
			this.ConfigureManualPackageSelectionState();
			this.ConfigureAutomaticPackageSelectionState();
			this.ConfigureAwaitGenericDeviceState();
			this.ConfigureAwaitHtcState();
			this.ConfigureAwaitAnalogDeviceState();
			this.ConfigureFlashingState();
			this.ConfigureSummaryState();
			this.ConfigureManualManufacturerSelectionState();
			this.ConfigureAutomaticManufacturerSelectionState();
			this.ConfigurePackageIntegrityCheckState();
			this.ConfigureErrorState();
			this.ConfigureSettingsState();
			this.ConfigureCheckLatestPackageState();
			this.ConfigureDownloadPackageState();
			this.ConfigureAwaitRecoveryDeviceState();
			this.ConfigureDeviceDetectionState();
			this.ConfigureReadingDeviceInfoState();
			this.ConfigureReadingDeviceInfoWithThorState();
			this.ConfigureBatteryCheckingState();
			this.ConfigureDisclaimerState();
			this.ConfigureManualHtcRestartState();
			this.ConfigureRebootHtcState();
			this.ConfigureManualDeviceTypeSelectionState();
			this.ConfigureDownloadEmergencyPackageState();
			this.ConfigureAwaitRecoveryModeAfterEmergencyFlashingState();
			this.ConfigureAbsoluteConfirmationState();
			this.ConfigureManualGenericModelSelectionState();
			this.ConfigureManualGenericVariantSelectionState();
			this.ConfigureAwaitHoloLensAccessoryDeviceState();
			this.ConfigureUnsupportedDeviceState();
			this.ConfigureSurveyState();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000291C File Offset: 0x00000B1C
		protected override void InitializeStates()
		{
			this.checkAppAutoUpdateState = base.GetUiState<AppUpdateCheckingView, AppUpdateCheckingViewModel>(null);
			this.appAutoUpdateState = base.GetUiState<DownloadAppUpdateView, DownloadAppUpdateViewModel>(null);
			this.manualPackageSelectionState = base.GetUiState<ManualPackageSelectionView, ManualPackageSelectionViewModel>(null);
			this.automaticPackageSelectionState = base.GetUiState<AutomaticPackageSelectionView, AutomaticPackageSelectionViewModel>(null);
			this.checkLatestPackageState = base.GetUiState<CheckLatestPackageView, CheckLatestPackageViewModel>(null);
			this.downloadPackageState = base.GetUiState<DownloadPackageView, DownloadPackageViewModel>(null);
			this.awaitGenericDeviceState = base.GetUiState<AwaitGenericDeviceView, AwaitGenericDeviceViewModel>(null);
			this.awaitHtcState = base.GetUiState<AwaitHtcView, AwaitHtcViewModel>(null);
			this.awaitAnalogDeviceState = base.GetUiState<AwaitAnalogDeviceView, AwaitAnalogDeviceViewModel>(null);
			this.awaitRecoveryDeviceState = base.GetUiState<AwaitRecoveryDeviceView, AwaitRecoveryDeviceViewModel>(null);
			this.flashingState = base.GetUiState<FlashingView, FlashingViewModel>(null);
			this.summaryState = base.GetUiState<SummaryView, SummaryViewModel>(null);
			this.manualManufacturerSelectionState = base.GetUiState<ManualManufacturerSelectionView, ManualManufacturerSelectionViewModel>(null);
			this.automaticManufacturerSelectionState = base.GetUiState<AutomaticManufacturerSelectionView, AutomaticManufacturerSelectionViewModel>(null);
			this.packageIntegrityCheckState = base.GetUiState<PackageIntegrityCheckView, PackageIntegrityCheckViewModel>(null);
			this.errorState = base.GetUiState<ErrorView, ErrorViewModel>(null);
			this.deviceSelectionState = base.GetUiState<DeviceSelectionView, DeviceSelectionViewModel>(null);
			this.readingDeviceInfoState = base.GetUiState<ReadingDeviceInfoView, ReadingDeviceInfoViewModel>(null);
			this.readingDeviceInfoWithThorState = base.GetUiState<ReadingDeviceInfoView, ReadingDeviceInfoWithThorViewModel>(null);
			this.batteryCheckingState = base.GetUiState<BatteryCheckingView, BatteryCheckingViewModel>(null);
			this.settingsState = new SettingsState
			{
				Container = base.Container
			};
			this.disclaimerState = base.GetUiState<DisclaimerView, DisclaimerViewModel>(null);
			this.manualHtcRestartState = base.GetUiState<ManualHtcRestartView, ManualHtcRestartViewModel>(null);
			this.rebootHtcState = base.GetUiState<RebootHtcView, RebootHtcViewModel>(null);
			this.manualDeviceTypeSelectionState = base.GetUiState<ManualDeviceTypeSelectionView, ManualDeviceTypeSelectionViewModel>(null);
			this.downloadEmergencyPackageState = base.GetUiState<DownloadEmergencyPackageView, DownloadEmergencyPackageViewModel>(null);
			this.awaitRecoveryModeAfterEmergencyFlashingState = base.GetUiState<AwaitRecoveryDeviceView, AwaitRecoveryAfterEmergencyDeviceViewModel>(null);
			this.absoluteConfirmationState = base.GetUiState<AbsoluteConfirmationView, AbsoluteConfirmationViewModel>(null);
			this.helpState = new HelpState
			{
				Container = base.Container
			};
			this.manualGenericModelSelectionState = base.GetUiState<ManualGenericModelSelectionView, ManualGenericModelSelectionViewModel>(null);
			this.manualGenericVariantSelectionState = base.GetUiState<ManualGenericVariantSelectionView, ManualGenericVariantSelectionViewModel>(null);
			this.unsupportedDeviceState = base.GetUiState<UnsupportedDeviceView, UnsupportedDeviceViewModel>(null);
			this.surveyState = base.GetUiState<SurveyView, SurveyViewModel>(null);
			this.awaitFawkesDeviceState = base.GetUiState<AwaitFawkesDeviceView, AwaitFawkesDeviceViewModel>(null);
			this.settingsState.InitializeStateMachine();
			this.helpState.InitializeStateMachine();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002B14 File Offset: 0x00000D14
		protected override void InitializeTransitions()
		{
			this.appAutoUpdateTransition = new StateStatusTransition(this.appAutoUpdateState, "AppAutoUpdateState");
			this.checkAppAutoUpdateTransition = new StateStatusTransition(this.checkAppAutoUpdateState, "CheckAppAutoUpdateState");
			this.manualPackageSelectionTransition = new StateStatusTransition(this.manualPackageSelectionState, "ManualPackageSelectionState");
			this.automaticPackageSelectionTransition = new StateStatusTransition(this.automaticPackageSelectionState, "AutomaticPackageSelectionState");
			this.checkLatestPackageTransition = new StateStatusTransition(this.checkLatestPackageState, "CheckLatestPackageState");
			this.downloadPackageTransition = new StateStatusTransition(this.downloadPackageState, "DownloadPackageState");
			this.flashingStateTransition = new StateStatusTransition(this.flashingState, "FlashingState");
			this.awaitGenericDeviceTransition = new StateStatusTransition(this.awaitGenericDeviceState, "AwaitGenericDeviceState");
			this.awaitHtcTransition = new StateStatusTransition(this.awaitHtcState, "AwaitHtcState");
			this.awaitAnalogDeviceTransition = new StateStatusTransition(this.awaitAnalogDeviceState, "AwaitAnalogDeviceState");
			this.awaitRecoveryDeviceTransition = new StateStatusTransition(this.awaitRecoveryDeviceState, "AwaitRecoveryDeviceState");
			this.summaryTransition = new StateStatusTransition(this.summaryState, "SummaryState");
			this.manualManufacturerSelectionTransition = new StateStatusTransition(this.manualManufacturerSelectionState, "ManualManufacturerSelectionState");
			this.automaticManufacturerSelectionTransition = new StateStatusTransition(this.automaticManufacturerSelectionState, "AutomaticManufacturerSelectionState");
			this.packageIntegrityCheckTransition = new StateStatusTransition(this.packageIntegrityCheckState, "PackageIntegrityCheckState");
			this.previousTransition = new PreviousStateTransition(null, "PreviousState");
			this.errorTransition = new StateStatusTransition(this.errorState, "ErrorState");
			this.settingsTransition = new StateStatusTransition(this.settingsState, "SettingsState");
			this.deviceSelectionTransition = new StateStatusTransition(this.deviceSelectionState, "DeviceSelectionState");
			this.readingDeviceInfoTransition = new StateStatusTransition(this.readingDeviceInfoState, "ReadingDeviceInfoState");
			this.readingDeviceInfoWithThorTransition = new StateStatusTransition(this.readingDeviceInfoWithThorState, "ReadingDeviceInfoWithThorState");
			this.batteryCheckingTransition = new StateStatusTransition(this.batteryCheckingState, "BatteryCheckingState");
			this.disclaimerTransition = new StateStatusTransition(this.disclaimerState, "DisclaimerState");
			this.manualHtcRestartTransition = new StateStatusTransition(this.manualHtcRestartState, "ManualHtcRestartState");
			this.rebootHtcTransition = new StateStatusTransition(this.rebootHtcState, "RebootHtcState");
			this.manualDeviceTypeSelectionTransition = new StateStatusTransition(this.manualDeviceTypeSelectionState, "ManualDeviceTypeSelectionState");
			this.downloadEmergencyPackageTransition = new StateStatusTransition(this.downloadEmergencyPackageState, "DownloadEmergencyPackageState");
			this.awaitRecoveryModeAfterEmergencyFlashingTransition = new StateStatusTransition(this.awaitRecoveryModeAfterEmergencyFlashingState, "AwaitRecoveryModeAfterEmergencyFlashingState");
			this.rebootHtcAfterErrorTransition = new LambdaTransition(new Func<bool>(this.conditions.IsHtcConnected), this.rebootHtcState);
			this.helpTransition = new StateStatusTransition(this.helpState, "HelpState");
			this.manufacturerSelectionTransitionAfterError = new LambdaTransition(() => this.appContext.CurrentPhone == null || this.appContext.SelectedManufacturer != PhoneTypes.Htc, this.automaticManufacturerSelectionState);
			this.absoluteConfirmationTransition = new StateStatusTransition(this.absoluteConfirmationState, "AbsoluteConfirmationState");
			this.manualGenericModelSelectionTransition = new StateStatusTransition(this.manualGenericModelSelectionState, "ManualGenericModelSelectionState");
			this.manualGenericVariantSelectionTransition = new StateStatusTransition(this.manualGenericVariantSelectionState, "ManualGenericVariantSelectionState");
			this.awaitFawkesDeviceTransition = new StateStatusTransition(this.awaitFawkesDeviceState, "AwaitFawkesDeviceState");
			this.unsupportedDeviceTransition = new StateStatusTransition(this.unsupportedDeviceState, "UnsupportedDeviceState");
			this.surveyTransition = new StateStatusTransition(this.surveyState, "SurveyState");
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002E60 File Offset: 0x00001060
		private void ConfigureHelpState()
		{
			this.helpState.HideRegions(new string[] { "SettingsArea" });
			this.helpState.AddConditionalTransition(this.previousTransition);
			this.helpState.AddConditionalTransition(this.deviceSelectionTransition);
			this.helpState.AddConditionalTransition(this.manualManufacturerSelectionTransition);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002EC0 File Offset: 0x000010C0
		private void ConfigureManualDeviceTypeSelectionState()
		{
			this.manualDeviceTypeSelectionState.HideRegions(new string[] { "SettingsArea" });
			this.manualDeviceTypeSelectionState.AddConditionalTransition(this.previousTransition);
			this.manualDeviceTypeSelectionState.AddConditionalTransition(this.downloadEmergencyPackageTransition);
			this.manualDeviceTypeSelectionState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002F20 File Offset: 0x00001120
		private void ConfigureDownloadEmergencyPackageState()
		{
			this.downloadEmergencyPackageState.HideRegions(new string[] { "SettingsArea" });
			this.downloadEmergencyPackageState.AddConditionalTransition(this.previousTransition);
			this.downloadEmergencyPackageState.AddConditionalTransition(this.flashingStateTransition);
			this.downloadEmergencyPackageState.AddConditionalTransition(this.errorTransition);
			this.downloadEmergencyPackageState.AddConditionalTransition(this.summaryTransition);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002F90 File Offset: 0x00001190
		private void ConfigureAwaitRecoveryModeAfterEmergencyFlashingState()
		{
			this.awaitRecoveryModeAfterEmergencyFlashingState.HideRegions(new string[] { "SettingsArea" });
			this.awaitRecoveryModeAfterEmergencyFlashingState.AddConditionalTransition(this.errorTransition);
			this.awaitRecoveryModeAfterEmergencyFlashingState.AddConditionalTransition(this.awaitRecoveryDeviceTransition);
			this.awaitRecoveryModeAfterEmergencyFlashingState.AddConditionalTransition(this.summaryTransition);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002FF0 File Offset: 0x000011F0
		private void ConfigureFlashingState()
		{
			this.flashingState.HideRegions(new string[] { "SettingsArea" });
			this.flashingState.AddConditionalTransition(this.summaryTransition);
			this.flashingState.AddConditionalTransition(this.awaitRecoveryModeAfterEmergencyFlashingTransition);
			this.flashingState.AddConditionalTransition(this.errorTransition);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003050 File Offset: 0x00001250
		private void ConfigureSettingsState()
		{
			this.settingsState.HideRegions(new string[] { "SettingsArea" });
			this.settingsState.AddConditionalTransition(this.previousTransition);
			this.settingsState.AddConditionalTransition(this.errorTransition);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000309C File Offset: 0x0000129C
		private void ConfigureErrorState()
		{
			this.errorState.HideRegions(new string[] { "SettingsArea" });
			this.errorState.AddConditionalTransition(this.previousTransition);
			this.errorState.AddConditionalTransition(this.flashingStateTransition);
			this.errorState.AddConditionalTransition(this.summaryTransition);
			this.errorState.AddConditionalTransition(this.settingsTransition);
			this.errorState.AddConditionalTransition(this.awaitGenericDeviceTransition);
			this.errorState.AddConditionalTransition(this.manualManufacturerSelectionTransition);
			this.errorState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
			this.errorState.AddConditionalTransition(this.checkAppAutoUpdateTransition);
			this.errorState.AddConditionalTransition(this.rebootHtcAfterErrorTransition);
			this.errorState.AddConditionalTransition(this.manufacturerSelectionTransitionAfterError);
			this.errorState.AddConditionalTransition(this.helpTransition);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000318C File Offset: 0x0000138C
		private void ConfigureRebootHtcState()
		{
			this.rebootHtcState.ShowRegions(new string[] { "MainArea", "SettingsArea" });
			this.rebootHtcState.AddConditionalTransition(this.settingsTransition);
			this.rebootHtcState.AddConditionalTransition(this.errorTransition);
			this.rebootHtcState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
			this.rebootHtcState.AddConditionalTransition(this.helpTransition);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003204 File Offset: 0x00001404
		private void ConfigureCheckAppAutoUpdateState()
		{
			this.checkAppAutoUpdateState.ShowRegions(new string[] { "MainArea", "SettingsArea" });
			this.checkAppAutoUpdateState.AddConditionalTransition(this.appAutoUpdateTransition);
			this.checkAppAutoUpdateState.AddConditionalTransition(this.settingsTransition);
			this.checkAppAutoUpdateState.AddConditionalTransition(this.errorTransition);
			this.checkAppAutoUpdateState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
			this.checkAppAutoUpdateState.AddConditionalTransition(this.helpTransition);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003290 File Offset: 0x00001490
		private void ConfigureAppAutoUpdateState()
		{
			this.appAutoUpdateState.HideRegions(new string[] { "SettingsArea" });
			this.appAutoUpdateState.AddConditionalTransition(this.checkAppAutoUpdateTransition);
			this.appAutoUpdateState.AddConditionalTransition(this.errorTransition);
			this.appAutoUpdateState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000032F0 File Offset: 0x000014F0
		private void ConfigureManualPackageSelectionState()
		{
			this.manualPackageSelectionState.ShowRegions(new string[] { "MainArea", "SettingsArea" });
			this.manualPackageSelectionState.AddConditionalTransition(this.settingsTransition);
			this.manualPackageSelectionState.AddConditionalTransition(this.previousTransition);
			this.manualPackageSelectionState.AddConditionalTransition(this.errorTransition);
			this.manualPackageSelectionState.AddConditionalTransition(this.packageIntegrityCheckTransition);
			this.manualPackageSelectionState.AddConditionalTransition(this.helpTransition);
			this.manualPackageSelectionState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000338C File Offset: 0x0000158C
		private void ConfigureAutomaticPackageSelectionState()
		{
			this.automaticPackageSelectionState.ShowRegions(new string[] { "MainArea", "SettingsArea" });
			this.automaticPackageSelectionState.AddConditionalTransition(this.settingsTransition);
			this.automaticPackageSelectionState.AddConditionalTransition(this.manualManufacturerSelectionTransition);
			this.automaticPackageSelectionState.AddConditionalTransition(this.previousTransition);
			this.automaticPackageSelectionState.AddConditionalTransition(this.manualPackageSelectionTransition);
			this.automaticPackageSelectionState.AddConditionalTransition(this.errorTransition);
			this.automaticPackageSelectionState.AddConditionalTransition(this.packageIntegrityCheckTransition);
			this.automaticPackageSelectionState.AddConditionalTransition(this.helpTransition);
			this.automaticPackageSelectionState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000344C File Offset: 0x0000164C
		private void ConfigureCheckLatestPackageState()
		{
			this.checkLatestPackageState.ShowRegions(new string[] { "MainArea", "SettingsArea" });
			this.checkLatestPackageState.AddConditionalTransition(this.settingsTransition);
			this.checkLatestPackageState.AddConditionalTransition(this.downloadPackageTransition);
			this.checkLatestPackageState.AddConditionalTransition(this.manualManufacturerSelectionTransition);
			this.checkLatestPackageState.AddConditionalTransition(this.previousTransition);
			this.checkLatestPackageState.AddConditionalTransition(this.disclaimerTransition);
			this.checkLatestPackageState.AddConditionalTransition(this.errorTransition);
			this.checkLatestPackageState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
			this.checkLatestPackageState.AddConditionalTransition(this.rebootHtcTransition);
			this.checkLatestPackageState.AddConditionalTransition(this.manualPackageSelectionTransition);
			this.checkLatestPackageState.AddConditionalTransition(this.helpTransition);
			this.checkLatestPackageState.AddConditionalTransition(this.packageIntegrityCheckTransition);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003544 File Offset: 0x00001744
		private void ConfigureDownloadPackageState()
		{
			this.downloadPackageState.ShowRegions(new string[] { "MainArea" });
			this.downloadPackageState.HideRegions(new string[] { "SettingsArea" });
			this.downloadPackageState.AddConditionalTransition(this.manualManufacturerSelectionTransition);
			this.downloadPackageState.AddConditionalTransition(this.previousTransition);
			this.downloadPackageState.AddConditionalTransition(this.batteryCheckingTransition);
			this.downloadPackageState.AddConditionalTransition(this.flashingStateTransition);
			this.downloadPackageState.AddConditionalTransition(this.errorTransition);
			this.downloadPackageState.AddConditionalTransition(this.summaryTransition);
			this.downloadPackageState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003604 File Offset: 0x00001804
		private void ConfigurePackageIntegrityCheckState()
		{
			this.packageIntegrityCheckState.ShowRegions(new string[] { "MainArea" });
			this.packageIntegrityCheckState.HideRegions(new string[] { "SettingsArea" });
			this.packageIntegrityCheckState.AddConditionalTransition(this.previousTransition);
			this.packageIntegrityCheckState.AddConditionalTransition(this.batteryCheckingTransition);
			this.packageIntegrityCheckState.AddConditionalTransition(this.errorTransition);
			this.packageIntegrityCheckState.AddConditionalTransition(this.flashingStateTransition);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003690 File Offset: 0x00001890
		private void ConfigureManualManufacturerSelectionState()
		{
			this.manualManufacturerSelectionState.ShowRegions(new string[] { "MainArea", "SettingsArea" });
			this.manualManufacturerSelectionState.AddConditionalTransition(this.settingsTransition);
			this.manualManufacturerSelectionState.AddConditionalTransition(this.awaitGenericDeviceTransition);
			this.manualManufacturerSelectionState.AddConditionalTransition(this.awaitRecoveryDeviceTransition);
			this.manualManufacturerSelectionState.AddConditionalTransition(this.awaitHtcTransition);
			this.manualManufacturerSelectionState.AddConditionalTransition(this.awaitAnalogDeviceTransition);
			this.manualManufacturerSelectionState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
			this.manualManufacturerSelectionState.AddConditionalTransition(this.errorTransition);
			this.manualManufacturerSelectionState.AddConditionalTransition(this.previousTransition);
			this.manualManufacturerSelectionState.AddConditionalTransition(this.helpTransition);
			this.manualManufacturerSelectionState.AddConditionalTransition(this.manualGenericModelSelectionTransition);
			this.manualManufacturerSelectionState.AddConditionalTransition(this.awaitFawkesDeviceTransition);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003788 File Offset: 0x00001988
		private void ConfigureAutomaticManufacturerSelectionState()
		{
			this.automaticManufacturerSelectionState.ShowRegions(new string[] { "MainArea", "SettingsArea" });
			this.automaticManufacturerSelectionState.AddConditionalTransition(this.settingsTransition);
			this.automaticManufacturerSelectionState.AddConditionalTransition(this.manualManufacturerSelectionTransition);
			this.automaticManufacturerSelectionState.AddConditionalTransition(this.errorTransition);
			this.automaticManufacturerSelectionState.AddConditionalTransition(this.deviceSelectionTransition);
			this.automaticManufacturerSelectionState.AddConditionalTransition(this.helpTransition);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003814 File Offset: 0x00001A14
		private void ConfigureAwaitGenericDeviceState()
		{
			this.awaitGenericDeviceState.ShowRegions(new string[] { "MainArea", "SettingsArea" });
			this.awaitGenericDeviceState.AddConditionalTransition(this.previousTransition);
			this.awaitGenericDeviceState.AddConditionalTransition(this.readingDeviceInfoTransition);
			this.awaitGenericDeviceState.AddConditionalTransition(this.automaticPackageSelectionTransition);
			this.awaitGenericDeviceState.AddConditionalTransition(this.settingsTransition);
			this.awaitGenericDeviceState.AddConditionalTransition(this.manualManufacturerSelectionTransition);
			this.awaitGenericDeviceState.AddConditionalTransition(this.errorTransition);
			this.awaitGenericDeviceState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
			this.awaitGenericDeviceState.AddConditionalTransition(this.helpTransition);
			this.awaitGenericDeviceState.AddConditionalTransition(this.flashingStateTransition);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000038E8 File Offset: 0x00001AE8
		private void ConfigureAwaitHtcState()
		{
			this.awaitHtcState.ShowRegions(new string[] { "MainArea", "SettingsArea" });
			this.awaitHtcState.AddConditionalTransition(this.previousTransition);
			this.awaitHtcState.AddConditionalTransition(this.readingDeviceInfoTransition);
			this.awaitHtcState.AddConditionalTransition(this.checkLatestPackageTransition);
			this.awaitHtcState.AddConditionalTransition(this.settingsTransition);
			this.awaitHtcState.AddConditionalTransition(this.manualManufacturerSelectionTransition);
			this.awaitHtcState.AddConditionalTransition(this.errorTransition);
			this.awaitHtcState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
			this.awaitHtcState.AddConditionalTransition(this.helpTransition);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000039A8 File Offset: 0x00001BA8
		private void ConfigureAwaitAnalogDeviceState()
		{
			this.awaitAnalogDeviceState.ShowRegions(new string[] { "MainArea", "SettingsArea" });
			this.awaitAnalogDeviceState.AddConditionalTransition(this.previousTransition);
			this.awaitAnalogDeviceState.AddConditionalTransition(this.readingDeviceInfoTransition);
			this.awaitAnalogDeviceState.AddConditionalTransition(this.checkLatestPackageTransition);
			this.awaitAnalogDeviceState.AddConditionalTransition(this.settingsTransition);
			this.awaitAnalogDeviceState.AddConditionalTransition(this.manualManufacturerSelectionTransition);
			this.awaitAnalogDeviceState.AddConditionalTransition(this.errorTransition);
			this.awaitAnalogDeviceState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
			this.awaitAnalogDeviceState.AddConditionalTransition(this.helpTransition);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003A68 File Offset: 0x00001C68
		private void ConfigureAwaitRecoveryDeviceState()
		{
			this.awaitRecoveryDeviceState.ShowRegions(new string[] { "MainArea" });
			this.awaitRecoveryDeviceState.HideRegions(new string[] { "SettingsArea" });
			this.awaitRecoveryDeviceState.AddConditionalTransition(this.previousTransition);
			this.awaitRecoveryDeviceState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
			this.awaitRecoveryDeviceState.AddConditionalTransition(this.checkLatestPackageTransition);
			this.awaitRecoveryDeviceState.AddConditionalTransition(this.errorTransition);
			this.awaitRecoveryDeviceState.AddConditionalTransition(this.settingsTransition);
			this.awaitRecoveryDeviceState.AddConditionalTransition(this.manualDeviceTypeSelectionTransition);
			this.awaitRecoveryDeviceState.AddConditionalTransition(this.helpTransition);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003B28 File Offset: 0x00001D28
		private void ConfigureDeviceDetectionState()
		{
			this.deviceSelectionState.ShowRegions(new string[] { "MainArea", "SettingsArea" });
			this.deviceSelectionState.AddConditionalTransition(this.checkLatestPackageTransition);
			this.deviceSelectionState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
			this.deviceSelectionState.AddConditionalTransition(this.manualManufacturerSelectionTransition);
			this.deviceSelectionState.AddConditionalTransition(this.errorTransition);
			this.deviceSelectionState.AddConditionalTransition(this.settingsTransition);
			this.deviceSelectionState.AddConditionalTransition(this.readingDeviceInfoTransition);
			this.deviceSelectionState.AddConditionalTransition(this.awaitHtcTransition);
			this.deviceSelectionState.AddConditionalTransition(this.awaitGenericDeviceTransition);
			this.deviceSelectionState.AddConditionalTransition(this.manualPackageSelectionTransition);
			this.deviceSelectionState.AddConditionalTransition(this.helpTransition);
			this.deviceSelectionState.AddConditionalTransition(this.manualGenericVariantSelectionTransition);
			this.deviceSelectionState.AddConditionalTransition(this.awaitFawkesDeviceTransition);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003C30 File Offset: 0x00001E30
		private void ConfigureBatteryCheckingState()
		{
			this.batteryCheckingState.ShowRegions(new string[] { "MainArea", "SettingsArea" });
			this.batteryCheckingState.AddConditionalTransition(this.flashingStateTransition);
			this.batteryCheckingState.AddConditionalTransition(this.errorTransition);
			this.batteryCheckingState.AddConditionalTransition(this.settingsTransition);
			this.batteryCheckingState.AddConditionalTransition(this.summaryTransition);
			this.batteryCheckingState.AddConditionalTransition(this.helpTransition);
			this.batteryCheckingState.AddConditionalTransition(this.absoluteConfirmationTransition);
			this.batteryCheckingState.AddConditionalTransition(this.awaitGenericDeviceTransition);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003CE0 File Offset: 0x00001EE0
		private void ConfigureSummaryState()
		{
			this.summaryState.ShowRegions(new string[] { "MainArea", "SettingsArea" });
			this.summaryState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
			this.summaryState.AddConditionalTransition(this.settingsTransition);
			this.summaryState.AddConditionalTransition(this.rebootHtcTransition);
			this.summaryState.AddConditionalTransition(this.helpTransition);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003D58 File Offset: 0x00001F58
		private void ConfigureReadingDeviceInfoState()
		{
			this.readingDeviceInfoState.ShowRegions(new string[] { "MainArea" });
			this.readingDeviceInfoState.HideRegions(new string[] { "SettingsArea" });
			this.readingDeviceInfoState.AddConditionalTransition(this.previousTransition);
			this.readingDeviceInfoState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
			this.readingDeviceInfoState.AddConditionalTransition(this.errorTransition);
			this.readingDeviceInfoState.AddConditionalTransition(this.deviceSelectionTransition);
			this.readingDeviceInfoState.AddConditionalTransition(this.readingDeviceInfoWithThorTransition);
			this.readingDeviceInfoState.AddConditionalTransition(this.checkLatestPackageTransition);
			this.readingDeviceInfoState.AddConditionalTransition(this.manualHtcRestartTransition);
			this.readingDeviceInfoState.AddConditionalTransition(this.rebootHtcTransition);
			this.readingDeviceInfoState.AddConditionalTransition(this.unsupportedDeviceTransition);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003E3C File Offset: 0x0000203C
		private void ConfigureReadingDeviceInfoWithThorState()
		{
			this.readingDeviceInfoWithThorState.ShowRegions(new string[] { "MainArea" });
			this.readingDeviceInfoWithThorState.HideRegions(new string[] { "SettingsArea" });
			this.readingDeviceInfoWithThorState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
			this.readingDeviceInfoWithThorState.AddConditionalTransition(this.errorTransition);
			this.readingDeviceInfoWithThorState.AddConditionalTransition(this.deviceSelectionTransition);
			this.readingDeviceInfoWithThorState.AddConditionalTransition(this.checkLatestPackageTransition);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003EC8 File Offset: 0x000020C8
		private void ConfigureDisclaimerState()
		{
			this.disclaimerState.ShowRegions(new string[] { "MainArea" });
			this.disclaimerState.HideRegions(new string[] { "SettingsArea" });
			this.disclaimerState.AddConditionalTransition(this.previousTransition);
			this.disclaimerState.AddConditionalTransition(this.errorTransition);
			this.disclaimerState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
			this.disclaimerState.AddConditionalTransition(this.downloadPackageTransition);
			this.disclaimerState.AddConditionalTransition(this.packageIntegrityCheckTransition);
			this.disclaimerState.AddConditionalTransition(this.surveyTransition);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003F78 File Offset: 0x00002178
		private void ConfigureManualHtcRestartState()
		{
			this.manualHtcRestartState.ShowRegions(new string[] { "MainArea" });
			this.manualHtcRestartState.AddConditionalTransition(this.previousTransition);
			this.manualHtcRestartState.AddConditionalTransition(this.awaitHtcTransition);
			this.manualHtcRestartState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003FD8 File Offset: 0x000021D8
		private void ConfigureAbsoluteConfirmationState()
		{
			this.absoluteConfirmationState.ShowRegions(new string[] { "MainArea" });
			this.absoluteConfirmationState.HideRegions(new string[] { "SettingsArea" });
			this.absoluteConfirmationState.AddConditionalTransition(this.errorTransition);
			this.absoluteConfirmationState.AddConditionalTransition(this.flashingStateTransition);
			this.absoluteConfirmationState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
			this.absoluteConfirmationState.AddConditionalTransition(this.batteryCheckingTransition);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004064 File Offset: 0x00002264
		private void ConfigureManualGenericModelSelectionState()
		{
			this.manualGenericModelSelectionState.ShowRegions(new string[] { "MainArea" });
			this.manualGenericModelSelectionState.HideRegions(new string[] { "SettingsArea" });
			this.manualGenericModelSelectionState.AddConditionalTransition(this.previousTransition);
			this.manualGenericModelSelectionState.AddConditionalTransition(this.errorTransition);
			this.manualGenericModelSelectionState.AddConditionalTransition(this.manualManufacturerSelectionTransition);
			this.manualGenericModelSelectionState.AddConditionalTransition(this.manualGenericVariantSelectionTransition);
			this.manualGenericModelSelectionState.AddConditionalTransition(this.checkLatestPackageTransition);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004100 File Offset: 0x00002300
		private void ConfigureManualGenericVariantSelectionState()
		{
			this.manualGenericVariantSelectionState.AddConditionalTransition(this.checkLatestPackageTransition);
			this.manualGenericVariantSelectionState.AddConditionalTransition(this.previousTransition);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004128 File Offset: 0x00002328
		private void ConfigureAwaitHoloLensAccessoryDeviceState()
		{
			this.awaitFawkesDeviceState.ShowRegions(new string[] { "MainArea", "SettingsArea" });
			this.awaitFawkesDeviceState.AddConditionalTransition(this.previousTransition);
			this.awaitFawkesDeviceState.AddConditionalTransition(this.readingDeviceInfoTransition);
			this.awaitFawkesDeviceState.AddConditionalTransition(this.checkLatestPackageTransition);
			this.awaitFawkesDeviceState.AddConditionalTransition(this.settingsTransition);
			this.awaitFawkesDeviceState.AddConditionalTransition(this.manualManufacturerSelectionTransition);
			this.awaitFawkesDeviceState.AddConditionalTransition(this.errorTransition);
			this.awaitFawkesDeviceState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
			this.awaitFawkesDeviceState.AddConditionalTransition(this.helpTransition);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000041E8 File Offset: 0x000023E8
		private void ConfigureUnsupportedDeviceState()
		{
			this.unsupportedDeviceState.ShowRegions(new string[] { "MainArea", "SettingsArea" });
			this.unsupportedDeviceState.AddConditionalTransition(this.automaticManufacturerSelectionTransition);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004220 File Offset: 0x00002420
		private void ConfigureSurveyState()
		{
			this.surveyState.ShowRegions(new string[] { "MainArea" });
			this.surveyState.HideRegions(new string[] { "SettingsArea" });
			this.surveyState.AddConditionalTransition(this.previousTransition);
			this.surveyState.AddConditionalTransition(this.errorTransition);
			this.surveyState.AddConditionalTransition(this.downloadPackageTransition);
			this.surveyState.AddConditionalTransition(this.packageIntegrityCheckTransition);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000042AC File Offset: 0x000024AC
		protected T GetMachineState<T>() where T : BaseStateMachineState
		{
			T t = base.Container.Get<T>();
			t.Container = base.Container;
			t.InitializeStateMachine();
			return t;
		}

		// Token: 0x0400000B RID: 11
		[Import]
		private Microsoft.WindowsDeviceRecoveryTool.ApplicationLogic.AppContext appContext;

		// Token: 0x0400000C RID: 12
		[Import]
		private Conditions conditions;

		// Token: 0x0400000D RID: 13
		private UiBaseState appAutoUpdateState;

		// Token: 0x0400000E RID: 14
		private UiBaseState checkAppAutoUpdateState;

		// Token: 0x0400000F RID: 15
		private UiBaseState manualPackageSelectionState;

		// Token: 0x04000010 RID: 16
		private UiBaseState manualManufacturerSelectionState;

		// Token: 0x04000011 RID: 17
		private UiBaseState automaticManufacturerSelectionState;

		// Token: 0x04000012 RID: 18
		private UiBaseState automaticPackageSelectionState;

		// Token: 0x04000013 RID: 19
		private UiBaseState checkLatestPackageState;

		// Token: 0x04000014 RID: 20
		private UiBaseState downloadPackageState;

		// Token: 0x04000015 RID: 21
		private UiBaseState flashingState;

		// Token: 0x04000016 RID: 22
		private UiBaseState awaitGenericDeviceState;

		// Token: 0x04000017 RID: 23
		private UiBaseState awaitHtcState;

		// Token: 0x04000018 RID: 24
		private UiBaseState awaitAnalogDeviceState;

		// Token: 0x04000019 RID: 25
		private UiBaseState awaitRecoveryDeviceState;

		// Token: 0x0400001A RID: 26
		private UiBaseState summaryState;

		// Token: 0x0400001B RID: 27
		private UiBaseState packageIntegrityCheckState;

		// Token: 0x0400001C RID: 28
		private UiBaseState errorState;

		// Token: 0x0400001D RID: 29
		private UiBaseState deviceSelectionState;

		// Token: 0x0400001E RID: 30
		private UiBaseState readingDeviceInfoState;

		// Token: 0x0400001F RID: 31
		private UiBaseState readingDeviceInfoWithThorState;

		// Token: 0x04000020 RID: 32
		private UiBaseState batteryCheckingState;

		// Token: 0x04000021 RID: 33
		private UiBaseState disclaimerState;

		// Token: 0x04000022 RID: 34
		private UiBaseState manualHtcRestartState;

		// Token: 0x04000023 RID: 35
		private UiBaseState rebootHtcState;

		// Token: 0x04000024 RID: 36
		private UiBaseState manualDeviceTypeSelectionState;

		// Token: 0x04000025 RID: 37
		private UiBaseState downloadEmergencyPackageState;

		// Token: 0x04000026 RID: 38
		private UiBaseState awaitRecoveryModeAfterEmergencyFlashingState;

		// Token: 0x04000027 RID: 39
		private UiBaseState absoluteConfirmationState;

		// Token: 0x04000028 RID: 40
		private UiBaseState manualGenericModelSelectionState;

		// Token: 0x04000029 RID: 41
		private UiBaseState manualGenericVariantSelectionState;

		// Token: 0x0400002A RID: 42
		private UiBaseState unsupportedDeviceState;

		// Token: 0x0400002B RID: 43
		private UiBaseState surveyState;

		// Token: 0x0400002C RID: 44
		private UiBaseState awaitFawkesDeviceState;

		// Token: 0x0400002D RID: 45
		private HelpState helpState;

		// Token: 0x0400002E RID: 46
		private SettingsState settingsState;

		// Token: 0x0400002F RID: 47
		private BaseTransition appAutoUpdateTransition;

		// Token: 0x04000030 RID: 48
		private BaseTransition checkAppAutoUpdateTransition;

		// Token: 0x04000031 RID: 49
		private BaseTransition manualPackageSelectionTransition;

		// Token: 0x04000032 RID: 50
		private BaseTransition automaticPackageSelectionTransition;

		// Token: 0x04000033 RID: 51
		private BaseTransition checkLatestPackageTransition;

		// Token: 0x04000034 RID: 52
		private BaseTransition downloadPackageTransition;

		// Token: 0x04000035 RID: 53
		private BaseTransition manualManufacturerSelectionTransition;

		// Token: 0x04000036 RID: 54
		private BaseTransition automaticManufacturerSelectionTransition;

		// Token: 0x04000037 RID: 55
		private BaseTransition flashingStateTransition;

		// Token: 0x04000038 RID: 56
		private BaseTransition awaitGenericDeviceTransition;

		// Token: 0x04000039 RID: 57
		private BaseTransition awaitHtcTransition;

		// Token: 0x0400003A RID: 58
		private BaseTransition awaitAnalogDeviceTransition;

		// Token: 0x0400003B RID: 59
		private BaseTransition awaitRecoveryDeviceTransition;

		// Token: 0x0400003C RID: 60
		private BaseTransition summaryTransition;

		// Token: 0x0400003D RID: 61
		private BaseTransition packageIntegrityCheckTransition;

		// Token: 0x0400003E RID: 62
		private BaseTransition errorTransition;

		// Token: 0x0400003F RID: 63
		private BaseTransition settingsTransition;

		// Token: 0x04000040 RID: 64
		private BaseTransition deviceSelectionTransition;

		// Token: 0x04000041 RID: 65
		private BaseTransition readingDeviceInfoTransition;

		// Token: 0x04000042 RID: 66
		private BaseTransition readingDeviceInfoWithThorTransition;

		// Token: 0x04000043 RID: 67
		private BaseTransition batteryCheckingTransition;

		// Token: 0x04000044 RID: 68
		private BaseTransition disclaimerTransition;

		// Token: 0x04000045 RID: 69
		private BaseTransition manualHtcRestartTransition;

		// Token: 0x04000046 RID: 70
		private BaseTransition rebootHtcTransition;

		// Token: 0x04000047 RID: 71
		private BaseTransition rebootHtcAfterErrorTransition;

		// Token: 0x04000048 RID: 72
		private BaseTransition manufacturerSelectionTransitionAfterError;

		// Token: 0x04000049 RID: 73
		private BaseTransition manualDeviceTypeSelectionTransition;

		// Token: 0x0400004A RID: 74
		private BaseTransition downloadEmergencyPackageTransition;

		// Token: 0x0400004B RID: 75
		private BaseTransition awaitRecoveryModeAfterEmergencyFlashingTransition;

		// Token: 0x0400004C RID: 76
		private BaseTransition absoluteConfirmationTransition;

		// Token: 0x0400004D RID: 77
		private BaseTransition helpTransition;

		// Token: 0x0400004E RID: 78
		private BaseTransition manualGenericModelSelectionTransition;

		// Token: 0x0400004F RID: 79
		private BaseTransition manualGenericVariantSelectionTransition;

		// Token: 0x04000050 RID: 80
		private BaseTransition unsupportedDeviceTransition;

		// Token: 0x04000051 RID: 81
		private BaseTransition surveyTransition;

		// Token: 0x04000052 RID: 82
		private PreviousStateTransition previousTransition;

		// Token: 0x04000053 RID: 83
		private BaseTransition awaitFawkesDeviceTransition;
	}
}
