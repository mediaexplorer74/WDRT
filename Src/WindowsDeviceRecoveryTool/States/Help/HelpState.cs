using System;
using System.ComponentModel.Composition;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.Transitions;
using Microsoft.WindowsDeviceRecoveryTool.States.BaseStates;

namespace Microsoft.WindowsDeviceRecoveryTool.States.Help
{
	// Token: 0x0200005D RID: 93
	[Export]
	public class HelpState : UiStateMachineState<MainHelpView, MainHelpViewModel>
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00013CCC File Offset: 0x00011ECC
		public string CurrentStateName
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00013CE4 File Offset: 0x00011EE4
		public override void InitializeStateMachine()
		{
			base.SetViewViewModel(base.Container.Get<MainHelpView>(), base.Container.Get<MainHelpViewModel>());
			base.InitializeStateMachine();
			base.Machine.AddState(this.lumiaChooseHelpState);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00013D1D File Offset: 0x00011F1D
		protected override void ConfigureStates()
		{
			this.ConfigureLumiaChooseHelpState();
			this.ConfigureLumiaEmergencyHelpState();
			this.ConfigureLumiaFlashingHelpState();
			this.ConfigureLumiaNormalHelpState();
			this.ConfigureHtcChooseHelpState();
			this.ConfigureHtcBootloaderHelpState();
			this.ConfigureHtcNormalHelpState();
			this.ConfigureLumiaOldFlashingHelpState();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00013D58 File Offset: 0x00011F58
		private void ConfigureHtcNormalHelpState()
		{
			this.htcNormalHelpState.AddConditionalTransition(this.lumiaChooseHelpTransition);
			this.htcNormalHelpState.AddConditionalTransition(this.lumiaEmergencyHelpTransition);
			this.htcNormalHelpState.AddConditionalTransition(this.lumiaFlashingHelpTransition);
			this.htcNormalHelpState.AddConditionalTransition(this.lumiaNormalHelpTransition);
			this.htcNormalHelpState.AddConditionalTransition(this.htcChooseHelpTransition);
			this.htcNormalHelpState.AddConditionalTransition(this.htcBootloaderHelpTransition);
			this.htcNormalHelpState.AddConditionalTransition(this.lumiaOldFlashingHelpTransition);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00013DE4 File Offset: 0x00011FE4
		private void ConfigureHtcBootloaderHelpState()
		{
			this.htcBootloaderHelpState.AddConditionalTransition(this.lumiaChooseHelpTransition);
			this.htcBootloaderHelpState.AddConditionalTransition(this.lumiaEmergencyHelpTransition);
			this.htcBootloaderHelpState.AddConditionalTransition(this.lumiaFlashingHelpTransition);
			this.htcBootloaderHelpState.AddConditionalTransition(this.lumiaNormalHelpTransition);
			this.htcBootloaderHelpState.AddConditionalTransition(this.htcChooseHelpTransition);
			this.htcBootloaderHelpState.AddConditionalTransition(this.htcNormalHelpTransition);
			this.htcBootloaderHelpState.AddConditionalTransition(this.lumiaOldFlashingHelpTransition);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00013E70 File Offset: 0x00012070
		private void ConfigureHtcChooseHelpState()
		{
			this.htcChooseHelpState.AddConditionalTransition(this.lumiaChooseHelpTransition);
			this.htcChooseHelpState.AddConditionalTransition(this.lumiaEmergencyHelpTransition);
			this.htcChooseHelpState.AddConditionalTransition(this.lumiaFlashingHelpTransition);
			this.htcChooseHelpState.AddConditionalTransition(this.lumiaNormalHelpTransition);
			this.htcChooseHelpState.AddConditionalTransition(this.htcBootloaderHelpTransition);
			this.htcChooseHelpState.AddConditionalTransition(this.htcNormalHelpTransition);
			this.htcChooseHelpState.AddConditionalTransition(this.lumiaOldFlashingHelpTransition);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00013EFC File Offset: 0x000120FC
		private void ConfigureLumiaNormalHelpState()
		{
			this.lumiaNormalHelpState.AddConditionalTransition(this.lumiaChooseHelpTransition);
			this.lumiaNormalHelpState.AddConditionalTransition(this.lumiaEmergencyHelpTransition);
			this.lumiaNormalHelpState.AddConditionalTransition(this.lumiaFlashingHelpTransition);
			this.lumiaNormalHelpState.AddConditionalTransition(this.htcChooseHelpTransition);
			this.lumiaNormalHelpState.AddConditionalTransition(this.htcBootloaderHelpTransition);
			this.lumiaNormalHelpState.AddConditionalTransition(this.htcNormalHelpTransition);
			this.lumiaNormalHelpState.AddConditionalTransition(this.lumiaOldFlashingHelpTransition);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00013F88 File Offset: 0x00012188
		private void ConfigureLumiaFlashingHelpState()
		{
			this.lumiaFlashingHelpState.AddConditionalTransition(this.lumiaChooseHelpTransition);
			this.lumiaFlashingHelpState.AddConditionalTransition(this.lumiaEmergencyHelpTransition);
			this.lumiaFlashingHelpState.AddConditionalTransition(this.lumiaNormalHelpTransition);
			this.lumiaFlashingHelpState.AddConditionalTransition(this.htcChooseHelpTransition);
			this.lumiaFlashingHelpState.AddConditionalTransition(this.htcBootloaderHelpTransition);
			this.lumiaFlashingHelpState.AddConditionalTransition(this.htcNormalHelpTransition);
			this.lumiaFlashingHelpState.AddConditionalTransition(this.lumiaOldFlashingHelpTransition);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00014014 File Offset: 0x00012214
		private void ConfigureLumiaEmergencyHelpState()
		{
			this.lumiaEmergencyHelpState.AddConditionalTransition(this.lumiaChooseHelpTransition);
			this.lumiaEmergencyHelpState.AddConditionalTransition(this.lumiaFlashingHelpTransition);
			this.lumiaEmergencyHelpState.AddConditionalTransition(this.lumiaNormalHelpTransition);
			this.lumiaEmergencyHelpState.AddConditionalTransition(this.htcChooseHelpTransition);
			this.lumiaEmergencyHelpState.AddConditionalTransition(this.htcBootloaderHelpTransition);
			this.lumiaEmergencyHelpState.AddConditionalTransition(this.htcNormalHelpTransition);
			this.lumiaEmergencyHelpState.AddConditionalTransition(this.lumiaOldFlashingHelpTransition);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000140A0 File Offset: 0x000122A0
		private void ConfigureLumiaChooseHelpState()
		{
			this.lumiaChooseHelpState.AddConditionalTransition(this.lumiaEmergencyHelpTransition);
			this.lumiaChooseHelpState.AddConditionalTransition(this.lumiaFlashingHelpTransition);
			this.lumiaChooseHelpState.AddConditionalTransition(this.lumiaNormalHelpTransition);
			this.lumiaChooseHelpState.AddConditionalTransition(this.htcChooseHelpTransition);
			this.lumiaChooseHelpState.AddConditionalTransition(this.htcBootloaderHelpTransition);
			this.lumiaChooseHelpState.AddConditionalTransition(this.htcNormalHelpTransition);
			this.lumiaChooseHelpState.AddConditionalTransition(this.lumiaOldFlashingHelpTransition);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001412C File Offset: 0x0001232C
		private void ConfigureLumiaOldFlashingHelpState()
		{
			this.lumiaOldFlashingHelpState.AddConditionalTransition(this.lumiaChooseHelpTransition);
			this.lumiaOldFlashingHelpState.AddConditionalTransition(this.lumiaEmergencyHelpTransition);
			this.lumiaOldFlashingHelpState.AddConditionalTransition(this.lumiaFlashingHelpTransition);
			this.lumiaOldFlashingHelpState.AddConditionalTransition(this.lumiaNormalHelpTransition);
			this.lumiaOldFlashingHelpState.AddConditionalTransition(this.htcChooseHelpTransition);
			this.lumiaOldFlashingHelpState.AddConditionalTransition(this.htcBootloaderHelpTransition);
			this.lumiaOldFlashingHelpState.AddConditionalTransition(this.htcNormalHelpTransition);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000141B8 File Offset: 0x000123B8
		protected override void InitializeStates()
		{
			this.lumiaChooseHelpState = base.GetUiState<LumiaChooseHelpView, LumiaChooseHelpViewModel>(null);
			this.lumiaEmergencyHelpState = base.GetUiState<LumiaEmergencyHelpView, LumiaEmergencyHelpViewModel>(null);
			this.lumiaFlashingHelpState = base.GetUiState<LumiaFlashingHelpView, LumiaFlashingHelpViewModel>(null);
			this.lumiaNormalHelpState = base.GetUiState<LumiaNormalHelpView, LumiaNormalHelpViewModel>(null);
			this.htcChooseHelpState = base.GetUiState<HtcChooseHelpView, HtcChooseHelpViewModel>(null);
			this.htcBootloaderHelpState = base.GetUiState<HtcBootloaderHelpView, HtcBootloaderHelpViewModel>(null);
			this.htcNormalHelpState = base.GetUiState<HtcNormalHelpView, HtcNormalHelpViewModel>(null);
			this.lumiaOldFlashingHelpState = base.GetUiState<LumiaOldFlashingHelpView, LumiaOldFlashingHelpViewModel>(null);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00014230 File Offset: 0x00012430
		protected override void InitializeTransitions()
		{
			this.lumiaChooseHelpTransition = new StateStatusTransition(this.lumiaChooseHelpState, "LumiaChooseHelpState");
			this.lumiaEmergencyHelpTransition = new StateStatusTransition(this.lumiaEmergencyHelpState, "LumiaEmergencyHelpState");
			this.lumiaFlashingHelpTransition = new StateStatusTransition(this.lumiaFlashingHelpState, "LumiaFlashingHelpState");
			this.lumiaNormalHelpTransition = new StateStatusTransition(this.lumiaNormalHelpState, "LumiaNormalHelpState");
			this.htcChooseHelpTransition = new StateStatusTransition(this.htcChooseHelpState, "HtcChooseHelpState");
			this.htcBootloaderHelpTransition = new StateStatusTransition(this.htcBootloaderHelpState, "HtcBootloaderHelpState");
			this.htcNormalHelpTransition = new StateStatusTransition(this.htcNormalHelpState, "HtcNormalHelpState");
			this.lumiaOldFlashingHelpTransition = new StateStatusTransition(this.lumiaOldFlashingHelpState, "LumiaOldFlashingHelpState");
		}

		// Token: 0x0400018C RID: 396
		private UiBaseState lumiaChooseHelpState;

		// Token: 0x0400018D RID: 397
		private UiBaseState lumiaEmergencyHelpState;

		// Token: 0x0400018E RID: 398
		private UiBaseState lumiaFlashingHelpState;

		// Token: 0x0400018F RID: 399
		private UiBaseState lumiaNormalHelpState;

		// Token: 0x04000190 RID: 400
		private UiBaseState htcChooseHelpState;

		// Token: 0x04000191 RID: 401
		private UiBaseState htcBootloaderHelpState;

		// Token: 0x04000192 RID: 402
		private UiBaseState htcNormalHelpState;

		// Token: 0x04000193 RID: 403
		private UiBaseState lumiaOldFlashingHelpState;

		// Token: 0x04000194 RID: 404
		private StateStatusTransition lumiaChooseHelpTransition;

		// Token: 0x04000195 RID: 405
		private StateStatusTransition lumiaEmergencyHelpTransition;

		// Token: 0x04000196 RID: 406
		private StateStatusTransition lumiaFlashingHelpTransition;

		// Token: 0x04000197 RID: 407
		private StateStatusTransition lumiaNormalHelpTransition;

		// Token: 0x04000198 RID: 408
		private StateStatusTransition htcChooseHelpTransition;

		// Token: 0x04000199 RID: 409
		private StateStatusTransition htcBootloaderHelpTransition;

		// Token: 0x0400019A RID: 410
		private StateStatusTransition htcNormalHelpTransition;

		// Token: 0x0400019B RID: 411
		private StateStatusTransition lumiaOldFlashingHelpTransition;
	}
}
