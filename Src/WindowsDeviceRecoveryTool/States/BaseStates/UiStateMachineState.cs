using System;
using System.Windows;
using Microsoft.WindowsDeviceRecoveryTool.Common;
using Microsoft.WindowsDeviceRecoveryTool.Framework;

namespace Microsoft.WindowsDeviceRecoveryTool.States.BaseStates
{
	// Token: 0x02000077 RID: 119
	public abstract class UiStateMachineState<TView, TViewModel> : BaseStateMachineState where TView : FrameworkElement where TViewModel : BaseViewModel
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x000159E9 File Offset: 0x00013BE9
		protected void SetViewViewModel(TView view, TViewModel viewModel)
		{
			this.view = view;
			this.viewModel = viewModel;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000159FC File Offset: 0x00013BFC
		public override void Start()
		{
			this.view.DataContext = this.viewModel;
			RegionAttribute attribute = this.view.GetAttribute<RegionAttribute>();
			bool flag = attribute == null;
			if (flag)
			{
				throw new NotImplementedException(string.Format("The class {0} should have RegionAttribute. Please correct it.", this.view.GetType().Name));
			}
			string text = attribute.Names[0];
			RegionManager.Instance.ShowView(text, this.view);
			base.Start();
			this.viewModel.IsStarted = true;
			this.viewModel.OnStarted();
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00015AB3 File Offset: 0x00013CB3
		public override void Stop()
		{
			this.viewModel.IsStarted = false;
			this.viewModel.OnStopped();
			base.Stop();
		}

		// Token: 0x040001CA RID: 458
		private TView view;

		// Token: 0x040001CB RID: 459
		private TViewModel viewModel;
	}
}
