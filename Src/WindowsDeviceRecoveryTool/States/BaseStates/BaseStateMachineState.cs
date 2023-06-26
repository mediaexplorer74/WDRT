using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Microsoft.WindowsDeviceRecoveryTool.Framework;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.DefaultTypes;

namespace Microsoft.WindowsDeviceRecoveryTool.States.BaseStates
{
	// Token: 0x02000076 RID: 118
	public abstract class BaseStateMachineState : StateMachineState
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x000158E4 File Offset: 0x00013AE4
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x000158EC File Offset: 0x00013AEC
		internal CompositionContainer Container { get; set; }

		// Token: 0x060003FF RID: 1023 RVA: 0x000158F5 File Offset: 0x00013AF5
		public void ShowRegions(params string[] regions)
		{
			this.VisibleRegions.AddRange(regions);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00015905 File Offset: 0x00013B05
		public void HideRegions(params string[] regions)
		{
			this.InvisibleRegions.AddRange(regions);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00015918 File Offset: 0x00013B18
		public override void Start()
		{
			this.VisibleRegions.ForEach(delegate(string region)
			{
				RegionManager.Instance.ShowRegion(region);
			});
			this.InvisibleRegions.ForEach(delegate(string region)
			{
				RegionManager.Instance.HideRegion(region);
			});
			base.Start();
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00015983 File Offset: 0x00013B83
		public virtual void InitializeStateMachine()
		{
			this.InitializeStates();
			this.InitializeTransitions();
			this.ConfigureStates();
		}

		// Token: 0x06000403 RID: 1027
		protected abstract void InitializeTransitions();

		// Token: 0x06000404 RID: 1028
		protected abstract void InitializeStates();

		// Token: 0x06000405 RID: 1029
		protected abstract void ConfigureStates();

		// Token: 0x06000406 RID: 1030 RVA: 0x0001599C File Offset: 0x00013B9C
		protected UiState<TView, TViewModel> GetUiState<TView, TViewModel>(string showInRegion = null) where TView : FrameworkElement where TViewModel : BaseViewModel
		{
			return new UiState<TView, TViewModel>(this.Container.Get<TView>(), this.Container.Get<TViewModel>(), showInRegion);
		}

		// Token: 0x040001C7 RID: 455
		protected readonly List<string> VisibleRegions = new List<string>();

		// Token: 0x040001C8 RID: 456
		protected readonly List<string> InvisibleRegions = new List<string>();
	}
}
