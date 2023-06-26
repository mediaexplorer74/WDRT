using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.Transitions;
using Microsoft.WindowsDeviceRecoveryTool.States.BaseStates;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Settings
{
	// Token: 0x02000033 RID: 51
	[Export]
	public class SettingsState : UiStateMachineState<MainSettingsView, MainSettingsViewModel>
	{
		// Token: 0x06000230 RID: 560 RVA: 0x0000D1F2 File Offset: 0x0000B3F2
		public override void InitializeStateMachine()
		{
			base.SetViewViewModel(base.Container.Get<MainSettingsView>(), base.Container.Get<MainSettingsViewModel>());
			base.InitializeStateMachine();
			base.Machine.AddState(this.preferencesState);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000D22B File Offset: 0x0000B42B
		protected override void ConfigureStates()
		{
			this.ConfigurePreferencesState();
			this.ConfigureNetworkState();
			this.ConfigureTraceState();
			this.ConfigurePackagesState();
			this.ConfigureApplicationDataState();
			this.ConfigureFolderBrowseState();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000D258 File Offset: 0x0000B458
		protected override void InitializeStates()
		{
			this.preferencesState = base.GetUiState<PreferencesView, PreferencesViewModel>(null);
			this.networkState = base.GetUiState<NetworkView, NetworkViewModel>(null);
			this.traceState = base.GetUiState<TraceView, TraceViewModel>(null);
			this.packagesState = base.GetUiState<PackagesView, PackagesViewModel>(null);
			this.applicationDataState = base.GetUiState<ApplicationDataView, ApplicationDataViewModel>(null);
			this.folderBrowseState = base.GetUiState<FolderBrowsingView, FolderBrowsingViewModel>(null);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000D2B4 File Offset: 0x0000B4B4
		protected override void InitializeTransitions()
		{
			this.preferencesTransition = new StateStatusTransition(this.preferencesState, "PreferencesState");
			this.networkTransition = new StateStatusTransition(this.networkState, "NetworkState");
			this.traceTransition = new StateStatusTransition(this.traceState, "TraceState");
			this.packagesTransition = new StateStatusTransition(this.packagesState, "PackagesState");
			this.applicationDataTransition = new StateStatusTransition(this.applicationDataState, "ApplicationDataState");
			this.folderBrowseTransition = new StateStatusTransition(this.folderBrowseState, "FolderBrowseAreaState");
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000D348 File Offset: 0x0000B548
		private void ConfigureTraceState()
		{
			this.traceState.AddConditionalTransition(this.preferencesTransition);
			this.traceState.AddConditionalTransition(this.networkTransition);
			this.traceState.AddConditionalTransition(this.traceTransition);
			this.traceState.AddConditionalTransition(this.packagesTransition);
			this.traceState.AddConditionalTransition(this.applicationDataTransition);
			this.traceState.AddConditionalTransition(this.folderBrowseTransition);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000D3C4 File Offset: 0x0000B5C4
		private void ConfigureNetworkState()
		{
			this.networkState.AddConditionalTransition(this.preferencesTransition);
			this.networkState.AddConditionalTransition(this.networkTransition);
			this.networkState.AddConditionalTransition(this.traceTransition);
			this.networkState.AddConditionalTransition(this.packagesTransition);
			this.networkState.AddConditionalTransition(this.applicationDataTransition);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000D42C File Offset: 0x0000B62C
		private void ConfigurePreferencesState()
		{
			this.preferencesState.AddConditionalTransition(this.preferencesTransition);
			this.preferencesState.AddConditionalTransition(this.networkTransition);
			this.preferencesState.AddConditionalTransition(this.traceTransition);
			this.preferencesState.AddConditionalTransition(this.packagesTransition);
			this.preferencesState.AddConditionalTransition(this.applicationDataTransition);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000D494 File Offset: 0x0000B694
		private void ConfigurePackagesState()
		{
			this.packagesState.AddConditionalTransition(this.preferencesTransition);
			this.packagesState.AddConditionalTransition(this.networkTransition);
			this.packagesState.AddConditionalTransition(this.traceTransition);
			this.packagesState.AddConditionalTransition(this.packagesTransition);
			this.packagesState.AddConditionalTransition(this.applicationDataTransition);
			this.packagesState.AddConditionalTransition(this.folderBrowseTransition);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000D510 File Offset: 0x0000B710
		private void ConfigureApplicationDataState()
		{
			this.applicationDataState.AddConditionalTransition(this.preferencesTransition);
			this.applicationDataState.AddConditionalTransition(this.networkTransition);
			this.applicationDataState.AddConditionalTransition(this.traceTransition);
			this.applicationDataState.AddConditionalTransition(this.packagesTransition);
			this.applicationDataState.AddConditionalTransition(this.applicationDataTransition);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000D578 File Offset: 0x0000B778
		private void ConfigureFolderBrowseState()
		{
			this.folderBrowseState.AddConditionalTransition(this.traceTransition);
			this.folderBrowseState.AddConditionalTransition(this.packagesTransition);
		}

		// Token: 0x0400010C RID: 268
		private UiBaseState preferencesState;

		// Token: 0x0400010D RID: 269
		private UiBaseState traceState;

		// Token: 0x0400010E RID: 270
		private UiBaseState networkState;

		// Token: 0x0400010F RID: 271
		private UiBaseState packagesState;

		// Token: 0x04000110 RID: 272
		private UiBaseState applicationDataState;

		// Token: 0x04000111 RID: 273
		private UiBaseState folderBrowseState;

		// Token: 0x04000112 RID: 274
		private StateStatusTransition preferencesTransition;

		// Token: 0x04000113 RID: 275
		private StateStatusTransition networkTransition;

		// Token: 0x04000114 RID: 276
		private StateStatusTransition traceTransition;

		// Token: 0x04000115 RID: 277
		private StateStatusTransition packagesTransition;

		// Token: 0x04000116 RID: 278
		private StateStatusTransition applicationDataTransition;

		// Token: 0x04000117 RID: 279
		private StateStatusTransition folderBrowseTransition;
	}
}
